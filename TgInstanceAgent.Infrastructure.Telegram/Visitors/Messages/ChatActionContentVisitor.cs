using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Messages;

/// <summary>
/// Класс активности
/// </summary>
public class Activity
{
    /// <summary>
    /// Максимальное значение итераций
    /// </summary>
    private const int MaxIterationsCount = 8;

    /// <summary>
    /// Вид активности
    /// </summary>
    public required TdApi.ChatAction Action { get; init; }

    /// <summary>
    /// Количество итераций отправления активности
    /// </summary>
    private readonly int _iterations;

    /// <summary>
    /// Количество итераций активности перед отправкой сообщения
    /// </summary>
    public required int Iterations
    {
        get => _iterations;
        init => _iterations = value > MaxIterationsCount ? MaxIterationsCount : value;
    }
}

/// <summary>
/// Реализация посетителя сообщений для активностей
/// </summary>
public class ChatActionContentVisitor(TdApi.Client client) : IMessageContentVisitor
{
    /// <summary>
    /// Активность
    /// </summary>
    public TdApi.ChatAction? Action { get; private set; }

    /// <summary>
    /// Предварительная активность
    /// </summary>
    public Activity? PreliminaryActivity { get; private set; }
    
    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения dice сообщения
    /// </summary>
    public Task VisitAsync(TgInputDiceMessage inputDiceMessage)
    {
        // Определяем предварительную активность
        PreliminaryActivity = new Activity
        {
            // Действие
            Action = new TdApi.ChatAction.ChatActionTyping(),

            // Задержка
            Iterations = 1
        };

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод посещения сообщения о местоположении
    /// </summary>
    public Task VisitAsync(TgInputLocationMessage inputLocationMessage)
    {
        // Определяем предварительную активность
        PreliminaryActivity = new Activity
        {
            // Действие
            Action = new TdApi.ChatAction.ChatActionChoosingLocation(),

            // Задержка
            Iterations = 1
        };
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения текстового сообщения
    /// </summary>
    public Task VisitAsync(TgInputTextMessage inputTextMessage)
    {
        // Определяем предварительную активность
        PreliminaryActivity = new Activity
        {
            // Действие
            Action = new TdApi.ChatAction.ChatActionTyping(),

            // Задержка
            Iterations = inputTextMessage.Text.Length / 25
        };

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения аудио сообщения
    /// </summary>
    public Task VisitAsync(TgInputAudioMessage inputAudioMessage)
    {
        // Определяем действие
        Action = new TdApi.ChatAction.ChatActionUploadingDocument();

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения голосового сообщения
    /// </summary>
    public async Task VisitAsync(TgInputVoiceNoteMessage inputVoiceNoteMessage)
    {
        // Определяем действие
        Action = new TdApi.ChatAction.ChatActionUploadingVoiceNote();

        // Определяем предварительную активность
        PreliminaryActivity = new Activity
        {
            // Действие
            Action = new TdApi.ChatAction.ChatActionRecordingVoiceNote(),

            // Задержка
            Iterations = await GetRecordingIterationsAsync(inputVoiceNoteMessage, RecordingType.VoiceNote)
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения видеосообщения
    /// </summary>
    public async Task VisitAsync(TgInputVideoNoteMessage inputVideoNoteMessage)
    {
        // Определяем действие
        Action = new TdApi.ChatAction.ChatActionUploadingVideoNote();

        // Определяем предварительную активность
        PreliminaryActivity = new Activity
        {
            // Действие
            Action = new TdApi.ChatAction.ChatActionRecordingVideoNote(),

            // Задержка
            Iterations = await GetRecordingIterationsAsync(inputVideoNoteMessage, RecordingType.VideoNote)
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения видео сообщения
    /// </summary>
    public async Task VisitAsync(TgInputVideoMessage inputVideoMessage)
    {
        // Определяем действие
        Action = new TdApi.ChatAction.ChatActionUploadingVideo();

        // Если необходимо показать предварительную активность о записи видео
        if (inputVideoMessage.NeedShowRecordingActivity)
        {
            // Определяем предварительную активность
            PreliminaryActivity = new Activity
            {
                // Действие
                Action = new TdApi.ChatAction.ChatActionRecordingVideo(),

                // Задержка
                Iterations = await GetRecordingIterationsAsync(inputVideoMessage, RecordingType.Video)
            };
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения фото сообщения
    /// </summary>
    public Task VisitAsync(TgInputPhotoMessage inputPhotoMessage)
    {
        // Определяем действие
        Action = new TdApi.ChatAction.ChatActionUploadingPhoto();

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения сообщения с документом
    /// </summary>
    public Task VisitAsync(TgInputDocumentMessage inputDocumentMessage)
    {
        // Определяем действие
        Action = new TdApi.ChatAction.ChatActionUploadingDocument();

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения сообщения с контактом
    /// </summary>
    public Task VisitAsync(TgInputContactMessage inputContactMessage)
    {
        // Определяем предварительную активность
        PreliminaryActivity = new Activity
        {
            // Действие
            Action = new TdApi.ChatAction.ChatActionChoosingContact(),

            // Задержка
            Iterations = 1
        };
        
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения сообщения со стикером
    /// </summary>
    public Task VisitAsync(TgInputStickerMessage inputStickerMessage)
    {
        // Определяем предварительную активность
        PreliminaryActivity = new Activity
        {
            // Действие
            Action = new TdApi.ChatAction.ChatActionChoosingSticker(),

            // Задержка
            Iterations = 1
        };
        
        return Task.CompletedTask;
    }

    public Task VisitAsync(TgInputAnimationMessage animationMessage)
    {
        // Определяем предварительную активность
        PreliminaryActivity = new Activity
        {
            // Действие
            Action = new TdApi.ChatAction.ChatActionWatchingAnimations(),

            // Задержка
            Iterations = 1
        };
        
        return Task.CompletedTask;
    }

    public Task VisitAsync(TgInputStoryMessage inputStoryMessage)
    {
        return Task.CompletedTask;
    }


    /// <summary>
    /// Метод рассчитывает длительность сообщения
    /// </summary>
    /// <param name="message">Сообщение</param>
    /// <param name="type">Тип сообщения</param>
    private async Task<int> GetRecordingIterationsAsync(ITgInputDataWithFile message, RecordingType type)
    {
        var visitor = new ContentRecordingDurationVisitor(client, type);

        await message.File.AcceptAsync(visitor);
        
        // Рассчитываем значение и возвращаем его
        return visitor.Duration / 5;
    }
}