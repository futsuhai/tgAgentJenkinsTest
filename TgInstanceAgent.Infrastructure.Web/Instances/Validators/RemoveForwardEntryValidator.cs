using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Instances.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Instances.Validators;

/// <summary>
/// Валидатор для RemoveForwardEntryInputModel
/// </summary>
public class RemoveForwardEntryValidator : AbstractValidator<RemoveForwardEntryInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public RemoveForwardEntryValidator()
    {
        // Правило для For
        RuleFor(x => x.For)

            // Не пустой
            .NotEmpty()

            // С сообщением
            .WithMessage("Идентификатор чата 'For' не должен быть пустым");

        // Правило для To
        RuleFor(x => x.To)

            // Не пустой
            .NotEmpty()

            // С сообщением
            .WithMessage("Идентификатор чата 'To' не должен быть пустым")

            // Не равно For
            .NotEqual(x => x.For)
            
            // С сообщением
            .WithMessage("Идентификаторы чатов 'For' и 'To' не должны быть равны");
    }
}