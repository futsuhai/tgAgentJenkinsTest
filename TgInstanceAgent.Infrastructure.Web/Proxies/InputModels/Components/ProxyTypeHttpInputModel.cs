namespace TgInstanceAgent.Infrastructure.Web.Proxies.InputModels.Components;

/// <summary>
/// Модель входных данных для типа прокси Http
/// </summary>
public class ProxyTypeHttpInputModel
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Username { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; set; }
    
    /// <summary>
    /// Истинно, если прокси-сервер поддерживает только HTTP-запросы и не поддерживает прозрачные TCP-соединения через метод HTTP CONNECT.
    /// </summary>
    public bool HttpOnly { get; set; }
}