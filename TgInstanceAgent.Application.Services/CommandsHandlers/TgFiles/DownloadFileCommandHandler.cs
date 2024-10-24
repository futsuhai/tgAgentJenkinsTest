using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Commands.TgFiles;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.TgFiles;

/// <summary>
/// Обработчик команды на загрузку файла по Id
/// </summary>
public class DownloadFileCommandHandler(IInstancesService instancesService, IInstanceRepository repository, IMemoryCache cache)
    : IRequestHandler<DownloadFileCommand>
{
    /// <summary>
    /// Обрабатывает команду загрузки файла по Id.
    /// </summary>
    /// <param name="request">Команда на загрузку файла.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public async Task Handle(DownloadFileCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша/репозитория по указанному идентификатору асинхронно
        var instance = await cache.GetInstanceAsync(request.InstanceId, repository, cancellationToken);
        
        // Проверяем возможность загрузки файлом
        // instance.CheckDownloadRestrictions();
        
        // Запуск телеграм клиента инстанса.
        var telegramClient = await instancesService.StartClientAsync(request.InstanceId, cancellationToken);

        // Получаем запрос на получение файла
        var fileRequest = await request.GetFileRequest(telegramClient, cancellationToken);
        
        // Запрос на загрузку файла
        await telegramClient.DownloadFileAsync(fileRequest, cancellationToken);
        
        // Обновляем органичения загрузке
        // instance.UpdateDownloadRestrictions();
        
        // Обновляем данные в базе данных
        await repository.UpdateAsync(instance);
    }
}