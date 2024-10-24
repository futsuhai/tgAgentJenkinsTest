namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет контакт.
/// </summary>
public class TgContact
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// Номер телефона контакта.
    /// </summary>
    public required string PhoneNumber { get; init; }

    /// <summary>
    /// Идентификатор пользователя.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный пользователь.
    /// </summary>
    public long? ContactId { get; init; }

    /// <summary>
    /// Визитка.
    /// </summary>
    public string? VCard { get; init; }
}