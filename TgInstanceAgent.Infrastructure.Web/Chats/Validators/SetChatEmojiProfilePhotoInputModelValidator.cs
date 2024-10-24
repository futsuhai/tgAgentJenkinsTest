using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatEmojiProfilePhotoInputModel
/// </summary>
public class SetChatEmojiProfilePhotoInputModelValidator : AbstractValidator< SetChatEmojiProfilePhotoInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetChatEmojiProfilePhotoInputModelValidator()
    {
        // Добавляем валидатор для фона
        Include(new WithBackgroundValidator());

        // Правило для ChatId
        RuleFor(x => x.ChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ChatId is required");
        
        // Правило для StickerId
        RuleFor(x => x.CustomEmojiId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Emoji Id is required")

            // Значение должно быть больше 0
            .GreaterThan(0)

            // С сообщением
            .WithMessage("Emoji ID must be greater than 0.");
    }
}