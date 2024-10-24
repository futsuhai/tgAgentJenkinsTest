using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Users;

/// <summary>
/// Реализация посетителя запросов с указанием пользователя
/// </summary>
public class UserIdVisitor(TdApi.Client client) : IUserVisitor
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public long? UserId { get; private set; }

    /// <summary>
    /// Посетить пользователя, используя идентификатор.
    /// </summary>
    public Task VisitAsync(TgInputUserId tgUserId)
    {
        // Устанавливаем идентификатор пользователя
        UserId = tgUserId.UserId;
        
        // Возвращаем завершенный Task. При дальнейшем выполнении возможно окажется, что пользователь неизвестен TdLib.
        return Task.CompletedTask;
    }

    /// <summary>
    /// Посетить чат, используя имя пользователя.
    /// </summary>
    public async Task VisitAsync(TgInputUserUsername tgChatUsername)
    {
        // Ищем публичный чат.
        var chat = await client.SearchPublicChatAsync(tgChatUsername.Username);

        // Если это приватный чат, то данные пользователя будут загружены в TdLib, если еще не были загружены
        // и взаимодействие с пользователем будет возможно.
        if (chat.Type is TdApi.ChatType.ChatTypePrivate s)
        {
            // Устанавливаем идентификатор пользователя
            UserId = s.UserId;
        }
    }

    /// <summary>
    /// Посетить чат, используя номер телефона.
    /// </summary>
    public async Task VisitAsync(TgInputUserPhoneNumber tgChatPhone)
    {
        // Получаем пользователя по номеру телефона
        var user = await client.SearchUserByPhoneNumberAsync(tgChatPhone.PhoneNumber);
        
        // Устанавливаем идентификатор пользователя
        UserId = user.Id;
    }
}