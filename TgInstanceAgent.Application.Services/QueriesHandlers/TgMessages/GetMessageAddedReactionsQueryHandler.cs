using MediatR;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgMessages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgMessages;

/// <summary>
/// Обработчик запроса получения списка добавленных на сообщение реакций.
/// </summary>
public class GetMessageAddedReactionsQueryHandler(IInstancesService instancesService)
    : IRequestHandler<GetMessageAddedReactionsQuery, TgMessageAddedReactions>
{
    /// <summary>
    /// Обрабатывает запрос получения списка добавленных на сообщение реакций.
    /// </summary>
    /// <param name="request">Запрос на получение списка добавленных на сообщение реакций</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    public async Task<TgMessageAddedReactions> Handle(GetMessageAddedReactionsQuery request,
        CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var instanceService = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Тип реакции - изначально не установлена
        TgInputReaction? type = null;

        // Если в запросе обычная реакция
        if (!string.IsNullOrEmpty(request.Emoji))
        {
            type = new TgInputReactionEmoji { Emoji = request.Emoji };
        }
        
        // Если в запросе обычная премиум реакция
        else if (request.EmojiId.HasValue)
        {
            type = new TgInputCustomPremiumEmoji { EmojiId = request.EmojiId.Value };
        }
        
        // Получание списка добавленных на сообщение реакций через сервис инстансов
        return await instanceService.GetAddedMessageReactionsAsync(new TgGetMessageAddedReactionsRequest
        {
            // Получаем чат
            Chat = request.GetChat(),

            // Устанавливаем реакцию
            Reaction = type,
            
            // Id сообщения
            MessageId = request.MessageId,
            
            // Лимит реакций
            Limit = request.Limit,
            
            // Смещение
            Offset = request.Offset
        }, cancellationToken);
    }
}