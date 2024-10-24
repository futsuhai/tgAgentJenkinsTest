using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Profile.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Profile.Validators;

/// <summary>
/// Валидатор для SetUserBioInputModel
/// </summary>
public class SetUserBioInputModelValidator : AbstractValidator<SetUserBioInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SetUserBioInputModelValidator()
    {
        // Правило Bio
        RuleFor(x => x.Bio)
            
            // Максимально значение
            .MaximumLength(128)
            
            // С сообщением
            .WithMessage("Bio cannot be greater than 128 symbols");

    }
}