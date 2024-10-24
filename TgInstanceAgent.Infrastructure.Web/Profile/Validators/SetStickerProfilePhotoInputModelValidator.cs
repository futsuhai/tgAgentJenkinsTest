using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Profile.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Profile.Validators;

/// <summary>
/// Валидатор для IWithInputBackground
/// </summary>
public class SetStickerProfilePhotoInputModelValidator : AbstractValidator<SetStickerProfilePhotoInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetStickerProfilePhotoInputModelValidator()
    {
        // Добавляем валидатор для фона
        Include(new WithBackgroundValidator());

        // Правило для StickerId
        RuleFor(x => x.StickerId)

            // Значение должно быть положительным
            .GreaterThan(0)

            // С сообщением
            .WithMessage("The sticker ID must be greater than 0.");

        // Правило для StickerSetId
        RuleFor(x => x.StickerSetId)

            // Значение должно быть положительным
            .GreaterThan(0)

            // С сообщением
            .WithMessage("The sticker set ID must be greater than 0.");
    }
}