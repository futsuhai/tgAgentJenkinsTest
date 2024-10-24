using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatDiscussionGroupInputModel.
/// </summary>
public class SetChatDiscussionGroupInputModelValidator : AbstractValidator<SetChatDiscussionGroupInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetChatDiscussionGroupInputModelValidator()
    {
        // Правило для ChatId
        RuleFor(x => x.ChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ChatId is required");
        
        // Правило для DiscussionChatId
        RuleFor(x => x.DiscussionChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("DiscussionChatId is required");
    }
}