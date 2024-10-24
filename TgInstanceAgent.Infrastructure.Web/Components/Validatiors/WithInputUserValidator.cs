using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

/// <summary>
/// Валидатор для классов, реализующих интерфейс IWithInputUser
/// </summary>
public class WithInputUserValidator : AbstractValidator<IWithInputUser>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public WithInputUserValidator()
    {
        // Правило для UserId
        RuleFor(x => x.UserId)

            // Не пустое
            .NotNull()

            // Когда не пустое Username и PhoneNumber
            .When(x => string.IsNullOrWhiteSpace(x.Username) && string.IsNullOrWhiteSpace(x.PhoneNumber))

            // С сообщением
            .WithMessage("You must specify one of the identification methods: chat Id, username or phone number");

        // Правило для Username
        RuleFor(x => x.Username)

            // Не пустое
            .NotEmpty()

            // Когда не пустое UserId и PhoneNumber
            .When(x => x.UserId == null && string.IsNullOrWhiteSpace(x.PhoneNumber))

            // С сообщением
            .WithMessage("You must specify one of the identification methods: chat Id, username or phone number");

        // Когда  пустое UserId и Username
        When(x => x.UserId == null && string.IsNullOrWhiteSpace(x.Username), () =>
        {
            // Правило для PhoneNumber
            RuleFor(x => x.PhoneNumber)
            
            // Не пустое
            .NotEmpty()
            
            // С сообщением
            .WithMessage("You must specify one of the identification methods: chat Id, username or phone number")
            
            // Соответствует регулярному выражению
            .Matches(@"^\+?[1-9][0-9]{7,14}$")
            
            // С сообщением
            .WithMessage("Phone number must have the correct format");
        });
    }
}