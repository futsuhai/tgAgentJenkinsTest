using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Profile.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Profile.Validators;

/// <summary>
/// Валидатор для SetEmojiProfilePhotoInputModel
/// </summary>
public class SetEmojiProfilePhotoInputModelValidator : AbstractValidator<SetEmojiProfilePhotoInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetEmojiProfilePhotoInputModelValidator()
    {
        // Добавляем валидатор для фона
        Include(new WithBackgroundValidator());

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