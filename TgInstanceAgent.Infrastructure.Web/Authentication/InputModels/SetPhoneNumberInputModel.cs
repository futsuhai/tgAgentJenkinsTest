namespace TgInstanceAgent.Infrastructure.Web.Authentication.InputModels;

/// <summary>
/// Модель входных данных для установки номера телефона телеграм аккаунта
/// </summary>
public class SetPhoneNumberInputModel
{
    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? PhoneNumber { get; init; }
}