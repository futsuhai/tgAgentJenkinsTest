using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Files.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Files.Validators;

/// <summary>
/// Валидатор для FileFromMessageInputModel.
/// </summary>
public class FileFromMessageInputModelValidator : AbstractValidator<FileFromMessageInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public FileFromMessageInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());

        RuleFor(m => m.MessageId)

            // Минимальное значение
            .GreaterThan(0)

            // С сообщением
            .WithMessage("Message Id cannot be less than 0");
    }
}