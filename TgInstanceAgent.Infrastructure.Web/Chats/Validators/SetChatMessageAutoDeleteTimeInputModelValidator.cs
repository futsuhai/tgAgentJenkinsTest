using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatMessageAutoDeleteTimeInputModel.
/// </summary>
public class SetChatMessageAutoDeleteTimeInputModelValidator : AbstractValidator<SetChatMessageAutoDeleteTimeInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SetChatMessageAutoDeleteTimeInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Правило для MessageAutoDeleteTime
        RuleFor(x => x.MessageAutoDeleteTime)
            // Проверяем, что значение находится в диапазоне от 0 до 365 * 86400
            .InclusiveBetween(0, 365 * 86400)
            .WithMessage("MessageAutoDeleteTime must be between 0 and 365 days (in seconds).")

            // Проверяем, что значение кратно 86400 или равно 0
            .Must(time => time == 0 || time % 86400 == 0)
            .WithMessage("MessageAutoDeleteTime must be divisible by 86400 seconds or be 0.");
    }
}