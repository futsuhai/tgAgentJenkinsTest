using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Базовый класс, представляющий файл для отправки
/// </summary>
public abstract class TgInputFileData
{
    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public abstract Task AcceptAsync(IFileDataVisitor messageVisitor);
}

/// <summary>
/// Класс, представляющий файл для отправки с помощью локального пути
/// </summary>
public class TgInputFileDataLocalPath : TgInputFileData
{
    /// <summary>
    /// Путь к локальному файлу
    /// </summary>
    public required string Path { get; init; }
    
    public override Task AcceptAsync(IFileDataVisitor visitor)
    {
        return visitor.VisitAsync(this);
    }
}

/// <summary>
/// Класс, представляющий файл для отправки с помощью локального идентификатора
/// </summary>
public class TgInputFileDataLocalId : TgInputFileData
{
    /// <summary>
    /// Локальный идентификатор файла
    /// </summary>
    public required int LocalId { get; init; }
    
    public override Task AcceptAsync(IFileDataVisitor visitor)
    {
        return visitor.VisitAsync(this);
    }
}

/// <summary>
/// Класс, представляющий файл для отправки с помощью удаленного идентификатора
/// </summary>
public class TgInputFileDataRemoteId : TgInputFileData
{
    /// <summary>
    /// Удаленный идентификатор файла
    /// </summary>
    public required string RemoteId { get; init; }
    
    public override Task AcceptAsync(IFileDataVisitor visitor)
    {
        return visitor.VisitAsync(this);
    }
}