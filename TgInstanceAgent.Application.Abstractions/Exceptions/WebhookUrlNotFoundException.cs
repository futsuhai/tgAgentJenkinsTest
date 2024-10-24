namespace TgInstanceAgent.Application.Abstractions.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда не удается найти URL вебхука.
/// </summary>
public class WebhookUrlNotFoundException() : Exception("Webhook url not found");