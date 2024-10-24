namespace TgInstanceAgent.Application.Abstractions.Interfaces;

public interface IWithReaction
{
    string? Emoji { get; init; }
    
    long? EmojiId { get; init; }
}