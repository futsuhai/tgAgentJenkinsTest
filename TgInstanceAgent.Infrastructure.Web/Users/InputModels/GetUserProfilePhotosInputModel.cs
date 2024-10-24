using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Users.InputModels;

/// <summary>
/// Модель входных данных для получения фотографий пользователя.
/// </summary>
public class GetUserProfilePhotosInputModel : IWithInputUser
{
    /// <summary>
    /// Данные чата.
    /// </summary>
    public long? UserId { get; init; }

    /// <summary>
    /// Имя пользователя чата. Если установлено, Chat может быть не задан.
    /// </summary>
    public string? Username { get; init; }

    /// <summary>
    /// Номер телефона пользователя. Если установлено, Chat и Username могут быть не заданы.
    /// </summary>
    public string? PhoneNumber { get; init; }
    
    /// <summary>
    /// Смещение (Пример - У пользователя 3 фотки, смещение 2 -> получаем 1 фотку).
    /// </summary>
    public int Offset { get; init; }

    /// <summary>
    /// Число необходимых фотографий.
    /// </summary>
    public int Limit { get; init; } = 1;
}