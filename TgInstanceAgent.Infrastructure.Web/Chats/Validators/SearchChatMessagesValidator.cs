using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SearchChatMessagesInputModel.
/// </summary>
public class SearchChatMessagesValidator : AbstractValidator<SearchChatMessagesInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SearchChatMessagesValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputLimitValidator());
        
        // Правило для Query
        RuleFor(x => x.Query)
            
            // Максимальная длина
            .MaximumLength(256)
            
            // С сообщением
            .WithMessage("Максимальная длина Query 256 символов");
        
        // Правило для SenderChatId
        RuleFor(x => x.SenderChatId)
            
            // Больше 0
            .GreaterThan(0)
            
            // С сообщением
            .WithMessage("Идентификатор чата должен быть больше 0.");

        
        // Правило для SenderUserId
        RuleFor(x => x.SenderUserId)
            
            // Больше 0
            .GreaterThan(0)
            
            // С сообщением
            .WithMessage("Идентификатор пользователя должен быть больше 0.");

        
        // Правило для FromMessageId
        RuleFor(x => x.FromMessageId)
            
            // Больше 0
            .GreaterThan(0)
            
            // С сообщением
            .WithMessage("Идентификатор сообщения должен быть больше 0.");

        // Правило для MessageThreadId
        RuleFor(x => x.MessageThreadId)
            
            // Больше 0
            .GreaterThan(0)
            
            // С сообщением
            .WithMessage("Идентификатор потока сообщений должен быть больше 0.");

        // Правило для SavedMessagesTopicId
        RuleFor(x => x.SavedMessagesTopicId)
            
            // Больше 0
            .GreaterThan(0)
            
            // С сообщением
            .WithMessage("Идентификатор темы сохраненных сообщений должен быть больше 0.");
        
        // Правило для Filter
        RuleFor(x => x.Filter)
            
            // Не пустое
            .NotEmpty()
            
            // С сообщением
            .WithMessage("Тип фильтра обязателен для заполнения.");
    }
}