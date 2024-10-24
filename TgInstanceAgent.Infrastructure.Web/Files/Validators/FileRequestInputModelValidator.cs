using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Files.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Files.Validators;

/// <summary>
/// Валидатор для FileRequestInputModel.
/// </summary>
public class FileRequestInputModelValidator : AbstractValidator<FileRequestInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public FileRequestInputModelValidator()
    {
        // Когда FileFromMessage и RemoteId пустые
        When(x => x.FileFromMessage == null && x.RemoteId == null, () =>
        {
            // Правило для FileId
            RuleFor(x => x.LocalId)

                // Минимальное значение
                .GreaterThan(0)

                // С сообщением
                .WithMessage("File Id cannot be less than 0");
        });

        // Когда LocalId и RemoteId пустые
        When(x => x.LocalId == null && x.RemoteId == null, () =>
        {
            // Правило для FileFromMessage
            RuleFor(x => x.FileFromMessage)

                // Устанавливаем валидатор
                .SetValidator(new FileFromMessageInputModelValidator()!);
        });
        
        // Когда LocalId и FileFromMessage пустые
        When(x => x.LocalId == null && x.FileFromMessage == null, () =>
        {
            // Правило для FileFromMessage
            RuleFor(x => x.RemoteId)

                // Не пустое
                .NotEmpty()

                // С сообщением
                .WithMessage("RemoteId cannot be empty");
        });
    }
}