using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Authentication.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Authentication.Validatiors;

/// <summary>
/// Валидатор для SetPhoneNumberInputModel
/// </summary>
public class SetPhoneNumberInputModelValidator : AbstractValidator<SetPhoneNumberInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SetPhoneNumberInputModelValidator()
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
    }
}