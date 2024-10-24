using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Chats;

/// <summary>
/// Реализация посетителя запросов с указанием статуса емозди чата
/// </summary>
public class ChatEmojiStatusVisitor : IChatEmojiStatusVisitor
{
    /// <summary>
    /// Статус емозди чата
    /// </summary>
    public TdApi.EmojiStatus? EmojiStatus { get; private set; }
    
    /// <summary>
    /// Посетить отправителя, используя идентификатор емозди и её время истечения.
    /// </summary>
    public void Visit(long? emojiId, int? expirationDate)
    {
        // Проверяем, задан ли идентификатор эмодзи
        if (emojiId.HasValue && expirationDate.HasValue)
        {
            // Создаем новый статус эмодзи с указанным идентификатором
            EmojiStatus = new TdApi.EmojiStatus
            {
                CustomEmojiId = emojiId.Value,
                ExpirationDate = expirationDate.Value
            };
        }
        // Если идентификатор эмодзи не задан, устанавливаем статус в null
        else
        {
            EmojiStatus = null;
        }
    }
}