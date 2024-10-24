using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatNotificationSettingsInputModel.
/// </summary>
public class SetChatNotificationSettingsInputModelValidator : AbstractValidator<SetChatNotificationSettingsInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetChatNotificationSettingsInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputChatValidator());
        
        // Правило для UseDefaultMuteFor
        RuleFor(x => x.UseDefaultMuteFor)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("UseDefaultMuteFor is required");

        // Правило для MuteFor
        RuleFor(x => x.MuteFor)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("MuteFor is required");

        // Правило для UseDefaultSound
        RuleFor(x => x.UseDefaultSound)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("UseDefaultSound is required");

        // Правило для SoundId
        RuleFor(x => x.SoundId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("SoundId is required");

        // Правило для UseDefaultShowPreview
        RuleFor(x => x.UseDefaultShowPreview)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("UseDefaultShowPreview is required");

        // Правило для ShowPreview
        RuleFor(x => x.ShowPreview)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ShowPreview is required");

        // Правило для UseDefaultMuteStories
        RuleFor(x => x.UseDefaultMuteStories)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("UseDefaultMuteStories is required");

        // Правило для MuteStories
        RuleFor(x => x.MuteStories)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("MuteStories is required");

        // Правило для UseDefaultStorySound
        RuleFor(x => x.UseDefaultStorySound)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("UseDefaultStorySound is required");

        // Правило для StorySoundId
        RuleFor(x => x.StorySoundId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("StorySoundId is required");

        // Правило для UseDefaultShowStorySender
        RuleFor(x => x.UseDefaultShowStorySender)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("UseDefaultShowStorySender is required");

        // Правило для ShowStorySender
        RuleFor(x => x.ShowStorySender)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ShowStorySender is required");

        // Правило для UseDefaultDisablePinnedMessageNotifications
        RuleFor(x => x.UseDefaultDisablePinnedMessageNotifications)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("UseDefaultDisablePinnedMessageNotifications is required");

        // Правило для DisablePinnedMessageNotifications
        RuleFor(x => x.DisablePinnedMessageNotifications)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("DisablePinnedMessageNotifications is required");

        // Правило для UseDefaultDisableMentionNotifications
        RuleFor(x => x.UseDefaultDisableMentionNotifications)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("UseDefaultDisableMentionNotifications is required");

        // Правило для DisableMentionNotifications
        RuleFor(x => x.DisableMentionNotifications)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("DisableMentionNotifications is required");
    }
}