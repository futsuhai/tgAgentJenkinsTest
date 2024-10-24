using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;

namespace TgInstanceAgent.Application.Services.InstancesService;

/// <summary> 
/// Сервиса автоматической пересылки сообщений.
/// </summary> 
/// <param name="instanceRepository">Репозиторий для работы с инстансами.</param>
/// <param name="memoryCache">Кэш.</param>
public class AutoForwardService(IMemoryCache memoryCache, IInstanceRepository instanceRepository) : IAutoForwardService
{
    /// <inheritdoc/>
    /// <summary>
    /// Пересылает сообщение, если для него настроен форвард.
    /// </summary>
    public async Task<TgMessage?> ForwardIfNeededAsync(ITelegramClient client, Guid instanceId, TgMessage message, CancellationToken cancellationToken = default)
    {
        // Если это исходящее сообщение или оно не может быть перенаправлено - ничего не делаем
        if (message.IsOutgoing || !message.CanBeForwarded) return null;
        
        // Получаем инстанс из репозитория
        var instance = await memoryCache.GetInstanceAsync(instanceId, instanceRepository, cancellationToken);
        
        // Проверяем, есть ли форвард на сообщение
        var forwardEntry = instance.ForwardEntries.FirstOrDefault(x => x.For == message.ChatId);

        // Если пересылка не найдена, выбрасываем исключение
        if (forwardEntry == null) return null;

        // Выполняем пересылку сообщения
        var forwardMessagesRequest = new TgForwardMessagesRequest
        {
            // Чат
            Chat = new TgInputChatId
            {
                ChatId = forwardEntry.To
            },

            // Идентификатор чата
            FromChatId = message.ChatId,

            // Массив идентификаторов сообщений для пересылки
            MessageIds = [message.Id],

            // Истинно, если требуется переслать сообщения без ссылок на пользователей этих сообщений
            SendCopy = forwardEntry.SendCopy
        };

        // Пересылаем сообщение
        var forwardedMessages = await client.ForwardMessagesAsync(forwardMessagesRequest, cancellationToken);

        // Возвращаем первое сообщение из коллекции
        var forwardedMessage = forwardedMessages.First();

        // Возвращаем пересланное сообщение
        return forwardedMessage;
    }
}