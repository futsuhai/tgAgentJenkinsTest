using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Instances.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Instances.Validators;

/// <summary>
/// Валидатор для UrlInputModel
/// </summary>
public class UrlValidator : AbstractValidator<UrlInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public UrlValidator()
    {
        // Правило для Url
        RuleFor(x => x.Url)
            
            // Не пустой
            .NotEmpty()
            
            // Сообщение об ошибке, если Url пустой
            .WithMessage("URL не должен быть пустым.")
            
            // Проверка корректности формата URL
            .Must(BeAValidUrl)
            
            // Сообщение об ошибке, если URL некорректен
            .WithMessage("Указанный URL имеет некорректный формат.");
    }

    /// <summary>
    /// Проверка корректности формата URL
    /// </summary>
    /// <param name="url">URL-адрес</param>
    /// <returns>Истина, если URL валиден, иначе ложь</returns>
    private static bool BeAValidUrl(string url)
    {
        // Проверка формата URL с помощью Uri.TryCreate
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}