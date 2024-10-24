using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Абстрактный контент истории
/// </summary>
public abstract class TgInputStoryContent
{
    /// <summary>
    /// Принять посетителя контента истории.
    /// </summary>
    public abstract void Accept(IStoryContentVisitor privacySettingsVisitor);
}

/// <summary>
/// Контент истории в виде фотографии
/// </summary>
public class TgInputStoryContentPhoto : TgInputStoryContent, ITgInputDataWithFile
{
    /// <summary>
    /// Файл, содержащий фото истории.
    /// </summary>
    public required TgInputFileData File { get; init; }
    
    /// <summary>
    /// Принять посетителя контента истории.
    /// </summary>
    public override void Accept(IStoryContentVisitor privacySettingsVisitor) => privacySettingsVisitor.Visit(this);
}

/// <summary>
/// Контент истории в виде видео
/// </summary>
public class TgInputStoryContentVideo : TgInputStoryContent, ITgInputDataWithFile
{
    /// <summary>
    /// Файл, содержащий видео истории.
    /// </summary>
    public required TgInputFileData File { get; init; }
    
    /// <summary>
    /// Истинно, если видео без звука
    /// </summary>
    public required bool IsAnimation { get; init; }
    
    /// <summary>
    /// Принять посетителя контента истории.
    /// </summary>
    public override void Accept(IStoryContentVisitor privacySettingsVisitor) => privacySettingsVisitor.Visit(this);
}
