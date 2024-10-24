using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Files;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя запрашиваемых файлов Telegram
/// </summary>
public interface IFileRequestVisitor
{
    /// <summary>
    /// Посетить файл с данными из удаленного источника.
    /// </summary>
    /// <param name="file">Файл с данными из удаленного источника.</param>
    Task VisitAsync(TgInputFileRequestRemoteId file);

    /// <summary>
    /// Посетить файл с данными из локального источника по идентификатору.
    /// </summary>
    /// <param name="file">Файл с данными из локального источника по идентификатору.</param>
    Task VisitAsync(TgInputFileRequestLocalId file);
}