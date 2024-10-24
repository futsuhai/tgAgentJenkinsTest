namespace TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

/// <summary>
/// Интерфейс для отправки файлового сообщения.
/// </summary>
public interface IWithInputCaption
{
    /// <summary>
    /// Подпись к файловому сообщению.
    /// </summary>
    public string? Caption { get; init; }
}