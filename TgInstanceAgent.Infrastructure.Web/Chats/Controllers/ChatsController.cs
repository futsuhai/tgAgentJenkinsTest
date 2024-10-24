using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TgInstanceAgent.Application.Abstractions.Commands.TgChats;
using TgInstanceAgent.Application.Abstractions.Queries.TgChats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;
using TgInstanceAgent.Infrastructure.Web.Filters;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Controllers;

/// <summary>
/// Контроллер отвечающий за работу с чатами
/// </summary>
/// <param name="mediator">Медиатор</param>
[Authorize]
[InstanceFilter]
[ApiController]
[Route("apiTg/[controller]/[action]/{instanceId:guid}")]
public class ChatsController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить чат
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные поиска</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgChat> GetChat(Guid instanceId, [FromQuery] ChatInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<GetChatQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на получение истории чата и возвращаем ее
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Создать чат.
    /// Данный метод создает (или загружает) чат.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные поиска</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task<TgChat> CreateChat(Guid instanceId, CreateChatInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<GetChatQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на получение истории чата и возвращаем ее
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Открыть чат.
    /// Данный метод открывает чат.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Входные данные чата</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public Task<TgChat> OpenChat(Guid instanceId, ChatInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<OpenChatCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на открытие чата
        return mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Закрыть чат.
    /// Данный метод закрывает чат.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Входные данные чата</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpPost]
    public async Task CloseChat(Guid instanceId, ChatInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<CloseChatCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на закрытие чата
        await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получить папки чатов
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgChatFolders> GetChatFolders(Guid instanceId, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = new GetChatFoldersQuery { InstanceId = instanceId };

        // Отправляем запрос медиатору на получение папок чатов и возвращаем их
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получить количество непрочитанных чатов в списках чатов
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<IEnumerable<TgUnreadChatsCount>> GetUnreadChatsCount(Guid instanceId, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = new GetUnreadChatsCountQuery { InstanceId = instanceId };

        // Отправляем запрос медиатору на получение папок чатов и возвращаем их
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получить количество непрочитанных сообщений в списках чатов
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<IEnumerable<TgUnreadMessagesCount>> GetUnreadMessagesCount(Guid instanceId, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = new GetUnreadMessagesCountQuery { InstanceId = instanceId };

        // Отправляем запрос медиатору на получение папок чатов и возвращаем их
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получить чаты
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные поиска</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgChats> GetChats(Guid instanceId, [FromQuery] GetChatsInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<GetChatsQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на получение истории чата и возвращаем ее
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Получить историю чата
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Модель с информацией о чате</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <response code="400">Невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgMessages> GetChatHistory(Guid instanceId,
        [FromQuery] GetChatHistoryInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<GetChatHistoryQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос медиатору на получение истории чата и возвращаем ее
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Поиск сообщений в чате
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="messageModel">Данные для поиска сообщений в чате</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Коллекция найденных сообщений и их колличетво</returns>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgFoundMessages> SearchMessages(Guid instanceId,
        [FromQuery] SearchMessagesInputModel messageModel, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SearchMessagesQuery>(messageModel, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на поиск сообщений в чате
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Поиск сообщений в чате
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="messageModel">Данные для поиска сообщений в чате</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Коллекция найденных сообщений и их колличетво</returns>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgCountResult<TgMessage>> SearchChatMessages(Guid instanceId,
        [FromQuery] SearchChatMessagesInputModel messageModel, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SearchChatMessagesQuery>(messageModel, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на поиск сообщений в чате
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Поиск чатов
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные для поиска чатов</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Коллекция найденных чатов и их колличетво</returns>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgCountResult<TgChat>> SearchChats(Guid instanceId,
        [FromQuery] SearchChatsInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SearchChatsQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на поиск чатов
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Поиск публичных чатов
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные для поиска публичных чатов</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Коллекция найденных публичных чатов и их колличетво</returns>
    /// <response code="400">Некорректные входные данные или невалидный запрос</response>
    /// <response code="500">Возникла ошибка на сервере</response>
    [HttpGet]
    public async Task<TgCountResult<TgChat>> SearchPublicChats(Guid instanceId,
        [FromQuery] SearchPublicChatsInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SearchPublicChatsQuery>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на поиск чатов
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Изменение заголовка чата
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные для смены заголовка чата</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetChatTitle(Guid instanceId, SetChatTitleInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetChatTitleCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на изменение заголовка чата
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Изменение описания чата
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные для смены описания чата</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetChatDescription(Guid instanceId, SetChatDescriptionInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetChatDescriptionCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на изменение описания чата
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Изменение настроек уведомлений чата
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные для смены настроек уведомлений чата</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetChatNotificationSettings(Guid instanceId, SetChatNotificationSettingsInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetChatNotificationSettingsCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на изменение настроек уведомлений чата
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Изменение цвета акцента чата.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="model">Данные для изменения цвета акцента чата.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetChatAccentColor(Guid instanceId, SetChatAccentColorInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetChatAccentColorCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на изменение цвета акцента чата
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Изменение разрешений чата.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для изменения разрешений чата.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    [HttpPost]
    public async Task SetChatPermissions(Guid instanceId, SetChatPermissionsInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetChatPermissionsCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на изменение разрешений чата
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Изменение задержки отправки сообщений для чата.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для изменения задержки отправки сообщений чата.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    [HttpPost]
    public async Task SetChatSlowModeDelay(Guid instanceId, SetChatSlowModeDelayInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetChatSlowModeDelayCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на изменение задержки отправки сообщений чата
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Изменение времени автоудаления сообщений в чате
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для изменения времени автоудаления сообщений в чате.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    [HttpPost]
    public async Task SetChatMessageAutoDeleteTime(Guid instanceId, SetChatMessageAutoDeleteTimeInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetChatMessageAutoDeleteTimeCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на изменение времени автоудаления сообщений в чате
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Изменение доступных в чате реакций
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для изменения доступных в чате реакций.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    [HttpPost]
    public async Task SetChatAvailableReactions(Guid instanceId, SetChatAvailableReactionsInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetChatAvailableReactionsCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем изменение доступных в чате реакций
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Изменение отправителя сообщений в групповом чате
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для изменения отправителя сообщений в групповом чате.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    [HttpPost]
    public async Task SetChatMessageSender(Guid instanceId, SetChatMessageSenderInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetChatMessageSenderCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос на изменение отправителя сообщений в групповом чате
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Изменение емозди статуса чата. Supergroup + Boost
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для изменения емозди статуса чата.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    [HttpPost]
    public async Task SetChatEmojiStatus(Guid instanceId, SetChatEmojiStatusInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetChatEmojiStatusCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос на изменение емозди статуса чата
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Изменение цвета профиля группового чата. Supergroup + Boost
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для изменения цвета профиля группового чата.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    [HttpPost]
    public async Task SetChatProfileAccentColor(Guid instanceId, SetChatProfileAccentColorInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetChatProfileAccentColorCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем запрос на изменение цвета профиля группового чата.
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Устанавливает группу обсуждения. Channels
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для изменения группы обсуждения канала</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param>
    [HttpPost]
    public async Task SetChatDiscussionGroupAsync(Guid instanceId, SetChatDiscussionGroupInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<SetChatDiscussionGroupCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду для установки группы обсуждения канала.
        await mediator.Send(command, cancellationToken);
    }
    
    /// <summary>
    /// Устанавливает фотографию профиля группового чата.
    /// Перед установкой по идентификатору файла необходимо предварительно убедиться, что файл загружен на сервер.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для установки фотографии профиля группового чата.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetChatProfilePhoto(Guid instanceId, SetChatProfilePhotoInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SetChatProfilePhotoCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        try
        {
            // Отправляем запрос медиатору на установку фото профиля группового чата
            await mediator.Send(command, cancellationToken);
        }
        finally
        {
            // Закрываем поток с файлом вручную после выполнения команды
            if (command.File != null) await command.File.DisposeAsync();
        }
    }
    
    /// <summary>
    /// Устанавливает фотографию профиля чата в виде кастомного эмоджи.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса.</param>
    /// <param name="model">Данные для установки фотографии профиля.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    [HttpPost]
    public async Task SetChatEmojiProfilePhoto(Guid instanceId, SetChatEmojiProfilePhotoInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем команду
        var command =
            mapper.Map<SetChatEmojiProfilePhotoCommand>(model, options => options.Items.Add("InstanceId", instanceId));

        // Отправляем команду на установку профиля чата в виде кастомного эмоджи.
        await mediator.Send(command, cancellationToken);
    }
    
    // /// <summary>
    // /// Изменение статуса участника чата
    // /// </summary>
    // /// <param name="instanceId">Идентификатор инстанса.</param>
    // /// <param name="model">Данные для изменения статуса участника чата.</param>
    // /// <param name="cancellationToken">Токен для отмены операции.</param>
    // [HttpPost]
    // public async Task SetChatMemberStatus(Guid instanceId, SetChatMemberStatusInputModel model, CancellationToken cancellationToken)
    // {
    //     // Создаем команду
    //     var command = mapper.Map<>(model, options => options.Items.Add("InstanceId", instanceId));
    //
    //     // Отправляем запрос на изменение статуса участника чата
    //     await mediator.Send(command, cancellationToken);
    // }
}