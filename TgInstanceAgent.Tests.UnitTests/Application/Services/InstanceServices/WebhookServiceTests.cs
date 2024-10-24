using Moq;
using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Tests.UnitTests.Core.MocksData.InstanceServices;
using TgInstanceAgent.Tests.UnitTests.Core.MockServices;
using TgInstanceAgent.Tests.UnitTests.Core.MocksRepositories;
using Xunit;

namespace TgInstanceAgent.Tests.UnitTests.Application.Services.InstanceServices;

/// <summary>
/// Класс для тестирования сервиса отправки вебхуков
/// </summary>
public class WebhookServiceTests : IDisposable
{
    /// <summary>
    /// Репозитории, используемые в тестах
    /// </summary>
    private MockRepositories _repositories;

    /// <summary>
    /// Сервисы, используемые в тестах
    /// </summary>
    private MockServices _services;

    /// <summary>
    /// Тестируемый сервис, который мы будем проверять
    /// </summary>
    private IWebhookService _service;

    /// <summary>
    /// Конструктор для инициализации полей
    /// </summary>
    public WebhookServiceTests()
    {
        // Создаем новый экземпляр MockRepositories для использования в тестах
        _repositories = new MockRepositories();
        
        // Создаем новый экземпляр MockServices для использования в тестах
        _services = new MockServices();

        // Создаем тестируемый экземпляр IReportService
        _service = _services.CreateWebhookService(_repositories);
    }
    
    // Метод Dispose будет вызван после каждого теста
    public void Dispose()
    {
        // Подавляем вызов финализатора для текущего объекта. 
        GC.SuppressFinalize(this);
        
        // Сбрасываем репозитории
        _repositories = new MockRepositories();
        
        // Сбрасываем сервисы
        _services = new MockServices();   
        
        // Пересоздаем сервис
        _service = _services.CreateWebhookService(_repositories);
    }
    
        /// <summary>
    /// Тест проверяет, что будет выброшено исключение, если инстанс не найден
    /// </summary>
    [Fact]
    public async Task PublishWebhookIfNeededAsync_InstanceNull_ThrowsInstanceNotFoundException()
    {
        // Получаем моковую модель инстанса
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InstanceAggregate)null!);
        
        // Act & Assert
        // Проверяем что было выброшено исключение
        await Assert.ThrowsAsync<InstanceNotFoundException>(() => 
            _service.PublishWebhookIfNeededAsync(WebhookServiceTestsMockData.InstanceIdMock, WebhookServiceTestsMockData.TgAuthenticatedEventMock));
    }

    /// <summary>
    /// Тест проверяет, что метод возвращает управление если флаг NeedSend установлен в false
    /// </summary>
    [Fact]
    public async Task PublishWebhookIfNeededAsync_VisitorNeedSendIsFalse_ShouldNotSendWebhook()
    {
        // Arrange
        // Получаем моковую модель инстанса
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(WebhookServiceTestsMockData.InstanceWithoutWeebhooksMock);
        
        // Мокаем получение инстанса из кэша
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object instance) =>
            {
                instance = WebhookServiceTestsMockData.InstanceWithoutWeebhooksMock;
                return true;
            });

        // Act
        // Запускаем метод PublishWebhookIfNeededAsync, передавая идентификатор инстанса, событие и токен отмены.
        await _service.PublishWebhookIfNeededAsync(WebhookServiceTestsMockData.InstanceWithoutWeebhooksMock.Id,
            WebhookServiceTestsMockData.TgAuthenticatedEventMock);

        // Assert
        // Проверяем что, метод PushToQueue не был вызван
        _services.WeebhookSenderMock.Verify(s => 
            s.PushToQueue(It.IsAny<TgEvent>(), It.IsAny<Uri>(), It.IsAny<string>()), Times.Never);
    }

    /// <summary>
    /// Тест проверяет, что метод отправляет вебхук если флаг NeedSend установлен в true
    /// </summary>
    [Fact]
    public async Task PublishWebhookIfNeededAsync_VisitorNeedSendIsTrue_ShouldSendWebhook()
    {
        // Arrange
        // Добавляем инстансу ссылку для отправки вебхука
        WebhookServiceTestsMockData.InstanceWithWeebhooksMock.AddWebhookUrl(new Uri("https://test.test.ru/"));
        
        // Получаем моковую модель инстанса
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(WebhookServiceTestsMockData.InstanceWithWeebhooksMock);
        
        // Мокаем получение инстанса из кэша
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object instance) =>
            {
                instance = WebhookServiceTestsMockData.InstanceWithWeebhooksMock;
                return true;
            });

        // Act
        // Запускаем метод PublishWebhookIfNeededAsync, передавая идентификатор инстанса, событие и токен отмены.
        await _service.PublishWebhookIfNeededAsync(WebhookServiceTestsMockData.InstanceWithWeebhooksMock.Id,
            WebhookServiceTestsMockData.TgUpdateChatMessageAutoDeleteTimeEventMock);

        // Assert
        // Проверяем что, метод PushToQueue был вызван 1 раз
        _services.WeebhookSenderMock.Verify(s => 
            s.PushToQueue(It.IsAny<TgEvent>(), It.IsAny<Uri>(), It.IsAny<string>()), Times.Once());
    }
}