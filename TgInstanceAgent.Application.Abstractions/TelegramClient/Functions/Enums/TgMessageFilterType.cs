namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;

/// <summary>
/// Тип фильтра для поиска сообщений
/// </summary>
public enum TgMessageFilterType
{
    /// <summary>
    /// Фильтр для поиска анимаций (GIF).
    /// </summary>
    Animation,
    
    /// <summary>
    /// Фильтр для поиска аудиосообщений.
    /// </summary>
    Audio,
    
    /// <summary>
    /// Фильтр для поиска сообщений, содержащих фотографии чата.
    /// </summary>
    ChatPhoto,
    
    /// <summary>
    /// Фильтр для поиска документов.
    /// </summary>
    Document,
    
    /// <summary>
    /// Пустой фильтр
    /// </summary>
    Empty,
    
    /// <summary>
    /// Фильтр для поиска сообщений, которые не удалось отправить.
    /// </summary>
    FailedToSend,
    
    /// <summary>
    /// Фильтр для поиска сообщений, содержащих упоминания.
    /// </summary>
    Mention,
    
    /// <summary>
    /// Фильтр для поиска фотографий.
    /// </summary>
    Photo,
    
    /// <summary>
    /// Фильтр для поиска фотографий и видео.
    /// </summary>
    PhotoAndVideo,
    
    /// <summary>
    /// Фильтр для поиска закрепленных сообщений.
    /// </summary>
    Pinned,
    
    /// <summary>
    /// Фильтр для поиска непрочитанных сообщений, содержащих упоминания.
    /// </summary>
    UnreadMention,
    
    /// <summary>
    /// Фильтр для поиска непрочитанных сообщений с реакциями.
    /// </summary>
    UnreadReaction,
    
    /// <summary>
    /// Фильтр для поиска сообщений, содержащих URL.
    /// </summary>
    Url,
    
    /// <summary>
    /// Фильтр для поиска видео.
    /// </summary>
    Video,
    
    /// <summary>
    /// Фильтр для поиска видеосообщений.
    /// </summary>
    VideoNote,
    
    /// <summary>
    /// Фильтр для поиска голосовых и видеосообщений.
    /// </summary>
    VoiceAndVideoNote,
    
    /// <summary>
    /// Фильтр для поиска голосовых сообщений.
    /// </summary>
    VoiceNote
}