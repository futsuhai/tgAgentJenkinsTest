using Microsoft.Extensions.Caching.Memory;
using Moq;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.Proxies.ProxyClient;
using TgInstanceAgent.Application.Abstractions.Proxies.ProxyPurchasingService;
using TgInstanceAgent.Application.Abstractions.ReportService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Interfaces;
using TgInstanceAgent.Application.Abstractions.WebhookSender;
using TgInstanceAgent.Application.Services.InstancesService;
using TgInstanceAgent.Application.Services.ProxyServices;
using TgInstanceAgent.Application.Services.ReportService;
using TgInstanceAgent.Tests.UnitTests.Core.MocksRepositories;

namespace TgInstanceAgent.Tests.UnitTests.Core.MockServices;

/// <summary>
/// Класс MockServices содержит моки (Mock-объекты) для различных сервисов, используемых в тестах.
/// Этот класс помогает изолировать тестируемую логику от зависимостей, предоставляя управляемые моки.
/// </summary>
public class MockServices
{
    /// <summary>
    /// Мок объекта IProxyClient
    /// </summary>
    public Mock<IProxyClient> ProxyClientMock { get; }

    /// <summary>
    /// Мок объекта IMemoryCache, который используется для работы с кэшированием в сервисах.
    /// </summary>
    public Mock<IMemoryCache> MemoryCacheMock { get; }

    /// <summary>
    /// Мок объекта ITelegramClient.
    /// </summary>
    public Mock<ITelegramClient> TelegramClientMock { get; }

    /// <summary>
    /// Мок объекта IWhatsAppClientFactory
    /// </summary>
    public Mock<ITelegramClientFactory> TelegramClientFactoryMock { get; }

    /// <summary>
    /// Мок объекта IAutoForwardService
    /// </summary>
    public Mock<IAutoForwardService> AutoForwardServiceMock { get; }

    /// <summary>
    /// Мок объекта IWebhookService
    /// </summary>
    public Mock<IWebhookService> WebhookServiceMock { get; }

    /// <summary>
    /// Мок объекта IReportService
    /// </summary>
    public Mock<IReportService> ReportServiceMock { get; }

    /// <summary>
    /// Мок объекта IWebhookSender
    /// </summary>
    public Mock<IWebhookSender> WeebhookSenderMock { get; }

    /// <summary>
    /// Конструктор класса MockServices инициализирует моки сервисов.
    /// </summary>
    public MockServices()
    {
        // Инициализация мока IProxyClient
        ProxyClientMock = new Mock<IProxyClient>();

        // Инициализация мока IMemoryCache
        MemoryCacheMock = new Mock<IMemoryCache>();

        // Инициализация мока ITelegramClient
        TelegramClientMock = new Mock<ITelegramClient>();

        // Инициализация мока для IWhatsAppClientFactory
        TelegramClientFactoryMock = new Mock<ITelegramClientFactory>();

        // Инициализация мока для IAutoForwardService
        AutoForwardServiceMock = new Mock<IAutoForwardService>();

        // Инициализация мока для IWebhookService
        WebhookServiceMock = new Mock<IWebhookService>();

        // Инициализация мока для IReportService
        ReportServiceMock = new Mock<IReportService>();

        // Инициализация мока для IWebhookSender
        WeebhookSenderMock = new Mock<IWebhookSender>();
    }

    /// <summary>
    /// Метод CreateProxyPurchasingService создает экземпляр ProxyPurchasingService с использованием предоставленных репозиториев и сервисов.
    /// </summary>
    /// <param name="repositories">Экземпляр MockRepositories, содержащий моки репозиториев, используемых сервисом.</param>
    /// <param name="countryMock">Мок данных для страны, которая будет использоваться в тестируемом сервисе.</param>
    /// <param name="instancesCoefficientMock">Мок коэффициента количества инстансов, используемый в сервисе.</param>
    /// <returns>Возвращает экземпляр ProxyPurchasingService, готовый для использования в тестах.</returns>
    public IProxyPurchasingService CreateProxyPurchasingService(MockRepositories repositories, string countryMock,
        int instancesCoefficientMock)
    {
        // Создание и возврат экземпляра ProxyPurchasingService с использованием переданных зависимостей и моков
        return new ProxyPurchasingService(
            repositories.InstanceRepositoryMock.Object,
            repositories.SystemProxyRepositoryMock.Object,
            MockRepositories.CreateLoggerMock<IProxyPurchasingService>().Object,
            ProxyClientMock.Object,
            countryMock,
            instancesCoefficientMock);
    }

    /// <summary>
    /// Метод CreateReportService создает экземпляр ReportService с использованием предоставленных репозиториев и кэша.
    /// </summary>
    /// <param name="repositories">Экземпляр MockRepositories, содержащий моки репозиториев.</param>
    /// <returns>Возвращает экземпляр ReportService, готовый для использования в тестах.</returns>
    public IReportService CreateReportService(MockRepositories repositories)
    {
        // Создание и возврат экземпляра ReportService с использованием переданных зависимостей и моков
        return new ReportService(
            repositories.ReportRepositoryMock.Object,
            MemoryCacheMock.Object);
    }

    /// <summary>
    /// Метод CreateAutoForwardService создает экземпляр AutoForwardService с использованием предоставленных репозиториев и кэша.
    /// </summary>
    /// <param name="repositories">Экземпляр MockRepositories, содержащий моки репозиториев.</param>
    /// <returns>Возвращает экземпляр AutoForwardService, готовый для использования в тестах.</returns>
    public IAutoForwardService CreateAutoForwardService(MockRepositories repositories)
    {
        // Создание и возврат экземпляра AutoForwardService с использованием переданных зависимостей и моков
        return new AutoForwardService(
            MemoryCacheMock.Object,
            repositories.InstanceRepositoryMock.Object);
    }

    /// <summary>
    /// Метод CreateInstancesService создает экземпляр InstancesService с использованием предоставленных репозиториев и кэша.
    /// </summary>
    /// <param name="repositories">Экземпляр MockRepositories, содержащий моки репозиториев.</param>
    /// <returns>Возвращает экземпляр InstancesService, готовый для использования в тестах.</returns>
    public IInstancesService CreateInstancesService(MockRepositories repositories)
    {
        // Создание и возврат экземпляра InstancesService с использованием переданных зависимостей и моков
        return new InstancesService(
            repositories.InstanceRepositoryMock.Object,
            repositories.SystemProxyRepositoryMock.Object,
            MemoryCacheMock.Object,
            TelegramClientFactoryMock.Object,
            WebhookServiceMock.Object,
            AutoForwardServiceMock.Object,
            ReportServiceMock.Object,
            MockRepositories.CreateLoggerMock<IInstancesService>().Object,
            false);
    }

    /// <summary>
    /// Метод CreateWebhookService создает экземпляр WebhookService с использованием предоставленных репозиториев и кэша.
    /// </summary>
    /// <param name="repositories">Экземпляр MockRepositories, содержащий моки репозиториев.</param>
    /// <returns>Возвращает экземпляр WebhookService, готовый для использования в тестах.</returns>
    public IWebhookService CreateWebhookService(MockRepositories repositories)
    {
        // Создание и возврат экземпляра WebhookService с использованием переданных зависимостей и моков
        return new WebhookService(
            MemoryCacheMock.Object,
            repositories.InstanceRepositoryMock.Object,
            WeebhookSenderMock.Object);
    }
}