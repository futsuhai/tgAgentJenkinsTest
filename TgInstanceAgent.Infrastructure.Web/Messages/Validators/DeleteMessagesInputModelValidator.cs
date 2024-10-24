using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для DeleteMessagesInputModel
/// </summary>
public class DeleteMessagesInputModelValidator : AbstractValidator<DeleteMessagesInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public DeleteMessagesInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Правило для MessageIds
        RuleFor(x => x.MessageIds)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("MessageIds is required");
        
        // Правило для каждого MessageId в MessageIds
        RuleForEach(x => x.MessageIds)
            
            // Значение должно быть больше 0
            .GreaterThan(0)

            // С сообщением
            .WithMessage("Message ID must be greater than 0.");
    }
}