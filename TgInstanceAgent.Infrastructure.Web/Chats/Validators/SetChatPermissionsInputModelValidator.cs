using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Chats.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Chats.Validators;

/// <summary>
/// Валидатор для SetChatPermissionsInputModel.
/// </summary>
public class SetChatPermissionsInputModelValidator : AbstractValidator<SetChatPermissionsInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetChatPermissionsInputModelValidator()
    {
        // Правило для ChatId
        RuleFor(x => x.ChatId)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("ChatId is required");
        
        
        // Правило для CanSendBasicMessages
        RuleFor(x => x.CanSendBasicMessages)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanSendBasicMessages is required");

        // Правило для CanSendAudios
        RuleFor(x => x.CanSendAudios)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanSendAudios is required");

        // Правило для CanSendDocuments
        RuleFor(x => x.CanSendDocuments)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanSendDocuments is required");

        // Правило для CanSendPhotos
        RuleFor(x => x.CanSendPhotos)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanSendPhotos is required");

        // Правило для CanSendVideos
        RuleFor(x => x.CanSendVideos)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanSendVideos is required");

        // Правило для CanSendVideoNotes
        RuleFor(x => x.CanSendVideoNotes)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanSendVideoNotes is required");

        // Правило для CanSendVoiceNotes
        RuleFor(x => x.CanSendVoiceNotes)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanSendVoiceNotes is required");

        // Правило для CanSendPolls
        RuleFor(x => x.CanSendPolls)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanSendPolls is required");

        // Правило для CanSendOtherMessages
        RuleFor(x => x.CanSendOtherMessages)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanSendOtherMessages is required");

        // Правило для CanAddWebPagePreviews
        RuleFor(x => x.CanAddWebPagePreviews)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanAddWebPagePreviews is required");

        // Правило для CanChangeInfo
        RuleFor(x => x.CanChangeInfo)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanChangeInfo is required");

        // Правило для CanInviteUsers
        RuleFor(x => x.CanInviteUsers)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanInviteUsers is required");

        // Правило для CanPinMessages
        RuleFor(x => x.CanPinMessages)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanPinMessages is required");

        // Правило для CanCreateTopics
        RuleFor(x => x.CanCreateTopics)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("CanCreateTopics is required");
    }
}