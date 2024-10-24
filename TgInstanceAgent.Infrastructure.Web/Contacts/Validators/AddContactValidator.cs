using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Contacts.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Contacts.Validators;

/// <summary>
/// Валидатор для AddContactInputModel
/// </summary>
public class AddContactValidator : AbstractValidator<AddContactInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public AddContactValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputUserValidator());
        
        // Правило для FirstName
        RuleFor(x => x.FirstName)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("First name is required")

            // Максимальная длинна
            .MaximumLength(255)

            // С сообщением
            .WithMessage("First name must not exceed 255 characters");
        
        // Правило для LastName
        RuleFor(x => x.LastName)

            // Максимальная длинна
            .MaximumLength(255)

            // С сообщением
            .WithMessage("Last name must not exceed 255 characters");
    }
}