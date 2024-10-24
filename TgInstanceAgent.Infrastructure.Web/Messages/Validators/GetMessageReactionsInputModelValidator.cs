using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для GetMessageReactionsModelValidator
/// </summary>
public class GetMessageReactionsInputModelValidator : AbstractValidator<GetMessageReactionsInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public GetMessageReactionsInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Правило для MessageId
        RuleFor(x => x.MessageId)

            // Минимальное значение
            .GreaterThan(0)

            // С сообщением
            .WithMessage("Message Id cannot be less than 0");
    }
}