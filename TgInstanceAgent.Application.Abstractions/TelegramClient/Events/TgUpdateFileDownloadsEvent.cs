using TgInstanceAgent.Application.Abstractions.TelegramClient.Events.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

/// <summary>
/// Событие изменения состояния списка загружаемых файлов
/// </summary>
public class TgUpdateFileDownloadsEvent : TgEvent
{
    /// <summary>
    /// Общий размер файлов в списке загружаемых файлов, в байтах
    /// </summary>
    public required long TotalSize { get; init; }

    /// <summary>
    /// Общее количество файлов в списке загружаемых файлов
    /// </summary>
    public required int TotalCount { get; init; }

    /// <summary>
    /// Общий размер загруженных файлов в списке загружаемых файлов, в байтах
    /// </summary>
    public required long DownloadedSize { get; init; }

    /// <summary>
    /// Принять посетителя события.
    /// </summary>
    public override void Accept(ITgEventVisitor eventVisitor) => eventVisitor.Visit(this);
}