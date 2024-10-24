namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя контента истории
/// </summary>
public interface IStoryContentVisitor
{
    /// <summary>
    /// Посетить контект истории в виде фотографии.
    /// </summary>
    /// <param name="tgInputStoryContentPhoto">Контект истории в виде фотографии.</param>
    void Visit(TgInputStoryContentPhoto tgInputStoryContentPhoto);

    /// <summary>
    /// Посетить контект истории в виде видео.
    /// </summary>
    /// <param name="tgInputStoryContentVideo">Контект истории в виде видео.</param>
    void Visit(TgInputStoryContentVideo tgInputStoryContentVideo);
}