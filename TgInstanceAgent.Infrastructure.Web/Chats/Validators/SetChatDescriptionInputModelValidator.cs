using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatDescriptionInputModel.
/// </summary>
public class SetChatDescriptionInputModelValidator : AbstractValidator<SetChatDescriptionInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetChatDescriptionInputModelValidator()
    {
        // Правило для ChatId
        RuleFor(x => x.ChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ChatId is required");
    }
}