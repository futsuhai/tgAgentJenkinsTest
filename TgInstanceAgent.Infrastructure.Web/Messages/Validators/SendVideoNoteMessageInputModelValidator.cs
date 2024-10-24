using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для SendVideoNoteMessageInputModel
/// </summary>
public class SendVideoNoteMessageInputModelValidator : AbstractValidator<SendVideoNoteMessageInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SendVideoNoteMessageInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputFileValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputSendOptionValidator());
    }
}