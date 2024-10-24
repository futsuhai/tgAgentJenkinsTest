using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для SendAnimationMessageInputModel
/// </summary>
public class SendAnimationMessageInputModelValidator : AbstractValidator<SendAnimationMessageInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SendAnimationMessageInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputCaptionValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputFileValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputSendOptionValidator());
    }
}