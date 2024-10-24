using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

/// <summary>
/// Валидатор для IWithInputBackground
/// </summary>
public class WithBackgroundValidator : AbstractValidator<IWithInputBackground>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public WithBackgroundValidator()
    {
        // Правило для Color
        RuleFor(x => x.Color)

            // Поле не должно быть пустым, если другие поля фона не заполнены
            .NotEmpty()

            // Когда Colors и Gradient не указаны
            .When(x => x.Colors == null && x.Gradient == null)

            // С сообщением
            .WithMessage(
                "You need to specify one of the possible background fill representations: color, colors, gradient")

            // Должен соответствовать валидному HEX цвета
            .Matches("^#([A-Fa-f0-9]{6})$")

            // С сообщением
            .WithMessage("You must specify a valid HEX color");

        // Правило для Colors
        RuleFor(x => x.Colors)

            // Список цветов не должен быть пустым, если другие поля фона не заполнены
            .NotEmpty()

            // Когда Color и Gradient не указаны
            .When(x => string.IsNullOrWhiteSpace(x.Color) && x.Gradient == null)

            // С сообщением
            .WithMessage(
                "You need to specify one of the possible background fill representations: color, colors, gradient")

            // Длина списка должна быть 3 или 4, если он указан
            .Must(x => x == null || x.Length == 3 || x.Length == 4)

            // С сообщением
            .WithMessage("A list of 3 or 4 colors is required for freeform gradients.");

        // Правило для каждого элемента Colors
        RuleForEach(x => x.Colors)
            
            // Поле не должно быть пустым
            .NotEmpty()

            // С сообщением
            .WithMessage("The color cannot be empty")

            // Должен соответствовать валидному HEX цвета
            .Matches("^#([A-Fa-f0-9]{6})$")

            // С сообщением
            .WithMessage("You must specify a valid HEX color");

        // Правило для Gradient
        RuleFor(x => x.Gradient)

            // Градиент не должен быть пустым, если другие поля фона не заполнены
            .NotEmpty()
            
            // Когда Color и Colors не указаны
            .When(x => string.IsNullOrWhiteSpace(x.Color) && x.Colors == null)

            // С сообщением
            .WithMessage(
                "You need to specify one of the possible background fill representations: color, colors, gradient")

            // Устанавливаем валидатор
            .SetValidator(new BackgroundGradientValidator()!);
    }
}