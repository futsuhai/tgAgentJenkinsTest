using TgInstanceAgent.Infrastructure.Web.Authentication.Hubs;
using TgInstanceAgent.Infrastructure.Web.Events.Hubs;

namespace TgInstanceAgent.Start.Extensions;

/// <summary>
/// Маппинг хабов к URL-ам
/// </summary>
public static class HubMapping
{
    /// <summary>
    /// Маппинг хабов к URL-ам
    /// </summary>
    public static void MapHubs(this WebApplication app)
    {
        // Привязка вспомогательного хаба SignalR к URL-адресу
        app.MapHub<QrAuthenticationHub>("apiTg/Authenticate");

        // Привязка хаба SignalR к URL-адресу
        app.MapHub<EventHub>("apiTg/StartReceive");
    }
}