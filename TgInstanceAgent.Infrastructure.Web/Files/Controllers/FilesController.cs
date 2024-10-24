using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TgInstanceAgent.Application.Abstractions.Commands.TgFiles;
using TgInstanceAgent.Application.Abstractions.Queries.TgFiles;
using TgInstanceAgent.Infrastructure.Web.Files.InputModels;
using TgInstanceAgent.Infrastructure.Web.Filters;

namespace TgInstanceAgent.Infrastructure.Web.Files.Controllers;

/// <summary>
/// Контроллер отвечающий за загрузку файлов
/// </summary>
/// <param name="mediator">Медиатор</param>
[Authorize]
[InstanceFilter]
[ApiController]
[Route("apiTg/[controller]/[action]/{instanceId:guid}")]
public class FilesController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Запрос на загрузку файла по идентификатору файла или из сообщения
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель с идентификатором файла или данными сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task DownloadFile(Guid instanceId, FileRequestInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<DownloadFileCommand>(model, options => options.Items.Add("InstanceId", instanceId));
        
        // Отправляем запрос медиатору на начало загрузки файла
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получение файла по идентификатору файла или из сообщения
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель с идентификатором файла и данными сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<FileStreamResult> GetFile(Guid instanceId, [FromQuery] FileRequestInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<GetFileQuery>(model, options => options.Items.Add("InstanceId", instanceId));
        
        // Отправляем запрос медиатору на получение файла
        var file =  await mediator.Send(command, cancellationToken);

        // Отправляем файл. Поддерживает отправку больших файлов, файл будет отправляться порционно. После отправки поток закроется
        return File(file.FileStream, "application/octet-stream", file.Name, true);
    }
    
    /// <summary>
    /// Загрузка и получение файла по идентификатору из сообщения
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель с идентификатором чата и сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<FileStreamResult> DownloadAndGetFile(Guid instanceId, [FromQuery] FileRequestInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<DownloadAndGetFileCommand>(model, options => options.Items.Add("InstanceId", instanceId));
        
        // Отправляем запрос медиатору на получение файла
        var file =  await mediator.Send(command, cancellationToken);
        
        // Отправляем файл. Поддерживает отправку больших файлов, файл будет отправляться порционно. После отправки поток закроется
        return File(file.FileStream, "application/octet-stream", file.Name, true);
    }
}