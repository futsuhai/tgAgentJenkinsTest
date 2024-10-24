using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Proxies.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Proxies.Validators;

/// <summary>
/// Валидатор для SetProxyInputModel
/// </summary>
public class SetProxyValidator : AbstractValidator<SetProxyInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetProxyValidator()
    {
        // Правило для Server
        RuleFor(x => x.Server)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Server address is required");

        // Правило для Port
        RuleFor(x => x.Port)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Port is required");

        // Правило для ExpirationTimeUtc
        RuleFor(x => x.ExpirationTimeUtc)

            // Больше чем UtcNow
            .GreaterThan(DateTime.UtcNow)

            // С сообщением
            .WithMessage("Expiration time must be greater than the current UTC time");

        // Когда ProxyTypeHttpInputModel и ProxyTypeSocksInputModel типы не указаны
        When(x => x.ProxyTypeHttpInputModel == null && x.ProxyTypeSocksInputModel == null, () =>
        {
            // Правило для ProxyTypeMtprotoInputModel
            RuleFor(x => x.ProxyTypeMtprotoInputModel)

                // Не должно быть пустым, если другие поля типы прокси не заполнены
                .NotEmpty()

                // С сообщением
                .WithMessage("You need to specify one of the possible proxy type representations")

                // Устанавливаем валидатор для ProxyTypeMtprotoInputModel
                .SetValidator(new ProxyTypeMtprotoValidator()!);
        });

        // Когда ProxyTypeMtprotoInputModel и ProxyTypeSocksInputModel типы не указаны
        When(x => x.ProxyTypeMtprotoInputModel == null && x.ProxyTypeSocksInputModel == null, () =>
        {
            // Правило для ProxyTypeHttpInputModel
            RuleFor(x => x.ProxyTypeHttpInputModel)

                // Не должно быть пустым, если другие поля типы прокси не заполнены
                .NotEmpty()

                // С сообщением
                .WithMessage("You need to specify one of the possible proxy type representations")

                // Устанавливаем валидатор для ProxyTypeHttpInputModel
                .SetValidator(new ProxyTypeHttpValidator()!);
        });

        // Когда ProxyTypeHttpInputModel и ProxyTypeMtprotoInputModel типы не указаны
        When(x => x.ProxyTypeHttpInputModel == null && x.ProxyTypeMtprotoInputModel == null, () =>
        {

            // Правило для ProxyTypeSocksInputModel
            RuleFor(x => x.ProxyTypeSocksInputModel)

                // Не должно быть пустым, если другие поля типы прокси не заполнены
                .NotEmpty()

                // С сообщением
                .WithMessage("You need to specify one of the possible proxy type representations")

                // Устанавливаем валидатор для ProxyTypeSocksInputModel
                .SetValidator(new ProxyTypeSocksValidator()!);
        });
    }
}