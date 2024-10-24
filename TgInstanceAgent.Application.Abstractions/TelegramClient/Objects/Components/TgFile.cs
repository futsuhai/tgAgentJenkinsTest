namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет файл
/// </summary>
public class TgFile
{
    /// <summary>
    /// Идентификатор файла.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Размер файла.
    /// </summary>
    public required long Size { get; init; }

    /// <summary>
    /// Приблизительный размер файла в байтах на случай, если точный размер файла неизвестен. Может использоваться для отображения хода загрузки.
    /// </summary>
    public required long ExpectedSize { get; init; }

    /// <summary>
    /// Информация о локальной копии файла.
    /// </summary>
    public required TgLocalFile Local { get; init; }

    /// <summary>
    /// Информация об удаленной копии файла.
    /// </summary>
    public required TgRemoteFile Remote { get; init; }
}