using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SearchPublicChatsInputModel.
/// </summary>
public class SearchPublicChatsInputModelValidator : AbstractValidator< SearchPublicChatsInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SearchPublicChatsInputModelValidator()
    {
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