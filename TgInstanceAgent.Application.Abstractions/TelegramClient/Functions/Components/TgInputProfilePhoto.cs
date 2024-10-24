using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Запрос на установку фотографии.
/// </summary>
public abstract class TgInputProfilePhoto
{
    /// <summary>
    /// Принять посетителя запроса.
    /// </summary>
    public abstract Task AcceptAsync(IProfilePhotoVisitor profilePhotoVisitor);
}

/// <summary>
/// Запрос на установку фотографии.
/// </summary>
public class TgInputProfilePhotoPicture : TgInputProfilePhoto, ITgInputDataWithFile
{
    /// <summary>
    /// Данные файла.
    /// </summary>
    public required TgInputFileData File { get; init; }

    /// <summary>
    /// Принять посетителя запроса.
    /// </summary>
    public override Task AcceptAsync(IProfilePhotoVisitor profilePhotoVisitor) => profilePhotoVisitor.VisitAsync(this);
}

/// <summary>
/// Запрос на установку фотографии.
/// </summary>
public class TgInputProfilePhotoAnimation : TgInputProfilePhoto
{
    /// <summary>
    /// Данные файла.
    /// </summary>
    public required TgInputFileData File { get; init; }
    
    /// <summary>
    /// Временная метка кадра, с которой закончится анимация.
    /// </summary>
    public double MainFrameTimestamp { get; init; }

    /// <summary>
    /// Принять посетителя запроса.
    /// </summary>
    public override Task AcceptAsync(IProfilePhotoVisitor profilePhotoVisitor) => profilePhotoVisitor.VisitAsync(this);
}

/// <summary>
/// Запрос на установку фотографии.
/// </summary>
public class TgInputProfilePhotoPrevious : TgInputProfilePhoto
{
    /// <summary>
    /// Идентификатор одной из предыдущих фотографий, которые есть у пользователя.
    /// </summary>
    public required long PhotoId { get; init; }
    
    /// <summary>
    /// Принять посетителя запроса.
    /// </summary>
    public override Task AcceptAsync(IProfilePhotoVisitor profilePhotoVisitor) => profilePhotoVisitor.VisitAsync(this);
}

/// <summary>
/// Запрос на установку фотографии.
/// </summary>
public class TgInputProfilePhotoSticker : TgInputProfilePhoto
{
    /// <summary>
    /// Идентификатор набора стикеров
    /// </summary>
    public required long StickerSetId { get; init; }

    /// <summary>
    /// Идентификатор стикера
    /// </summary>
    public required long StickerId { get; init; }
    
    /// <summary>
    /// Задний фон
    /// </summary>
    public required TgInputBackgroundFill BackgroundFill { get; init; }

    /// <summary>
    /// Принять посетителя запроса.
    /// </summary>
    public override Task AcceptAsync(IProfilePhotoVisitor profilePhotoVisitor) => profilePhotoVisitor.VisitAsync(this);
}

/// <summary>
/// Запрос на установку фотографии.
/// </summary>
public class TgInputProfilePhotoEmoji : TgInputProfilePhoto
{
    /// <summary>
    /// Идентификатор кастомного эмоджи
    /// </summary>
    public required long CustomEmojiId { get; init; }
    
    /// <summary>
    /// Задний фон
    /// </summary>
    public required TgInputBackgroundFill BackgroundFill { get; init; }

    /// <summary>
    /// Принять посетителя запроса.
    /// </summary>
    public override Task AcceptAsync(IProfilePhotoVisitor profilePhotoVisitor) => profilePhotoVisitor.VisitAsync(this);
}