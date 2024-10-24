using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для SendDocumentMessageInputModel
/// </summary>
public class SendDocumentMessageInputModelValidator : AbstractValidator<SendDocumentMessageInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SendDocumentMessageInputModelValidator()
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

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Title is required")

            // Максимальная длинна
            .MaximumLength(64)

            // С сообщением
            .WithMessage("Title length cannot exceed 64 characters");
    }
}