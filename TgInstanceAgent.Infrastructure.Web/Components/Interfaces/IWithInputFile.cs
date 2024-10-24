namespace TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

/// <summary>
/// Интерфейс для моделей с входными данными, представляющие информацию о файле.
/// </summary>
public interface IWithInputFile
{
    /// <summary>
    /// Файл
    /// </summary>
    public IFormFile? File { get; init; }
    
    /// <summary>
    /// Локальный идентификатор файла
    /// </summary>
    public int? LocalId { get; init; }

    /// <summary>
    /// Удаленный идентификатор файла
    /// </summary>
    public string? RemoteId { get; init; }
}
