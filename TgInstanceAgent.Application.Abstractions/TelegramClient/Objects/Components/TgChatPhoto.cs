namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Полная информация о фото профиля или чата
/// </summary>
public class TgChatPhoto
{
    /// <summary>
    /// Уникальный идентификатор фотографии.
    /// </summary>
    public required long Id { get; init; }

    /// <summary>
    /// Момент времени, когда фотография была добавлена.
    /// </summary>
    public required DateTime AddedDate { get; init; }

    /// <summary>
    /// Маленькое превью.
    /// </summary>
    public TgMiniThumbnail? MiniThumbnail { get; init; }

    /// <summary>
    /// Вариации фотографии в разных размерах.
    /// </summary>
    public required TgPhotoSize[] Sizes { get; init; }

    /// <summary>
    /// Большой (до 1280x1280) анимированный вариант фотографии в формате MPEG4.
    /// </summary>
    public TgAnimatedChatPhoto? Animation { get; init; }

    /// <summary>
    /// Небольшой (160х160) анимированный вариант фотографии в формате MPEG4; может быть нулевым, даже если доступна большая анимация
    /// </summary>
    public TgAnimatedChatPhoto? SmallAnimation { get; init; }
}