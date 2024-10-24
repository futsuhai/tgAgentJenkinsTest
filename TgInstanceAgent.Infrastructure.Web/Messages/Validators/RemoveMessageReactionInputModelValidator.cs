using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для RemoveMessageReactionInputModel
/// </summary>
public class RemoveMessageReactionInputModelValidator : AbstractValidator<RemoveMessageReactionInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public RemoveMessageReactionInputModelValidator()
    {
        Include(new WithInputChatValidator());
        Include(new WithInputReactionValidator());
        
        // Правило для MessageId
        RuleFor(x => x.MessageId)

            // Минимальное значение
            .GreaterThan(0)

            // С сообщением
            .WithMessage("Message Id cannot be less than 0");
    }
}