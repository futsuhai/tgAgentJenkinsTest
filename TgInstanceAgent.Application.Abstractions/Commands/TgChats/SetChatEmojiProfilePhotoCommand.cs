using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgChats;

/// <summary>
/// Команда для установки фотографии профиля чата в виде кастомного емозди.
/// </summary>
public class SetChatEmojiProfilePhotoCommand : IWithBackgroundFill, IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Идентификатор чата.
    /// </summary>
    public required long ChatId { get; init; }
    
    /// <summary>
    /// Идентификатор кастомного эмоджи.
    /// </summary>
    public required long CustomEmojiId { get; init; }
    
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