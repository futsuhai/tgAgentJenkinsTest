using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Files;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Files;

/// <inheritdoc/>
/// <summary>
/// Реализация посетителя запрашиваемых файлов Telegram
/// </summary>
public class FileVisitor(TdApi.Client client) : IFileRequestVisitor
{
    /// <summary>
    /// Получает или задает удаленный файл.
    /// </summary>
    public TdApi.File? File { get; private set; }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения удаленного на сервере файла
    /// </summary>
    public async Task VisitAsync(TgInputFileRequestRemoteId file)
    {
        // Создаем новый объект TdApi.InputFile.InputFileRemote
        File = await client.GetRemoteFileAsync(file.RemoteId);
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения файла по локальному id
    /// </summary>
    public async Task VisitAsync(TgInputFileRequestLocalId file)
    {
        // Создаем новый объект TdApi.InputFile.InputFileId
        File = await client.GetFileAsync(file.LocalId);
    }
}