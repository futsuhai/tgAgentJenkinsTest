using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для ForwardMessagesInputModel
/// </summary>
public class ForwardMessagesInputModelValidator : AbstractValidator<ForwardMessagesInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public ForwardMessagesInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());

        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputSendOptionValidator());

        // Правило для FromChatId
        RuleFor(x => x.FromChatId)

            // Значение должно быть больше 0
            .GreaterThan(0)

            // С сообщением
            .WithMessage("FromChat ID must be greater than 0.");

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