using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SearchChatsInputModel.
/// </summary>
public class SearchChatsInputModelValidator : AbstractValidator<SearchChatsInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SearchChatsInputModelValidator()
    {
        // Правило для Limit
        RuleFor(x => x.Limit)

            // Максимальное значение
            .LessThanOrEqualTo(100)

            // С сообщением
            .WithMessage("Максимальное значение для Limit 100");
        
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