namespace TgInstanceAgent.Infrastructure.Web.Authentication.ViewModels;

/// <summary>
/// Базовая модель представления для аутентификации.
/// </summary>
public abstract class AuthenticationViewModel
{
    /// <summary>
    /// Возвращает имя типа модели без суффикса "ViewModel".
    /// </summary>
    public string Type => GetType().Name.Replace("ViewModel", "");
}
