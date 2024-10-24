using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Chats;

/// <summary>
/// Реализация посетителя запросов с указанием чата
/// </summary>
public class ChatIdVisitor(TdApi.Client client) : IChatVisitor
{
    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public long? ChatId { get; private set; }

    /// <summary>
    /// Посетить чат, используя идентификатор.
    /// </summary>
    public Task VisitAsync(TgInputChatId tgChatId)
    {
        // Устанавливаем идентификатор чата
        ChatId = tgChatId.ChatId;
        
        // Возвращаем завершенный Task. При дальнейшем выполнении возможно окажется, что чат неизвестен TdLib.
        return Task.CompletedTask;
    }

    /// <summary>
    /// Посетить чат, используя имя пользователя.
    /// </summary>
    public async Task VisitAsync(TgInputChatUsername tgChatUsername)
    {
        // Ищем публичный чат.
        var chat = await client.SearchPublicChatAsync(tgChatUsername.Username);

        // Устанавливаем идентификатор чата
        ChatId = chat.Id;
    }

    /// <summary>
    /// Посетить чат, используя номер телефона.
    /// </summary>
    public async Task VisitAsync(TgInputChatPhoneNumber tgChatPhone)
    {
        // Получаем пользователя по номеру телефона.
        var user = await client.SearchUserByPhoneNumberAsync(tgChatPhone.PhoneNumber);

        // Создаем приватный чат с пользователем (он может быть не создан или не загружен).
        var chat = await client.CreatePrivateChatAsync(user.Id, force: true);

        // Устанавливаем Данные чата.
        ChatId = chat.Id;
    }
}