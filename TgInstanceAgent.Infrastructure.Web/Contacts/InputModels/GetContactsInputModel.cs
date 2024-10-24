using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Contacts.InputModels;

/// <summary>
/// Модель входных данных для получения контактов.
/// </summary>
public class GetContactsInputModel : IWithInputLimit
{
    /// <summary>
    /// Лимит контактов.
    /// </summary>
    public int Limit { get; init; } = 100;

    /// <summary>
    /// Смещение.
    /// </summary>
    public int? Offset { get; init; }

    /// <summary>
    /// Начиная с идентификатора контакта.
    /// </summary>
    public long? FromContactId { get; init; }
}