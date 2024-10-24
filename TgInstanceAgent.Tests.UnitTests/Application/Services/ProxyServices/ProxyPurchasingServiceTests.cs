using Moq;
using TgInstanceAgent.Application.Abstractions.Proxies.DataModels;
using TgInstanceAgent.Application.Abstractions.Proxies.ProxyPurchasingService;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Domain.Instances.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications.Abstractions;
using TgInstanceAgent.Domain.SystemProxy;
using TgInstanceAgent.Domain.SystemProxy.Enums;
using TgInstanceAgent.Domain.SystemProxy.Specifications.Visitor;
using TgInstanceAgent.Tests.UnitTests.Core.MocksData;
using TgInstanceAgent.Tests.UnitTests.Core.MocksData.ProxyServices;
using TgInstanceAgent.Tests.UnitTests.Core.MockServices;
using TgInstanceAgent.Tests.UnitTests.Core.MocksRepositories;
using Xunit;

namespace TgInstanceAgent.Tests.UnitTests.Application.Services.ProxyServices;

/// <summary>
/// Класс для тестирования сервиса покупки и продления прокси.
/// </summary>
public class ProxyPurchasingServiceTests
{
    /// <summary>
    /// Репозитории, используемые в тестах
    /// </summary>
    private readonly MockRepositories _repositories;

    /// <summary>
    /// Сервисы, используемые в тестах
    /// </summary>
    private readonly MockServices _services;

    /// <summary>
    /// Тестируемый сервис, который мы будем проверять
    /// </summary>
    private readonly IProxyPurchasingService _service;
    
    /// <summary>
    /// Конструктор для инициализации полей
    /// </summary>
    public ProxyPurchasingServiceTests()
    {
        // Создаем новый экземпляр MockRepositories для использования в тестах
        _repositories = new MockRepositories();
        
        // Создаем новый экземпляр MockServices для использования в тестах
        _services = new MockServices();
        
        // Создаем тестируемый экземпляр IProxyPurchasingService
        _service = _services.CreateProxyPurchasingService(_repositories, ProxyPurchasingServiceTestsMockData.CountryMock, ProxyPurchasingServiceTestsMockData.InstancesCoefficientMock);
    }
    
    /// <summary>
    /// Тест, который проверяет
    /// Что никакие действия с прокси не были вызваны
    /// </summary>
    [Fact]
    public async Task PurchaseProxyIfNeededAsync_ProxyNoActions()
    {
        // Arrange
        // Устанавливаем ожидаемый результат количества инстансов
        _repositories.InstanceRepositoryMock.Setup(repo => repo
                .CountAsync(It.IsAny<ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>>()))
            .ReturnsAsync(5);

        // Устанавливаем ожидаемый результат системных прокси, которые будут живы ещё минимум 1 сутки
        _repositories.SystemProxyRepositoryMock.Setup(repo => repo
                .CountAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>()))
            .ReturnsAsync(2);

        // Устанавливаем ожидаемый результат прокси, время жизни которых менее 1 суток.
        _repositories.SystemProxyRepositoryMock.Setup(repo => repo
                .FindAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>(), null, 0, 0))
            .ReturnsAsync(new List<SystemProxyAggregate>());

        // Act
        await _service.PurchaseProxyIfNeededAsync();

        // Assert
        // Проверяем, что метод ExtendAsync не был вызван
        _services.ProxyClientMock.Verify(client =>
            client.ExtendAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never());

        // Проверяем, что метод UpdateAsync не был вызван
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.UpdateAsync(It.IsAny<SystemProxyAggregate>()), Times.Never());

        // Проверяем, что метод BuyProxyForCountryAsync не был вызван
        _services.ProxyClientMock.Verify(client =>
            client.BuyProxyForCountryAsync(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<ProxyType>(),
                It.IsAny<IpVersion>(), It.IsAny<CancellationToken>()), Times.Never());

        // Проверяем, что метод AddAsync не был вызван
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.AddAsync(It.IsAny<SystemProxyAggregate>(), It.IsAny<CancellationToken>()), Times.Never());
    }

    /// <summary>
    /// Тест, который проверяет продление прокси
    /// </summary>
    [Fact]
    public async Task PurchaseProxyIfNeededAsync_ProxyExtend()
    {
        // Arrange
        // Устанавливаем ожидаемый результат количества инстансов
        _repositories.InstanceRepositoryMock.Setup(repo => 
                repo.CountAsync(It.IsAny<ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>>()))
            .ReturnsAsync(5);
        
        // Устанавливаем ожидаемый результат системных прокси, которые будут живы ещё минимум 1 сутки
        _repositories.SystemProxyRepositoryMock.Setup(repo => 
                repo.CountAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>()))
            .ReturnsAsync(1);
        
        // Устанавливаем ожидаемый результат прокси, время жизни которых менее 1 суток.
        _repositories.SystemProxyRepositoryMock.Setup(repo => 
                repo.FindAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>(), null, 0, 1))
            .ReturnsAsync(new List<SystemProxyAggregate> { ProxyPurchasingServiceTestsMockData.ExpiringProxyMock }); 
        
        // Устанавливаем ожидаемый результат для продленных прокси
        _services.ProxyClientMock.Setup(client => 
                client.ExtendAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ExtendResult> { ProxyPurchasingServiceTestsMockData.ProlongedProxyMock });
        
        // Act
        // Вызываем метод, который тестируем
        await _service.PurchaseProxyIfNeededAsync();
        
        // Assert
        // Проверяем, что метод ExtendAsync был вызван ровно 1 раз
        _services.ProxyClientMock.Verify(client => 
            client.ExtendAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод UpdateAsync был вызван ровно 1 раз
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.UpdateAsync(It.IsAny<SystemProxyAggregate>()), Times.Exactly(1));
        
        // Проверяем, что метод BuyProxyForCountryAsync не был вызван
        _services.ProxyClientMock.Verify(client =>
            client.BuyProxyForCountryAsync(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<ProxyType>(),
                It.IsAny<IpVersion>(), It.IsAny<CancellationToken>()), Times.Never());
        
        // Проверяем, что метод AddAsync не был вызван
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.AddAsync(It.IsAny<SystemProxyAggregate>(),It.IsAny<CancellationToken>()), Times.Never());
    }

    /// <summary>
    /// Тест, который проверяет покупку прокси
    /// Количество доступных для покупки прокси, больше чем нужное нам количество прокси
    /// </summary>
    [Fact]
    public async Task PurchaseProxyIfNeededAsync_ProxyBuyLessThatCount()
    {
        // Arrange
        // Устанавливаем ожидаемый результат количества инстансов
        _repositories.InstanceRepositoryMock.Setup(repo => 
                repo.CountAsync(It.IsAny<ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>>()))
            .ReturnsAsync(3);
        
        // Устанавливаем ожидаемый результат системных прокси, которые будут живы ещё минимум 1 сутки
        _repositories.SystemProxyRepositoryMock.Setup(repo => 
                repo.CountAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>()))
            .ReturnsAsync(0);
        
        // Устанавливаем ожидаемый результат прокси, время жизни которых менее 1 суток.
        _repositories.SystemProxyRepositoryMock.Setup(repo => 
                repo.FindAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>(), null, 0, 1))
            .ReturnsAsync(new List<SystemProxyAggregate>());
        
        // Устанавливаем ожидаемый результат для продленных прокси
        _services.ProxyClientMock.Setup(client =>
                client.ExtendAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ExtendResult>());
        
        // Устанавливаем ожидаемый результат для количества доступных прокси в указанной стране
        _services.ProxyClientMock.Setup(client =>
                client.GetCountAvailableInCountryAsync(It.IsAny<string>(), It.IsAny<IpVersion>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(5);
        
        // Устанавливаем ожидаемый результат для купленных прокси
        _services.ProxyClientMock.Setup(client => client
                .BuyProxyForCountryAsync(It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<int>(), It.IsAny<ProxyType>(),
                    It.IsAny<IpVersion>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new [] { ProxyPurchasingServiceTestsMockData.BoughtProxyMock });
        
        // Act
        // Вызываем метод, который тестируем
        await _service.PurchaseProxyIfNeededAsync();
        
        // Assert
        // Проверяем, что метод ExtendAsync не был вызван
        _services.ProxyClientMock.Verify(client => 
            client.ExtendAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never());
        
        // Проверяем, что метод UpdateAsync не был вызван
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.UpdateAsync(It.IsAny<SystemProxyAggregate>()), Times.Never());
        
        // Проверяем, что метод GetCountAvailableInCountryAsync был вызван 1 раз
        _services.ProxyClientMock.Verify(client => 
            client.GetCountAvailableInCountryAsync(It.IsAny<string>(), 
                It.IsAny<IpVersion>(),It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод BuyProxyForCountryAsync был вызван 1 раз
        _services.ProxyClientMock.Verify(client =>
            client.BuyProxyForCountryAsync(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<ProxyType>(),
                It.IsAny<IpVersion>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод AddAsync был вызван 1 раз
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.AddAsync(It.IsAny<SystemProxyAggregate>(),It.IsAny<CancellationToken>()), Times.Exactly(1));
    }

    /// <summary>
    /// Тест, который проверяет покупку прокси
    /// Количество доступных для покупки прокси, меньше чем нужное нам количество прокси
    /// </summary>
    [Fact]
    public async Task PurchaseProxyIfNeededAsync_ProxyBuyMoreThatCount()
    {
        // Arrange
        // Устанавливаем ожидаемый результат количества инстансов
        _repositories.InstanceRepositoryMock.Setup(repo => 
                repo.CountAsync(It.IsAny<ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>>()))
            .ReturnsAsync(8);
        
        // Устанавливаем ожидаемый результат системных прокси, которые будут живы ещё минимум 1 сутки
        _repositories.SystemProxyRepositoryMock.Setup(repo => 
                repo.CountAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>()))
            .ReturnsAsync(0);
        
        // Устанавливаем ожидаемый результат прокси, время жизни которых менее 1 суток.
        _repositories.SystemProxyRepositoryMock.Setup(repo => 
                repo.FindAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>(), null, 0, 3))
            .ReturnsAsync(new List<SystemProxyAggregate>());
        
        // Устанавливаем ожидаемый результат для продленных прокси
        _services.ProxyClientMock.Setup(client =>
                client.ExtendAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ExtendResult>());
        
        // Устанавливаем ожидаемый результат для количества доступных прокси в указанной стране
        _services.ProxyClientMock.Setup(client =>
                client.GetCountAvailableInCountryAsync(It.IsAny<string>(), It.IsAny<IpVersion>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        
        // Устанавливаем ожидаемый результат для купленных прокси
        _services.ProxyClientMock.Setup(client => client
                .BuyProxyForCountryAsync(It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<int>(), It.IsAny<ProxyType>(),
                    It.IsAny<IpVersion>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new [] { ProxyPurchasingServiceTestsMockData.BoughtProxyMock });
        
        // Act
        // Вызываем метод, который тестируем
        await _service.PurchaseProxyIfNeededAsync();

        // Assert
        // Проверяем, что метод ExtendAsync не был вызван
        _services.ProxyClientMock.Verify(client => 
            client.ExtendAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never());
        
        // Проверяем, что метод UpdateAsync не был вызван
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.UpdateAsync(It.IsAny<SystemProxyAggregate>()), Times.Never());
        
        // Проверяем, что метод GetCountAvailableInCountryAsync был вызван 1 раз
        _services.ProxyClientMock.Verify(client => 
            client.GetCountAvailableInCountryAsync(It.IsAny<string>(), 
                It.IsAny<IpVersion>(),It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод BuyProxyForCountryAsync был вызван 1 раз
        _services.ProxyClientMock.Verify(client =>
            client.BuyProxyForCountryAsync(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<ProxyType>(),
                It.IsAny<IpVersion>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод AddAsync был вызван 1 раз
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.AddAsync(It.IsAny<SystemProxyAggregate>(),It.IsAny<CancellationToken>()), Times.Exactly(1));
    }

    /// <summary>
    /// Тест, который проверяет продление и покупку прокси.
    /// Количество доступных для покупки прокси, меньше чем нужное нам количество прокси
    /// </summary>
    [Fact]
    public async Task PurchaseProxyIfNeededAsync_ProlongProxyAndBuyLessThatCount()
    {
        // Arrange
        // Устанавливаем ожидаемый результат количества инстансов
        _repositories.InstanceRepositoryMock.Setup(repo => 
                repo.CountAsync(It.IsAny<ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>>()))
            .ReturnsAsync(8);
        
        // Устанавливаем ожидаемый результат системных прокси, которые будут живы ещё минимум 1 сутки
        _repositories.SystemProxyRepositoryMock.Setup(repo => 
                repo.CountAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>()))
            .ReturnsAsync(1);
        
        // Устанавливаем ожидаемый результат прокси, время жизни которых менее 1 суток.
        _repositories.SystemProxyRepositoryMock.Setup(repo => 
                repo.FindAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>(), null, 0, 2))
            .ReturnsAsync(new List<SystemProxyAggregate> { ProxyPurchasingServiceTestsMockData.ExpiringProxyMock });
        
        // Устанавливаем ожидаемый результат для продленных прокси
        _services.ProxyClientMock.Setup(client =>
                client.ExtendAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ExtendResult> { ProxyPurchasingServiceTestsMockData.ProlongedProxyMock });
        
        // Устанавливаем ожидаемый результат для количества доступных прокси в указанной стране
        _services.ProxyClientMock.Setup(client =>
                client.GetCountAvailableInCountryAsync(It.IsAny<string>(), It.IsAny<IpVersion>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        
        // Устанавливаем ожидаемый результат для купленных прокси
        _services.ProxyClientMock.Setup(client => client
                .BuyProxyForCountryAsync(It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<int>(), It.IsAny<ProxyType>(),
                    It.IsAny<IpVersion>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new [] { ProxyPurchasingServiceTestsMockData.BoughtProxyMock });
        
        // Act
        // Вызываем метод, который тестируем
        await _service.PurchaseProxyIfNeededAsync();

        // Assert
        // Проверяем, что метод ExtendAsync был вызван ровно 1 раз
        _services.ProxyClientMock.Verify(client => 
            client.ExtendAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод UpdateAsync был вызван ровно 1 раз
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.UpdateAsync(It.IsAny<SystemProxyAggregate>()), Times.Exactly(1));
        
        // Проверяем, что метод GetCountAvailableInCountryAsync был вызван 1 раз
        _services.ProxyClientMock.Verify(client => 
            client.GetCountAvailableInCountryAsync(It.IsAny<string>(), 
                It.IsAny<IpVersion>(),It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод BuyProxyForCountryAsync был вызван 1 раз
        _services.ProxyClientMock.Verify(client =>
            client.BuyProxyForCountryAsync(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<ProxyType>(),
                It.IsAny<IpVersion>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод AddAsync был вызван 1 раз
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.AddAsync(It.IsAny<SystemProxyAggregate>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
    }

    /// <summary>
    /// Тест, который проверяет продление и покупку прокси.
    /// Количество доступных для покупки прокси, больше чем нужное нам количество прокси
    /// </summary>
    [Fact]
    public async Task PurchaseProxyIfNeededAsync_ProlongProxyAndBuyMoreThatCount()
    {
        // Arrange
        // Устанавливаем ожидаемый результат количества инстансов
        _repositories.InstanceRepositoryMock.Setup(repo => 
                repo.CountAsync(It.IsAny<ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>>()))
            .ReturnsAsync(8);
        
        // Устанавливаем ожидаемый результат системных прокси, которые будут живы ещё минимум 1 сутки
        _repositories.SystemProxyRepositoryMock.Setup(repo => 
                repo.CountAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>()))
            .ReturnsAsync(0);
        
        // Устанавливаем ожидаемый результат прокси, время жизни которых менее 1 суток.
        _repositories.SystemProxyRepositoryMock.Setup(repo => 
                repo.FindAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>(), null, 0, 3))
            .ReturnsAsync(new List<SystemProxyAggregate> { ProxyPurchasingServiceTestsMockData.ExpiringProxyMock });
        
        // Устанавливаем ожидаемый результат для продленных прокси
        _services.ProxyClientMock.Setup(client =>
                client.ExtendAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ExtendResult> { ProxyPurchasingServiceTestsMockData.ProlongedProxyMock });
        
        // Устанавливаем ожидаемый результат для количества доступных прокси в указанной стране
        _services.ProxyClientMock.Setup(client =>
                client.GetCountAvailableInCountryAsync(It.IsAny<string>(), It.IsAny<IpVersion>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        
        // Устанавливаем ожидаемый результат для купленных прокси
        _services.ProxyClientMock.Setup(client => client
                .BuyProxyForCountryAsync(It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<int>(), It.IsAny<ProxyType>(),
                    It.IsAny<IpVersion>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new [] { ProxyPurchasingServiceTestsMockData.BoughtProxyMock });
        
        // Act
        // Вызываем метод, который тестируем
        await _service.PurchaseProxyIfNeededAsync();
        
        // Assert
        // Проверяем, что метод ExtendAsync был вызван ровно 1 раз
        _services.ProxyClientMock.Verify(client => 
            client.ExtendAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод UpdateAsync был вызван ровно 1 раз
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.UpdateAsync(It.IsAny<SystemProxyAggregate>()), Times.Exactly(1));
        
        // Проверяем, что метод GetCountAvailableInCountryAsync был вызван 1 раз
        _services.ProxyClientMock.Verify(client => 
            client.GetCountAvailableInCountryAsync(It.IsAny<string>(), 
                It.IsAny<IpVersion>(),It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод BuyProxyForCountryAsync был вызван 1 раз
        _services.ProxyClientMock.Verify(client =>
            client.BuyProxyForCountryAsync(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<ProxyType>(),
                It.IsAny<IpVersion>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод AddAsync был вызван 1 раз
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.AddAsync(It.IsAny<SystemProxyAggregate>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
    }

    /// <summary>
    /// Тест, который проверяет что будет куплено 1 прокси, если количество инстансов и доступных прокси в системе равно 0
    /// </summary>
    [Fact]
    public async Task PurchaseProxyIfNeededAsync_BuyProxyWhenInstancesAndProxyIsNull()
    {
        // Arrange
        // Устанавливаем ожидаемый результат количества инстансов
        _repositories.InstanceRepositoryMock.Setup(repo => 
                repo.CountAsync(It.IsAny<ISpecification<InstanceAggregate, IInstanceSpecificationVisitor>>()))
            .ReturnsAsync(0);
        
        // Устанавливаем ожидаемый результат системных прокси, которые будут живы ещё минимум 1 сутки
        _repositories.SystemProxyRepositoryMock.Setup(repo => 
                repo.CountAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>()))
            .ReturnsAsync(0);
        
        // Устанавливаем ожидаемый результат прокси, время жизни которых менее 1 суток.
        _repositories.SystemProxyRepositoryMock.Setup(repo => 
                repo.FindAsync(It.IsAny<ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>>(), null, 0, 1))
            .ReturnsAsync(new List<SystemProxyAggregate>());
        
        // Устанавливаем ожидаемый результат для количества доступных прокси в указанной стране
        _services.ProxyClientMock.Setup(client =>
                client.GetCountAvailableInCountryAsync(It.IsAny<string>(), It.IsAny<IpVersion>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        
        // Устанавливаем ожидаемый результат для купленных прокси
        _services.ProxyClientMock.Setup(client => client
                .BuyProxyForCountryAsync(It.IsAny<int>(), It.IsAny<string>(),
                    It.IsAny<int>(), It.IsAny<ProxyType>(),
                    It.IsAny<IpVersion>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new [] { ProxyPurchasingServiceTestsMockData.BoughtProxyMock });
        
        // Act
        // Вызываем метод, который тестируем
        await _service.PurchaseProxyIfNeededAsync();
        
        // Assert
        // Проверяем, что метод ExtendAsync не был вызван
        _services.ProxyClientMock.Verify(client => 
            client.ExtendAsync(It.IsAny<IEnumerable<string>>(), It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never());
        
        // Проверяем, что метод UpdateAsync не был вызван
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.UpdateAsync(It.IsAny<SystemProxyAggregate>()), Times.Never());
        
        // Проверяем, что метод GetCountAvailableInCountryAsync был вызван 1 раз
        _services.ProxyClientMock.Verify(client => 
            client.GetCountAvailableInCountryAsync(It.IsAny<string>(), 
                It.IsAny<IpVersion>(),It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод BuyProxyForCountryAsync был вызван 1 раз
        _services.ProxyClientMock.Verify(client =>
            client.BuyProxyForCountryAsync(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<int>(), It.IsAny<ProxyType>(),
                It.IsAny<IpVersion>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
        
        // Проверяем, что метод AddAsync был вызван 1 раз
        _repositories.SystemProxyRepositoryMock.Verify(repo =>
            repo.AddAsync(It.IsAny<SystemProxyAggregate>(), It.IsAny<CancellationToken>()), Times.Exactly(1));
    }
}