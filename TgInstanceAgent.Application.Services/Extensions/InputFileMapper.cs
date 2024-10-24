using TgInstanceAgent.Application.Abstractions.Commands.TgMessages;
using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Abstractions.FileStorage;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

namespace TgInstanceAgent.Application.Services.Extensions;

/// <summary>
/// Класс, предоставляющий методы для преобразования данных отправляемого файла.
/// </summary>
public static class InputFileMapper
{
    /// <summary>
    /// Асинхронно получает данные о файле типа TgInputFileData из объекта, реализующего интерфейс IWithFile.
    /// </summary>
    /// <param name="file">Объект, содержащий данные о файле.</param>
    /// <param name="fileStorage">Хранилище файлов.</param>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <returns>Данные о файле типа TgInputFileData.</returns>
    public static async Task<TgInputFileData> GetInputFileAsync(this IWithFile file, IFileStorage fileStorage,
        Guid instanceId)
    {
        // Если передан поток файла
        if (file.File != null)
        {
            // Сохраняем файл локально из потока и получаем путь к нему
            var localPath = await fileStorage.StoreAsync(file.File.Stream, GetDirectory(instanceId), file.File.Name);

            // Возвращаем данные о файле типа TgInputFileDataLocalPath
            return new TgInputFileDataLocalPath
            {
                // Устанавливаем путь к файлу
                Path = localPath
            };
        }

        // Если передан локальный идентификатор файла
        if (file.LocalId.HasValue)
        {
            // Возвращаем данные о файле типа TgInputFileDataLocalId
            return new TgInputFileDataLocalId
            {
                // Устанавливаем локальный идентификатор файла
                LocalId = file.LocalId.Value
            };
        }

        // Если передан удаленный идентификатор файла
        if (!string.IsNullOrEmpty(file.RemoteId))
        {
            // Возвращаем данные о файле типа TgInputFileDataRemoteId
            return new TgInputFileDataRemoteId
            {
                // Устанавливаем удаленный идентификатор файла
                RemoteId = file.RemoteId
            };
        }

        // Если не удалось определить тип файла, выбрасываем исключение
        throw new InvalidFileRequestException();
    }

    /// <summary>
    /// Асинхронно получает данные о миниатюре файла типа TgInputThumbnail из объекта, реализующего интерфейс IWithThumbnail.
    /// </summary>
    /// <param name="message">Объект, содержащий данные о миниатюре файла.</param>
    /// <param name="fileStorage">Хранилище файлов.</param>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <returns>Данные о миниатюре файла типа TgInputThumbnail.</returns>
    public static async Task<TgInputThumbnail?> GetInputThumbnailAsync(this SendAudioMessageCommand message,
        IFileStorage fileStorage, Guid instanceId)
    {
        // Если данные файла не переданы
        if (message.Thumbnail == null) return null;

        // Сохраняем файл локально и получаем его путь
        var localPath =
            await fileStorage.StoreAsync(message.Thumbnail.Stream, GetDirectory(instanceId), message.Thumbnail.Name);

        // Возвращаем данные о файле типа TgInputThumbnail
        return new TgInputThumbnail
        {
            // Устанавливаем путь к файлу
            Path = localPath
        };
    }

    /// <summary>
    /// Возвращает директорию на основе уникального идентификатора.
    /// </summary>
    private static string GetDirectory(Guid id) => $"instance-{id}";
}