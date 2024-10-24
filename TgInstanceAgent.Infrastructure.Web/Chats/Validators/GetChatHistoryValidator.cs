using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для GetChatHistoryInputModel.
/// </summary>
public class GetChatHistoryValidator : AbstractValidator<GetChatHistoryInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public GetChatHistoryValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputLimitValidator());
    }
}