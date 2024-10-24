using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

/// <summary>
/// Валидатор для классов, реализующих интерфейс IWithInputLimit
/// </summary>
public class WithInputLimitValidator : AbstractValidator<IWithInputLimit>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public WithInputLimitValidator()
    {
        // Правило для Limit
        RuleFor(x => x.Limit)

            // Максимальное значение
            .LessThanOrEqualTo(200)

            // С сообщением
            .WithMessage("Limit cannot exceed 500");


        // Правило для Offset
        RuleFor(x => x.Offset)

            // Минимальное значение
            .GreaterThanOrEqualTo(0)

            // С сообщением
            .WithMessage("Offset cannot be less than 0");
    }
}