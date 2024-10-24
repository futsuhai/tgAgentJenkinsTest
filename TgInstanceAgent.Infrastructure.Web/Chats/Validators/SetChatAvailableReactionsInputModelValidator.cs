using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatAvailableReactionsInputModel
/// </summary>
public class SetChatAvailableReactionsInputModelValidator : AbstractValidator<SetChatAvailableReactionsInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetChatAvailableReactionsInputModelValidator()
    {
        // Правило для ChatId
        RuleFor(x => x.ChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ChatId is required");
        
        // Правило для MaxReactionCount
        RuleFor(x => x.MaxReactionCount)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("MaxReactionCount is required");
    }
}