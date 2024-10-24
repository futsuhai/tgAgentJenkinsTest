using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;

namespace TgInstanceAgent.Infrastructure.Web.Authentication.ViewModels;

/// <summary>
/// Модель представления для прошедшего аутентификацию пользователя.
/// Наследуется от базовой модели аутентификации AuthenticationViewModel.
/// </summary>
public class AuthenticatedViewModel : AuthenticationViewModel
{
    /// <summary>
    /// Пользователь Telegram, прошедший аутентификацию.
    /// </summary>
    public required TgUser User { get; init; }
}