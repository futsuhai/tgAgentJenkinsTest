using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Domain.Reports;
using TgInstanceAgent.Domain.Reports.Specifications;

namespace TgInstanceAgent.Application.Services.Extensions;

/// <summary>
/// Расширение для работы с кэшем.
/// </summary>
public static class CacheService
{
    /// <summary>
    /// Строка для формирования ключа инстанса в кэше
    /// </summary>
    private const string InstanceFormat = "instance_{0}";
    
    /// <summary>
    /// Строка для формирования ключа отчёта в кэше
    /// </summary>
    private const string ReportFormat = "report_{0}_{1}";
    
    /// <summary>
    /// Метод для получения агрегата инстанса
    /// </summary>
    /// <param name="cache">Кэш приложения</param>
    /// <param name="id">Идентификатор инстанса</param>
    /// <param name="repository">Репозиторий инстансов</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Инстанс</returns>
    public static async Task<InstanceAggregate> GetInstanceAsync(this IMemoryCache cache, Guid id,
        IInstanceRepository repository, CancellationToken cancellationToken = default)
    {
        // Формируем ключ
        var key = string.Format(InstanceFormat, id);
        
        // Пытаемся получить инстанс из кэша и возвращаем его кэша
        if (cache.TryGetValue(key, out InstanceAggregate? instance)) return instance!;

        // Если инстанс не найден в кэше, получаем его из репозитория
        instance = await repository.GetAsync(id, cancellationToken);

        // Если найденный инстанс null вызываем исключение - инстанс не найден
        if (instance == null) throw new InstanceNotFoundException();

        // Устанавливаем этот инстанс в кэш
        cache.Set(key, instance, new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5) });

        // Возвращаем инстанс
        return instance;
    }

    /// <summary>
    /// Метод для получения агрегата инстанса
    /// </summary>
    /// <param name="cache">Кэш приложения</param>
    /// <param name="instanceId">Идентификатор инстанса</param>
    /// <param name="date">Дата отчета</param>
    /// <param name="repository">Репозиторий отчетов</param>
    /// <returns>Инстанс</returns>
    public static async Task<ReportAggregate?> GetReportAsync(this IMemoryCache cache, Guid instanceId, DateOnly date, IReportRepository repository)
    {
        // Формируем ключ
        var key = string.Format(ReportFormat, instanceId, date);
        
        // Пытаемся получить отчёт из кэша и возвращаем его кэша
        if (cache.TryGetValue(key, out ReportAggregate? report)) return report!;

        // Спецификация отчетов за текущий день.
        var dateSpecification = new DateReportSpecification(instanceId, date);
        
        // Если отчёт не найден в кэше, получаем его из репозитория
        var reports = await repository.FindAsync(dateSpecification);

        // Получаем первый отчет
        report = reports.FirstOrDefault();
        
        // Если отчёт не найден - возвращаем null
        if (report == null) return null;

        // Устанавливаем этот отчёт в кэш
        cache.Set(key, report, new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5) });

        // Возвращаем отчёт
        return report;
    }
}