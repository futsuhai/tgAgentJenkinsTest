using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatAccentColorInputModel.
/// </summary>
public class SetChatAccentColorInputModelValidator : AbstractValidator<SetChatAccentColorInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetChatAccentColorInputModelValidator()
    {
        // Правило для ChatId
        RuleFor(x => x.ChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ChatId is required");
    }
}