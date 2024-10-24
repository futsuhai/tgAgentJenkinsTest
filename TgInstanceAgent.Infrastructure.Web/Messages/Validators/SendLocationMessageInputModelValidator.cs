using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для SendLocationMessageInputModel
/// </summary>
public class SendLocationMessageInputModelValidator : AbstractValidator<SendLocationMessageInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SendLocationMessageInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());

        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputSendOptionValidator());

        // Правило для Latitude
        RuleFor(x => x.Latitude)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Latitude is required")

            // В диапазоне
            .InclusiveBetween(-90, 90)

            // С сообщением
            .WithMessage("Latitude must be between -90 and 90 degrees");


        // Правило для Longitude
        RuleFor(x => x.Longitude)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Longitude is required")

            // В диапазоне
            .InclusiveBetween(-180, 180)

            // С сообщением
            .WithMessage("Longitude must be between -180 and 180 degrees");
    }
}