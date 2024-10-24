using System.Runtime.Serialization;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет локальный файл
/// </summary>
public class TgLocalFile
{
    /// <summary>
    /// True, если возможно скачать или сгенерировать файл
    /// </summary>
    public required bool CanBeDownloaded { get; init; }

    /// <summary>
    /// True, если файл может быть удален
    /// </summary>
    public required bool CanBeDeleted { get; init; }

    /// <summary>
    /// True, если файл в данный момент скачивается (или локальная копия генерируется другим способом)
    /// </summary>
    public required bool IsDownloadingActive { get; init; }

    /// <summary>
    /// True, если локальная копия полностью доступна
    /// </summary>
    public required bool IsDownloadingCompleted { get; init; }

    /// <summary>
    /// Скачивание будет начато с этого смещения.
    /// DownloadedPrefixSize рассчитывается от этого смещения.
    /// </summary>
    public required long DownloadOffset { get; init; }

    /// <summary>
    /// Если IsDownloadingCompleted равно false, то только часть файла, начиная с DownloadOffset, готова для чтения.
    /// DownloadedPrefixSize — это размер этой части в байтах.
    /// </summary>
    public required long DownloadedPrefixSize { get; init; }

    /// <summary>
    /// Общий размер скачанного файла в байтах.
    /// Может использоваться только для расчета прогресса скачивания.
    /// Фактический размер файла может быть больше, и некоторые его части могут содержать мусор.
    /// </summary>
    public required long DownloadedSize { get; init; }
    
    /// <summary>
    /// Путь к файлу.
    /// </summary>
    [IgnoreDataMember]
    public required string Path { get; init; }
}
