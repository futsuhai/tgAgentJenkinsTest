using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

/// <summary>
/// Валидатор для StoryPrivacySettingsContactsInputModel
/// </summary>
public class StoryPrivacySettingsContactsValidator : AbstractValidator<StoryPrivacySettingsContactsInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public StoryPrivacySettingsContactsValidator()
    {
        // Правило для ExceptUserIds
        RuleFor(x => x.ExceptUserIds)

            // Не null
            .NotNull()

            // С сообщением
            .WithMessage("Except User ids is required");
        
        // Правило для каждого элемента в массиве ExceptUserIds
        RuleForEach(x => x.ExceptUserIds)

            // Больше 0
            .GreaterThan(0)

            // С сообщением
            .WithMessage("Each User ID must be greater than 0.");

    }
}