using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для SendAudioMessageInputModel
/// </summary>
public class SendAudioMessageInputModelValidator : AbstractValidator<SendAudioMessageInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SendAudioMessageInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());

        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputFileValidator());

        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputCaptionValidator());

        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputSendOptionValidator());

        // Правило для Title
        RuleFor(x => x.Title)

            // Максимальная длинна
            .MaximumLength(64)

            // С сообщением
            .WithMessage("Title length cannot exceed 64 characters");


        // Правило для Performer
        RuleFor(x => x.Performer)

            // Максимальная длинна
            .MaximumLength(64)

            // С сообщением
            .WithMessage("Performer length cannot exceed 64 characters");
    }
}