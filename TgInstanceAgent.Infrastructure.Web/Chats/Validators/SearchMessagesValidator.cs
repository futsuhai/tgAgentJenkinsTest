using FluentValidation;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SearchMessagesInputModel.
/// </summary>
public class SearchMessagesValidator : AbstractValidator<SearchMessagesInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SearchMessagesValidator()
    {
        // Правило для Limit
        RuleFor(x => x.Limit)

            // Максимальное значение
            .LessThanOrEqualTo(200)

            // С сообщением
            .WithMessage("Limit cannot exceed 500");
        
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

        // Правило для List
        RuleFor(x => x.List)

            // Больше 0
            .Must(l => l != TgChatList.Folder)

            // С сообщением
            .WithMessage("Поиск по папкам не поддерживается");


        // Правило для MinDate
        RuleFor(x => x.MinDate)

            // Меньше или равно максимальной дате
            .LessThanOrEqualTo(m=>m.MaxDate)
            
            // Когда
            .When(m => m.MaxDate != null)

            // С сообщением
            .WithMessage("Минимальная дата должна быть меньше или равна максимальной дате");
        
        // Правило для Filter
        RuleFor(x => x.Filter)
            
            // Не пустое
            .NotEmpty()
            
            // С сообщением
            .WithMessage("Тип фильтра обязателен для заполнения.");
    }
}