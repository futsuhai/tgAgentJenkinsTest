using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Files;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Interfaces;

namespace TgInstanceAgent.Application.Services.Extensions;

/// <summary>
/// Класс, предоставляющий методы для преобразования данных получаемого файла.
/// </summary>
public static class FileRequestMapper
{
    /// <summary>
    /// Получает данные о файле типа TgInputFileData из объекта, реализующего интерфейс IWithFile.
    /// </summary>
    /// <param name="fileRequest">Объект, содержащий данные о файле.</param>
    /// <param name="telegramClient">Клиент телеграм</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Данные о файле типа TgInputFileData.</returns>
    public static async Task<TgInputFileRequest> GetFileRequest(this IWithFileRequest fileRequest,
        ITelegramClient telegramClient, CancellationToken cancellationToken)
    {

        // Если передан локальный идентификатор файла
        if (fileRequest.LocalId.HasValue)
        {
            // Возвращаем данные о файле типа TgInputFileDataLocalId
            return new TgInputFileRequestLocalId
            {
                // Устанавливаем локальный идентификатор файла
                LocalId = fileRequest.LocalId.Value
            };
        }

        // Если передан удаленный идентификатор файла
        if (!string.IsNullOrEmpty(fileRequest.RemoteId))
        {
            // Возвращаем данные о файле типа TgInputFileDataRemoteId
            return new TgInputFileRequestRemoteId
            {
                // Устанавливаем удаленный идентификатор файла
                RemoteId = fileRequest.RemoteId
            };
        }

        if (fileRequest.FileFromMessage != null)
        {
            // Если передан идентификатор сообщения получаем сообщение
            var message = await telegramClient.GetMessageAsync(new TgGetMessageRequest
            {
                Chat = fileRequest.FileFromMessage.GetChat(),
                MessageId = fileRequest.FileFromMessage.MessageId
            }, cancellationToken);

            // Если тело сообщения не содержит файл, то вызываем исключение
            if (message.Content is not ITgObjectWithFile content) throw new MessageNotContainFileException();

            // Возвращаем данные о файле типа TgInputFileDataLocalId
            return new TgInputFileRequestLocalId
            {
                // Устанавливаем локальный идентификатор файла
                LocalId = content.GetFile().Id
            };
        }

        // Если не удалось определить тип файла, выбрасываем исключение
        throw new InvalidFileRequestException();
    }
}