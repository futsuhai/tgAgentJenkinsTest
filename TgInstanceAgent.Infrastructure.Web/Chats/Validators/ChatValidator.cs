using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для ChatInputModel
/// </summary>
public class ChatValidator : AbstractValidator<ChatInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public ChatValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
    }
}