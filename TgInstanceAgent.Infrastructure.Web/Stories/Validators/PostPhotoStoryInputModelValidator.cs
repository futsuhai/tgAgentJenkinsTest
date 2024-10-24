using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Stories.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Stories.Validators;

/// <summary>
/// Валидатор для SendPhotoStoryInputModel
/// </summary>
public class PostPhotoStoryInputModelValidator : AbstractValidator<PostPhotoStoryInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public PostPhotoStoryInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputFileValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputCaptionValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputStoryPrivacySettingsValidator());
        
        // Правило для ActivePeriod
        RuleFor(x => x.ActivePeriod)
            
            // Значение 6 или 12 или 24 или 48
            .Must(x => x is 6 or 12 or 24 or 48)
            
            // С сообщением
            .WithMessage("ActivePeriod must be 6, 12, 24 or 48.");
    }
}