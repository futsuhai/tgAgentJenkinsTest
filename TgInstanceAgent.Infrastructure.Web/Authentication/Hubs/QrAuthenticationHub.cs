using System.Security.Claims;
using System.Threading.Channels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.Commands.TgAuth;
using TgInstanceAgent.Application.Abstractions.Queries.TgUsers;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;
using TgInstanceAgent.Infrastructure.Web.Authentication.ViewModels;

namespace TgInstanceAgent.Infrastructure.Web.Authentication.Hubs;

/// <summary>
/// Хаб авторизации телеграм клиента
/// </summary>
/// <param name="mediator">Медиатор</param>
[Authorize]
public class QrAuthenticationHub(ISender mediator) : Hub
{
    /// <summary>
    /// Поток аутентификации
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task<ChannelReader<AuthenticationViewModel>> Authenticate(Guid instanceId, CancellationToken cancellationToken)
    {
        // Получаем идентификатор пользователя из утверждений
        var userId = Guid.Parse(Context.User?.FindFirstValue(ClaimTypes.NameIdentifier) ??
                                throw new NullReferenceException());
        
        // Отправляем команду CheckInstanceOwnershipCommand через медиатор для проверки владения инстансом
        await mediator.Send(new CheckInstanceOwnershipCommand { InstanceId = instanceId, UserId = userId }, cancellationToken);

        // Отправка команды на аутентификацию
        var innerChannel = await mediator.Send(new QrCodeAuthCommand { InstanceId = instanceId }, cancellationToken);

        // Создаем выходной канал (входной канал может завершиться исключением, мы должны будем это исключение отправить пользователю)
        // Невозможно это сделать в рамках одного канала, поэтому ловим события из одного канала и добавляем в другой
        // Если ловим исключение - так же отправляем его как объект в выходной канал, чтоб пользователь его получил
        var outputChannel = Channel.CreateUnbounded<AuthenticationViewModel>();

        // Запускаем в отдельном потоке обработку каналов
        _ = ProcessQrCodeAsync(instanceId, innerChannel, outputChannel, cancellationToken);

        // Возвращаем выходной канал
        return outputChannel.Reader;
    }

    /// <summary>
    /// Метод обрабатывает входной канал QR-кодов и перенаправляет данные в выходной канал
    /// </summary>
    /// <param name="id">Идентификатор инстанса</param>
    /// <param name="innerChannel">Входной канал</param>
    /// <param name="outerChannel">Выходной канал</param>
    /// <param name="token">Токен отмены операции</param>
    private async Task ProcessQrCodeAsync(Guid id, Channel<string, string> innerChannel, Channel<AuthenticationViewModel, AuthenticationViewModel> outerChannel, CancellationToken token)
    {
        try
        {
            // Цикл асинхронного чтения из innerChannel
            await foreach (var item in innerChannel.Reader.ReadAllAsync(token))
            {
                // Отправка QR-кода во outerChannel, как только он приходит
                await outerChannel.Writer.WriteAsync(new QrCodeViewModel { Code = item }, token);
            }

            // Получение информации о пользователе
            var me = await mediator.Send(new GetMeQuery { InstanceId = id }, token);
           
            // Отправка информации о пользователе в outerChannel
            await outerChannel.Writer.WriteAsync(new AuthenticatedViewModel { User = me }, token);
        }
        // Обработка исключения PasswordNeededException
        catch (PasswordNeededException ex)
        {
            // Отправка сообщения о пароле в outerChannel
            await outerChannel.Writer.WriteAsync(new PasswordViewModel { Hint = ex.Hint }, token);
        }
        // Обработка остальных исключений
        catch (Exception)
        {
            // Отправка команды на остановку инстанса при отмене операции, так как клиент остается подключен
            await mediator.Send(new StopInstanceCommand {CommandId = Guid.NewGuid(), InstanceId = id }, CancellationToken.None);
        }
        finally
        {
            // Завершение записи в outerChannel
            outerChannel.Writer.Complete();
        }
    }
}