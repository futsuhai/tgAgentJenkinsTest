using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Stories.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Stories.Validators;

/// <summary>
/// Валидатор для GetChatActiveStoriesInputModel
/// </summary>
public class GetChatActiveStoriesInputModelValidator : AbstractValidator<GetChatActiveStoriesInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public GetChatActiveStoriesInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
    }
}