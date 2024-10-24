using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Authentication.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Authentication.Validatiors;

/// <summary>
/// Валидатор для CheckCodeInputModel
/// </summary>
public class CheckCodeInputModelValidator : AbstractValidator<CheckCodeInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public CheckCodeInputModelValidator()
    {
        // Правило для
        RuleFor(x => x.Code)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Code is required");
    }
}