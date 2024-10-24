namespace TgInstanceAgent.Infrastructure.Webhook.Structs;

/// <summary>
/// Сообщение веб-хука.
/// </summary>
public class WebhookMessage
{
    /// <summary>
    /// Событие для отправки.
    /// </summary>
    public required string Event { get; init; }

    /// <summary>
    /// URL-адрес для отправки сообщения.
    /// </summary>
    public required Uri Url { get; init; }
}

