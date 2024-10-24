namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Класс, представляющий системное сообщение на создание базовой группы
/// </summary>
public class TgGroupChatCreateMessage: TgMessageContent
{
    /// <summary>
    /// Название группы
    /// </summary>
    public required string Title { get; init; }
    
    /// <summary>
    /// Список пользовательских Id, которые добавлены в группу при создании
    /// </summary>
    public required long[] MemberUserIds { get; init; }
}