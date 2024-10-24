using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatProfilePhotoInputModel
/// </summary>
public class SetChatProfilePhotoInputModelValidator: AbstractValidator<SetChatProfilePhotoInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetChatProfilePhotoInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputFileValidator());
        
        // Правило для ChatId
        RuleFor(x => x.ChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ChatId is required");
    }
}