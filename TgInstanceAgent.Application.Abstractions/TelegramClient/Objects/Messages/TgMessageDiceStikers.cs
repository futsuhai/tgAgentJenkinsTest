using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Абстратные данные специального сообщения Dice
/// </summary>
public abstract class TgMessageDiceStickers;

/// <summary>
/// Содержит анимированный стикер для рендеринга анимиции dice
/// </summary>
public class TgMessageDiceStickersRegular : TgMessageDiceStickers
{
    /// <summary>
    /// Анимированный стикер
    /// </summary>
    public required TgSticker Sticker { get; init; }
}

/// <summary>
/// Анимированные стикеры, которые будут скомбинированы в слот машину
/// </summary>
public class TgMessageDiceStickersSlotMachine : TgMessageDiceStickers
{
    /// <summary>
    /// Анимированный стикер с фоном слот машины
    /// </summary>
    public required TgSticker Background { get; init; }
    
    /// <summary>
    /// Анимированный стикер с анимацией рычага
    /// </summary>
    public required TgSticker Lever { get; init; }
    
    /// <summary>
    /// Анимированный стикер на левом барабане
    /// </summary>
    public required TgSticker LeftReel { get; init; }
    
    /// <summary>
    /// Анимированный стикер на центральном барабане
    /// </summary>
    public required TgSticker CenterReel { get; init; }
    
    /// <summary>
    /// Анимированный стикер на правом барабане
    /// </summary>
    public required TgSticker RightReel { get; init; }
}