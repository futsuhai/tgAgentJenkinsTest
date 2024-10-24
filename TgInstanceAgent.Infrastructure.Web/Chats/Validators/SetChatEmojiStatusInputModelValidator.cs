using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatEmojiStatusInputModel.
/// </summary>
public class SetChatEmojiStatusInputModelValidator : AbstractValidator<SetChatEmojiStatusInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SetChatEmojiStatusInputModelValidator()
    {
        // Правило для ChatId
        RuleFor(x => x.ChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ChatId is required");
    }
}