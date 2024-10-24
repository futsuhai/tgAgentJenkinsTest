using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;

namespace TgInstanceAgent.Application.Abstractions.WebhookSender;

/// <summary>
/// Интерфейс сервиса отправки веб-хуков.
/// </summary>
public interface IWebhookSender
{
    /// <summary>
    /// Отправляет событие в очередь для последующей отправки веб-хука.
    /// </summary>
    /// <param name="event">Событие, которое будет отправлено.</param>
    /// <param name="url">URL-адрес, по которому будет отправлен веб-хук.</param>
    /// <param name="routing">Имя очереди.</param>
    void PushToQueue(TgEvent @event, Uri url, string routing);

    /// <summary>
    /// Начинает обработку сообщений из очереди.
    /// </summary>
    void Start();

    /// <summary>
    /// Завершает обработку сообщений из очереди.
    /// </summary>
    void Stop();
}