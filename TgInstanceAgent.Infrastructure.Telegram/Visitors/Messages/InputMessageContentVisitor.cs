using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;
using TgInstanceAgent.Infrastructure.Telegram.Visitors.Files;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.Messages;

/// <inheritdoc/>
/// <summary>
/// Реализация посетителя сообщений
/// </summary>
public class InputMessageContentVisitor : IMessageContentVisitor
{
    /// <summary>
    /// Контент сообщения
    /// </summary>
    public TdApi.InputMessageContent? Content { get; private set; }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения dice сообщения
    /// </summary>
    public Task VisitAsync(TgInputDiceMessage inputDiceMessage)
    {
        // Создание контента dice сообщения
        Content = new TdApi.InputMessageContent.InputMessageDice
        {
            Emoji = inputDiceMessage.Emoji,
            ClearDraft = inputDiceMessage.ClearDraft
        };
        
        // Возвращаем завершенный таск
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения сообщения с историей 
    /// </summary>
    public Task VisitAsync(TgInputStoryMessage inputStoryMessage)
    {
        // Создание контента сообщения с историей
        Content = new TdApi.InputMessageContent.InputMessageStory
        {
            // Устанавливаем идентификатор чата, в котором опубликована история
            StorySenderChatId = inputStoryMessage.StorySenderChatId,
            
            // Устанавливаем идентификатор истории
            StoryId = inputStoryMessage.StoryId
        };
        
        // Возвращаем завершенный таск
        return Task.CompletedTask;
    }
    
    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения сообщения о местоположении
    /// </summary>
    public Task VisitAsync(TgInputLocationMessage locationMessage)
    {
        // Создание контента сообщения с геолокацией
        Content = new TdApi.InputMessageContent.InputMessageLocation
        {
            // Установка координат геолокации
            Location = new TdApi.Location
            {
                // Устанавливаем значение свойства HorizontalAccuracy из locationMessage.HorizontalAccuracy
                HorizontalAccuracy = locationMessage.HorizontalAccuracy,

                // Устанавливаем значение свойства Latitude из locationMessage.Latitude
                Latitude = locationMessage.Latitude,

                // Устанавливаем значение свойства Longitude из locationMessage.Longitude
                Longitude = locationMessage.Longitude
            }
        };

        // Возвращаем завершенный таск
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения текстового сообщения
    /// </summary>
    public Task VisitAsync(TgInputTextMessage textMessage)
    {
        // Создание контента текстового сообщения
        Content = new TdApi.InputMessageContent.InputMessageText
        {
            // Установка текста сообщения
            Text = new TdApi.FormattedText { Text = textMessage.Text },

            // Установка опций предпросмотра ссылки
            LinkPreviewOptions = new TdApi.LinkPreviewOptions
            {
                // Устанавливаем URL
                Url = textMessage.UrlPreview,

                // Устанавливаем флаг отключения превью ссылки
                IsDisabled = textMessage.DisableUrlPreview
            }
        };

        // Возвращаем завершенный таск
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения аудио-сообщения
    /// </summary>
    public async Task VisitAsync(TgInputAudioMessage audioMessage)
    {
        // Создание контента аудио-сообщения
        Content = new TdApi.InputMessageContent.InputMessageAudio
        {
            // Установка обложки альбома
            AlbumCoverThumbnail = audioMessage.AlbumCoverThumbnail == null
                ? null
                : new TdApi.InputThumbnail
                {
                    // Создаем новый экземпляр класса InputFileLocal и устанавливаем значение для свойства Path
                    Thumbnail = new TdApi.InputFile.InputFileLocal
                    {
                        Path = audioMessage.AlbumCoverThumbnail.Path
                    }
                },

            // Установка аудиофайла
            Audio = await GetFileAsync(audioMessage),

            // Установка исполнителя
            Performer = audioMessage.Performer,

            // Установка названия аудио
            Title = audioMessage.Title,

            // Установка подписи к аудио
            Caption = audioMessage.Caption == null
                ? null
                : new TdApi.FormattedText
                {
                    // Устанавливаем текст
                    Text = audioMessage.Caption
                }
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения голосового сообщения
    /// </summary>
    public async Task VisitAsync(TgInputVoiceNoteMessage voiceNoteMessage)
    {
        // Создание контента голосового сообщения
        Content = new TdApi.InputMessageContent.InputMessageVoiceNote
        {
            // Установка голосового сообщения
            VoiceNote = await GetFileAsync(voiceNoteMessage),

            // Установка подписи к видео
            Caption = voiceNoteMessage.Caption == null
                ? null
                : new TdApi.FormattedText
                {
                    // Устанавливаем текст
                    Text = voiceNoteMessage.Caption
                }
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения видеосообщения
    /// </summary>
    public async Task VisitAsync(TgInputVideoNoteMessage videoNoteMessage)
    {
        // Создание контента видео-заметки
        Content = new TdApi.InputMessageContent.InputMessageVideoNote
        {
            // Установка видео-заметки
            VideoNote = await GetFileAsync(videoNoteMessage)
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения видео сообщения
    /// </summary>
    public async Task VisitAsync(TgInputVideoMessage videoMessage)
    {
        // Создание контента видео сообщения
        Content = new TdApi.InputMessageContent.InputMessageVideo
        {
            // Установка видеофайла
            Video = await GetFileAsync(videoMessage),

            // Установка подписи к видео
            Caption = videoMessage.Caption == null
                ? null
                : new TdApi.FormattedText
                {
                    // Устанавливаем текст
                    Text = videoMessage.Caption
                }
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения фото сообщения
    /// </summary>
    public async Task VisitAsync(TgInputPhotoMessage photoMessage)
    {
        // Создание контента фото сообщения
        Content = new TdApi.InputMessageContent.InputMessagePhoto
        {
            // Установка фото
            Photo = await GetFileAsync(photoMessage),

            // Установка подписи к фото
            Caption = photoMessage.Caption == null
                ? null
                : new TdApi.FormattedText
                {
                    // Устанавливаем текст
                    Text = photoMessage.Caption
                }
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения сообщения с документом
    /// </summary>
    public async Task VisitAsync(TgInputDocumentMessage documentMessage)
    {
        // Создание контента документа
        Content = new TdApi.InputMessageContent.InputMessageDocument
        {
            // Установка документа
            Document = await GetFileAsync(documentMessage),
            
            // Установка подписи к документу
            Caption = documentMessage.Caption == null
                ? null
                : new TdApi.FormattedText
                {
                    // Устанавливаем текст
                    Text = documentMessage.Caption
                },
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения сообщения с контактом
    /// </summary>
    public Task VisitAsync(TgInputContactMessage contactMessage)
    {
        // Создание контента документа
        Content = new TdApi.InputMessageContent.InputMessageContact
        {
            // Установка данных контакта
            Contact = new TdApi.Contact
            {
                // Устанавливаем значение свойства FirstName из contactMessage.FirstName
                FirstName = contactMessage.FirstName,

                // Устанавливаем значение свойства LastName из contactMessage.LastName
                LastName = contactMessage.LastName,

                // Устанавливаем значение свойства PhoneNumber из contactMessage.PhoneNumber
                PhoneNumber = contactMessage.PhoneNumber,

                // Устанавливаем значение свойства Vcard из contactMessage.VCard
                Vcard = contactMessage.VCard,

                // Устанавливаем значение свойства UserId из contactMessage.Id, если contactMessage.Id не равно null, иначе устанавливаем значение 0
                UserId = contactMessage.Id ?? 0
            }
        };

        // Возвращаем завершенный таск
        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения сообщения с стикером
    /// </summary>
    public async Task VisitAsync(TgInputStickerMessage stickerMessage)
    {
        // Создание контента стикера
        Content = new TdApi.InputMessageContent.InputMessageSticker
        {
            // Установка файла стикера
            Sticker = await GetFileAsync(stickerMessage)
        };
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для посещения сообщения с анимацией
    /// </summary>
    public async Task VisitAsync(TgInputAnimationMessage animationMessage)
    {
        // Создание контента анимации
        Content = new TdApi.InputMessageContent.InputMessageAnimation
        {
            // Установка файла анимации
            Animation = await GetFileAsync(animationMessage),
            
            // Ширина (без указания высоты и ширины будет отправлена как документ, а сами эти параметры ни на что не влияют)
            Width = 200,
            
            // Высота (без указания высоты и ширины будет отправлена как документ, а сами эти параметры ни на что не влияют)
            Height = 200,
            
            // Подпись
            Caption = new TdApi.FormattedText
            {
                Text = animationMessage.Caption
            },
            
            //Истинно, если превью анимации должно быть закрыто
            HasSpoiler = animationMessage.HasSpoiler
        };
    }
    
    /// <summary>
    /// Получает файл из данных сообщения.
    /// </summary>
    /// <param name="messageData">Данные сообщения.</param>
    /// <returns>Представление файла в TdLib.</returns>
    private static async Task<TdApi.InputFile?> GetFileAsync(ITgInputDataWithFile messageData)
    {
        // Создаем экземпляр класса InputFileVisitor
        InputFileVisitor inputFileVisitor = new();

        // Принимаем посетителя файла для сообщения данных
        await messageData.File.AcceptAsync(inputFileVisitor);

        // Возвращаем кортеж с файлом и миниатюрой
        return inputFileVisitor.File;
    }
}