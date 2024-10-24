namespace TgInstanceAgent.Infrastructure.Web.Profile.InputModels;

/// <summary>
/// Модель входных данных для установки био пользователя
/// </summary>
public class SetUserBioInputModel
{
    /// <summary>
    /// Строка био
    /// </summary>
    public string? Bio { get; init; }
}