using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Chats;

/// <summary>
/// Описание фона, установленного для конкретного чата
/// </summary>
public class TgChatBackground
{
    /// <summary>
    /// Фон
    /// </summary>
    public required TgBackground Background { get; init; }
    
    /// <summary>
    /// Затемнение фона в темных темах, в процентах; 0-100.
    /// Применяется только для типов фона «Обои»(Wallpaper) и «Заливка»(Fill).
    /// </summary>
    public required int DarkThemeDimming { get; init; }
}