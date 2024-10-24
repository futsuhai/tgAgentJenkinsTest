using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

/// <summary>
/// Валидатор для классов, реализующих интерфейс IWithInputChat
/// </summary>
public class WithInputChatValidator : AbstractValidator<IWithInputChat>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public WithInputChatValidator()
    {
        
        // Когда пустое Username и PhoneNumber
        When(x => x.Username == null && x.PhoneNumber == null, () =>
        {
            // Правило для ChatId
            RuleFor(x => x.ChatId)

                // Не пустое
                .NotEmpty()

                // С сообщением
                .WithMessage("You must specify one of the identification methods: chat Id, username or phone number");
        });
        
        // Когда пустое ChatId и PhoneNumber
        When(x => x.ChatId == null && x.PhoneNumber == null, () =>
        {
            // Правило для Username
            RuleFor(x => x.Username)

                // Не пустое
                .NotEmpty()

                // С сообщением
                .WithMessage("You must specify one of the identification methods: chat Id, username or phone number");
        });
            
        // Когда пустое ChatId и Username
        When(x => x.ChatId == null && string.IsNullOrWhiteSpace(x.Username), () =>
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