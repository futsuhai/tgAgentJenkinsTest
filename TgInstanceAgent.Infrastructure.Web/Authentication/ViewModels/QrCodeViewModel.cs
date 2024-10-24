namespace TgInstanceAgent.Infrastructure.Web.Authentication.ViewModels;

/// <summary>
/// Модель представления для входа с QR-кодом.
/// Наследуется от базовой модели аутентификации AuthenticationViewModel.
/// </summary>
public class QrCodeViewModel : AuthenticationViewModel
{
    /// <summary>
    /// QR-код для входа.
    /// </summary>
    public required string Code { get; init; }
}
