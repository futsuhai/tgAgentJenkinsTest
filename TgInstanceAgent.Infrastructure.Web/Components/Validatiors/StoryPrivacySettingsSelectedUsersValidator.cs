using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

/// <summary>
/// Валидатор для StoryPrivacySettingsSelectedUsersInputModel
/// </summary>
public class StoryPrivacySettingsSelectedUsersValidator : AbstractValidator<StoryPrivacySettingsSelectedUsersInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public StoryPrivacySettingsSelectedUsersValidator()
    {
        // Правило для UserIds
        RuleFor(x => x.UserIds)
            
            // Не null
            .NotNull()
            
            // С сообщением
            .WithMessage("User ids is required");
        
        // Правило для каждого элемента в массиве ExceptUserIds
        RuleForEach(x => x.UserIds)

            // Больше 0
            .GreaterThan(0)

            // С сообщением
            .WithMessage("Each User ID must be greater than 0.");
    }
}