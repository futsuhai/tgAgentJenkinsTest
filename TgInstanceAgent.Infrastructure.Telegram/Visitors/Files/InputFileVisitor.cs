using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Files;

/// <inheritdoc/>
/// <summary>
/// Реализация посетителя отправляемых файлов Telegram
/// </summary>
public class InputFileVisitor : IFileDataVisitor
{
    /// <summary>
    /// Получает или задает удаленный файл.
    /// </summary>
    public TdApi.InputFile? File { get; private set; }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения удаленного на сервере файла
    /// </summary>
    public Task VisitAsync(TgInputFileDataRemoteId file)
    {
        // Создаем новый объект TdApi.InputFile.InputFileRemote
        File = new TdApi.InputFile.InputFileRemote
        {
            // Устанавливаем удаленный идентификатор файла
            Id = file.RemoteId
        };

        // Возвращаем завершенный таск
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения файла по локальному id
    /// </summary>
    public Task VisitAsync(TgInputFileDataLocalId file)
    {
        // Создаем новый объект TdApi.InputFile.InputFileId
        File = new TdApi.InputFile.InputFileId
        {
            // Устанавливаем локальный идентификатор файла
            Id = file.LocalId
        };

        // Возвращаем завершенный таск
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения файла по локальному пути
    /// </summary>
    public Task VisitAsync(TgInputFileDataLocalPath file)
    {
        // Создаем новый объект TdApi.InputFile.InputFileLocal
        File = new TdApi.InputFile.InputFileLocal
        {
            // Устанавливаем свойство Path из file.Path
            Path = file.Path
        };

        // Возвращаем завершенный таск
        return Task.CompletedTask;
    }
}