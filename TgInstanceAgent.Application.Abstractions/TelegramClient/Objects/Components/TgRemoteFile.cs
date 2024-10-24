namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет удаленный файл
/// </summary>
public class TgRemoteFile
{
    /// <summary>
    /// Идентификатор удаленного файла.
    /// Может использоваться текущим пользователем через перезапуски приложения или даже с других устройств.
    /// Уникально идентифицирует файл, но файл может иметь много разных допустимых идентификаторов.
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// Уникальный идентификатор файла.
    /// Уникальный идентификатор файла, который одинаков для одного и того же файла даже для разных пользователей и сохраняется со временем.
    /// </summary>
    public string? UniqueId { get; init; }

    /// <summary>
    /// True, если файл в данный момент загружается (или удаленная копия генерируется другим способом)
    /// </summary>
    public required bool IsUploadingActive { get; init; }

    /// <summary>
    /// True, если удаленная копия полностью доступна
    /// </summary>
    public required bool IsUploadingCompleted { get; init; }

    /// <summary>
    /// Размер удаленной доступной части файла в байтах.
    /// </summary>
    public long? UploadedSize { get; init; }
}
