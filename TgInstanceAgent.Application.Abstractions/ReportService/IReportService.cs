using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;

namespace TgInstanceAgent.Application.Abstractions.ReportService;

/// <summary>
/// Интерфейс сервиса отчётов отправки и получения сообщений
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Метод для добавления в отчет сообщения.
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="message">Сообщение</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>Новое сообщение, если прошлое было переслано</returns>
    Task ProcessMessage(Guid instanceId, TgMessage message, CancellationToken token = default);
}