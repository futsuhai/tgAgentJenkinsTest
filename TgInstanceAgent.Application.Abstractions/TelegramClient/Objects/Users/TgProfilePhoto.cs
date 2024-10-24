using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

/// <summary>
/// Описывает фото профиля пользователя
/// </summary>
public class TgProfilePhoto
{
    /// <summary>
    /// Идентификатор фото; null для пустого фото. Может быть использован для поиска фото в списке фото профиля пользователя
    /// </summary>
    public long? Id { get; init; }

    /// <summary>
    /// Маленькое (160x160) фото профиля пользователя. Файл может быть загружен только до изменения фото
    /// </summary>
    public required TgFile Small { get; init; }

    /// <summary>
    /// Большое (640x640) фото профиля пользователя. Файл может быть загружен только до изменения фото
    /// </summary>
    public required TgFile Big { get; init; }

    /// <summary>
    /// Миниатюра фото профиля пользователя; может быть null
    /// </summary>
    public TgMiniThumbnail? MiniThumbnail { get; init; }

    /// <summary>
    /// True, если фото имеет анимированный вариант
    /// </summary>
    public required bool HasAnimation { get; init; }

    /// <summary>
    /// True, если фото видно только для текущего пользователя
    /// </summary>
    public required bool IsPersonal { get; init; }
}
