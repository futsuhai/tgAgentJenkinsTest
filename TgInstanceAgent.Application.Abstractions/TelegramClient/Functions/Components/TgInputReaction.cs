using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

public abstract class TgInputReaction
{
    public abstract void Accept(IReactionVisitor visitor);
}

public class TgInputReactionEmoji : TgInputReaction {
    
    public required string Emoji { get; init; }
    
    public override void Accept(IReactionVisitor visitor) => visitor.Visit(this);
}

public class TgInputCustomPremiumEmoji : TgInputReaction
{
    public required long EmojiId { get; init; }
    
    public override void Accept(IReactionVisitor visitor) => visitor.Visit(this);
}