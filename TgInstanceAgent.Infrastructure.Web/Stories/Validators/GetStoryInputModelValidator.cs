using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Stories.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Stories.Validators;

/// <summary>
/// Валидатор для GetStoryInputModel
/// </summary>
public class GetStoryInputModelValidator : AbstractValidator<GetStoryInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public GetStoryInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        //Правило для StoryId
        RuleFor(x => x.StoryId)

            // Больше 0
            .GreaterThan(0)

            // С сообщением
            .WithMessage("The story ID must be greater than 0.");
    }
}