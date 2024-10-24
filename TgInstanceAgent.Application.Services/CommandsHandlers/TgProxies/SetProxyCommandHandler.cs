using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Commands.TgProxies;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Proxy;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Instances.ValueObjects;
using TgInstanceAgent.Domain.SystemProxy.Enums;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgProxies;

/// <summary>
/// Обработчик команды для установки прокси сервера
/// </summary>
/// <param name="instancesService">Сервис для работы с инстансами.</param>
/// <param name="cache">Кэш</param>
/// <param name="instanceRepository">Репозиторий для работы с инстансами</param>
public class SetProxyCommandHandler(
    IInstancesService instancesService,
    IMemoryCache cache,
    IInstanceRepository instanceRepository)
    : IRequestHandler<SetProxyCommand>
{
    /// <summary>
    /// Обрабатывает команду установки прокси сервера
    /// </summary>
    /// <param name="request">Команда установки прокси сервера</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task Handle(SetProxyCommand request, CancellationToken cancellationToken)
    {
        // Запуск клиента для обработки команды.
        var service = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получаем инстанс
        var instance = await cache.GetInstanceAsync(request.InstanceId, instanceRepository, cancellationToken);

        // Отправляем запрос на установку прокси
        await service.SetProxyAsync(new TgSetProxyRequest
        {
            // Домен или ip адрес сервера
            Server = request.Server,

            // Порт
            Port = request.Port,

            // Получаем тип прокси
            ProxyType = request.GetProxyType()
        }, cancellationToken);

        // Получаем Enum для типа прокси на основании команды
        var proxyTypeEnum = request.GetProxyTypeEnum();
        
        // Создаем модель прокси по данным из команды
        var proxy = new Proxy
        {
            // Домен или ip адрес сервера
            Host = request.Server,
            
            // Порт
            Port = request.Port,
            
            // Тип прокси
            Type = proxyTypeEnum,
            
            // Логин прокси
            Login = request.GetProxyTypeEnum() switch
            {
                ProxyType.Http => request.ProxyTypeHttp?.Username,
                ProxyType.Socks => request.ProxyTypeSocks?.Username,
                _ => null
            },
            
            // Пароль прокси
            Password = request.GetProxyTypeEnum() switch
            {
                ProxyType.Http => request.ProxyTypeHttp?.Password,
                ProxyType.Socks => request.ProxyTypeSocks?.Password,
                _ => null
            },
            
            // Время истечения прокси
            ExpirationTimeUtc = request.ExpirationTimeUtc
        };

        // Устанавливаем прокси у инстанса
        instance.Proxy = proxy;
        
        // Обновляем инстанс
        await instanceRepository.UpdateAsync(instance);
    }
}