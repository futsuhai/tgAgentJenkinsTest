using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatProfileAccentColorInputModel.
/// </summary>
public class SetChatProfileAccentColorInputModelValidator : AbstractValidator<SetChatProfileAccentColorInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetChatProfileAccentColorInputModelValidator()
    {
        // Правило для ChatId
        RuleFor(x => x.ChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ChatId is required");
        
        // Правило для ProfileAccentColorId
        RuleFor(x => x.ProfileAccentColorId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ProfileAccentColorId is required");

        // Правило для ProfileBackgroundCustomEmojiId
        RuleFor(x => x.ProfileBackgroundCustomEmojiId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ProfileBackgroundCustomEmojiId is required");
    }
}