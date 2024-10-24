using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TgInstanceAgent.Application.Abstractions.Commands.TgMessages;
using TgInstanceAgent.Application.Abstractions.Queries.TgMessages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Infrastructure.Web.Filters;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Controllers;

/// <summary>
/// Контроллер отвечающий за работу с сообщениями
/// </summary>
/// <param name="mediator">Медиатор</param>
[Authorize]
[InstanceFilter]
[ApiController]
[Route("apiTg/[controller]/[action]/{instanceId:guid}")]
public class MessagesController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить данные сообщения
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель с информацией о сообщении</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgMessage> GetMessage(Guid instanceId, [FromQuery] GetMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<GetMessageQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на получение сообщения и возвращаем его
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получить список доступных реакций на сообщение
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель с информацией о сообщении</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgMessageAvailableReactions> GetMessageAvailableReactions(Guid instanceId,
        [FromQuery] GetMessageReactionsInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<GetMessageAvailableReactionsQuery>(model,
                options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на получение доступных реакций на сообщение и возвращаем их
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получить список добавленных реакций на сообщение
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель с информацией о сообщении</param>
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgMessageAddedReactions> GetMessageAddedReactions(Guid instanceId,
        [FromQuery] GetMessageAddedReactionsInputModel model)
    {
        // Создаем команду
        var command =
            mapper.Map<GetMessageAddedReactionsQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на получение добавленных на сообщение реакций и возвращаем их
        return await mediator.Send(command);
    }

    /// <summary>
    /// Отправка текстового сообщения
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendTextMessage(Guid instanceId, SendTextMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SendTextMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на отправку текстового сообщения
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Отправка dice сообщения
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendDiceMessage(Guid instanceId, SendDiceMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SendDiceMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на отправку dice сообщения
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Отправка геолокации
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendLocationMessage(Guid instanceId, SendLocationMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SendLocationMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на отправку сообщения с геолокацией
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Отправка аудио-сообщения
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendAudioMessage(Guid instanceId, SendAudioMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SendAudioMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на отправку анимации
            return await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();

            // Закрываем поток с превью вручную после выполнения команды
            if (command.Thumbnail != null) await command.Thumbnail.DisposeAsync();
        }
    }

    /// <summary>
    /// Отправка голосового сообщения
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendVoiceNoteMessage(Guid instanceId, SendVoiceNoteMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SendVoiceNoteMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на отправку анимации
            return await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();
        }
    }

    /// <summary>
    /// Отправка кружочка
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendVideoNoteMessage(Guid instanceId, SendVideoNoteMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SendVideoNoteMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на отправку анимации
            return await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();
        }
    }

    /// <summary>
    /// Отправка видео
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendVideoMessage(Guid instanceId, SendVideoMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SendVideoMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на отправку анимации
            return await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();
        }
    }

    /// <summary>
    /// Отправка документа
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendDocumentMessage(Guid instanceId, SendDocumentMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SendDocumentMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на отправку анимации
            return await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();
        }
    }

    /// <summary>
    /// Отправка фото
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendPhotoMessage(Guid instanceId, SendPhotoMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SendPhotoMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на отправку анимации
            return await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();
        }
    }


    /// <summary>
    /// Отправка стикера
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendStickerMessage(Guid instanceId, SendStickerMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SendStickerMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на отправку анимации
            return await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();
        }
    }

    /// <summary>
    /// Отправка истории
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendStoryMessage(Guid instanceId, SendStoryMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SendStoryMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на отправку истории
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Отправка анимации(гиф)
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendAnimationMessage(Guid instanceId, SendAnimationMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SendAnimationMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на отправку анимации
            return await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();
        }
    }

    /// <summary>
    /// Отправка контакта
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgMessage> SendContactMessage(Guid instanceId, SendContactMessageInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SendContactMessageCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на отправку фото
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Добавление реакции на сообщение
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные реакции</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task AddMessageReaction(Guid instanceId, AddMessageReactionInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<AddMessageReactionCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на добавление реакции на сообщение
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Удаление реакции с сообщения
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные реакции</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task RemoveMessageReaction(Guid instanceId, RemoveMessageReactionInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<RemoveMessageReactionCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на удаление реакции с сообщения
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Пересылка сообщений
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщений</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Коллекцию пересланных сообщений</returns>
    [HttpPost]
    public async Task<IReadOnlyCollection<TgMessage>> ForwardMessages(Guid instanceId, ForwardMessagesInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<ForwardMessagesCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на пересылку сообщений
        return await mediator.Send(command, cancellationToken);
    }


    /// <summary>
    /// Удаление сообщений
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные сообщений</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task DeleteMessages(Guid instanceId, DeleteMessagesInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<DeleteMessagesCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на удаление сообщений
        await mediator.Send(command, cancellationToken);
    }
}