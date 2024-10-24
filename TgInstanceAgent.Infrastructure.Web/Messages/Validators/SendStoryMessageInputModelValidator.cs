using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Messages.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Messages.Validators;

/// <summary>
/// Валидатор для SendStoryMessageInputModel
/// </summary>
public class SendStoryMessageInputModelValidator : AbstractValidator<SendStoryMessageInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SendStoryMessageInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputSendOptionValidator());

        // Правило для ChatId
        RuleFor(x => x.StoryId)

            // Минимальное значение
            .GreaterThan(0)

            // С сообщением
            .WithMessage("Story Id cannot be less than 0");

        // Правило для ChatId
        RuleFor(x => x.StorySenderChatId)

            // Минимальное значение
            .GreaterThan(0)

            // С сообщением
            .WithMessage("StorySenderChat Id cannot be less than 0");
    }
}