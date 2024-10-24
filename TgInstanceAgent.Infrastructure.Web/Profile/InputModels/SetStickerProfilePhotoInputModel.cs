using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Profile.InputModels;

/// <summary>
/// Модель входных данных для установки стикера в качестве фото профиля.
/// </summary>
public class SetStickerProfilePhotoInputModel : IWithInputBackground
{
    /// <summary>
    /// Идентификатор набора стикеров
    /// </summary>
    public long StickerSetId { get; init; }

    /// <summary>
    /// Идентификатор стикера
    /// </summary>
    public long StickerId { get; init; }

    /// <summary>
    /// Цвет фона в формате HEX
    /// </summary>
    public string? Color { get; set; }
    
    /// <summary>
    /// Список из 3 или 4 цветов градиентов произвольной формы в формате HEX
    /// </summary>
    public string[]? Colors { get; set; }
    
    /// <summary>
    /// Модель входных данных для возможности установки градиентного фона
    /// </summary>
    public BackgroundGradientInputModel? Gradient { get; set; }
}