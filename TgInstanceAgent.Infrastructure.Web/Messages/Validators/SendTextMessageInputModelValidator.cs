using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для SendTextMessageInputModel
/// </summary>
public class SendTextMessageInputModelValidator : AbstractValidator<SendTextMessageInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SendTextMessageInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputSendOptionValidator());

        // Правило для Message
        RuleFor(x => x.Message)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Message is required")

            // Максимальная длинна
            .MaximumLength(4096)

            // С сообщением
            .WithMessage("Message length cannot exceed 4096 characters");

        // Правило для UrlPreview
        RuleFor(x => x.UrlPreview)

            // Должно соблюдать
            .Must(BeAValidUrl!)

            // Когда UrlPreview не пустое
            .When(x => !string.IsNullOrWhiteSpace(x.UrlPreview))

            // С сообщением
            .WithMessage("Url preview must be a valid URL");
    }

    /// <summary>
    /// Метод проверяет, является ли переданный в качестве параметра url валидным URL-адресом.
    /// </summary>
    /// <param name="url">URL Адрес</param>
    /// <returns>Флаг корректности URL</returns>
    private static bool BeAValidUrl(string url)
    {
        // Создаем объект Uri из переданного url-адреса, указывая, что адрес должен быть абсолютным.
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri)) return false;

        // Проверяем, что схема url-адреса соответствует http или https.
        return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
    }
}