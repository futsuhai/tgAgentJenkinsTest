using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для GetMessageAddedReactionsModelValidator.
/// </summary>
public class GetMessageAddedReactionsUnputModelValidator : AbstractValidator<GetMessageAddedReactionsInputModel>
{
    /// <summary>
    ///  Конструктор
    /// </summary>
    public GetMessageAddedReactionsUnputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Правило для MessageId
        RuleFor(x => x.MessageId)

            // Минимальное значение
            .GreaterThan(0)

            // С сообщением
            .WithMessage("Message Id cannot be less than 0");
        
        // Правило для Limit
        RuleFor(x => x.Limit)

            // Максимальное значение
            .LessThanOrEqualTo(500)

            // С сообщением
            .WithMessage("Limit cannot exceed 500");
        
        RuleFor(x => x.EmojiId)
            
            // Больше 0
            .GreaterThan(0)
            
            // Когда пустое EmojiId
            .When(x => !x.EmojiId.HasValue)
            
            // С сообщением
            .WithMessage("EmojiId must be greater than 0.");
    }
}