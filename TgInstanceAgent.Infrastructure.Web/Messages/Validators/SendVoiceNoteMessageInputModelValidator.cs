using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для SendVoiceNoteMessageInputModel
/// </summary>
public class SendVoiceNoteMessageInputModelValidator : AbstractValidator<SendVoiceNoteMessageInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SendVoiceNoteMessageInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор 
        Include(new WithInputChatValidator());

        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputFileValidator());

        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputCaptionValidator());

        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputSendOptionValidator());
    }
}