using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;
using TgInstanceAgent.Infrastructure.Telegram.Visitors.Files;

namespace TgInstanceAgent.Infrastructure.Telegram.Visitors.ProfilePhoto;

/// <summary>
/// Посетитель для обработки фотографии профиля.
/// </summary>
/// <param name="client">Клиент TdLib для отправки запросов.</param>
public class InputChatPhotoVisitor(TdApi.Client client) : IProfilePhotoVisitor
{
    /// <summary>
    /// Фотография профиля.
    /// </summary>
    public TdApi.InputChatPhoto? Photo { get; private set; }

    /// <summary>
    ///  Метод посещения объектов для типа ProfilePhotoPrevious.
    /// </summary>
    /// <param name="profilePhotoPrevious">Объект профильной фотографии типа profilePhotoPrevious</param>
    /// <returns>Задача, представляющая асинхронное выполнение метода.</returns>
    public Task VisitAsync(TgInputProfilePhotoPrevious profilePhotoPrevious)
    {
        // Устанавливаем фотографию профиля
        Photo = new TdApi.InputChatPhoto.InputChatPhotoPrevious
        {
            // Устанавливаем идентификатор одной из предыдущих фотографий пользователя
            ChatPhotoId = profilePhotoPrevious.PhotoId
        };

        return Task.CompletedTask;
    }

    /// <summary>
    ///  Метод посещения объектов для типа ProfilePhotoEmoji.
    /// </summary>
    /// <param name="profilePhotoEmoji">Объект профильной фотографии типа ProfilePhotoEmoji</param>
    /// <returns>Задача, представляющая асинхронное выполнение метода.</returns>
    public async Task VisitAsync(TgInputProfilePhotoEmoji profilePhotoEmoji)
    {
        // Создаем посетителя фонов
        var backgroundFillVisitor = new BackgroundFillVisitor();

        // Принимаем посетителя
        profilePhotoEmoji.BackgroundFill.Accept(backgroundFillVisitor);
        
        // Получаем кастомное эмоджи асинхронно
        await client.GetCustomEmojiStickersAsync([profilePhotoEmoji.CustomEmojiId]);
        
        // Устанавливаем фотографию профиля типа "стикер"
        Photo = new TdApi.InputChatPhoto.InputChatPhotoSticker
        {
            // Создаем нового объекта стикера чата
            Sticker = new TdApi.ChatPhotoSticker
            {
                // Устанавливаем идентификатор кастомного эмоджи
                Type = new TdApi.ChatPhotoStickerType.ChatPhotoStickerTypeCustomEmoji
                {
                    CustomEmojiId = profilePhotoEmoji.CustomEmojiId
                },
        
                // Установка заполнения фона
                BackgroundFill = backgroundFillVisitor.BackgroundFill
            }
        };
    }

    /// <summary>
    ///  Метод посещения объектов для типа ProfilePhotoAnimation.
    /// </summary>
    /// <param name="profilePhotoPicture">Объект профильной фотографии типа ProfilePhotoAnimation</param>
    /// <returns>Задача, представляющая асинхронное выполнение метода.</returns>
    public async Task VisitAsync(TgInputProfilePhotoAnimation profilePhotoPicture)
    {
        // Создаем посетителя файлов
        var fileVisitor = new InputFileVisitor();

        // Посещаем файл
        await profilePhotoPicture.File.AcceptAsync(fileVisitor);
        
        // Устанавливаем фотографию профиля типа "анмация"
        Photo = new TdApi.InputChatPhoto.InputChatPhotoAnimation
        {
            // Создаем экземпляр класса InputFileLocal
            Animation = fileVisitor.File!,
            
            // Устанавлиаем временную метку кадра, с которой закончится анимация.
            MainFrameTimestamp = profilePhotoPicture.MainFrameTimestamp
        };
    }

    /// <summary>
    /// Метод посещения объектов для типа ProfilePhotoPicture.
    /// </summary>
    /// <param name="profilePhotoPicture">Объект профильной фотографии типа ProfilePhotoPicture.</param>
    /// <returns>Задача, представляющая асинхронное выполнение метода.</returns>
    public async Task VisitAsync(TgInputProfilePhotoPicture profilePhotoPicture)
    {
        // Создаем посетителя файлов
        var fileVisitor = new InputFileVisitor();

        // Посещаем файл
        await profilePhotoPicture.File.AcceptAsync(fileVisitor);

        // Установить фотографию профиля
        Photo = new TdApi.InputChatPhoto.InputChatPhotoStatic
        {
            // Устанавливаем фото
            Photo = fileVisitor.File
        };
    }

    /// <summary>
    /// Метод посещения объектов для типа ProfilePhotoSticker.
    /// </summary>
    /// <param name="profilePhotoSticker">Объект профильной фотографии типа ProfilePhotoSticker.</param>
    /// <returns>Задача, представляющая асинхронное выполнение метода.</returns>
    public async Task VisitAsync(TgInputProfilePhotoSticker profilePhotoSticker)
    {
        // Создаем посетителя фонов
        var backgroundFillVisitor = new BackgroundFillVisitor();

        // Принимаем посетителя
        profilePhotoSticker.BackgroundFill.Accept(backgroundFillVisitor);
                
        // Получаем набор стикеров асинхронно
        await client.GetStickerSetAsync(profilePhotoSticker.StickerSetId);
            
        // Устанавливаем фотографию профиля типа "стикер"
        Photo = new TdApi.InputChatPhoto.InputChatPhotoSticker
        {
            // Создаем нового объекта стикера чата
            Sticker = new TdApi.ChatPhotoSticker
            {
                // Устанавливаем тип стикера 
                Type = new TdApi.ChatPhotoStickerType.ChatPhotoStickerTypeRegularOrMask
                {
                    // Идентификатор набора стикеров
                    StickerSetId = profilePhotoSticker.StickerSetId,
                    
                    // Идентификатор стикера
                    StickerId = profilePhotoSticker.StickerId
                },
        
                // Установка заполнения фона
                BackgroundFill = backgroundFillVisitor.BackgroundFill
            }
        };
    }
}