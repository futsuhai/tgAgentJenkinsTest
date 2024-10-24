using System.Threading.Channels;
using AutoMapper;
using TdLib;
using TdLib.Bindings;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Files;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Profile;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Proxy;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Stories;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Users;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Stories;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;
using TgInstanceAgent.Infrastructure.Telegram.Enums;
using TgInstanceAgent.Infrastructure.Telegram.Extensions;
using TgInstanceAgent.Infrastructure.Telegram.Structs;
using TgInstanceAgent.Infrastructure.Telegram.Visitors.Chats;
using TgInstanceAgent.Infrastructure.Telegram.Visitors.Files;
using TgInstanceAgent.Infrastructure.Telegram.Visitors.Messages;
using TgInstanceAgent.Infrastructure.Telegram.Visitors.ProfilePhoto;
using TgInstanceAgent.Infrastructure.Telegram.Visitors.Proxies;
using TgInstanceAgent.Infrastructure.Telegram.Visitors.Stories;
using TgInstanceAgent.Infrastructure.Telegram.Visitors.Users;
using TdLogLevel = TdLib.Bindings.TdLogLevel;
using TgChatFolderInfo = TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats.TgChatFolderInfo;

namespace TgInstanceAgent.Infrastructure.Telegram.Client;

/// <inheritdoc/>
/// <summary>
/// Сервис клиента Telegram.
/// </summary>
public class TelegramClient : ITelegramClient
{
    /// <summary>
    /// Состояние клиента.
    /// </summary>
    private ClientState _clientState = ClientState.NotReady;

    /// <summary>
    /// Клиент Telegram.
    /// </summary>
    private readonly TdClient _client;

    /// <summary>
    /// Событие, которое срабатывает при получении события от Telegram.
    /// </summary>
    public event NewEvent? NewEvent;

    /// <summary>
    /// Задача запуска.
    /// </summary>
    private TaskCompletionSource? _start;

    /// <summary>
    /// Данные авторизации по коду.
    /// </summary>
    private CodeAuthenticationWaitData? _codeAuthentication;

    /// <summary>
    /// Данные авторизации по QR-коду.
    /// </summary>
    private QrAuthenticationWaitData? _qrAuthentication;

    /// <summary>
    /// Словарь пользователей
    /// </summary>
    private readonly Users _users = new();

    /// <summary>
    /// Словарь чатов
    /// </summary>
    private readonly Chats _chats = new();

    /// <summary>
    /// Словарь папок чатов
    /// </summary>
    private readonly ChatFolders _chatFolders = new();

    /// <summary>
    /// Количество непрочитанных чатов
    /// </summary>
    private readonly UnreadChatsCount _unreadChatsCount = new();

    /// <summary>
    /// Количество непрочитанных сообщений
    /// </summary>
    private readonly UnreadMessagesCount _unreadMessagesCount = new();

    /// <summary>
    /// Данные приложения телеграм
    /// </summary>
    private readonly TelegramApp _telegramApp;

    /// <summary>
    /// Локаци файлов
    /// </summary>
    private readonly string _filesLocation;

    /// <summary>
    /// Маппер
    /// </summary>
    private readonly IMapper _mapper;

    /// <summary>
    /// Сервис клиента Telegram.
    /// </summary>
    /// <param name="telegramApp">Приложение Telegram.</param>
    /// <param name="filesLocation">Местоположение файлов.</param>
    /// /// <param name="mapper">Маппер.</param>
    public TelegramClient(TelegramApp telegramApp, string filesLocation, IMapper mapper)
    {
        // Устанавливаем настройки телеграма
        _telegramApp = telegramApp;

        // Устанавливаем локацию файлов
        _filesLocation = filesLocation;

        // Устанавливаем маппер
        _mapper = mapper;

        // Создаем объект TaskCompletionSource для отслеживания завершения старта. 
        _start = new TaskCompletionSource();

        // Создаем новый инстанс TdClient. 
        _client = new TdClient();

        // Устанавливаем уровень логирования на Fatal. 
        _client.Bindings.SetLogVerbosityLevel(TdLogLevel.Warning);

        // Подписываемся на событие UpdateReceived и вызываем метод ProcessUpdates для обработки обновлений. 
        _client.UpdateReceived += ProcessUpdate;
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно устанавливает соединение с TDLib. 
    /// </summary> 
    public async Task ConnectAsync()
    {
        // Проверяем состояние клиента, должно быть не готов к работе 
        if (_start == null) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Ожидаем завершения старта с помощью TaskCompletionSource. 
            await _start.Task;
        }
        finally
        {
            // Устанавливаем TaskCompletionSource в null. 
            _start = null;
        }
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно получает информацию о текущем аккаунте. 
    /// </summary> 
    public async Task<TgUser> GetMeAsync(CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Вызываем метод GetMeAsync
            var me = await _client.GetMeAsync().WaitAsync(cancellationToken);

            // Маппим результат в объект TgUser. 
            return _mapper.Map<TgUser>(me);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно выполняет выход из аккаунта. 
    /// </summary> 
    public async Task LogOutAsync(CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Выполняем выход из аккаунта с помощью метода LogOutAsync. 
            await _client.LogOutAsync().WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException с кодом 400, создаем и выбрасываем новое исключение InvalidCodeException. 
            if (ex.Error.Code == 400) throw new InvalidCodeException(ex.Error.Code, ex.Error.Message);

            // В противном случае, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно получает контакты. 
    /// </summary>
    public Task<TgCountResult<TgUser>> GetContactsAsync(TgGetContactsRequest request)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        // Получаем контакты
        var allContacts = _users.UsersList
            .Where(u => u.IsContact)
            .OrderBy(c => c.FirstName)
            .ThenBy(c => c.LastName)
            .ToArray();

        var portion = _mapper.Map<TgUser[]>(allContacts.Skip(request.Offset ?? 0).Take(request.Limit).ToArray());

        return Task.FromResult(new TgCountResult<TgUser>
        {
            List = portion,
            TotalCount = allContacts.Length
        });
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод получения чата
    /// </summary>
    public async Task<TgChat> GetChatAsync(TgInputChat request, CancellationToken cancellationToken)
    {
        // Проверяем текущее состояние клиента, чтобы удостовериться, что он готов к работе.
        // Если клиент не в состоянии Ready, выбрасываем исключение ClientNotExpectActionException.
        if (_clientState != ClientState.Ready)
            throw new ClientNotExpectActionException(_clientState.ToString());

        // Получаем Id чата из запроса
        var chatId = await GetChatIdAsync(request).WaitAsync(cancellationToken);

        // Находим чат в коллекции чатов.
        // Вызываем First, потому что если мы дошли до выполнения этой инструкции, значит чат уже должен быть в коллекции.
        var chat = _chats.ChatsList.First(c => c.Id == chatId);

        // Маппим и возвращаем чат.
        return _mapper.Map<TgChat>(chat);
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод отдает количество непрочитанных чатов в списках чатов
    /// </summary>
    public IReadOnlyCollection<TgUnreadChatsCount> GetUnreadChatsCount()
    {
        // Возвращаем количество непрочитанных чатов в списках чатов
        return _mapper.Map<TgUnreadChatsCount[]>(_unreadChatsCount.ChatLists);
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод отдает количество непрочитанных сообщений в списках чатов
    /// </summary>
    public IReadOnlyCollection<TgUnreadMessagesCount> GetUnreadMessagesCount()
    {
        // Возвращаем количество непрочитанных сообщений в списках чатов
        return _mapper.Map<TgUnreadMessagesCount[]>(_unreadMessagesCount.ChatLists);
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод отдает коллекцию папок чатов
    /// </summary>
    public TgChatFolders GetChatFolders()
    {
        // Возвращаем список папок
        return new TgChatFolders
        {
            ChatFolders = _mapper.Map<TgChatFolderInfo[]>(_chatFolders.Folders),
            MainChatListPosition = _chatFolders.MainChatListPosition,
            AreTagsEnabled = _chatFolders.AreTagsEnabled,
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод получения списка чатов
    /// </summary>
    public async Task<TgChats> GetChatsAsync(TgGetChatsRequest request, CancellationToken cancellationToken)
    {
        // Проверяем текущее состояние клиента, чтобы удостовериться, что он готов к работе.
        // Если клиент не в состоянии Ready, выбрасываем исключение ClientNotExpectActionException.
        if (_clientState != ClientState.Ready)
            throw new ClientNotExpectActionException(_clientState.ToString());

        // Исходя из указанных параметров запроса, фильтруем список чатов.
        IEnumerable<TdApi.Chat> chatsEnumerable = request.List switch
        {
            // Если указано, что список - это папка, фильтруем чаты в соответствии с этим и сортируем их по порядку в папке.
            TgChatList.Folder => _chats.ChatsList
                .Where(c => c.Positions.Any(p =>
                    p.List is TdApi.ChatList.ChatListFolder folder && folder.ChatFolderId == request.ChatFolderId))
                .OrderByDescending(c => c.Positions.First(p =>
                        p.List is TdApi.ChatList.ChatListFolder folder && folder.ChatFolderId == request.ChatFolderId)
                    .Order),

            // Если указано, что список - это главный список, фильтруем соответствующим образом.
            TgChatList.Main => _chats.ChatsList
                .Where(c => c.Positions.Any(p => p.List is TdApi.ChatList.ChatListMain))
                .OrderByDescending(c => c.Positions.First(p => p.List is TdApi.ChatList.ChatListMain).Order),

            // Если указано, что список - это архив, фильтруем и сортируем по порядку в архиве.
            TgChatList.Archive => _chats.ChatsList
                .Where(c => c.Positions.Any(p => p.List is TdApi.ChatList.ChatListArchive))
                .OrderByDescending(c => c.Positions.First(p => p.List is TdApi.ChatList.ChatListArchive).Order),

            // Если указанное значение списка чатов вне диапазона допустимых значений, выбрасываем исключение.
            _ => throw new ArgumentOutOfRangeException(nameof(request))
        };

        // Если задано значение FromChatId, пропускаем все чаты до указанного ID (исключительно).
        if (request.FromChatId.HasValue)
            chatsEnumerable = chatsEnumerable.SkipWhile(c => c.Id != request.FromChatId).Skip(1);

        // Преобразуем оставшиеся чаты в массив для удобства дальнейшей обработки.
        var allChats = chatsEnumerable.ToArray();

        // Берем определенное количество чатов, начиная с указанного смещения (Offset).
        var portion = allChats.Skip(request.Offset ?? 0).Take(request.Limit).ToArray();

        // Маппим чаты
        var mappedPortion = _mapper.Map<TgChat[]>(portion);

        // Рассчитываем количество оставшихся чатов.
        var rest = allChats.Length - (request.Offset ?? 0 + portion.Length);

        // Получаем пользователей
        var users = _mapper.Map<IReadOnlyCollection<TgUser>>(_users.GetUsersForChats(portion));

        // Если есть еще не выданные чаты после текущей порции
        if (rest > 0)
        {
            // Возвращаем результат со списком выданных чатов, без коллекции пользователей
            if (!request.IncludeUsers) return new TgChats { Chats = mappedPortion, HasMore = true };

            // Возвращаем результат со списком выданных чатов и коллекцией пользователей
            return new TgChats
            {
                Chats = mappedPortion,
                Users = _mapper.Map<IReadOnlyCollection<TgUser>>(users),
                HasMore = true
            };
        }

        // Иначе предполагаем, что могут быть дополнительные чаты.
        var hasMore = true;

        // Создаем объект списка чатов, который используется для запроса дополнительных чатов.
        TdApi.ChatList list = request.List switch
        {
            TgChatList.Main => new TdApi.ChatList.ChatListMain(),
            TgChatList.Archive => new TdApi.ChatList.ChatListArchive(),
            TgChatList.Folder => new TdApi.ChatList.ChatListFolder { ChatFolderId = request.ChatFolderId ?? 0 },
            _ => throw new ArgumentOutOfRangeException()
        };

        try
        {
            // Пытаемся загрузить дополнительные чаты асинхронно.
            await _client.LoadChatsAsync(list, request.Limit).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если список чатов пуст (код ошибки 404), устанавливаем флаг hasMore в false.
            if (ex.Error.Code == 404) hasMore = false;

            // Иначе вызываем исключение
            else throw new ClientException(ex.Error.Code, ex.Error.Message);
        }

        // Возвращаем результат со списком выданных чатов, без коллекции пользователей
        if (!request.IncludeUsers) return new TgChats { Chats = mappedPortion, HasMore = true };

        // Возвращаем результат со списком выданных чатов и коллекцией пользователей
        return new TgChats
        {
            Chats = mappedPortion,
            Users = _mapper.Map<IReadOnlyCollection<TgUser>>(users),
            HasMore = hasMore
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет поиск публичных чатов
    /// </summary>
    public async Task<TgCountResult<TgChat>> SearchPublicChatsAsync(string query, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Выполняем запрос клиенту на поиск публичных чатов.
            var publicChats = await _client.SearchPublicChatsAsync(query).WaitAsync(cancellationToken);

            // Фильтруем публичные чаты по полученным Id.
            var searchedPublicChats = _chats.ChatsList.Where(x => publicChats.ChatIds.Contains(x.Id));

            // Маппим.
            var resultChats = _mapper.Map<TgChat[]>(searchedPublicChats);

            // Возвращаем список публичных чатов.
            return new TgCountResult<TgChat>
            {
                List = resultChats,
                TotalCount = publicChats.TotalCount
            };
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет поиск чатов
    /// </summary>
    public async Task<TgCountResult<TgChat>> SearchChatsAsync(TgSearchChatsRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Выполняем запрос клиенту на поиск чатов.
            var chats = await _client.SearchChatsAsync(request.Query, request.Limit).WaitAsync(cancellationToken);

            // Фильтруем чаты по полученным Id.
            var searchedChats = _chats.ChatsList.Where(x => chats.ChatIds.Contains(x.Id));

            // Маппим.
            var resultChats = _mapper.Map<TgChat[]>(searchedChats);

            // Возвращаем список публичных чатов.
            return new TgCountResult<TgChat>
            {
                List = resultChats,
                TotalCount = chats.TotalCount
            };
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет изменение заголовка чата
    /// </summary>
    public async Task SetChatTitleAsync(TgSetChatTitleRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Изменяем заголовок чата
            await _client.SetChatTitleAsync(request.ChatId, request.Title).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет изменение описания чата
    /// </summary>
    public async Task SetChatDescriptionAsync(TgSetChatDescriptionRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Изменяем описание чата
            await _client.SetChatDescriptionAsync(request.ChatId, request.Description).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет изменение настроек уведомлений чата
    /// </summary>
    public async Task SetChatNotificationSettingsAsync(TgSetChatNotificationSettingsRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем Id чата из запроса
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            // Изменяем настройки уведомлений чата
            await _client.SetChatNotificationSettingsAsync(chatId, new TdApi.ChatNotificationSettings
            {
                UseDefaultMuteFor = request.UseDefaultMuteFor,
                MuteFor = request.MuteFor,
                UseDefaultSound = request.UseDefaultSound,
                SoundId = request.SoundId,
                UseDefaultShowPreview = request.UseDefaultShowPreview,
                ShowPreview = request.ShowPreview,
                UseDefaultMuteStories = request.UseDefaultMuteStories,
                MuteStories = request.MuteStories,
                UseDefaultStorySound = request.UseDefaultStorySound,
                StorySoundId = request.StorySoundId,
                UseDefaultShowStorySender = request.UseDefaultShowStorySender,
                ShowStorySender = request.ShowStorySender,
                UseDefaultDisablePinnedMessageNotifications = request.UseDefaultDisablePinnedMessageNotifications,
                DisablePinnedMessageNotifications = request.DisablePinnedMessageNotifications,
                UseDefaultDisableMentionNotifications = request.UseDefaultDisableMentionNotifications,
                DisableMentionNotifications = request.DisableMentionNotifications
            }).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет изменение акцентного цвета чата
    /// </summary>
    public async Task SetChatAccentColorAsync(TgSetChatAccentColorRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Изменяем акцентный цвет чата
            await _client.SetChatAccentColorAsync(
                request.ChatId,
                request.AccentColorId ?? 0,
                request.BackgroundCustomEmojiId ?? 0).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет изменение разрешений в чате
    /// </summary>
    public async Task SetChatPermissionsAsync(TgSetChatPermissionsRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Изменяем разрешения в чате
            await _client.SetChatPermissionsAsync(
                request.ChatId,
                new TdApi.ChatPermissions
                {
                    // Устанавливаем разрешения
                    CanSendBasicMessages = request.CanSendBasicMessages,
                    CanSendAudios = request.CanSendAudios,
                    CanSendDocuments = request.CanSendDocuments,
                    CanSendPhotos = request.CanSendPhotos,
                    CanSendVideos = request.CanSendVideos,
                    CanSendVideoNotes = request.CanSendVideoNotes,
                    CanSendVoiceNotes = request.CanSendVoiceNotes,
                    CanSendPolls = request.CanSendPolls,
                    CanSendOtherMessages = request.CanSendOtherMessages,
                    CanAddWebPagePreviews = request.CanAddWebPagePreviews,
                    CanChangeInfo = request.CanChangeInfo,
                    CanInviteUsers = request.CanInviteUsers,
                    CanPinMessages = request.CanPinMessages,
                    CanCreateTopics = request.CanCreateTopics,
                }).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет изменение задержки отправки сообщений в чате
    /// </summary>
    public async Task SetChatSlowModeDelayAsync(TgSetChatSlowModeDelayRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Изменяем задержку отправки сообщений в чате
            await _client.SetChatSlowModeDelayAsync(request.ChatId, request.SlowModeDelay).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет установку времени автоудаления сообщений в чате
    /// </summary>
    public async Task SetChatMessageAutoDeleteTimeAsync(TgSetChatMessageAutoDeleteTimeRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем Id чата из запроса
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            // Устанавливаем время автоудаления сообщений в чате
            await _client.SetChatMessageAutoDeleteTimeAsync(chatId, request.MessageAutoDeleteTime)
                .WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет изменение доступных реакций в чате
    /// </summary>
    public async Task SetChatAvailableReactionsAsync(TgSetChatAvailableReactionsRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Создаем визитор для обработки реакций.
            var reactionVisitor = new ReactionTypeVisitor();

            // Прогоняем все реакции через визитор и собираем результат.
            var tdReactions = request.Reactions
                .Select(reaction =>
                {
                    reaction.Accept(reactionVisitor);
                    return reactionVisitor.Type;
                })
                .Where(type => type != null)
                .ToList();

            // Создаем визитор для доступных реакций чата.
            var chatAvailableReactionsVisitor = new ChatAvailableReactionsVisitor();
            chatAvailableReactionsVisitor.Visit(tdReactions.ToArray(), request.MaxReactionCount);

            // Получаем тип доступных реакций из визитора.
            var chatAvailableReactionsType = chatAvailableReactionsVisitor.Type;

            // Устанавливаем разрешенные в чате реакции.
            await _client.SetChatAvailableReactionsAsync(request.ChatId, chatAvailableReactionsType)
                .WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }
    
    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет изменение отправителя сообщений в групповом чате
    /// </summary>
    public async Task SetChatMessageSenderAsync(TgSetChatMessageSenderRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Создаем экземпляр класса MessageSenderVisitor для получения отправителя сообщения
            var visitor = new MessageSenderVisitor();

            // Вызываем метод Accept объекта TgInputSenderMessage, передавая ему экземпляр класса MessageSenderVisitor
            request.MessageSender?.Accept(visitor);

            // Устанавливаем отправителя сообщений в групповом чате
            await _client.SetChatMessageSenderAsync(request.ChatId, visitor.MessageSender)
                .WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }
    
    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет изменение статуса емозди в групповом чате
    /// </summary>
    public async Task SetChatEmojiStatusAsync(TgSetChatEmojiStatusRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Создаем экземпляр класса ChatEmojiStatusVisitor для получения статуса емозди чата
            var visitor = new ChatEmojiStatusVisitor();
            
            // Получаем статус емозди чата
            visitor.Visit(request.CustomEmojiId, request.ExpirationDate);

            // Устанавливаем статус емозди в групповом чате
            await _client.SetChatEmojiStatusAsync(request.ChatId, visitor.EmojiStatus)
                .WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }
    
    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет изменение цвета профиля группового чата
    /// </summary>
    public async Task SetChatProfileAccentColorAsync(TgSetChatProfileAccentColorRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Устанавливаем цвет профиля группового чата
            await _client.SetChatProfileAccentColorAsync(request.ChatId, request.ProfileAccentColorId, request.ProfileBackgroundCustomEmojiId)
                .WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }
    
    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет изменение обсуждаемой группы
    /// </summary>
    public async Task SetChatDiscussionGroupAsync(TgSetChatDiscussionGroupRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Устанавливаем обсуждаемую группу
            await _client.SetChatDiscussionGroupAsync(request.ChatId, request.DiscussionChatId)
                .WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }
    
    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет изменение фото группы
    /// </summary>
    public async Task SetChatPhotoAsync(TgSetChatProfilePhotoRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Создание объекта для обработки файла
            var photoVisitor = new InputChatPhotoVisitor(_client);

            // Передача данных файла в объект посетителя
            await request.ChatProfilePhoto.AcceptAsync(photoVisitor);
            
            // Устанавливаем новое фото группового чата
            await _client.SetChatPhotoAsync(request.ChatId, photoVisitor.Photo)
                .WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если ошибка содержит фразу "Can't use file of type"
            if (ex.Error.Message.Contains("Can't use file of type", StringComparison.InvariantCultureIgnoreCase))
            {
                // Разбираем ошибку на слова
                var words = ex.Error.Message.ExtractWords();

                // Вызываем исключение UnexpectedFileTypeException
                throw new UnexpectedFileTypeException(ex.Error.Code, ex.Error.Message)
                {
                    // Указываем тип файла взятый из текста ошибки
                    FileType = words[5],

                    // Указываем ожидаемый тип файла взятый из текста ошибки
                    ExpectedType = words[7]
                };
            }

            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод создания (загрузки) чата.
    /// </summary>
    public async Task<TgChat> CreateChatAsync(TgCreateChatRequest request, CancellationToken cancellationToken)
    {
        // Создаем чат на основании полученного идентификатора
        // Это нужно затем, что нам может быть известен пользователь или группа, но с ней еще не создан или не загружен чат
        var task = request.ChatType switch
        {
            // Создаем приватный чат, если переданный идентификатор является идентификатором пользователя
            TgInputChatType.PrivateChat => _client.CreatePrivateChatAsync(request.Id),

            // Создаем чат базовой группы, если переданный идентификатор является идентификатором базовой группы
            TgInputChatType.BasicGroupChat => _client.CreateBasicGroupChatAsync(request.Id),

            // Создаем чат супер группы, если переданный идентификатор является идентификатором супер группы
            TgInputChatType.SuperGroupChat => _client.CreateSupergroupChatAsync(request.Id),

            // Создаем чат секретного чата, если переданный идентификатор является идентификатором секретного чата
            TgInputChatType.SecretChat => _client.CreateSecretChatAsync((int)request.Id),

            // Вызываем исключение, если передан неизвесный тип чата
            _ => throw new ArgumentOutOfRangeException()
        };

        // Ожидаем создание чата
        var chat = await task.WaitAsync(cancellationToken);

        // Маппим и возвращаем чат.
        return _mapper.Map<TgChat>(chat);
    }

    /// <summary> 
    /// Открывает чат. 
    /// </summary> 
    /// <param name="tgInputChat">данные пользователя</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task<TgChat> OpenChatAsync(TgInputChat tgInputChat, CancellationToken cancellationToken)
    {
        // Проверяем текущее состояние клиента, чтобы удостовериться, что он готов к работе.
        // Если клиент не в состоянии Ready, выбрасываем исключение ClientNotExpectActionException.
        if (_clientState != ClientState.Ready)
            throw new ClientNotExpectActionException(_clientState.ToString());

        // Получаем Id чата из запроса
        var chatId = await GetChatIdAsync(tgInputChat).WaitAsync(cancellationToken);

        try
        {
            // Отправляем запрос на открытие чата
            await _client.OpenChatAsync(chatId).WaitAsync(cancellationToken);

            // Получаем чат
            var chat = _chats.ChatsList.First(c => c.Id == chatId);

            // Маппим и возвращаем чат.
            return _mapper.Map<TgChat>(chat);
        }
        catch (TdException ex)
        {
            if (ex.Error.Code == 400) throw new ChatNotFoundException();

            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <summary> 
    /// Закрывает чат. 
    /// </summary> 
    /// <param name="tgInputChat">данные пользователя</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task CloseChatAsync(TgInputChat tgInputChat, CancellationToken cancellationToken)
    {
        // Проверяем текущее состояние клиента, чтобы удостовериться, что он готов к работе.
        // Если клиент не в состоянии Ready, выбрасываем исключение ClientNotExpectActionException.
        if (_clientState != ClientState.Ready)
            throw new ClientNotExpectActionException(_clientState.ToString());

        // Получаем Id чата из запроса
        var chatId = await GetChatIdAsync(tgInputChat).WaitAsync(cancellationToken);

        try
        {
            // Отправляем запрос на закрытие чата
            await _client.CloseChatAsync(chatId);
        }
        catch (TdException ex)
        {
            if (ex.Error.Code == 400) throw new ChatNotFoundException();

            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно устанавливает номер телефона для аутентификации. 
    /// </summary> 
    public async Task SetAuthenticationPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть ожидание ввода номера телефона. 
        if (_clientState != ClientState.WaitPhoneNumber)
            throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Устанавливаем номер телефона для аутентификации с помощью метода SetAuthenticationPhoneNumberAsync. 
            await _client.SetAuthenticationPhoneNumberAsync(phoneNumber).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException с кодом 400, создаем и выбрасываем новое исключение InvalidPhoneNumberException. 
            if (ex.Error.Code == 400) throw new InvalidPhoneNumberException(ex.Error.Code, ex.Error.Message);

            // Если возникает исключение TdException с кодом 406, создаем и выбрасываем новое исключение TooManyRequestsException. 
            if (ex.Error.Code == 406) throw new TooManyRequestsException(ex.Error.Code, ex.Error.Message, null);

            // В противном случае, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно проверяет код аутентификации. 
    /// </summary> 
    public async Task SetAuthenticationCodeAsync(string code, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть ожидание ввода кода аутентификации. 
        if (_clientState != ClientState.WaitAuthenticationCode)
            throw new ClientNotExpectActionException(_clientState.ToString());

        // Создаем объект CodeAuthenticationWaitData для отслеживания завершения проверки кода аутентификации. 
        _codeAuthentication = new CodeAuthenticationWaitData();

        try
        {
            // Проверяем код аутентификации с помощью метода CheckAuthenticationCodeAsync. 
            await _client.CheckAuthenticationCodeAsync(code).WaitAsync(cancellationToken);

            // Ожидаем завершения проверки кода аутентификации с помощью объекта CodeAuthenticationWaitData. 
            await _codeAuthentication.Waiter;
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException с кодом 400, создаем и выбрасываем новое исключение InvalidCodeException. 
            if (ex.Error.Code == 400) throw new InvalidCodeException(ex.Error.Code, ex.Error.Message);

            // В противном случае, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
        finally
        {
            // Помечаем таск отмененным
            _codeAuthentication.Dispose();

            // Устанавливаем объект CodeAuthenticationWaitData в null. 
            _codeAuthentication = null;
        }
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно устанавливает пароль для аутентификации Telegram. 
    /// </summary> 
    public async Task SetAuthenticationPasswordAsync(string password, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть ожидание ввода пароля. 
        if (_clientState != ClientState.WaitPassword) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Устанавливаем пароль для аутентификации с помощью метода CheckAuthenticationPasswordAsync. 
            await _client.CheckAuthenticationPasswordAsync(password).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException с кодом 400, создаем и выбрасываем новое исключение InvalidPasswordException. 
            if (ex.Error.Code == 400) throw new InvalidPasswordException(ex.Error.Code, ex.Error.Message);

            // Если возникает исключение TdException с кодом 429, создаем и выбрасываем новое исключение TooManyRequestsException. 
            if (ex.Error.Code == 429)
                throw new TooManyRequestsException(ex.Error.Code, ex.Error.Message, ex.Error.Message.FindNumber());

            // В противном случае, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод аутентифицирует пользователя с помощью qr кода
    /// </summary>
    public async Task<Channel<string>> QrCodeAuthenticateAsync(CancellationToken token)
    {
        // Проверка текущего состояния клиента
        if (_clientState != ClientState.WaitPhoneNumber)
            throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Инициализация объекта аутентификации через QR-код
            _qrAuthentication = new QrAuthenticationWaitData();

            // Отправка запроса на аутентификацию через QR-код
            await _client.RequestQrCodeAuthenticationAsync().WaitAsync(token);

            // Возвращаем канал
            return _qrAuthentication.QrChannel;
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }

        // Finally не нужен. Нам не нужно уничтожать QrAuthenticationWaitData, так как в нем содержится канал кодов
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод отправляет сообщение
    /// </summary>
    public async Task<TgMessage> SendMessageAsync(TgSendMessageRequest messageRequest,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        // Создание объекта для обработки контента сообщения
        var messageContentVisitor = new InputMessageContentVisitor();

        // Передача данных сообщения в объект посетителя
        await messageRequest.InputMessageData.AcceptAsync(messageContentVisitor);

        // Параметры отправки сообщения
        TdApi.MessageSendOptions? options = null;

        // Если тип планирования не null
        if (messageRequest.MessageSendOptions?.SchedulingState != null)
        {
            // Создание объекта для обработки настроек сообщения
            var messageOptionsVisitor = new MessageSchedulingStateAsyncVisitor();

            // Принимаем посетителя
            messageRequest.MessageSendOptions.SchedulingState.Accept(messageOptionsVisitor);

            // Задаем параметры отправки сообщения
            options = new TdApi.MessageSendOptions
            {
                // Отключение уведомелния
                DisableNotification = messageRequest.MessageSendOptions.DisableNotification,

                // Защищенное сообщения от пересылок и сохранения
                ProtectContent = messageRequest.MessageSendOptions.ProtectContent,

                // Тип планирования отправки сообщения
                SchedulingState = messageOptionsVisitor.SchedulingState,
            };
        }

        try
        {
            // Активность
            TdApi.ChatAction? action = null;

            // Предварительная активность
            Activity? preliminaryActivity = null;

            // Если необходимо отобразить активность чата
            if (messageRequest.NeedShowActivity)
            {
                // Создание объекта для предоставления действия к сообщению
                var messageActivityVisitor = new ChatActionContentVisitor(_client);

                // Передача данных в объект посетителя
                await messageRequest.InputMessageData.AcceptAsync(messageActivityVisitor);

                // Устанавливаем предварительную активность
                preliminaryActivity = messageActivityVisitor.PreliminaryActivity;

                // Устанавливаем активность
                action = messageActivityVisitor.Action;
            }

            // Получение идентификатора чата
            var chatId = await GetChatIdAsync(messageRequest.Chat).WaitAsync(cancellationToken);

            // Отправка сообщения в указанный чат
            var message =
                await SendMessageAsync(chatId, messageContentVisitor.Content!, preliminaryActivity, action, options,
                    cancellationToken);

            // Маппим сообщение и возвращаем
            return _mapper.Map<TgMessage>(message);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод добавляет реакцию на сообщение
    /// </summary>
    public async Task AddMessageReactionAsync(TgMessageReactionRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получение идентификатора чата
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            // Создаем визитора для реакции
            var visitor = new ReactionTypeVisitor();

            // Получаем реакцию
            request.Reaction.Accept(visitor);

            // Добавление реакции на сообщение
            await _client.AddMessageReactionAsync(chatId, request.MessageId, reactionType: visitor.Type)
                .WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод удаляет реакцию на сообщение
    /// </summary>
    public async Task RemoveMessageReactionAsync(TgMessageReactionRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Создаем визитора для реакции
            var visitor = new ReactionTypeVisitor();

            // Получение идентификатора чата
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            // Получаем реакцию
            request.Reaction.Accept(visitor);

            // Удаление реакции с сообщения
            await _client.RemoveMessageReactionAsync(chatId, request.MessageId, visitor.Type)
                .WaitAsync(cancellationToken);
        }

        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод отдает историю чата
    /// </summary>
    public async Task<TgMessages> GetChatHistoryAsync(TgGetChatHistoryRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получение идентификатора чата
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            // Получаем сообщения
            var messages = await _client
                .GetChatHistoryAsync(chatId, request.FromMessageId ?? 0, request.Offset ?? 0, request.Limit)
                .WaitAsync(cancellationToken);

            // Если не нужны пользователи
            if (!request.IncludeUsers)
            {
                // Возвращаем сообщения без коллекции пользователей
                return new TgMessages
                {
                    Messages = _mapper.Map<TgMessage[]>(messages.Messages_),
                    TotalCount = messages.TotalCount
                };
            }

            // Получаем обьект чата из TdLib
            var chat = await _client.GetChatAsync(chatId);

            // Получаем пользователей
            var users = _mapper.Map<IReadOnlyCollection<TgUser>>(_users.GetUsersForChats([chat]));

            // Возвращаем сообщения с коллекцией пользователей
            return new TgMessages
            {
                Messages = _mapper.Map<TgMessage[]>(messages.Messages_),
                Users = _mapper.Map<IReadOnlyCollection<TgUser>>(users),
                TotalCount = messages.TotalCount
            };
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет поиск сообщений в чате
    /// </summary>
    public async Task<TgCountResult<TgMessage>> SearchChatMessagesAsync(TgSearchChatMessagesRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получение идентификатора чата
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            // Получаем фильтр сообщений
            var messageFilter = _mapper.Map<TdApi.SearchMessagesFilter?>(request.Filter);

            // Создаем экземпляр класса MessageSenderVisitor для получения отправителя сообщения
            var visitor = new MessageSenderVisitor();

            // Вызываем метод Accept объекта TgInputSenderMessage, передавая ему экземпляр класса MessageSenderVisitor
            request.MessageSender?.Accept(visitor);

            // Выполняем запрос клиенту на поиск сообщений.
            var foundMessages = await _client.SearchChatMessagesAsync(
                    chatId: chatId, request.Query, limit: request.Limit,
                    offset: request.Offset ?? 0, fromMessageId: request.FromMessageId ?? 0,
                    messageThreadId: request.MessageThreadId ?? 0,
                    savedMessagesTopicId: request.SavedMessagesTopicId ?? 0,
                    filter: messageFilter, senderId: visitor.MessageSender)
                .WaitAsync(cancellationToken);

            // Маппим полученные сообщения.
            var messages = _mapper.Map<TgMessage[]>(foundMessages.Messages).ToArray();

            // Возвращаем список найденных сообщений.
            return new TgCountResult<TgMessage>
            {
                List = messages,
                TotalCount = foundMessages.TotalCount
            };
        }
        catch (TdException ex)
        {
            // В противном случае, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет поиск сообщений во всех чатах
    /// </summary>
    public async Task<TgFoundMessages> SearchMessagesAsync(TgSearchMessagesRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем фильтр сообщений
            var messageFilter = _mapper.Map<TdApi.SearchMessagesFilter?>(request.Filter);

            // Создаем объект списка чатов, который используется для запроса.
            TdApi.ChatList? list = request.List switch
            {
                TgChatList.Main => new TdApi.ChatList.ChatListMain(),
                TgChatList.Archive => new TdApi.ChatList.ChatListArchive(),
                _ => null
            };

            // Получаем минимальную дату в UNIX
            var unixMin = request.MinDate?.Subtract(new DateTime(1970, 1, 1)).TotalSeconds ?? 0d;

            // Получаем максимальную дату в UNIX
            var unixMax = request.MaxDate?.Subtract(new DateTime(1970, 1, 1)).TotalSeconds ?? 0d;

            // Выполняем запрос клиенту на поиск сообщений.
            var foundMessages = await _client.SearchMessagesAsync(list, request.OnlyInChannels,
                    request.Query, request.Offset,
                    request.Limit, messageFilter,
                    (int)unixMin, (int)unixMax)
                .WaitAsync(cancellationToken);

            // Маппим полученные сообщения.
            var messages = _mapper.Map<TgMessage[]>(foundMessages.Messages).ToArray();

            // Возвращаем список найденных сообщений.
            return new TgFoundMessages
            {
                List = messages,
                TotalCount = foundMessages.TotalCount,
                NextOffset = string.IsNullOrEmpty(foundMessages.NextOffset) ? null : foundMessages.NextOffset
            };
        }
        catch (TdException ex)
        {
            // В противном случае, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод отдает данные сообщения
    /// </summary>
    public async Task<TgMessage> GetMessageAsync(TgGetMessageRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получение идентификатора чата
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            // Получаем сообщение
            var message = await _client.GetMessageAsync(chatId, request.MessageId).WaitAsync(cancellationToken);

            // Маппим сообщение и возвращаем его
            return _mapper.Map<TgMessage>(message);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException с кодом 400, создаем и выбрасываем новое исключение MessageNotFoundException. 
            if (ex.Error.Code == 404) throw new MessageNotFoundException(ex.Error.Code, ex.Error.Message);

            // В противном случае, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод добавляет контакт
    /// </summary>
    public async Task AddContactAsync(TgAddContactRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем идентификатор контакта
            var chatId = await GetUserIdAsync(request.User).WaitAsync(cancellationToken);

            // Добавляем контакт
            await _client.AddContactAsync(new TdApi.Contact
            {
                // Имя контакта
                FirstName = request.FirstName,

                // Фамилия контакта
                LastName = request.LastName,

                // Идентификатор контакта
                UserId = chatId
            }).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            if (ex.Error.Code == 400) throw new ChatNotFoundException();

            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод выполняет поиск контактов
    /// </summary>
    public async Task<TgCountResult<TgUser>> SearchContactsAsync(TgSearchContactsRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Выполняем запрос клиенту на поиск контактов.
            var contacts = await _client.SearchContactsAsync(request.Query, request.Limit).WaitAsync(cancellationToken);

            // Фильтруем контакты по полученным Id.
            var searchedContacts = _users.UsersList.Where(x => contacts.UserIds.Contains(x.Id));

            // Маппим.
            var users = _mapper.Map<TgUser[]>(searchedContacts);

            // Возвращаем список контактов.
            return new TgCountResult<TgUser>
            {
                List = users,
                TotalCount = contacts.TotalCount
            };
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод импортирует контакты
    /// </summary>
    public async Task<TgImportedContacts> ImportContactsAsync(IEnumerable<TgImportContactRequest> request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        // Формируем запрос на импорт контактов
        var contacts = request.Select(c => new TdApi.Contact
        {
            // Имя
            FirstName = c.FirstName,

            //Фамилия
            LastName = c.LastName,

            // Номер телефона
            PhoneNumber = c.PhoneNumber
        }).ToArray();

        try
        {
            // Импортируем контакты
            var importedContacts = await _client.ImportContactsAsync(contacts).WaitAsync(cancellationToken);

            // Маппим и возвращаем объект
            return _mapper.Map<TgImportedContacts>(importedContacts);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод удаляет контакт
    /// </summary>
    public async Task RemoveContactAsync(TgInputUser request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем идентификатор контакта
            var id = await GetUserIdAsync(request).WaitAsync(cancellationToken);

            // Удаляем контакт
            await _client.RemoveContactsAsync([id]).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод загружает файл
    /// </summary>
    public async Task DownloadFileAsync(TgInputFileRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Создание экземпляра класса FileVisitor и передача в качестве параметра _client
            var fileVisitor = new FileVisitor(_client);

            // Асинхронное выполнение метода AcceptAsync на объекте request.FileRequest с использованием fileVisitor
            await request.AcceptAsync(fileVisitor);

            // Отправляем запрос на загрузку файла
            var file = await _client.DownloadFileAsync(fileVisitor.File!.Id, priority: 1).WaitAsync(cancellationToken);

            // Если файл уже загружен - вызываем исключение
            if (file.Local.IsDownloadingCompleted) throw new FileAlreadyDownloadedException();
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод отдает путь к загруженному файлу
    /// </summary>
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="FileNotDownloadedException">Вызывается, если файл не скачан</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task<TgFile> GetFileAsync(TgInputFileRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Создание экземпляра класса FileVisitor и передача в качестве параметра _client
            var fileVisitor = new FileVisitor(_client);

            // Асинхронное выполнение метода AcceptAsync на объекте request.FileRequest с использованием fileVisitor
            await request.AcceptAsync(fileVisitor);

            // Получение файла из объекта fileVisitor
            var file = fileVisitor.File!;

            // Возвращаем файл
            return _mapper.Map<TgFile>(file);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод получает список доступных реакций на сообщение
    /// </summary>
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="MessageNotFoundException">Вызывается, если сообщение не найдено</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task<TgMessageAvailableReactions> GetMessageAvailableReactionsAsync(
        TgGetMessageReactionsRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получение идентификатора чата
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            // Получаем доступные для сообщения реакции
            var reactions = await _client.GetMessageAvailableReactionsAsync(chatId, request.MessageId)
                .WaitAsync(cancellationToken);

            // Мапим и возвращаем объект
            return _mapper.Map<TgMessageAvailableReactions>(reactions);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException с кодом 400, создаем и выбрасываем новое исключение MessageNotFoundException. 
            if (ex.Error.Code == 404) throw new MessageNotFoundException(ex.Error.Code, ex.Error.Message);

            // В противном случае, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод получает список добавленных на сообщение реакций
    /// </summary>
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="MessageNotFoundException">Вызывается, если сообщение не найдено</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task<TgMessageAddedReactions> GetAddedMessageReactionsAsync(
        TgGetMessageAddedReactionsRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получение идентификатора чата
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            TdApi.ReactionType? type = null;

            if (request.Reaction != null)
            {
                var visitor = new ReactionTypeVisitor();
                request.Reaction.Accept(visitor);

                type = visitor.Type;
            }

            // Получаем добавленных на сообщения реакции
            var reactions = await _client.GetMessageAddedReactionsAsync(chatId, request.MessageId, type ?? null,
                    offset: request.Offset, limit: request.Limit)
                .WaitAsync(cancellationToken);

            // Мапим и возвращаем объект
            return _mapper.Map<TgMessageAddedReactions>(reactions);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException с кодом 400, создаем и выбрасываем новое исключение MessageNotFoundException. 
            if (ex.Error.Code == 404) throw new MessageNotFoundException(ex.Error.Code, ex.Error.Message);

            // В противном случае, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <summary>
    /// Метод устанавливает фотографию телеграмм аккаунту
    /// </summary>
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task SetProfilePhotoAsync(TgSetProfilePhotoRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        // Создание объекта для обработки файла
        var photoVisitor = new InputChatPhotoVisitor(_client);

        // Передача данных файла в объект посетителя
        await request.ProfilePhoto.AcceptAsync(photoVisitor);

        try
        {
            // Устанавливаем новое фотографию
            await _client.SetProfilePhotoAsync(photoVisitor.Photo).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если ошибка содержит фразу "Can't use file of type"
            if (ex.Error.Message.Contains("Can't use file of type", StringComparison.InvariantCultureIgnoreCase))
            {
                // Разбираем ошибку на слова
                var words = ex.Error.Message.ExtractWords();

                // Вызываем исключение UnexpectedFileTypeException
                throw new UnexpectedFileTypeException(ex.Error.Code, ex.Error.Message)
                {
                    // Указываем тип файла взятый из текста ошибки
                    FileType = words[5],

                    // Указываем ожидаемый тип файла взятый из текста ошибки
                    ExpectedType = words[7]
                };
            }

            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <summary>
    /// Метод возвращает данные пользователя
    /// </summary>
    public async Task<TgUser> GetUserAsync(TgInputUser request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем id пользователя
            var id = await GetUserIdAsync(request).WaitAsync(cancellationToken);

            // Получаем пользователя
            var user = await _client.GetUserAsync(id).WaitAsync(cancellationToken);

            // Маппим и возвращаем пользователя
            return _mapper.Map<TgUser>(user);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <summary>
    /// Метод возвращает полную информацию о пользователе
    /// </summary>
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task<TgUserFullInfo> GetUserFullInfoAsync(TgInputUser request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем id пользователя
            var id = await GetUserIdAsync(request).WaitAsync(cancellationToken);

            // Получаем информацию о пользователе
            var userInfo = await _client.GetUserFullInfoAsync(id).WaitAsync(cancellationToken);

            // Мапим и возвращаем информацию о пользователе
            return _mapper.Map<TgUserFullInfo>(userInfo);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <summary>
    /// Метод получает список фотографий пользователя
    /// </summary>
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task<TgCountResult<TgChatPhoto>> GetProfilePhotosAsync(TgGetUserProfilePhotosRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем id пользователя
            var userId = await GetUserIdAsync(request.User).WaitAsync(cancellationToken);

            // Получаем фотографии пользователя
            var photos = await _client
                .GetUserProfilePhotosAsync(userId, request.Offset ?? 0, request.Limit)
                .WaitAsync(cancellationToken);


            return new TgCountResult<TgChatPhoto>
            {
                List = _mapper.Map<TgChatPhoto[]>(photos.Photos),
                TotalCount = photos.TotalCount
            };
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <summary>
    /// Метод возвращает список пересланных сообщений
    /// </summary>
    /// <param name="request">Запрос на пересылку сообщений</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task<IReadOnlyCollection<TgMessage>> ForwardMessagesAsync(TgForwardMessagesRequest request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем id чата
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            // Получаем список сообщений, передавая необходимые параметры для клиента
            var messages = await _client
                .ForwardMessagesAsync(chatId, fromChatId: request.FromChatId,
                    messageIds: request.MessageIds, sendCopy: request.SendCopy,
                    removeCaption: request.RemoveCaption).WaitAsync(cancellationToken);

            // Возвращаем список сообщений с помощью маппера
            return _mapper.Map<TgMessage[]>(messages.Messages_.Where(m => m != null).ToArray());
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для удаления сообщений в чате
    /// </summary>
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task DeleteMessagesAsync(TgDeleteMessagesRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем id чата
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            // Удаляем сообщения с помощью клиента, передавая необходимые параметры
            await _client.DeleteMessagesAsync(chatId, request.MessageIds, request.Revoke).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }


    /// <inheritdoc/>
    /// <summary>
    /// Метод для установки прокси сервера
    /// </summary>
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task SetProxyAsync(TgSetProxyRequest request, CancellationToken token)
    {
        // Сздаем посетителя прокси типов
        var proxyTypeVisitor = new ProxyVisitor();

        // Принимаем посетителя
        request.ProxyType.Accept(proxyTypeVisitor);

        try
        {
            // Получаем предыдущие прокси пользователя
            var proxies = await _client.GetProxiesAsync().WaitAsync(token);

            // Удаляем их в цикле
            foreach (var proxy in proxies.Proxies_)
            {
                await _client.RemoveProxyAsync(proxy.Id).WaitAsync(token);
            }

            // Получаем новый установленный прокси
            await _client.AddProxyAsync(request.Server, request.Port, true, proxyTypeVisitor.ProxyType)
                .WaitAsync(token);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод устанавливает новую историю пользователю
    /// Возвращает полную информацию об установленной истории
    /// </summary>
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task<TgStory> PostStoryAsync(TgPostStoryRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе @GNlIDA
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем id пользователя
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            // Создаем посетителя контента
            var storyContentAsyncVisitor = new StoryContentVisitor();

            // Принимаем посетителя
            request.StoryContent.Accept(storyContentAsyncVisitor);

            // Создаем посетителя настроек приватности
            var storyPrivacySettingsAsyncVisitor = new StoryPrivacySettingsVisitor();

            // Принимаем посетителя
            request.StoryPrivacySettings.Accept(storyPrivacySettingsAsyncVisitor);

            // Отправляем запрос на установку истории, передавая все необходимые параметры
            var postedStory = await _client.SendStoryAsync
            (chatId, storyContentAsyncVisitor.StoryContent,
                null, new TdApi.FormattedText { Text = request.Caption },
                storyPrivacySettingsAsyncVisitor.StoryPrivacySettings, request.ActivePeriod,
                protectContent: request.ProtectContent).WaitAsync(cancellationToken);

            // Маппим и возвращаем ифнормацию об истории
            return _mapper.Map<TgStory>(postedStory);
        }
        catch (TdException ex)
        {
            // Если ошибка содержит фразу "Can't use file of type"
            if (ex.Error.Message.Contains("Can't use file of type", StringComparison.InvariantCultureIgnoreCase))
            {
                // Разбираем ошибку на слова
                var words = ex.Error.Message.ExtractWords();

                // Вызываем исключение UnexpectedFileTypeException
                throw new UnexpectedFileTypeException(ex.Error.Code, ex.Error.Message)
                {
                    // Указываем тип файла взятый из текста ошибки
                    FileType = words[5],

                    // Указываем ожидаемый тип файла взятый из текста ошибки
                    ExpectedType = words[7]
                };
            }

            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод возвращает полную информацию об истории
    /// </summary>
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task<TgStory> GetStoryAsync(TgGetStoryRequest request, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем id пользователя
            var chatId = await GetChatIdAsync(request.Chat).WaitAsync(cancellationToken);

            // Получаем историю
            var story = await _client.GetStoryAsync(chatId, request.StoryId).WaitAsync(cancellationToken);

            // Маппим и возвращаем ифнормацию об истории
            return _mapper.Map<TgStory>(story);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод получает информацию об активных историях пользователя
    /// </summary>
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task<TgChatActiveStories> GetChatActiveStories(TgInputChat request,
        CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Получаем id пользователя
            var userId = await GetChatIdAsync(request).WaitAsync(cancellationToken);

            // Получаем информацию об историях
            var stories = await _client.GetChatActiveStoriesAsync(userId).WaitAsync(cancellationToken);

            // Маппим и возвращаем информацию об историях
            return _mapper.Map<TgChatActiveStories>(stories);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод устанавливает био текущему пользователю
    /// </summary>
    /// <exception cref="ClientNotExpectActionException">Вызывается, если состояние клиента не готово к работе</exception>
    /// <exception cref="ClientException">Вызывается, при ошибке запроса</exception>
    public async Task SetUserBioAsync(string? bio, CancellationToken cancellationToken)
    {
        // Проверяем состояние клиента, должно быть готов к работе 
        if (_clientState != ClientState.Ready) throw new ClientNotExpectActionException(_clientState.ToString());

        try
        {
            // Устанавливаем био
            await _client.SetBioAsync(bio).WaitAsync(cancellationToken);
        }
        catch (TdException ex)
        {
            // Если возникает исключение TdException, создаем и выбрасываем новое исключение ClientException. 
            throw new ClientException(ex.Error.Code, ex.Error.Message);
        }
    }

    /// <summary> 
    /// Асинхронно отправляет сообщение в указанный чат с заданным содержимым. 
    /// </summary> 
    /// <param name="id">Данные чата.</param> 
    /// <param name="messageContent">Содержимое сообщения.</param>
    /// <param name="preliminaryActivity">Предварительная активность в чате.</param>
    /// <param name="action">Активность в чате.</param>
    /// <param name="messageSendOptions">Настройки отправки сообщения</param>
    /// <param name="token">Токен отмены операции</param>
    /// <returns>Сообщение</returns>
    private async Task<TdApi.Message> SendMessageAsync(long id, TdApi.InputMessageContent messageContent,
        Activity? preliminaryActivity, TdApi.ChatAction? action, TdApi.MessageSendOptions? messageSendOptions,
        CancellationToken token)
    {
        try
        {
            // Отправляем предварительную активность
            if (preliminaryActivity != null)
                await SendChatActionAsync(id, preliminaryActivity.Action, preliminaryActivity.Iterations, token);

            // Отправляем сообщение в чат с указанным содержимым. 
            var messageRequest =
                _client.SendMessageAsync(id, inputMessageContent: messageContent, options: messageSendOptions)
                    .WaitAsync(token);

            // Если нет события для чата - просто ожидаем отправку сообщения
            if (action == null) return await messageRequest;

            // Пока сообщение не отправлено - отправляем пост-активность (например, отправляет аудиосообщение)
            while (messageRequest.IsCompleted) await SendChatActionAsync(id, action, 1, token);

            // Ожидаем отправку и возвращаем сообщение
            return await messageRequest;
        }
        catch (TdException ex)
        {
            // Если код ошибки не равен 400 (чат не найден) - выбрасываем исключение дальше
            if (ex.Error.Code != 400) throw;

            //TODO: File not found
            // Если ошибка содержит фразу "Can't use file of type"
            if (ex.Error.Message.Contains("Can't use file of type", StringComparison.InvariantCultureIgnoreCase))
            {
                // Разбираем ошибку на слова
                var words = ex.Error.Message.ExtractWords();

                // Вызываем исключение UnexpectedFileTypeException
                throw new UnexpectedFileTypeException(ex.Error.Code, ex.Error.Message)
                {
                    // Указываем тип файла взятый из текста ошибки
                    FileType = words[5],

                    // Указываем ожидаемый тип файла взятый из текста ошибки
                    ExpectedType = words[7]
                };
            }

            throw;
        }
    }

    /// <summary>
    /// Метод отправляет активность в чат
    /// </summary>
    /// <param name="id">Данные чата.</param>
    /// <param name="action">Активность сообщения.</param>
    /// <param name="iterations">Кол-во итераций.</param>
    /// <param name="token">Токен отмены операции</param>
    private async Task SendChatActionAsync(long id, TdApi.ChatAction action, int iterations, CancellationToken token)
    {
        // Константа с задержкой после отправки
        const int defaultDelay = 5000;

        // Заводим цикл
        for (var i = 0; i < iterations; i++)
        {
            // Отправляем активность в чат
            await _client.SendChatActionAsync(id, action: action).WaitAsync(token);

            // Останавливаем поток
            await Task.Delay(defaultDelay, token);
        }
    }

    /// <summary> 
    /// Асинхронно получает идентификатор чата на основе запроса. 
    /// </summary> 
    /// <param name="chat">Запрос для получения идентификатора чата.</param> 
    /// <returns>Данные чата.</returns> 
    private async Task<long> GetChatIdAsync(TgInputChat chat)
    {
        // Создаем экземпляр класса ChatIdVisitor для получения идентификатора чата
        var visitor = new ChatIdVisitor(_client);

        try
        {
            // Вызываем метод AcceptAsync объекта TgInputChat, передавая ему экземпляр класса ChatIdVisitor
            await chat.AcceptAsync(visitor);

            // Возвращаем идентификатор чата, полученный из экземпляра класса ChatIdVisitor
            // Если идентификатор чата не найден, генерируем исключение ChatNotFoundException
            return visitor.ChatId ?? throw new ChatNotFoundException();
        }
        catch (ClientException ex)
        {
            // Если код ошибки равен 400, генерируем исключение ChatNotFoundException.
            // Это возможно при вызове методов в Visitor, когда TdLib выбрасывает исключение с ошибкой chat info not found.
            if (ex.Code == 400) throw new ChatNotFoundException();

            // В противном случае перевыбрасываем исключение
            throw;
        }
    }

    /// <summary> 
    /// Асинхронно получает идентификатор пользователя на основе запроса. 
    /// </summary> 
    /// <param name="user">Запрос для получения идентификатора пользователя.</param> 
    /// <returns>Данные чата.</returns> 
    private async Task<long> GetUserIdAsync(TgInputUser user)
    {
        // Создаем экземпляр класса UserIdVisitor для получения идентификатора пользователя
        var visitor = new UserIdVisitor(_client);

        try
        {
            // Вызываем метод AcceptAsync объекта TgInputUser, передавая ему экземпляр класса UserIdVisitor
            await user.AcceptAsync(visitor);

            // Возвращаем идентификатор пользователя, полученный из экземпляра класса UserIdVisitor
            // Если идентификатор пользователя не найден, генерируем исключение UserNotFoundException
            return visitor.UserId ?? throw new UserNotFoundException();
        }
        catch (ClientException ex)
        {
            // Если код ошибки равен 400, генерируем исключение UserNotFoundException
            // Это возможно при вызове методов в Visitor, когда TdLib выбрасывает исключение с ошибкой user not found.
            if (ex.Code == 400) throw new UserNotFoundException();

            // В противном случае перевыбрасываем исключение
            throw;
        }
    }

    /// <summary> 
    /// Асинхронно обрабатывает обновления. 
    /// </summary>
    /// <param name="sender">Отправитель события</param>
    /// <param name="update">Обновление для обработки.</param> 
    private void ProcessUpdate(object? sender, TdApi.Update update)
    {
        // Передаем обновление на обработку чатов
        _chats.ProcessUpdate(update);

        // Передаем обновления на обработку пользователей
        _users.ProcessUpdate(update);

        // Передаем обновления на обработку папок чатов
        _chatFolders.ProcessUpdate(update);

        // Передаем обновления на обработку непрочитанных чатов
        _unreadChatsCount.ProcessUpdate(update);

        // Передаем обновления на обработку непрочитанных сообщений
        _unreadMessagesCount.ProcessUpdate(update);

        // Switch на основе типа обновления. 
        switch (update)
        {
            // Обновление состояния авторизации
            case TdApi.Update.UpdateAuthorizationState state:

                // Switch на основе состояния авторизации. 
                switch (state.AuthorizationState)
                {
                    // Ожидание параметров TDLib, выполняем установку параметров
                    case TdApi.AuthorizationState.AuthorizationStateWaitTdlibParameters:

                        // Устанавливаем параметры TDLib на основе данных приложения Telegram. 
                        _client.Send(new TdApi.SetTdlibParameters
                        {
                            ApiId = (int)_telegramApp.AppId,
                            ApiHash = _telegramApp.AppHash,
                            ApplicationVersion = _telegramApp.ApplicationVersion,
                            DatabaseDirectory = Path.Combine(_telegramApp.FilesLocation, _filesLocation),
                            FilesDirectory = Path.Combine(_telegramApp.FilesLocation, _filesLocation),
                            UseTestDc = false,
                            SystemLanguageCode = "en",
                            DeviceModel = "Windows Desktop",
                            SystemVersion = "21H2",
                            UseSecretChats = true,
                            UseMessageDatabase = true,
                            UseChatInfoDatabase = true,
                            UseFileDatabase = true
                        });
                        break;

                    // Ожидание ввода номера телефона
                    case TdApi.AuthorizationState.AuthorizationStateWaitPhoneNumber:

                        // Устанавливаем соответствующее состояние
                        _clientState = ClientState.WaitPhoneNumber;
                        break;

                    // Ожидание ввода кода аутентификации
                    case TdApi.AuthorizationState.AuthorizationStateWaitCode:

                        // Устанавливаем соответствующее состояние
                        _clientState = ClientState.WaitAuthenticationCode;
                        break;

                    // Ожидание подтверждения на другом устройстве
                    case TdApi.AuthorizationState.AuthorizationStateWaitOtherDeviceConfirmation link:

                        // Устанавливаем соответствующее состояние
                        _clientState = ClientState.WaitOtherDeviceConfirmation;

                        // Вызываем событие и передаем в него qr код
                        _qrAuthentication?.SetCode(link.Link);

                        break;

                    // Ожидание ввода пароля, устанавливаем состояние клиента. 
                    case TdApi.AuthorizationState.AuthorizationStateWaitPassword password:

                        // Устанавливаем соответствующее состояние
                        _clientState = ClientState.WaitPassword;

                        // Получаем подсказку пароля (если ее нет, то приходит пустая строка)
                        var passwordHint = string.IsNullOrEmpty(password.PasswordHint) ? null : password.PasswordHint;

                        // Если клиент в процессе аутентификации по qr коду
                        if (_qrAuthentication != null)
                        {
                            // Завершаем процесс с указанием подсказки пароля
                            _qrAuthentication.SetCompletedWithPassword(passwordHint);

                            // Уничтожаем объект аутентификации
                            _qrAuthentication.Dispose();

                            // Указываем, что он теперь null
                            _qrAuthentication = null;
                        }

                        // Завершаем кодовую аутентификацию с подсказкой пароля. 
                        _codeAuthentication?.SetCompletedWithPassword(passwordHint);
                        break;

                    // Готовность к работе
                    case TdApi.AuthorizationState.AuthorizationStateReady:

                        // Устанавливаем соответствующее состояние
                        _clientState = ClientState.Ready;

                        // Если клиент в процессе аутентификации по qr коду
                        if (_qrAuthentication != null)
                        {
                            // Завершаем процесс
                            _qrAuthentication.SetCompleted();

                            // Уничтожаем объект аутентификации
                            _qrAuthentication.Dispose();

                            // Указываем, что он теперь null
                            _qrAuthentication = null;
                        }

                        // Завершаем кодовую аутентификацию с подсказкой пароля. 
                        _codeAuthentication?.SetCompleted();

                        // Получаем контакты и чаты. 
                        _client.Send(new TdApi.GetContacts());
                        _client.Send(new TdApi.LoadChats { Limit = 100 });
                        _client.Send(new TdApi.OptimizeStorage());
                        break;

                    // Выход из системы
                    case TdApi.AuthorizationState.AuthorizationStateLoggingOut:

                        // Устанавливаем соответствующее состояние
                        _clientState = ClientState.LoggingOut;
                        break;
                }

                // Если состояние авторизации не ожидание параметров TDLib (то есть клиент запущен и готов к работе)
                if (state.AuthorizationState is not TdApi.AuthorizationState.AuthorizationStateWaitTdlibParameters)
                {
                    // Резолвим задачу ожидания подключения к TdLib, с данного момента клиент запущен и готов к работе
                    // Используется в методе Connect для ожидания готовности к работе
                    _start?.SetResult();
                }

                break;
        }

        // Маппим событие
        var tgEvent = GetTgEvent(update);

        // Если событие требуется для публикации - вызываем событие NewEvent
        if (tgEvent != null) NewEvent?.Invoke(tgEvent);
    }

    /// <summary>
    /// Преобразует объект Update в объект TgEvent, если это возможно.
    /// </summary>
    /// <param name="update">Объект Update.</param>
    /// <returns>Объект TgEvent или null, если преобразование невозможно.</returns>
    private TgEvent? GetTgEvent(TdApi.Update update)
    {
        // Если обновление является UpdateOption, возвращаем null
        if (update is TdApi.Update.UpdateOption) return null;

        // Если Update это ответ на наш запрос (Extra не пустой), значит возвращаем null;
        if (!string.IsNullOrEmpty(update.Extra)) return null;

        // Если обновление является UpdateDeleteMessages и IsPermanent равно false,
        // то есть сообщение просто удалено из кэша, возвращаем null.
        if (update is TdApi.Update.UpdateDeleteMessages { IsPermanent: false }) return null;

        // Если обновление является UpdateAuthorizationState, обрабатываем его
        if (update is TdApi.Update.UpdateAuthorizationState state)
        {
            // В зависимости от состояния авторизации возвращаем соответствующее событие
            return state.AuthorizationState switch
            {
                // Если состояние авторизации готово, возвращаем событие TgAuthenticatedEvent
                TdApi.AuthorizationState.AuthorizationStateReady => new TgAuthenticatedEvent(),

                // Если состояние авторизации выход, возвращаем событие TgLoggedOutEvent
                TdApi.AuthorizationState.AuthorizationStateLoggingOut => new TgLoggedOutEvent(),

                // В остальных случаях возвращаем null
                _ => null
            };
        }

        // Если обновление является UpdateNewChat, обрабатываем его отдельно,
        // так как у нас реализован свой TgNewChatEvent, несовместимый с событием TdLib
        if (update is TdApi.Update.UpdateNewChat newChat)
        {
            // Возвращаем событие TgNewChatEvent с чатом и пользователями, затронутыми в обновлении
            return new TgNewChatEvent
            {
                Chat = _mapper.Map<TgChat>(newChat.Chat),
                Users = _mapper.Map<IReadOnlyCollection<TgUser>>(_users.GetUsersForChats([newChat.Chat]))
            };
        }

        // Если обновление является UpdateChatPosition, обрабатываем его отдельно,
        // так как у нас реализован свой TgUpdateChatPositionEvent, несовместимый с событием TdLib
        if (update is TdApi.Update.UpdateChatPosition updateChatPosition)
        {
            // Находим чат по его идентификатору
            var chat = _chats.ChatsList.First(c => c.Id == updateChatPosition.ChatId);

            // Возвращаем событие TgUpdateChatPositionEvent с чатом и пользователями, затронутыми в обновлении
            return new TgUpdateChatPositionEvent
            {
                Chat = _mapper.Map<TgChat>(chat),
                Users = _mapper.Map<IReadOnlyCollection<TgUser>>(_users.GetUsersForChats([chat]))
            };
        }

        // Если обновление является UpdateChatLastMessage, обрабатываем его отдельно,
        // так как у нас реализован свой TgUpdateChatLastMessageEvent, несовместимый с событием TdLib
        if (update is TdApi.Update.UpdateChatLastMessage lastMessage)
        {
            // Находим чат по его идентификатору
            var chat = _chats.ChatsList.First(c => c.Id == lastMessage.ChatId);

            // Возвращаем событие TgUpdateChatLastMessageEvent с чатом и пользователями, затронутыми в обновлении
            return new TgUpdateChatLastMessageEvent
            {
                Chat = _mapper.Map<TgChat>(chat),
                Users = lastMessage.LastMessage != null
                    ? _mapper.Map<IReadOnlyCollection<TgUser>>(_users.GetUsersForMessage(lastMessage.LastMessage))
                    : []
            };
        }

        // Если обновление является UpdateNewMessage, обрабатываем его отдельно,
        // так как у нас реализован свой TgNewMessageEvent, несовместимый с событием TdLib
        if (update is TdApi.Update.UpdateNewMessage newMessage)
        {
            // Возвращаем событие TgNewMessageEvent с сообщением и пользователями, затронутыми в обновлении
            return new TgNewMessageEvent
            {
                Message = _mapper.Map<TgMessage>(newMessage.Message),
                Users = _mapper.Map<IReadOnlyCollection<TgUser>>(_users.GetUsersForMessage(newMessage.Message))
            };
        }

        try
        {
            // Выполняем маппинг объекта в TgEvent.
            return _mapper.Map<TgEvent>(update);
        }
        // Если произошло исключение ArgumentException, возвращаем null.
        catch (ArgumentException)
        {
            return null;
        }
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Освобождает ресурсы, связанные с объектом. 
    /// </summary> 
    public async ValueTask DisposeAsync()
    {
        // Подавляем вызов финализатора для текущего объекта. 
        GC.SuppressFinalize(this);

        // Освобождаем объект аутентификации по qr
        _qrAuthentication?.Dispose();

        // Освобождаем объект аутентификации по коду
        _codeAuthentication?.Dispose();

        // Если состояние клиента равно "Готов" или "Не готов" 
        if (_clientState is ClientState.Ready or ClientState.NotReady)
        {
            // Закрываем соединение
            await _client.CloseAsync();

            // Освобождаем клиент
            _client.Dispose();

            // Не продолжаем
            return;
        }

        // Закрываем соединение
        await _client.CloseAsync();

        // Удаляем файлы, связанные с Telegram, из указанного расположения. 
        await _client.DestroyAsync();

        // Освобождаем клиент
        _client.Dispose();
    }
}