using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Proxies.InputModels.Components;

namespace TgInstanceAgent.Infrastructure.Web.Proxies.Validators;

/// <summary>
/// Валидатор для ProxyTypeMtprotoInputModel
/// </summary>
public class ProxyTypeMtprotoValidator : AbstractValidator<ProxyTypeMtprotoInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public ProxyTypeMtprotoValidator()
    {
        // Правило для Secret
        RuleFor(x => x.Secret)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Secret is required")

            // Длина 32 символа
            .Length(32, 32)
        
            // С сообщением
            .WithMessage("Secret should have a length of 32 characters")

            // Только шестнадцатеричные символы
            .Matches("[0-9A-Fa-f]+")

            // С сообщением
            .WithMessage("Secret should contain only hexadecimal characters");
    }
}