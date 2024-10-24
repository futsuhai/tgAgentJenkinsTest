namespace TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

/// <summary>
/// Интерфейс для моделей, содержащий информацию об запрашиваемой порции
/// </summary>
public interface IWithInputLimit
{
    /// <summary>
    /// Лимит.
    /// </summary>
    public int Limit { get; init; }

    /// <summary>
    /// Смещение.
    /// </summary>
    public int? Offset { get; init; }
}