using MediatR;
using TgInstanceAgent.Application.Abstractions.DTOs.Files;
using TgInstanceAgent.Application.Abstractions.FileStorage;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Queries.TgFiles;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;
using TgInstanceAgent.Application.Services.Extensions;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.TgFiles;

/// <summary>
/// Обработчик запроса на получение файла по Id.
/// </summary>
public class GetFileQueryHandler(IInstancesService instancesService, IFileStorage storage)
    : IRequestHandler<GetFileQuery, FileDto>
{
    /// <summary>
    /// Обрабатывает запрос на получение файла по Id.
    /// </summary>
    /// <param name="request">Запрос на получение файла.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Коллекцию контактов.</returns>
    public async Task<FileDto> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получаем запрос на получение файла
        var fileRequest = await request.GetFileRequest(telegramClient, cancellationToken);
        
        // Получение пути к файлу
        var file = await telegramClient.GetFileAsync(fileRequest, cancellationToken);
        
        // Если файл загружен - возвращаем файл
        if (file.Local.IsDownloadingCompleted)
        {
            // Открываем файл на чтение и возвращаем информацию
            return new FileDto
            {
                // Получаем и устанавливаем поток с файлом
                FileStream = await storage.OpenAsync(file.Local.Path),

                // Получаем и устанавливаем имя файла
                Name = Path.GetFileName(file.Local.Path)
            };
        }

        // Считаем процент загрузки
        var progress = file.Local.DownloadedSize / (double)file.Size * 100;

        // Вызываем исключение
        throw new FileNotDownloadedException(progress);
    }
}