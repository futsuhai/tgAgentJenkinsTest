using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для SendDiceMessageInputModel
/// </summary>
public class SendDiceMessageInputModelValidator : AbstractValidator<SendDiceMessageInputModel>
{
    // Допустимые эмодзи для "dice" сообщений
    private static readonly string[] AllowedEmojis = ["🎲", "🎯", "🏀", "⚽", "🎳", "🎰"];
    
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SendDiceMessageInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputSendOptionValidator());
        
        // Правило для Message
        RuleFor(x => x.Emoji)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Emoji is required")

            // Проверка на допустимость эмодзи
            .Must(emoji => AllowedEmojis.Contains(emoji))

            // С сообщением
            .WithMessage("Only one of specific dice-related emojis are. Allowed emojis: 🎲, 🎯, 🏀, ⚽, 🎳, 🎰");
    }
}