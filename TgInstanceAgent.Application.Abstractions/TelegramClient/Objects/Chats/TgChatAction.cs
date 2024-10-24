namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Действие в чате
/// </summary>
public abstract class TgChatAction;

/// <summary>
/// Пользователь отменил предыдущее действие
/// </summary>
public class TgChatActionCancel : TgChatAction;

/// <summary>
/// Пользователь выбирает контакт
/// </summary>
public class TgChatActionChoosingContact : TgChatAction;

/// <summary>
/// Пользователь выбирает локацию
/// </summary>
public class TgChatActionChoosingLocation : TgChatAction;

/// <summary>
/// Пользователь выбирает стикер
/// </summary>
public class TgChatActionChoosingSticker : TgChatAction;

/// <summary>
/// Пользователь записывает видео
/// </summary>
public class TgChatActionRecordingVideo : TgChatAction;

/// <summary>
/// Пользователь записывает голосовое сообщение
/// </summary>
public class TgChatActionRecordingVideoNote : TgChatAction;

/// <summary>
/// Пользователь записывает видеосообщние
/// </summary>
public class TgChatActionRecordingVoiceNote : TgChatAction;

/// <summary>
/// Пользователь начал играть в игру
/// </summary>
public class TgChatActionStartPlayingGame : TgChatAction;

/// <summary>
/// Пользователь печатает сообщение
/// </summary>
public class TgChatActionTyping : TgChatAction;

/// <summary>
/// Пользователь загружает документ
/// </summary>
public class TgChatActionUploadingDocument : TgChatAction
{
    /// <summary>
    /// Прогресс загрузки в процентах
    /// </summary>
    public required int Progress { get; init; }
}

/// <summary>
/// Пользователь загружает фото
/// </summary>
public class TgChatActionUploadingPhoto : TgChatAction
{
    /// <summary>
    /// Прогресс загрузки в процентах
    /// </summary>
    public required int Progress { get; init; }
}

/// <summary>
/// Пользователь загружает видео
/// </summary>
public class TgChatActionUploadingVideo : TgChatAction
{
    /// <summary>
    /// Прогресс загрузки в процентах
    /// </summary>
    public required int Progress { get; init; }
}

/// <summary>
/// Пользователь загружает видеосообщение.
/// </summary>
public class TgChatActionUploadingVideoNote : TgChatAction
{
    /// <summary>
    /// Прогресс загрузки в процентах
    /// </summary>
    public required int Progress { get; init; }
}

/// <summary>
/// Пользователь загружает голосовое сообщение.
/// </summary>
public class TgChatActionUploadingVoiceNote : TgChatAction
{
    /// <summary>
    /// Прогресс загрузки в процентах
    /// </summary>
    public required int Progress { get; init; }
}

/// <summary>
/// Пользователь смотрит анимации, отправленные другим пользователем, нажимая на анимированный эмодзи
/// </summary>
public class TgChatActionWatchingAnimations : TgChatAction
{
    /// <summary>
    /// Анимированное емоджи
    /// </summary>
    public required string Emoji { get; init; }
}

