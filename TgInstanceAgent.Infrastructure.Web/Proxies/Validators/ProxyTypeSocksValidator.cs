using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Proxies.InputModels.Components;

namespace TgInstanceAgent.Infrastructure.Web.Proxies.Validators;

/// <summary>
/// Валидатор для ProxyTypeSocksInputModel
/// </summary>
public class ProxyTypeSocksValidator : AbstractValidator<ProxyTypeSocksInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public ProxyTypeSocksValidator()
    {
        // Когда указано имя пользователя или пароль
        When(m => !string.IsNullOrEmpty(m.Username) || !string.IsNullOrEmpty(m.Password), () =>
        {
            // Правило для Username
            RuleFor(x => x.Username)

                // Не должно быть пустым
                .NotEmpty()

                // С сообщением
                .WithMessage("Username should not be empty when Password is specified")

                // Максимальная длина 255 символов
                .MaximumLength(255)

                // С сообщением
                .WithMessage("Username should not exceed 255 characters in length");

            // Правило для Password
            RuleFor(x => x.Password)

                // Не должен быть пустым
                .NotEmpty()

                // С сообщением
                .WithMessage("Password should not be empty when Username is specified")

                // Максимальная длина 255 символов
                .MaximumLength(255)

                // С сообщением
                .WithMessage("Password should not exceed 255 characters in length");
        });
    }
}