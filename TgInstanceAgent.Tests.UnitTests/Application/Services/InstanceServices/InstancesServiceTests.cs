using System.Reflection;
using Moq;
using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Abstractions.InstancesService;
using TgInstanceAgent.Application.Abstractions.InstancesService.Exceptions;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Interfaces;
using TgInstanceAgent.Application.Services.InstancesService;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Domain.Instances.Exceptions;
using TgInstanceAgent.Domain.Instances.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications.Abstractions;
using TgInstanceAgent.Tests.UnitTests.Core.MocksData.InstanceServices;
using TgInstanceAgent.Tests.UnitTests.Core.MockServices;
using TgInstanceAgent.Tests.UnitTests.Core.MocksRepositories;
using Xunit;

namespace TgInstanceAgent.Tests.UnitTests.Application.Services.InstanceServices;

/// <summary>
/// Класс для тестирования сервиса инстансов
/// </summary>
public class InstancesServiceTests : IDisposable
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
    private IInstancesService _service;

    /// <summary>
    /// Конструктор для инициализации полей
    /// </summary>
    public InstancesServiceTests()
    {
        // Создаем новый экземпляр MockRepositories для использования в тестах
        _repositories = new MockRepositories();
        
        // Создаем новый экземпляр MockServices для использования в тестах
        _services = new MockServices();

        // Создаем тестируемый экземпляр IInstancesService
        _service = _services.CreateInstancesService(_repositories);
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
        _service = _services.CreateInstancesService(_repositories);
    }
    
        /// <summary>
    /// Тест проверяет, что словарь клиентов остался пустой, при вызове метода с пустым списком инстансов.
    /// И никакой из инстансов не был запущен.
    /// </summary>
    [Fact]
    public async Task StartAsync_NoInstances_NoInstancesStarted()
    {
        // Arrange
        // Настраиваем мок репозитория, чтобы он возвращал пустой список инстансов
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.FindAsync(It.IsAny<ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>>(), null, null, null))
            .ReturnsAsync(new List<InstanceAggregate>());

        // Act
        // Запускаем метод StartAsync, передавая токен отмены.
        await _service.StartAsync(new CancellationToken());

        // Assert
        // Проверяем, что метод ConnectAsync не был вызван
        _services.TelegramClientMock.Verify(client
            => client.ConnectAsync(), Times.Never());
        
        // Получаем приватное поле _services с помощью рефлексии.
        var servicesField = typeof(InstancesService)
            .GetField("_services", BindingFlags.NonPublic | BindingFlags.Instance);
        
        // Получаем словарь, содержащий экземпляры клиентов
        var services = (Dictionary<Guid, ITelegramClient>)servicesField?.GetValue(_service)!;
        
        // Проверяем, что словарь _services пуст
        Assert.Empty(services);
    }
    
    /// <summary>
    /// Тест проверяет, что все инстансы были запущены и их клиенты были записаны в словарь
    /// </summary>
    [Fact]
    public async Task StartAsync_AllInstancesStarted()
    {
        // Arrange
        // Настраиваем фабрику для возврата мокнутого клиента
        _services.TelegramClientFactoryMock.Setup(f => f.FactoryMethod(It.IsAny<string>()))
            .Returns(_services.TelegramClientMock.Object);

        // Настраиваем мок репозитория для возврата заранее подготовленного списка инстансов.
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.FindAsync(It.IsAny<ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>>(), null, null, null))
            .ReturnsAsync(InstancesServiceTestsMockData.InstancesMock);

        // Act
        // Запускаем метод StartAsync, передавая токен отмены.
        await _service.StartAsync(new CancellationToken());

        // Assert
        // Получаем приватное поле _services с помощью рефлексии.
        var servicesField = typeof(InstancesService)
            .GetField("_services", BindingFlags.NonPublic | BindingFlags.Instance);
        
        // Получаем словарь, содержащий экземпляры клиентов
        var services = (Dictionary<Guid, ITelegramClient>)servicesField?.GetValue(_service)!;
        
        // Проверяем, что метод ConnectAsync был вызван для каждого инстанса
        _services.TelegramClientMock.Verify(client => 
            client.ConnectAsync(), Times.Exactly(InstancesServiceTestsMockData.InstancesMock.Count));
        
        // Проверяем, что в словаре _services два клиента, соответствующие инстансам.
        Assert.Equal(2, services.Count);
    }

    /// <summary>
    /// Тест проверяет, что флаг готовности инстанса устанавливается в true, после выполнения метода
    /// </summary>
    [Fact]
    public async Task StartAsync_SetsIsReadyToTrue()
    {
        // Arrange
        // Настраиваем мок репозитория для возврата заранее подготовленного списка инстансов.
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.FindAsync(It.IsAny<ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>>(), null, null, null))
            .ReturnsAsync(InstancesServiceTestsMockData.InstancesMock);
        
        // Настройка фабрики, чтобы она возвращала мокнутый клиент
        _services.TelegramClientFactoryMock.Setup(f => f.FactoryMethod(It.IsAny<string>()))
            .Returns(_services.TelegramClientMock.Object);

        // Act
        // Запускаем метод StartAsync, передавая токен отмены.
        await _service.StartAsync(new CancellationToken());

        // Assert
        // Получаем приватное поле _isReady с помощью рефлексии.
        var isReadyField = typeof(InstancesService)
            .GetField("_isReady", BindingFlags.NonPublic | BindingFlags.Instance);
        
        // Проверяем, что значение поля _isReady равно true.
        Assert.True((bool)isReadyField?.GetValue(_service)!);
    }
    
    /// <summary>
    /// Тест проверяет, что флаг готовности инстанса устанавливается в false, при попадании в метод
    /// </summary>
    [Fact]
    public async Task StopAsync_SetsIsReadyToFalse()
    {
        // Arrange
        // Для данного теста не требуется дополнительная подготовка

        // Act
        // Запускаем метод StopAsync, передавая токен отмены.
        await _service.StopAsync(new CancellationToken());

        // Assert
        // Получаем поле _isReady
        var isReadyField = typeof(InstancesService)
            .GetField("_isReady", BindingFlags.NonPublic | BindingFlags.Instance);

        // Проверяем, что флаг _isReady установлен в false
        Assert.False((bool)isReadyField?.GetValue(_service)!);
    }
    
    /// <summary>
    /// Тест проверяет, что все клиенты были уничтожены
    /// </summary>
    [Fact]
    public async Task StopAsync_DisposesAllClients()
    {
        // Arrange
        // Получаем поле словаря клиентов
        var servicesField = typeof(InstancesService)
            .GetField("_services", BindingFlags.NonPublic | BindingFlags.Instance);
        
        // Получаем значение поля словаря
        var services = (Dictionary<Guid, ITelegramClient>)servicesField?.GetValue(_service)!;
        
        // Добавляем экземпляр клиента в словарь
        services[Guid.NewGuid()] = _services.TelegramClientMock.Object;

        // Act
        // Запускаем метод StopAsync, передавая токен отмены.
        await _service.StopAsync(new CancellationToken());

        // Assert
        // Проверяем, что добавленный клиент был уничтожен
        _services.TelegramClientMock.Verify(client => client.DisposeAsync(), Times.Once);
        
        // Проверяем что словарь клиентов был очищен
        Assert.Empty(services);
    }
    
    /// <summary>
    /// Тест проверяет, что если флаг isReady установлен в false, то выбрасывается исключение
    /// </summary>
    [Fact]
    public async Task StartClientAsync_ServiceNotReady_ThrowsInstanceServiceNotReadyException()
    {
        // Arrange
        // Получаем поле isReady
        var isReadyField = typeof(InstancesService)
            .GetField("_isReady", BindingFlags.NonPublic | BindingFlags.Instance);
        
        // Устанавливаем _isReady в false через рефлексию
        isReadyField?.SetValue(_service, false);
        
        // Act & Assert
        // Проверяем что было выброшено исключение
        await Assert.ThrowsAsync<InstanceServiceNotReadyException>(() => 
            _service.StartClientAsync(InstancesServiceTestsMockData.InstanceIdMock, CancellationToken.None));
    }
    
    /// <summary>
    /// Тест проверяет, что будет выброшено исключение, если инстанс просрочен
    /// </summary>
    [Fact]
    public async Task StartClientAsync_InstanceExpired_ThrowsInstanceExpiredException()
    {
        // Arrange
        // Получаем моковую модель истёкшего инстанса
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(InstancesServiceTestsMockData.ExpiredInstanceMock);
        
        // Мокаем получение истёкшего инстанса из кэша
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object instance) =>
            {
                instance = InstancesServiceTestsMockData.ExpiredInstanceMock;
                return true;
            });

        // Act & Assert
        // Проверяем что было выброшено исключение
        await Assert.ThrowsAsync<InstanceExpiredException>(() => 
            _service.StartClientAsync(InstancesServiceTestsMockData.InstanceIdMock, new CancellationToken()));
    }
    
    /// <summary>
    /// Тест проверяет, что будет выброшено исключение, если инстанс не найден
    /// </summary>
    [Fact]
    public async Task StartClientAsync_InstanceNull_ThrowsInstanceNotFoundException()
    {
        // Получаем моковую модель инстанса
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InstanceAggregate)null!);
        
        // Act & Assert
        // Проверяем что было выброшено исключение
        await Assert.ThrowsAsync<InstanceNotFoundException>(() => 
            _service.StartClientAsync(InstancesServiceTestsMockData.InstanceIdMock, new CancellationToken()));
    }
    
    /// <summary>
    /// Тест проверяет, что метод вернул экземпляр клиента
    /// </summary>
    [Fact]
    public async Task StartClientAsync_InstanceExistsInServices_ReturnsWhatsAppClient()
    {
        // Arrange
        // Получаем поле словаря клиентов
        var servicesField = typeof(InstancesService)
            .GetField("_services", BindingFlags.NonPublic | BindingFlags.Instance);
        
        // Получаем значение поля словаря
        var services = (Dictionary<Guid, ITelegramClient>)servicesField?.GetValue(_service)!;
        
        // Добавляем экземпляр клиента в словарь
        services[InstancesServiceTestsMockData.InstanceMock.Id] = _services.TelegramClientMock.Object;

        // Получаем моковую модель инстанса
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(InstancesServiceTestsMockData.InstanceMock);
        
        // Мокаем получение инстанса из кэша
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object instance) =>
            {
                instance = InstancesServiceTestsMockData.InstanceMock;
                return true;
            });

        // Act
        // Запускаем метод StartClientAsync, передавая идентификатор инстанса и токен отмены.
        var result = await _service.StartClientAsync(InstancesServiceTestsMockData.InstanceMock.Id, new CancellationToken());

        // Assert
        // Проверяем что метод вернул правильный экземпляр клиента
        Assert.Equal(_services.TelegramClientMock.Object, result);
        
        // Проверяем что клиент не был запущен
        _services.TelegramClientMock.Verify(client => client.ConnectAsync(), Times.Never());
    }
    
    /// <summary>
    /// Тест проверяет, что новый клиент был успешно запущен, если его не было в словаре клиетов
    /// </summary>
    [Fact]
    public async Task StartClientAsync_InstanceNotInServices_StartsAndReturnsNewWhatsAppClient()
    {
        // Arrange
        // Настраиваем фабрику для возврата мокнутого клиента
        _services.TelegramClientFactoryMock.Setup(f => f.FactoryMethod(It.IsAny<string>()))
            .Returns(_services.TelegramClientMock.Object);

        // Получаем моковую модель инстанса
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(InstancesServiceTestsMockData.InstanceMock);
        
        // Мокаем получение инстанса из кэша
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object instance) =>
            {
                instance = InstancesServiceTestsMockData.InstanceMock;
                return true;
            });

        // Act
        // Запускаем метод StartClientAsync, передавая идентификатор инстанса и токен отмены.
        var result = await _service.StartClientAsync(InstancesServiceTestsMockData.InstanceMock.Id, new CancellationToken());

        // Assert
        // Проверяем что методы вернул правильный экземпляр клиента
        Assert.Equal(_services.TelegramClientMock.Object, result);
        
        // Проверяем, что инстанс был включен
        Assert.True(InstancesServiceTestsMockData.InstanceMock.Enabled);
        
        // Проверяем, что метод ConnectAsync был вызван единажды
        _services.TelegramClientMock.Verify(client => 
            client.ConnectAsync(), Times.Once);
        
        // Проверяем, что инстанс был обновлён в базе данных
        _repositories.InstanceRepositoryMock.Verify(repo => 
            repo.UpdateAsync(InstancesServiceTestsMockData.InstanceMock), Times.Once);
    }
    
    /// <summary>
    /// Тест проверяет, что будет выброшено исключение, если инстанс не найден
    /// </summary>
    [Fact]
    public async Task StopClientAsync_InstanceNull_ThrowsInstanceNotFoundException()
    {
        // Arrange
        // Получаем поле словаря клиентов
        var servicesField = typeof(InstancesService)
            .GetField("_services", BindingFlags.NonPublic | BindingFlags.Instance);
        
        // Получаем значение поля словаря
        var services = (Dictionary<Guid, ITelegramClient>)servicesField!.GetValue(_service)!;
        
        // Добавляем экземпляр клиента в словарь
        services[InstancesServiceTestsMockData.InstanceIdMock] = _services.TelegramClientMock.Object;
        
        // Получаем моковую модель инстанса
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((InstanceAggregate)null!);
        
        // Act & Assert
        // Проверяем что было выброшено исключение
        await Assert.ThrowsAsync<InstanceNotFoundException>(() => 
            _service.StopClientAsync(InstancesServiceTestsMockData.InstanceIdMock, new CancellationToken()));
    }
    
    /// <summary>
    /// Тест проверяет, что если клиент с указанным instanceId не найден, метод завершает выполнение без ошибок.
    /// </summary>
    [Fact]
    public async Task StopClientAsync_ClientNotFound_DoesNothing()
    {
        // Arrange
        // Получаем поле словаря клиентов
        var servicesField = typeof(InstancesService)
            .GetField("_services", BindingFlags.NonPublic | BindingFlags.Instance);
        
        // Получаем значение поля словаря
        var services = (Dictionary<Guid, ITelegramClient>)servicesField!.GetValue(_service)!;
        
        // Проверяем что в словаре нет записи с ключем, равным идентификатору инстанса
        Assert.False(services.ContainsKey(InstancesServiceTestsMockData.InstanceIdMock));

        // Act
        // Запускаем метод StopClientAsync, передавая идентификатор инстанса и токен отмены.
        await _service.StopClientAsync(InstancesServiceTestsMockData.InstanceIdMock, new CancellationToken());

        // Assert
        // Проверяем, что Dispose не был вызван
        _services.TelegramClientMock.Verify(client => client.DisposeAsync(), Times.Never);

        // Проверяем, что метод UpdateAsync не был вызван
        _repositories.InstanceRepositoryMock.Verify(repo => 
            repo.UpdateAsync(It.IsAny<InstanceAggregate>()), Times.Never);
    }
    
    /// <summary>
    /// Тест проверяет, что клиент успешно останавливается и удаляется из словаря, а инстанс обновляется.
    /// </summary>
    [Fact]
    public async Task StopClientAsync_SuccessfullyStopsClient()
    {
        // Arrange
        // Получаем поле словаря клиентов
        var servicesField = typeof(InstancesService)
            .GetField("_services", BindingFlags.NonPublic | BindingFlags.Instance);
        
        // Получаем значение поля словаря
        var services = (Dictionary<Guid, ITelegramClient>)servicesField!.GetValue(_service)!;
        
        // Добавляем экземпляр клиента в словарь
        services[InstancesServiceTestsMockData.InstanceMock.Id] = _services.TelegramClientMock.Object;

        // Получаем моковую модель инстанса
        _repositories.InstanceRepositoryMock
            .Setup(repo => repo.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(InstancesServiceTestsMockData.InstanceMock);
        
        // Мокаем получение инстанса из кэша
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object instance) =>
            {
                instance = InstancesServiceTestsMockData.InstanceMock;
                return true;
            });

        // Act
        await _service.StopClientAsync(InstancesServiceTestsMockData.InstanceMock.Id, CancellationToken.None);

        // Assert
        // Проверяем, что клиент был удален из словаря
        Assert.False(services.ContainsKey(InstancesServiceTestsMockData.InstanceMock.Id));
    
        // Проверяем, что клиент был освобожден
        _services.TelegramClientMock.Verify(client => client.DisposeAsync(), Times.Once);

        // Проверяем, что инстанс был обновлен в репозитории
        _repositories.InstanceRepositoryMock.Verify(repo => 
            repo.UpdateAsync(It.Is<InstanceAggregate>(i => i == InstancesServiceTestsMockData.InstanceMock)), Times.Once);
    }
}