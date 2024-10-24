namespace TgInstanceAgent.Infrastructure.Web.Authentication.InputModels;

/// <summary>
/// Модель входных данных для проверки кода подтверждения
/// </summary>
public class CheckCodeInputModel
{
    /// <summary>
    /// Код подтверждения.
    /// </summary>
    public string? Code { get; init; }
}