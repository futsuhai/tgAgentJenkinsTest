using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для SendContactMessageInputModel
/// </summary>
public class SendContactMessageInputModelValidator : AbstractValidator<SendContactMessageInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SendContactMessageInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputSendOptionValidator());

        // Правило для FirstName
        RuleFor(x => x.FirstName)

            // Максимальная длинна
            .MaximumLength(255)

            // С сообщением
            .WithMessage("First name length cannot exceed 255 characters");

        // Правило для LastName
        RuleFor(x => x.LastName)

            // Максимальная длинна
            .MaximumLength(255)

            // С сообщением
            .WithMessage("Last name length cannot exceed 255 characters");

        // Правило для ContactPhoneNumber
        RuleFor(x => x.ContactPhoneNumber)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Phone number is required")

            // Соответствует регулярному выражению
            .Matches(@"^\+?[1-9][0-9]{7,14}$")

            // С сообщением
            .WithMessage("Phone number must have the correct format");

        // Правило для VCard
        RuleFor(x => x.VCard)

            // Должно соблюдать
            .Must(VCardLength!)

            // Когда VCard не пустое
            .When(x => !string.IsNullOrWhiteSpace(x.VCard))

            // С сообщением
            .WithMessage("VCard length cannot exceed 2048 bytes");
    }

    /// <summary>
    /// Метод проверяет, является ли строка меньше 2048 байт.
    /// </summary>
    /// <param name="info">Информация о контакте</param>
    /// <returns>Флаг о размере строки</returns>
    private static bool VCardLength(string info) => System.Text.Encoding.Unicode.GetByteCount(info) < 2048;
}