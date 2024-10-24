using FluentValidation;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для GetChatsInputModel.
/// </summary>
public class GetChatsValidator : AbstractValidator<GetChatsInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public GetChatsValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputLimitValidator());

        // Правило для ChatFolderId
        RuleFor(c => c.ChatFolderId)

            // Должно иметь значение
            .Must(c => c.HasValue)
            
            // Когда тип списка - папка
            .When(c => c.List == TgChatList.Folder)
            
            // С сообщением
            .WithMessage("ChatFolderId must be set when list is Folder");
    }
}