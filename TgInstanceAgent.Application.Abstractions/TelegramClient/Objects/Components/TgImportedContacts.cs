namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Импортированные контакты.
/// </summary>
public class TgImportedContacts
{
    /// <summary>
    /// Идентификаторы пользователей импортированных контактов.
    /// </summary>
    public required long[] UserIds { get; init; }

    /// <summary>
    /// Количество пользователей, импортировавших соответствующий контакт.
    /// </summary>
    public required int[] ImporterCount { get; init; }
}