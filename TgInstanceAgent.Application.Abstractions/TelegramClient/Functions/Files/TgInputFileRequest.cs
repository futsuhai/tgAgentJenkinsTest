using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Files;

/// <summary>
/// Базовый класс, представляющий запрос файла
/// </summary>
public abstract class TgInputFileRequest
{
    /// <summary>
    /// Принять посетителя запроса.
    /// </summary>
    public abstract Task AcceptAsync(IFileRequestVisitor messageVisitor);
}


/// <summary>
/// Класс, представляющий запрос файла с помощью локального идентификатора
/// </summary>
public class TgInputFileRequestLocalId : TgInputFileRequest
{
    /// <summary>
    /// Локальный идентификатор файла
    /// </summary>
    public required int LocalId { get; init; }
    
    /// <summary>
    /// Принять посетителя запроса.
    /// </summary>
    public override Task AcceptAsync(IFileRequestVisitor visitor)
    {
        return visitor.VisitAsync(this);
    }
}

/// <summary>
/// Класс, представляющий запрос файла с помощью удаленного идентификатора
/// </summary>
public class TgInputFileRequestRemoteId : TgInputFileRequest
{
    /// <summary>
    /// Удаленный идентификатор файла
    /// </summary>
    public required string RemoteId { get; init; }
    
    /// <summary>
    /// Принять посетителя запроса.
    /// </summary>
    public override Task AcceptAsync(IFileRequestVisitor visitor)
    {
        return visitor.VisitAsync(this);
    }
}