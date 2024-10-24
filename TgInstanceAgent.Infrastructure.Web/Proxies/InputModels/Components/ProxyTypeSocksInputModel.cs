namespace TgInstanceAgent.Infrastructure.Web.Proxies.InputModels.Components;

/// <summary>
/// Модель входных данных для типа прокси Socks
/// </summary>
public class ProxyTypeSocksInputModel
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Username { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; set; }
}