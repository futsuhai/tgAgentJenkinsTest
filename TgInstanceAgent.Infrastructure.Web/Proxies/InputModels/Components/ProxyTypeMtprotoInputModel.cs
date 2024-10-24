namespace TgInstanceAgent.Infrastructure.Web.Proxies.InputModels.Components;

/// <summary>
/// Модель входных данных для типа прокси Mtproto
/// </summary>
public class ProxyTypeMtprotoInputModel
{
    /// <summary>
    /// Секрет прокси в шестнадцатеричной кодировке
    /// </summary>
    public string? Secret { get; init; }
}