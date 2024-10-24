using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Profile.InputModels;

/// <summary>
/// Модель входных данных для установки анимации в виде фотографии профиля.
/// </summary>
public class SetAnimationProfilePhotoInputModel : IWithInputFile
{
    /// <summary>
    /// Файл
    /// </summary>
    public IFormFile? File { get; init; }

    /// <summary>
    /// Локальный идентификатор файла
    /// </summary>
    public int? LocalId { get; init; }

    /// <summary>
    /// Удаленный идентификатор файла
    /// </summary>
    public string? RemoteId { get; init; }
    
    /// <summary>
    /// Временная метка кадра, когда анимация будет отображаться статичной.
    /// </summary>
    public double MainFrameTimestamp { get; init; }
}