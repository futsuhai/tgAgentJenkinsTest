namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

/// <summary>
/// Специальное сообщение, с рандомно сгенерированным числом и эмодзи
/// </summary>
public class TgDiceMessage : TgMessageContent
{
    /// <summary>
    /// Анимированные стикеры с начальной анимацией кубика
    /// </summary>
    public required TgMessageDiceStickers InitialState { get; init; }
    
    /// <summary>
    /// Анимированные стикеры с конечной анимацией кубика
    /// </summary>
    public required TgMessageDiceStickers FinalState { get; init; }
    
    /// <summary>
    ///  Эмодзи, на основе которого происходит анимация броска кубика.
    /// </summary>
    public required string Emoji { get; init; }
    
    /// <summary>
    /// Значение кубика. Если значение равно 0, то у кубика еще нет конечного состояния.
    /// </summary>
    public required int Value { get; init; }
    
    /// <summary>
    /// Номер кадра после которого необходимо показать анимацию успеха
    /// </summary>
    public required int SuccessAnimationFrameNumber { get; init; }
}