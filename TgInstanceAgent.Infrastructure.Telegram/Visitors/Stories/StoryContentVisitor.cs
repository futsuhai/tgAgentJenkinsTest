using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;
using TgInstanceAgent.Infrastructure.Telegram.Visitors.Files;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Stories;

/// <summary>
/// Посетитель контента истории
/// </summary>
public class StoryContentVisitor : IStoryContentVisitor
{
    /// <summary>
    /// Контент истории
    /// </summary>
    public TdApi.InputStoryContent? StoryContent { get; private set; }

    /// <summary>
    /// Метод посещения контента истории в виде фотографии
    /// </summary>
    /// <param name="tgInputStoryContentPhoto">Контент истории в виде фотографии</param>
    public async void Visit(TgInputStoryContentPhoto tgInputStoryContentPhoto)
    {
        // Устаналвиваем контент истории
        StoryContent = new TdApi.InputStoryContent.InputStoryContentPhoto
        {
            // Получаем файл фотографии
            Photo = await GetFileAsync(tgInputStoryContentPhoto),
        };
    }

    /// <summary>
    /// Метод посещения контента истории в виде видео
    /// </summary>
    /// <param name="tgInputStoryContentVideo">Контент истории в виде видео</param>
    public async void Visit(TgInputStoryContentVideo tgInputStoryContentVideo)
    {
        // Устаналвиваем контент истории
        StoryContent = new TdApi.InputStoryContent.InputStoryContentVideo
        {
            // Получаем файл видео
            Video = await GetFileAsync(tgInputStoryContentVideo),
            
            // Истинно, если видео без звука
            IsAnimation = tgInputStoryContentVideo.IsAnimation
        };
    }
    
    /// <summary>
    /// Получает файл из данных сообщения.
    /// </summary>
    /// <param name="messageData">Данные сообщения.</param>
    /// <returns>Представление файла в TdLib.</returns>
    private static async Task<TdApi.InputFile> GetFileAsync(ITgInputDataWithFile messageData)
    {
        // Создаем экземпляр класса FileVisitor
        InputFileVisitor fileVisitor = new();

        // Принимаем посетителя файла для сообщения данных
        await messageData.File.AcceptAsync(fileVisitor);

        // Возвращаем кортеж с файлом и миниатюрой
        return fileVisitor.File!;
    }
}