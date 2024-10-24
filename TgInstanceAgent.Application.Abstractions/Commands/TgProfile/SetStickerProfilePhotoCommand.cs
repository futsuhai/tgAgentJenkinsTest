using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgProfile;

/// <summary>
/// Команда для установки фотографии профиля.
/// </summary>
public class SetStickerProfilePhotoCommand : IWithBackgroundFill, IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Идентификатор набора стикеров
    /// </summary>
    public required long StickerSetId { get; init; }

    /// <summary>
    /// Идентификатор стикера
    /// </summary>
    public required long StickerId { get; init; }

    /// <summary>
    /// Цвет фона в формате HEX
    /// </summary>
    public string? Color { get; init; }
    
    /// <summary>
    /// Список из 3 или 4 цветов градиентов произвольной формы в формате HEX
    /// </summary>
    public string[]? Colors { get; init; }
    
    /// <summary>
    /// Данные для возможности установки градиентного фона
    /// </summary>
    public BackgroundGradientData? Gradient { get; init; }
}


