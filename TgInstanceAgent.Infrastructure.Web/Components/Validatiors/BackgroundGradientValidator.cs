using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

/// <summary>
/// Валидатор для BackgroundGradientInputModel
/// </summary>
public class BackgroundGradientValidator : AbstractValidator<BackgroundGradientInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public BackgroundGradientValidator()
    {
        // Правило для TopColor
        RuleFor(x => x.TopColor)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Top color is required")
            
            // Должен соответствовать валидному HEX цвета
            .Matches("^#([A-Fa-f0-9]{6})$")
            
            // С сообщением
            .WithMessage("You must specify a valid HEX color");

        // Правило для BottomColor
        RuleFor(x => x.BottomColor)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Bottom color is required")
            
            // Должен соответствовать валидному HEX цвета
            .Matches("^#([A-Fa-f0-9]{6})$")
            
            // С сообщением
            .WithMessage("You must specify a valid HEX color");

        // Правило для RotationAngle
        RuleFor(x => x.RotationAngle)

            // В диапазоне
            .InclusiveBetween(0, 359)

            // С сообщением
            .WithMessage("Rotation angle must be between 0 and 359 degrees")
            
            // Угол поворота градиента должен быть кратен 45
            .Must(x => x % 45 == 0)

            // С сообщением
            .WithMessage("Rotation angle must be divisible by 45");
    }
}