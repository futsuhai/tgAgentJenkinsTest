namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя отправляемых файлов Telegram
/// </summary>
public interface IProfilePhotoVisitor
{
    /// <summary>
    /// Посетить профильное фото в виде изображения.
    /// </summary>
    /// <param name="profilePhotoPicture">Профильное фото в виде изображения.</param>
    Task VisitAsync(TgInputProfilePhotoPicture profilePhotoPicture);

    /// <summary>
    /// Посетить профильное фото в виде стикера.
    /// </summary>
    /// <param name="profilePhotoSticker">Профильное фото в виде стикера.</param>
    Task VisitAsync(TgInputProfilePhotoSticker profilePhotoSticker);

    /// <summary>
    /// Посетить одно из предыдущих профильных фото.
    /// </summary>
    /// <param name="profilePhotoPicture">Одно из предыдущих профильных фото.</param>
    Task VisitAsync(TgInputProfilePhotoPrevious profilePhotoPicture);

    /// <summary>
    /// Посетить профильное фото в виде эмодзи.
    /// </summary>
    /// <param name="profilePhotoPicture">Профильное фото в виде эмодзи.</param>
    Task VisitAsync(TgInputProfilePhotoEmoji profilePhotoPicture);

    /// <summary>
    /// Посетить профильное фото в виде анимации.
    /// </summary>
    /// <param name="profilePhotoPicture">Профильное фото в виде анимации.</param>
    Task VisitAsync(TgInputProfilePhotoAnimation profilePhotoPicture);
}