namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя сообщений Telegram
/// </summary>
public interface IMessageContentVisitor
{
    /// <summary>
    /// Метод для посещения сообщения о местоположении
    /// </summary>
    /// <param name="locationMessage">Сообщение с местоположением</param>
    Task VisitAsync(TgInputLocationMessage locationMessage);

    /// <summary>
    /// Метод для посещения текстового сообщения
    /// </summary>
    /// <param name="textMessage">Сообщение с текстом</param>
    Task VisitAsync(TgInputTextMessage textMessage);

    /// <summary>
    /// Метод для посещения аудио-сообщения
    /// </summary>
    /// <param name="audioMessage">Сообщение с аудио</param>
    Task VisitAsync(TgInputAudioMessage audioMessage);

    /// <summary>
    /// Метод для посещения голосового сообщения
    /// </summary>
    /// <param name="voiceNoteMessage">Голосовое сообщение</param>
    Task VisitAsync(TgInputVoiceNoteMessage voiceNoteMessage);

    /// <summary>
    /// Метод для посещения видеосообщения
    /// </summary>
    /// <param name="videoNoteMessage">Сообщение с кружочком</param>
    Task VisitAsync(TgInputVideoNoteMessage videoNoteMessage);

    /// <summary>
    /// Метод для посещения видео сообщения
    /// </summary>
    /// <param name="videoMessage">Сообщение с видео</param>
    Task VisitAsync(TgInputVideoMessage videoMessage);

    /// <summary>
    /// Метод для посещения фото сообщения
    /// </summary>
    /// <param name="photoMessage">Сообщение с фото</param>
    Task VisitAsync(TgInputPhotoMessage photoMessage);

    /// <summary>
    /// Метод для посещения сообщения с документом
    /// </summary>
    /// <param name="documentMessage">Сообщение с документом</param>
    Task VisitAsync(TgInputDocumentMessage documentMessage);

    /// <summary>
    /// Метод для посещения сообщения с контактом
    /// </summary>
    /// <param name="contactMessage">Сообщение с контактом</param>
    Task VisitAsync(TgInputContactMessage contactMessage);

    /// <summary>
    /// Метод для посещения сообщения с стикером
    /// </summary>
    /// <param name="stickerMessage">Сообщение с стикером</param>
    Task VisitAsync(TgInputStickerMessage stickerMessage);

    /// <summary>
    /// Метод для посещения сообщения с анимацией(гиф)
    /// </summary>
    /// <param name="animationMessage">Сообщение с анимацией(гиф)</param>
    Task VisitAsync(TgInputAnimationMessage animationMessage);
    
    /// <summary>
    /// Метод для посещения сообщения с историей
    /// </summary>
    /// <param name="inputStoryMessage">Сообщение с историей</param>
    Task VisitAsync(TgInputStoryMessage inputStoryMessage);
    
    /// <summary>
    /// Метод для посещения dice сообщения
    /// </summary>
    /// <param name="inputDiceMessage">Dice сообщение</param>
    Task VisitAsync(TgInputDiceMessage inputDiceMessage);
}