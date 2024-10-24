using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Commands.TgFiles;
using TgInstanceAgent.Application.Abstractions.DTOs.Files;
using TgInstanceAgent.Application.Abstractions.FileStorage;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgFiles;

/// <summary>
/// Обработчик запроса на загрузку и получение файла из сообщения.
/// </summary>
public class DownloadAndGetFileCommandHandler(
    IInstancesService instancesService,
    IInstanceRepository repository,
    IMemoryCache cache,
    IFileStorage storage)
    : IRequestHandler<DownloadAndGetFileCommand, FileDto>
{
    /// <summary>
    /// Объект TaskCompletionSource, который используется для асинхронного ожидания завершения загрузки файла.
    /// </summary>
    private readonly TaskCompletionSource<TgFile> _fileDownloadTaskCompletionSource = new();

    /// <summary>
    /// Обрабатывает команду загрузки и получение файла по Id.
    /// </summary>
    /// <param name="request">Команда на загрузку и получение файла.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task<FileDto> Handle(DownloadAndGetFileCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша/репозитория по указанному идентификатору асинхронно
        var instance = await cache.GetInstanceAsync(request.InstanceId, repository, cancellationToken);

        // Проверяем возможность загрузки файлом
        instance.CheckDownloadRestrictions();

        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получаем запрос на получение файла
        var fileRequest = await request.GetFileRequest(telegramClient, cancellationToken);

        var file = await telegramClient.GetFileAsync(fileRequest, cancellationToken);

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

        void OnNewEventHandler(TgEvent updateFile) => OnNewEvent(updateFile, file.Id);

        // Подписываемся на событие NewEvent
        telegramClient.NewEvent += @event => OnNewEvent(@event, file.Id);

        try
        {
            // Запускаем процесс загрузки файла асинхронно
            await telegramClient.DownloadFileAsync(fileRequest, cancellationToken);

            // Ожидаем завершения загрузки файла или отмены операции
            var fileData = await _fileDownloadTaskCompletionSource.Task.WaitAsync(cancellationToken);

            // Обновляем ограничения, связанные с загрузками, после завершения операции
            instance.UpdateDownloadRestrictions();

            // Обновляем данные инстанса в базе данных
            await repository.UpdateAsync(instance);

            // Открываем файл на чтение и возвращаем информацию
            return new FileDto
            {
                // Получаем и устанавливаем поток с файлом
                FileStream = await storage.OpenAsync(fileData.Local.Path),

                // Получаем и устанавливаем имя файла
                Name = Path.GetFileName(fileData.Local.Path)
            };
        }
        finally
        {
            // Отписываемся от события с использованием того же делегата
            telegramClient.NewEvent -= OnNewEventHandler;
        }
    }

    /// <summary>
    /// Приватный метод для обработки нового события обновления файла.
    /// </summary>
    private void OnNewEvent(TgEvent @event, int localId)
    {
        // Проверяем, является ли событие событием обновления файла
        if (@event is not TgUpdateFileEvent updateFileEvent) return;

        // Проверяем, совпадает ли идентификатор файла с идентификатором файла из события
        if (updateFileEvent.File.Id != localId) return;
        
        // Проверяем, загружен ли файл
        if (!updateFileEvent.File.Local.IsDownloadingCompleted) return;

        // Устанавливаем результат TaskCompletionSource, чтобы завершить ожидающий таск с данными о файле
        _fileDownloadTaskCompletionSource.SetResult(updateFileEvent.File);
    }
}