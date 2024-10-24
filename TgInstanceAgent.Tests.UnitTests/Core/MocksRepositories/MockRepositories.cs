using Microsoft.Extensions.Logging;
using Moq;
using TgInstanceAgent.Domain.Abstractions.Repositories;

namespace TgInstanceAgent.Tests.UnitTests.Core.MocksRepositories;

/// <summary>
/// Класс MockRepositories содержит моки (Mock-объекты) для различных репозиториев, используемых в тестах.
/// Этот класс помогает изолировать тестируемую логику от зависимостей на уровне данных, предоставляя управляемые моки.
/// </summary>
public class MockRepositories
{
    /// <summary>
    /// Мок объекта IInstanceRepository, используемого для работы с инстансами.
    /// </summary>
    public Mock<IInstanceRepository> InstanceRepositoryMock { get; }
    
    /// <summary>
    /// Мок объекта ISystemProxyRepository, используемого для работы с системными прокси.
    /// </summary>
    public Mock<ISystemProxyRepository> SystemProxyRepositoryMock { get; }
    
    /// <summary>
    /// Мок объекта IReportRepository, используемого для работы с отчётами о сообщениях.
    /// </summary>
    public Mock<IReportRepository> ReportRepositoryMock { get; }

    /// <summary>
    /// Метод CreateLoggerMock создает мок объекта ILogger для заданного типа T.
    /// Этот метод упрощает создание моков логгера для различных классов, используемых в тестах.
    /// </summary>
    /// <typeparam name="T">Тип класса, для которого создается мок логгера.</typeparam>
    /// <returns>Возвращает мок логгера</returns>
    public static Mock<ILogger<T>> CreateLoggerMock<T>() => new Mock<ILogger<T>>();

    /// <summary>
    /// Конструктор класса MockRepositories инициализирует моки репозиториев.
    /// </summary>
    public MockRepositories()
    {
        // Инициализация мока репозитория инстансов
        InstanceRepositoryMock = new Mock<IInstanceRepository>();
        
        // Инициализация мока репозитория системных прокси
        SystemProxyRepositoryMock = new Mock<ISystemProxyRepository>();
        
        // Инициализация мока репозитория отчётов о сообщениях
        ReportRepositoryMock = new Mock<IReportRepository>();
    }
}