using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Channels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.Commands.TgEvents;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

namespace TgInstanceAgent.Infrastructure.Web.Events.Hubs;

/// <summary>
/// Хаб для событий мессенджера(сообщения, реакции...)
/// </summary>
/// <param name="mediator">Медиатор</param>
[Authorize]
public class EventHub(ISender mediator) : Hub
{
    /// <summary>
    /// Поток ивентов мессенджера клиенту
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Поток для чтения пользователю</returns>
    public async IAsyncEnumerable<TgEvent> StartReceive(Guid instanceId,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        // Получаем идентификатор пользователя из утверждений
        var userId = Guid.Parse(Context.User?.FindFirstValue(ClaimTypes.NameIdentifier) ??
                                throw new NullReferenceException());
        
        // Отправляем команду CheckInstanceOwnershipCommand через медиатор для проверки владения инстансом
        await mediator.Send(new CheckInstanceOwnershipCommand { InstanceId = instanceId, UserId = userId }, cancellationToken);
        
        // Создаем канал для отправки ивентов клиенту
        var channel = Channel.CreateUnbounded<TgEvent>();

        // Запускаем в отдельном потоке обработку канала
        var task = ProcessEventsAsync(instanceId, channel, cancellationToken);

        // Цикл асинхронного чтения из канала.
        // Не передавать токен отмены!!!
        // Когда сработает токен отмены, канал будет просто закрыт, а исключение будет получено при ожидании таска ниже
        await foreach (var item in channel.Reader.ReadAllAsync(CancellationToken.None))
        {
            // Возвращаям элемент
            yield return item;
        }

        // Ожидаем задачу (она уже завершена, так как выходной канал закрыт).
        // Это сделано для получения исключения, если оно возникло при обработке входного канала
        await task;
    }


    /// <summary>
    /// Метод обрабатывает входной канал QR-кодов и перенаправляет данные в выходной канал
    /// </summary>
    /// <param name="id">Идентификатор инстанса</param>
    /// <param name="outerChannel">Выходной канал</param>
    /// <param name="token">Токен отмены операции</param>
    private async Task ProcessEventsAsync(Guid id, Channel<TgEvent, TgEvent> outerChannel, CancellationToken token)
    {
        // Создаем канал для отправки ивентов клиенту
        var channel = Channel.CreateUnbounded<TgEvent>();

        try
        {
            // Подписываем обработчик событий из месенджера
            // В данном случае, в Handler каждый ивент отправляется на клиент
            await mediator.Send(new StartReceiveCommand { InstanceId = id, Handler = Handler }, token);

            // Цикл асинхронного чтения из innerChannel
            await foreach (var item in channel.Reader.ReadAllAsync(token))
            {
                // Отправка QR-кода во outerChannel, как только он приходит
                await outerChannel.Writer.WriteAsync(item, token);
            }
        }
        catch (Exception)
        {
            // Отписываем Handler от ивентов
            await mediator.Send(new StopReceiveCommand { InstanceId = id, Handler = Handler }, CancellationToken.None);

            // Выбрасываем исключение дальше
            throw;
        }
        finally
        {
            // Завершение записи в channel
            channel.Writer.Complete();

            // Завершение записи в outerChannel
            outerChannel.Writer.Complete();
        }

        return;

        // Отправка ивентов клиенту
        async void Handler(TgEvent @event) => await channel.Writer.WriteAsync(@event, CancellationToken.None);
    }
}