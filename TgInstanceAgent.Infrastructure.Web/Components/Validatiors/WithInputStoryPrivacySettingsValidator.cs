using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

/// <summary>
/// Валидатор для StoryPrivacySettingsInputModel
/// </summary>
public class WithInputStoryPrivacySettingsValidator : AbstractValidator<IWithInputStoryPrivacySettings>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public WithInputStoryPrivacySettingsValidator()
    {
        // Правило для StoryPrivacySettingsContacts
        RuleFor(x => x.StoryPrivacySettingsContacts)

            // Не должно быть пустым, если другие поля настроек приватности истории не заполнены
            .NotEmpty()

            // Когда Everyone и SelectedUsers и CloseFriends настройки не указаны
            .When(x =>
                x.StoryPrivacySettingsEveryone == null &&
                x.StoryPrivacySettingsSelectedUsers == null &&
                x.StoryPrivacySettingsCloseFriends == null)

            // С сообщением
            .WithMessage(
                "You need to specify one of the possible story privacy settings representations")
            
            // Устанавливаем валидатор для StoryPrivacySettingsContacts
            .SetValidator(new StoryPrivacySettingsContactsValidator()!);
        
        // Правило для StoryPrivacySettingsSelectedUsers
        RuleFor(x => x.StoryPrivacySettingsSelectedUsers)

            // Не должно быть пустым, если другие поля настроек приватности истории не заполнены
            .NotEmpty()

            // Когда Everyone и Contacts и CloseFriends настройки не указаны
            .When(x =>
                x.StoryPrivacySettingsEveryone == null &&
                x.StoryPrivacySettingsContacts == null &&
                x.StoryPrivacySettingsCloseFriends == null)

            // С сообщением
            .WithMessage(
                "You need to specify one of the possible story privacy settings representations")
            
            // Устанавливаем валидатор для StoryPrivacySettingsSelectedUsers
            .SetValidator(new StoryPrivacySettingsSelectedUsersValidator()!);
        
        // Правило для StoryPrivacySettingsEveryone
        RuleFor(x => x.StoryPrivacySettingsEveryone)

            // Градиент не должен быть пустым, если другие поля фона не заполнены
            .NotEmpty()

            // Когда Contacts и SelectedUsers и CloseFriends настройки не указаны
            .When(x =>
                x.StoryPrivacySettingsContacts == null &&
                x.StoryPrivacySettingsSelectedUsers == null &&
                x.StoryPrivacySettingsCloseFriends == null)

            // С сообщением
            .WithMessage(
                "You need to specify one of the possible story privacy settings representations")
            
            // Устанавливаем валидатор для StoryPrivacySettingsEveryone
            .SetValidator(new StoryPrivacySettingsEveryoneValidator()!);
        
        // Правило для StoryPrivacySettingsCloseFriends
        RuleFor(x => x.StoryPrivacySettingsCloseFriends)

            // Не должно быть пустым, если другие поля настроек приватности истории не заполнены
            .NotEmpty()

            // Когда Everyone и Contacts и SelectedUsers настройки не указаны
            .When(x =>
                x.StoryPrivacySettingsEveryone == null &&
                x.StoryPrivacySettingsContacts == null &&
                x.StoryPrivacySettingsSelectedUsers == null)

            // С сообщением
            .WithMessage(
                "You need to specify one of the possible story privacy settings representations");
    }
}