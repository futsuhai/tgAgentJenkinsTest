using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatSlowModeDelayInputModel.
/// </summary>
public class SetChatSlowModeDelayInputModelValidator : AbstractValidator<SetChatSlowModeDelayInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetChatSlowModeDelayInputModelValidator()
    {
        // Правило для ChatId
        RuleFor(x => x.ChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ChatId is required");
        
        // Правило для SlowModeDelay
        RuleFor(x => x.SlowModeDelay)
            
            // Должно иметь одно из допустимых значений
            .Must(value => new int?[] { 0, 10, 30, 60, 300, 900, 3600 }.Contains(value))
            
            // С сообщением
            .WithMessage("SlowModeDelay must be one of the following values: 0, 10, 30, 60, 300, 900, 3600");
    }
}