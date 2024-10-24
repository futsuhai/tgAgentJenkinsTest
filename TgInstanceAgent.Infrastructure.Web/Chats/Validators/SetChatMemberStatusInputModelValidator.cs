using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatMemberStatusInputModel.
/// </summary>
public class SetChatMemberStatusInputModelValidator : AbstractValidator<SetChatMemberStatusInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public SetChatMemberStatusInputModelValidator()
    {
        // Правило для ChatId
        RuleFor(x => x.ChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ChatId is required");
        
        // Правило для SenderChatId
        RuleFor(x => x.SenderChatId)
            
            // Отрицательное
            .LessThan(0)
            
            // С сообщением
            .WithMessage("SenderChatId must be less than or equal to 0.");

        
        // Правило для SenderUserId
        RuleFor(x => x.SenderUserId)
            
            // Больше нуля
            .GreaterThan(0)
            
            // С сообщением
            .WithMessage("SenderUserId must be greater than 0.");
        
        // Правило для взаимоисключения
        RuleFor(x => x)
            
            // Взаимоисключаем значения
            .Must(x => (x.SenderChatId.HasValue && !x.SenderUserId.HasValue) || (!x.SenderChatId.HasValue && x.SenderUserId.HasValue))
            
            // С сообщением
            .WithMessage("Either SenderChatId or SenderUserId must be specified, but not both.");
    }
}