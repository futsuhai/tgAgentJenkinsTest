using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgProfile;

/// <summary>
/// Команда для установки фотографии профиля.
/// </summary>
public class SetPreviousProfilePhotoCommand : IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Идентификатор одной из предыдущих фотографий, которые есть у пользователя.
    /// </summary>
    public required long PhotoId { get; init; }
}