namespace TgInstanceAgent.Infrastructure.Web.Authentication.ViewModels;

/// <summary>
/// Модель представления для входа с паролем.
/// Наследуется от базовой модели аутентификации AuthViewViewModel.
/// </summary>
public class PasswordViewModel : AuthenticationViewModel
{
    /// <summary>
    /// Подсказка для пароля.
    /// </summary>
    public string? Hint { get; init; }
}
