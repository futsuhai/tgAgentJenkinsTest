using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.ReportService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Reports;

namespace TgInstanceAgent.Application.Services.ReportService;

/// <summary>
/// Сервис отчётов отправки и получения сообщений
/// </summary>
public class ReportService(IReportRepository reportRepository, IMemoryCache cache) : IReportService, IDisposable
{
    /// <summary>
    /// ConcurrentDictionary для хранения семафоров для каждого инстанса.
    /// </summary>
    private readonly ConcurrentDictionary<Guid, SemaphoreSlim> _semaphores = new();

    /// <summary>
    /// Метод учета полученных и отправленных сообщений в отчете
    /// </summary>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="message">Сообщение</param>
    /// <param name="token">Токен отмены</param>
    public async Task ProcessMessage(Guid instanceId, TgMessage message, CancellationToken token = default)
    {
        // Если это сообщение из комментариев или если это пост в чате - не учитываем
        if (message.MessageThreadId.HasValue || message.IsChannelPost || message.IsTopicMessage) return;

        // Получаем или создаем семафор для текущего инстанса
        var semaphore = _semaphores.GetOrAdd(instanceId, _ => new SemaphoreSlim(1, 1));

        // Ждем захвата семафора
        await semaphore.WaitAsync(token);

        try
        {
            // Получаем дату сообщения
            var date = DateOnly.FromDateTime(message.Date);
            
            // Получаем отчёт из кэша
            var report = await cache.GetReportAsync(instanceId, date, reportRepository);
            
            // По умолчанию устанавливаем, что отчёт найден
            var reportFound = true;

            // Если отчёт не найден
            if (report == null)
            {
                // Создаем новый отчёт
                report = new ReportAggregate(Guid.NewGuid()) { Date = date, InstanceId = instanceId };
                
                // Устанавливаем, что отчёт не найден
                reportFound = false;
            }

            // Если это отправленное сообщение.
            if (message.IsOutgoing) report.AddSent();

            // Если это полученное сообщение.
            else report.AddReceived();

            // Сохраняем или добавляем отчёт
            await (reportFound
                ? reportRepository.UpdateAsync(report)
                : reportRepository.AddAsync(report, token));
        }
        finally
        {
            // Освобождаем семафор
            semaphore.Release();
        }
    }

    /// <summary>
    /// Освобождает ресурсы семафоров
    /// </summary>
    public void Dispose()
    {
        // Предотвращаем вызов деструктора
        GC.SuppressFinalize(this);

        // Освобождаем ресурсы семафоров
        foreach (var semaphore in _semaphores.Values)
        {
            semaphore.Dispose();
        }
    }
}