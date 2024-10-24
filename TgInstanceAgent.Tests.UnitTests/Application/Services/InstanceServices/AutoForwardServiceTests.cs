using Moq;
using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Messages;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Tests.UnitTests.Core.MocksData.InstanceServices;
using TgInstanceAgent.Tests.UnitTests.Core.MockServices;
using TgInstanceAgent.Tests.UnitTests.Core.MocksRepositories;
using Xunit;

namespace TgInstanceAgent.Tests.UnitTests.Application.Services.InstanceServices;

/// <summary>
/// Класс для тестирования сервиса автоматической пересылки сообщений
/// </summary>
public class AutoForwardServiceTests : IDisposable
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
    private IAutoForwardService _service;

    /// <summary>
    /// Конструктор для инициализации полей
    /// </summary>
    public AutoForwardServiceTests()
    {
        // Создаем новый экземпляр MockRepositories для использования в тестах
        _repositories = new MockRepositories();
        
        // Создаем новый экземпляр MockServices для использования в тестах
        _services = new MockServices();

        // Создаем тестируемый экземпляр IAutoForwardService
        _service = _services.CreateAutoForwardService(_repositories);
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
        _service = _services.CreateAutoForwardService(_repositories);

        // Сбрасываем моковые модели
        AutoForwardServiceTestsMockData.ResetMockData();
    }

    /// <summary>
    /// Тест проверяет, что сообщение не пересылается, если оно исходящее или не может быть переслано.
    /// </summary>
    [Fact]
    public async Task ForwardIfNeededAsync_WhenMessageIsOutgoing_ShouldNotForward()
    {
        // Arrange
        // Для теста не требуется дополнительная подготовка данных, только моковые модели
        
        // Act
        // Результат пересылки исходящего сообщения
        var resultOutgoing = await _service.ForwardIfNeededAsync(
            _services.TelegramClientMock.Object, Guid.NewGuid(), AutoForwardServiceTestsMockData.MockMessageOutgoing);
        
        // Результат пересылки сообщения, которое нельзя переслать
        var resultNonForward = await _service.ForwardIfNeededAsync(
            _services.TelegramClientMock.Object, Guid.NewGuid(), AutoForwardServiceTestsMockData.MockMessageNotForward);
        
        // Assert
        // Проверяем, что сервис вернул null в обоих случаях пересылки сообщения
        Assert.Null(resultOutgoing);
        Assert.Null(resultNonForward);
    }

    /// <summary>
    /// Тест проверяет, что сообщение не пересылается, если для него не настроен форвард.
    /// </summary>
    [Fact]
    public async Task ForwardIfNeededAsync_WhenNoForwardSettingsExist_ShouldNotForward()
    {
        // Arrange
        // Получаем моковую модель инстанса
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(AutoForwardServiceTestsMockData.MockInstanceAggregate);
        
        // Мокаем получение инстанса из кэша
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object instance) =>
            {
                instance = AutoForwardServiceTestsMockData.MockInstanceAggregate;
                return true;
            });
        
        // Act
        // Получаем результат пересылки сообщения
        var forwardedMessage = await _service.ForwardIfNeededAsync(
            _services.TelegramClientMock.Object, AutoForwardServiceTestsMockData.MockInstanceId, AutoForwardServiceTestsMockData.MockMessage);

        // Assert
        // Проверяем что результат пересылки null, так как пересылка не настроена у инстанса
        Assert.Null(forwardedMessage);
    }

    /// <summary>
    /// Тест проверяет, что сообщение пересылается, если для него настроен форвард.
    /// </summary>
    [Fact]
    public async Task ForwardIfNeededAsync_WhenForwardSettingsExist_ShouldForward()
    {
        // Arrange
        // Добавляем настройку форварда в модель инстанса
        AutoForwardServiceTestsMockData.MockInstanceAggregate.AddForwardEntry(AutoForwardServiceTestsMockData.MockForwardEntry);
        
        // Получаем моковую модель инстанса
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(AutoForwardServiceTestsMockData.MockInstanceAggregate);
        
        // Мокаем получение инстанса из кэша
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object instance) =>
            {
                instance = AutoForwardServiceTestsMockData.MockInstanceAggregate;
                return true;
            });
        
        // Мокаем получение пересланного сообщения
        _services.TelegramClientMock
            .Setup(client =>
                client.ForwardMessagesAsync(It.IsAny<TgForwardMessagesRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<TgMessage> { AutoForwardServiceTestsMockData.MockMessage });
        
        // Act
        // Получаем результат пересылки сообщения
        var forwardedMessage = await _service.ForwardIfNeededAsync(
            _services.TelegramClientMock.Object, AutoForwardServiceTestsMockData.MockInstanceId, AutoForwardServiceTestsMockData.MockMessage);
        
        // Assert
        // Проверяем что результат пересылки не null
        Assert.NotNull(forwardedMessage);
        
        // Проверяем, что метод ForwardMessagesAsync был вызван с правильными параметрами ровно 1 раз
        _services.TelegramClientMock.Verify(client =>
            client.ForwardMessagesAsync(It.Is<TgForwardMessagesRequest>(req =>
                req.Chat is TgInputChatId && 
                ((TgInputChatId)req.Chat).ChatId == AutoForwardServiceTestsMockData.MockForwardEntry.To &&
                req.FromChatId == AutoForwardServiceTestsMockData.MockMessage.ChatId &&
                req.MessageIds.Contains(AutoForwardServiceTestsMockData.MockMessage.Id) &&
                req.SendCopy == false
            ), It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// Тест проверяет, что метод корректно обрабатывает исключение, выброшенное клиентом при пересылке сообщений.
    /// </summary>
    [Fact]
    public async Task ForwardIfNeededAsync_ClientThrowsException_ShouldHandleException()
    {
        // Arrange
        // Добавляем настройку форварда в модель инстанса
        AutoForwardServiceTestsMockData.MockInstanceAggregate.AddForwardEntry(AutoForwardServiceTestsMockData.MockForwardEntry);
        
        // Мокаем получение инстанса из репозитория
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(AutoForwardServiceTestsMockData.MockInstanceAggregate);

        // Мокаем получение инстанса из кэша
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object instance) =>
            {
                instance = AutoForwardServiceTestsMockData.MockInstanceAggregate;
                return true;
            });
        
        // Act
        // Мокаем вызов метода ForwardMessagesAsync так, чтобы он выбрасывал исключение
        _services.TelegramClientMock.Setup(client =>
                client.ForwardMessagesAsync(It.IsAny<TgForwardMessagesRequest>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(AutoForwardServiceTestsMockData.MockClientException);
        
        // Получаем исключение из клиента
        var ex = await Assert.ThrowsAsync<ClientException>(async () =>
            await _service.ForwardIfNeededAsync(
                _services.TelegramClientMock.Object, 
                AutoForwardServiceTestsMockData.MockInstanceId, 
                AutoForwardServiceTestsMockData.MockMessage)
        );
        
        // Assert
        // Проверяем, что ForwardMessagesAsync был вызван один раз
        _services.TelegramClientMock.Verify(client =>
            client.ForwardMessagesAsync(It.IsAny<TgForwardMessagesRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        
        // Проверяем, что выбрасываемое исключение соответствует ожидаемому
        Assert.Equal(AutoForwardServiceTestsMockData.MockClientException.Error, ex.Error);
        Assert.Equal(AutoForwardServiceTestsMockData.MockClientException.Message, ex.Message);
    }
    
    /// <summary>
    /// Тест проверяет, что будет выброшено исключение если инстанс null
    /// </summary>
    [Fact]
    public async Task ForwardIfNeededAsync_InstanceIsNull_ShouldHandleException()
    {
        // Arrange
        // Получаем моковую модель инстанса
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InstanceAggregate)null!);
        
        // Act & Assert
        // Проверяем что было выброшено исключение
        await Assert.ThrowsAsync<InstanceNotFoundException>(() => 
            _service.ForwardIfNeededAsync( _services.TelegramClientMock.Object,
                AutoForwardServiceTestsMockData.MockInstanceId, AutoForwardServiceTestsMockData.MockMessage));
    }
}