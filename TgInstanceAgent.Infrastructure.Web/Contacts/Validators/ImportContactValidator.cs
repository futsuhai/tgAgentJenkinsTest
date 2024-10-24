using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Contacts.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Contacts.Validators;

/// <summary>
/// Валидатор для ImportContactInputModel
/// </summary>
public class ImportContactValidator : AbstractValidator<ImportContactInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public ImportContactValidator()
    {
        // Правило для PhoneNumber
        RuleFor(x => x.PhoneNumber)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Phone number is required")

            // Соответствует регулярному выражению
            .Matches(@"^\+?[1-9][0-9]{7,14}$")

            // С сообщением
            .WithMessage("Phone number must have the correct format");
        
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