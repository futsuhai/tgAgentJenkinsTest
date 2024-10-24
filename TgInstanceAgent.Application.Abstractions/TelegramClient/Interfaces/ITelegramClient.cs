using System.Threading.Channels;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Files;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Profile;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Proxy;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Stories;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Users;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Interfaces;

/// <summary>
/// Интерфейс клиента телеграм
/// </summary>
public interface ITelegramClient : IAsyncDisposable
{
    /// <summary>
    /// Событие от WhatsApp
    /// </summary>
    public event NewEvent NewEvent;

    /// <summary>
    /// Метод инициирует подключение к Tdlib
    /// </summary>
    Task ConnectAsync();

    /// <summary>
    /// Метод отдает текущие данные аккаунта
    /// </summary>
    /// <returns>Возвращает минимальный набор данных об аккаунте</returns>
    Task<TgUser> GetMeAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Метод выполняет выход из аккаунта
    /// </summary>
    Task LogOutAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Метод возвращает контакты
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Коллекция кратких данных о контактах</returns>
    Task<TgCountResult<TgUser>> GetContactsAsync(TgGetContactsRequest request);

    /// <summary>
    /// Метод выполняет поиск контактов
    /// </summary>
    /// <param name="request">Запрос на поиск контактов</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Коллекция кратких данных о контактах</returns>
    Task<TgCountResult<TgUser>> SearchContactsAsync(TgSearchContactsRequest request,
        CancellationToken cancellationToken);

    /// <summary>
    /// Метод отдает чат
    /// </summary>
    /// <param name="request">Данные чата</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Данные чата</returns>
    Task<TgChat> GetChatAsync(TgInputChat request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод отдает коллекцию папок чатов
    /// </summary>
    /// <returns>Коллекция папок чатов</returns>
    TgChatFolders GetChatFolders();

    /// <summary>
    /// Метод отдает количество непрочитанных чатов в списках чатов
    /// </summary>
    /// <returns>Коллекция объектов количества непрочитанных чатов в списках чатов</returns>
    IReadOnlyCollection<TgUnreadChatsCount> GetUnreadChatsCount();

    /// <summary>
    /// Метод отдает количество непрочитанных сообщений в списках чатов
    /// </summary>
    /// <returns>Коллекция объектов количества непрочитанных сообщений в списках чатов</returns>
    IReadOnlyCollection<TgUnreadMessagesCount> GetUnreadMessagesCount();

    /// <summary>
    /// Метод получения списка чатов
    /// </summary>
    /// <param name="request">Запрос на получение списка чатов</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<TgChats> GetChatsAsync(TgGetChatsRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод выполняет поиск публичных чатов
    /// </summary>
    /// <param name="query">Запрос на поиск публичных чатов</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Коллекция найденных публичных чатов</returns>
    Task<TgCountResult<TgChat>> SearchPublicChatsAsync(string query, CancellationToken cancellationToken);

    /// <summary>
    /// Метод выполняет поиск чатов
    /// </summary>
    /// <param name="request">Запрос на поиск контактов</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Коллекция найденных чатов</returns>
    Task<TgCountResult<TgChat>> SearchChatsAsync(TgSearchChatsRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод создает (загружает) чат.
    /// </summary>
    /// <param name="request">Данные чата</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Объект с историей сообщений</returns>
    Task<TgChat> CreateChatAsync(TgCreateChatRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод выполняет изменение заголовка чата
    /// </summary>
    /// <param name="request">Запрос на изменение заголовка чата</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatTitleAsync(TgSetChatTitleRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод выполняет изменение описания чата
    /// </summary>
    /// <param name="request">Запрос на изменение описания чата</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatDescriptionAsync(TgSetChatDescriptionRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод выполняет изменение настроек уведомлений чата
    /// </summary>
    /// <param name="request">Запрос на изменение настроек уведомлений чата</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatNotificationSettingsAsync(TgSetChatNotificationSettingsRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод выполняет изменение акцентного цвета чата
    /// </summary>
    /// <param name="request">Запрос на изменение акцентного цвета чата</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatAccentColorAsync(TgSetChatAccentColorRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод выполняет изменение разрешений в чате
    /// </summary>
    /// <param name="request">Запрос на изменение разрешений в чате</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatPermissionsAsync(TgSetChatPermissionsRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод выполняет изменение задержки отправки сообщений в чате
    /// </summary>
    /// <param name="request">Запрос на изменение задержки отправки сообщений в чате</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatSlowModeDelayAsync(TgSetChatSlowModeDelayRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод выполняет установку времени автоудаления сообщений в чате
    /// </summary>
    /// <param name="request">Запрос на установку времени автоудаления сообщений в чате</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatMessageAutoDeleteTimeAsync(TgSetChatMessageAutoDeleteTimeRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод выполняет изменение доступных реакций в чате
    /// </summary>
    /// <param name="request">Запрос на изменение доступных реакций в чате</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatAvailableReactionsAsync(TgSetChatAvailableReactionsRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод выполняет изменение отправителя сообщений в групповом чате
    /// </summary>
    /// <param name="request">Запрос на изменение отправителя сообщений в групповом чате</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatMessageSenderAsync(TgSetChatMessageSenderRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод выполняет изменение статуса емозди в чате
    /// </summary>
    /// <param name="request">Запрос на изменение статуса емозди в чате</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatEmojiStatusAsync(TgSetChatEmojiStatusRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод выполняет изменение цвета профиля группового чата
    /// </summary>
    /// <param name="request">Запрос на изменение цвета профиля группового чата</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatProfileAccentColorAsync(TgSetChatProfileAccentColorRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод выполняет изменение обсуждаемой группы
    /// </summary>
    /// <param name="request">Запрос на изменение обсуждаемой группы</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatDiscussionGroupAsync(TgSetChatDiscussionGroupRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Метод выполняет изменение фото группы
    /// </summary>
    /// <param name="request">Запрос на изменение фото группы</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SetChatPhotoAsync(TgSetChatProfilePhotoRequest request, CancellationToken cancellationToken);

    /// <summary> 
    /// Метод открывает чат.
    /// </summary>
    /// <param name="tgInputChat">данные пользователя</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Данные чата</returns>
    Task<TgChat> OpenChatAsync(TgInputChat tgInputChat, CancellationToken cancellationToken);

    /// <summary>
    /// Метдот закрывает чат.
    /// </summary>
    /// <param name="tgInputChat">данные пользователя</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task CloseChatAsync(TgInputChat tgInputChat, CancellationToken cancellationToken);

    /// <summary>
    /// Метод возвращает данные пользователя
    /// </summary>
    /// <param name="request">Запрос на получение пользователя</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Данные пользователя</returns>
    Task<TgUser> GetUserAsync(TgInputUser request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод устанавливает номер телефона для аутентификации
    /// </summary>
    /// <param name="phoneNumber">Номер телефона</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    Task SetAuthenticationPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);

    /// <summary>
    /// Метод проверяет код аутентификации
    /// </summary>
    /// <param name="code">Код авторизации</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    Task SetAuthenticationCodeAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Метод устанавливает пароль для аутентификации Telegram
    /// </summary>
    /// <param name="password">Пароль</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    Task SetAuthenticationPasswordAsync(string password, CancellationToken cancellationToken);

    /// <summary>
    /// Метод аутентифицирует пользователя с помощью qr кода
    /// </summary>
    /// <param name="token">Токен отмены</param>
    /// <returns>Канал для записи кодов</returns>
    Task<Channel<string>> QrCodeAuthenticateAsync(CancellationToken token);

    /// <summary>
    /// Метод отправляет сообщение
    /// </summary>
    /// <param name="messageRequest">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns>Данные сообщения</returns>
    Task<TgMessage> SendMessageAsync(TgSendMessageRequest messageRequest, CancellationToken cancellationToken);

    /// <summary>
    /// Метод добавляет реакцию на сообщение
    /// </summary>
    /// <param name="request">Данные рекации</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    Task AddMessageReactionAsync(TgMessageReactionRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод удаляет реакцию с сообщения
    /// </summary>
    /// <param name="request">Данные реакции</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns></returns>
    Task RemoveMessageReactionAsync(TgMessageReactionRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод отдает историю чата
    /// </summary>
    /// <param name="request">Данные чата</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Объект с историей сообщений</returns>
    Task<TgMessages> GetChatHistoryAsync(TgGetChatHistoryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод выполняет поиск сообщений в чате
    /// </summary>
    /// <param name="request">Запрос на поиск сообщений в чате</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Коллекция найденных сообщений</returns>
    Task<TgCountResult<TgMessage>> SearchChatMessagesAsync(TgSearchChatMessagesRequest request,
        CancellationToken cancellationToken);

    /// <summary>
    /// Метод выполняет поиск сообщений во всех чатах
    /// </summary>
    /// <param name="request">Запрос на поиск сообщений во всех чатах</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Коллекция найденных сообщений</returns>
    Task<TgFoundMessages> SearchMessagesAsync(TgSearchMessagesRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод отдает данные сообщения
    /// </summary>
    /// <param name="request">Данные поиска сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns>Объект с данными сообщения</returns>
    Task<TgMessage> GetMessageAsync(TgGetMessageRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод добавляет контакты
    /// </summary>
    /// <param name="request">Данные контактов</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Результат импорта контактов</returns>
    Task<TgImportedContacts> ImportContactsAsync(IEnumerable<TgImportContactRequest> request,
        CancellationToken cancellationToken);

    /// <summary>
    /// Метод удаляет контакт
    /// </summary>
    /// <param name="request">Данные контакта</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    Task RemoveContactAsync(TgInputUser request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод добавляет контакт
    /// </summary>
    /// <param name="request">Данные контакта</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    Task AddContactAsync(TgAddContactRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод загружает файл
    /// </summary>
    /// <param name="request">Данные загружаемого файла</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    Task DownloadFileAsync(TgInputFileRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод отдает путь к загруженному файлу
    /// </summary>
    /// <param name="request">Данные загружаемого файла</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns>Путь к файлу</returns>
    Task<TgFile> GetFileAsync(TgInputFileRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод отдает список фотографий пользователя.
    /// </summary>
    /// <param name="request">Данные о пользователе и запросе</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns>Список фотографий</returns>
    Task<TgCountResult<TgChatPhoto>> GetProfilePhotosAsync(TgGetUserProfilePhotosRequest request,
        CancellationToken cancellationToken);

    /// <summary>
    /// Метод добавляет аватар в Telegram
    /// </summary>
    /// <param name="request">Данные с фотографией</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns></returns>
    Task SetProfilePhotoAsync(TgSetProfilePhotoRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод возвращает активные истории пользователя
    /// </summary>
    /// <param name="request">Данные пользователя</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns>Модель, содержащую информацию об активных историях пользователя</returns>
    Task<TgChatActiveStories> GetChatActiveStories(TgInputChat request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод устаналивает новую историю пользователю
    /// Возвращает подроную информацию об установленной истории
    /// </summary>
    /// <param name="request">Данные истории</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns>Модель, содержащую информацию об установленной истории</returns>
    Task<TgStory> PostStoryAsync(TgPostStoryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод возвращает подроную информацию об истории
    /// </summary>
    /// <param name="request">Данные для получение истории по идентификатору</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns>Модель, содержащую информацию о полученной истории</returns>
    Task<TgStory> GetStoryAsync(TgGetStoryRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод установки био текущего пользователя
    /// </summary>
    /// <param name="bio">Строка био</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns></returns>
    Task SetUserBioAsync(string? bio, CancellationToken cancellationToken);

    /// <summary>
    /// Метод возвращает полную информацию о пользователе
    /// </summary>
    /// <param name="request">Данные пользователя</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns>Модель, содержащая информацию о пользователе.</returns>
    Task<TgUserFullInfo> GetUserFullInfoAsync(TgInputUser request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод возвращает коллекцию сообщений
    /// </summary>
    /// <param name="request">Данные о сообщениях для пересылки</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns>Коллекция сообщений</returns>
    Task<IReadOnlyCollection<TgMessage>> ForwardMessagesAsync(TgForwardMessagesRequest request,
        CancellationToken cancellationToken);

    /// <summary>
    /// Метод удаления сообщейний в чате
    /// </summary>
    /// <param name="request">Данные о сообщениях для удаления</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    Task DeleteMessagesAsync(TgDeleteMessagesRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод для установки прокси сервера
    /// </summary>
    /// <param name="request">Данные о прокси сервере</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    Task SetProxyAsync(TgSetProxyRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Метод получает список доступных реакций на сообщение
    /// </summary>
    /// <param name="request">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns></returns>
    Task<TgMessageAvailableReactions> GetMessageAvailableReactionsAsync(TgGetMessageReactionsRequest request,
        CancellationToken cancellationToken);

    /// <summary>
    /// Метод получает список добавленных на сообщение реакций
    /// </summary>
    /// <param name="request">Данные сообщения</param>
    /// <param name="cancellationToken">Токен для отмены операции</param>
    /// <returns></returns>
    Task<TgMessageAddedReactions> GetAddedMessageReactionsAsync(TgGetMessageAddedReactionsRequest request,
        CancellationToken cancellationToken);
}