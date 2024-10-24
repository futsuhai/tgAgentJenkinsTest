namespace TgInstanceAgent.Infrastructure.Web.Authentication.InputModels;

/// <summary>
/// Модель входных данных для проверки пароля
/// </summary>
public class CheckPasswordInputModel
{
    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; init; }
}