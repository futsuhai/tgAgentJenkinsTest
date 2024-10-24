using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

/// <summary>
/// Валидатор для классов, реализующих интерфейс IWithInputCaption
/// </summary>
public class WithInputCaptionValidator : AbstractValidator<IWithInputCaption>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public WithInputCaptionValidator()
    {
        // Правило для Caption
        RuleFor(x => x.Caption)

            // Максимальная длинна
            .MaximumLength(2048)

            // С сообщением
            .WithMessage("Caption length cannot exceed 2048 characters");
    }
}