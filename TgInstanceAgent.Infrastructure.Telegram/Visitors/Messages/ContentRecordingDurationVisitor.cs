using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Messages;

/// <summary>
/// Перечисление типов сообщений при расчете длительности сообщения
/// </summary>
public enum RecordingType
{
    // Голосовое сообщение
    VoiceNote = 22000,

    // Кружок (видео сообщение)
    VideoNote = 140000,

    // Видео
    Video = 471000
}


/// <inheritdoc/>
/// <summary>
/// Реализация посетителя отправляемых файлов Telegram
/// </summary>
public class ContentRecordingDurationVisitor(TdApi.Client client, RecordingType type) : IFileDataVisitor
{
    /// <summary>
    /// Получает или задает удаленный файл.
    /// </summary>
    public int Duration { get; private set; }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения удаленного на сервере файла
    /// </summary>
    public async Task VisitAsync(TgInputFileDataRemoteId file)
    {
        var tgFile = await client.GetRemoteFileAsync(file.RemoteId);
        
        // Рассчитываем значение и возвращаем его
        Duration = (int)tgFile.Size / (int)type;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения файла по локальному id
    /// </summary>
    public async Task VisitAsync(TgInputFileDataLocalId file)
    {
        var tgFile = await client.GetFileAsync(file.LocalId);
        
        // Рассчитываем значение и возвращаем его
        Duration = (int)tgFile.Size / (int)type;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения файла по локальному пути
    /// </summary>
    public Task VisitAsync(TgInputFileDataLocalPath file)
    {
        // Получаем объект FileInfo у сообщения
        var fileInfo = new FileInfo(file.Path);
        
        // Рассчитываем значение и возвращаем его
        Duration = (int)(fileInfo.Length / (int)type);
        
        // Возвращаем завершенный таск
        return Task.CompletedTask;
    }
}