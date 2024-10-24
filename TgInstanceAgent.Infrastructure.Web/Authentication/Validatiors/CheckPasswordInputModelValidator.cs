using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Authentication.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Authentication.Validatiors;

/// <summary>
/// Валидатор для CheckPasswordInputModel
/// </summary>
public class CheckPasswordInputModelValidator : AbstractValidator<CheckPasswordInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public CheckPasswordInputModelValidator()
    {
        // Правило для Password
        RuleFor(x => x.Password)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Password is required");
    }
}