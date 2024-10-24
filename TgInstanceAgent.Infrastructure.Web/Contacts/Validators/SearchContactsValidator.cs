using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Contacts.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Contacts.Validators;

/// <summary>
/// Валидатор для SearchContactsInputModel.
/// </summary>
public class SearchContactsValidator : AbstractValidator<SearchContactsInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SearchContactsValidator()
    {
        // Правило для Limit
        RuleFor(x => x.Limit)

            // Максимальное значение
            .LessThanOrEqualTo(500)

            // С сообщением
            .WithMessage("Максимальное значение для Limit 500");
        
        // Правило для Query
        RuleFor(x => x.Query)
            
            // Не пусто
            .NotEmpty()
            
            // С сообщением
            .WithMessage("Query не должно быть пустым")
            
            // Максимальная длина
            .MaximumLength(256)
            
            // С сообщением
            .WithMessage("Максимальная длина Query 256 символов");
    }
}