using MediatR;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Application.Abstractions.Commands.TgProfile;

/// <summary>
/// Команда для установки фотографии профиля в виде анимации
/// </summary>
public class SetAnimationProfilePhotoCommand : IWithFile, IWithInstanceId, IRequest
{
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public required Guid InstanceId { get; init; }
    
    /// <summary>
    /// Файл.
    /// </summary>
    public FileData? File { get; init; }

    /// <summary>
    /// Локальный идентификатор файла
    /// </summary>
    public int? LocalId { get; init; }

    /// <summary>
    /// Удаленный идентификатор файла
    /// </summary>
    public string? RemoteId { get; init; }
    
    /// <summary>
    /// Временная метка кадра, с которой закончится анимация.
    /// </summary>
    public double MainFrameTimestamp { get; init; }
}