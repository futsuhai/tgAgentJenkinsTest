using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatTitleInputModel.
/// </summary>
public class SetChatTitleInputModelValidator : AbstractValidator<SetChatTitleInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetChatTitleInputModelValidator()
    {
        // Правило для ChatId
        RuleFor(x => x.ChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ChatId is required");
        
        // Правило для Title
        RuleFor(x => x.Title)
            
            // Не пустое
            .NotEmpty()
            
            // С сообщением
            .WithMessage("Title is required")
            
            // Максимальная длина 128 символов
            .MaximumLength(128)
            
            // С сообщением
            .WithMessage("Title must not exceed 128 characters");
    }
}