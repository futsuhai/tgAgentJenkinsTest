using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Profile.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Profile.Validators;

/// <summary>
/// Валидатор для SetAnimationProfilePhotoInputModel
/// </summary>
public class SetAnimationProfilePhotoInputModelValidator : AbstractValidator<SetAnimationProfilePhotoInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetAnimationProfilePhotoInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputFileValidator());
        
        // Правило для MainFrameTimestamp
        RuleFor(x => x.MainFrameTimestamp)
            
            // В диапазоне
            .InclusiveBetween(0, 10)

            // С сообщением
            .WithMessage("Timestamp must be between 0 and 10 seconds");
    }
}