using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

/// <summary>
/// Валидатор для классов, реализующих интерфейс IWithInputReaction
/// </summary>
public class WithInputReactionValidator : AbstractValidator<IWithInputReaction>
{

    /// <summary>
    /// Конструктор
    /// </summary>
    public WithInputReactionValidator()
    {
        // Правило для отправляемой Emoji
        RuleFor(x => x.Emoji)
            
            // Не пустое
            .NotEmpty()
            
            // Когда пустое EmojiId
            .When(x => !x.EmojiId.HasValue)
            
            // С сообщением
            .WithMessage("You must specify one of the identification methods: emoji or emoji id for premium reactions");

        // Когда пустое Emoji
        When(x => string.IsNullOrWhiteSpace(x.Emoji), () =>
        {
            // Правило для отправляемой EmojiId
            RuleFor(x => x.EmojiId)
                
                // Не пустое
                .NotEmpty()
                
                // С сообщением
                .WithMessage(
                    "You must specify one of the identification methods: emoji or emoji id for premium reactions")
                
                // Больше 0
                .GreaterThan(0)
                
                // С сообщением
                .WithMessage("Emoji Id cannot be less than 0");
        });
    }
}