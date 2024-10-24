namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя отправляемых файлов Telegram
/// </summary>
public interface IFileDataVisitor
{
    /// <summary>
    /// Посетить файл с данными из удаленного источника.
    /// </summary>
    /// <param name="file">Файл с данными из удаленного источника.</param>
    Task VisitAsync(TgInputFileDataRemoteId file);

    /// <summary>
    /// Посетить файл с данными из локального источника по идентификатору.
    /// </summary>
    /// <param name="file">Файл с данными из локального источника по идентификатору.</param>
    Task VisitAsync(TgInputFileDataLocalId file);

    /// <summary>
    /// Посетить файл с данными из локального источника по пути.
    /// </summary>
    /// <param name="file">Файл с данными из локального источника по пути.</param>
    Task VisitAsync(TgInputFileDataLocalPath file);
}