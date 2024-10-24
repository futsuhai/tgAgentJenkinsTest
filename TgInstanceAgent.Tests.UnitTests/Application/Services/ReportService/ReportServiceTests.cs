using Moq;
using TgInstanceAgent.Application.Abstractions.ReportService;
using TgInstanceAgent.Domain.Reports;
using TgInstanceAgent.Domain.Reports.Specifications.Visitor;
using TgInstanceAgent.Domain.Specifications.Abstractions;
using TgInstanceAgent.Tests.UnitTests.Core.MocksData.ReportService;
using TgInstanceAgent.Tests.UnitTests.Core.MockServices;
using TgInstanceAgent.Tests.UnitTests.Core.MocksRepositories;
using Xunit;

namespace TgInstanceAgent.Tests.UnitTests.Application.Services.ReportService;

/// <summary>
/// Класс для тестирования сервиса отчётов по сообщениям
/// </summary>
public class ReportServiceTests : IDisposable
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
    private IReportService _service;

    /// <summary>
    /// Конструктор для инициализации полей
    /// </summary>
    public ReportServiceTests()
    {
        // Создаем новый экземпляр MockRepositories для использования в тестах
        _repositories = new MockRepositories();
        
        // Создаем новый экземпляр MockServices для использования в тестах
        _services = new MockServices();

        // Создаем тестируемый экземпляр IReportService
        _service = _services.CreateReportService(_repositories);
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
        _service = _services.CreateReportService(_repositories);

        // Сбрасываем моковые модели
        ReportServiceTestsMockData.ResetMockData();
    }
    
    /// <summary>
    /// Тест проверяет, что метод ProcessMessage возвращает управление, 
    /// когда у сообщения есть идентификатор потока (MessageThreadId).
    /// </summary>
    [Fact]
    public async Task ProcessMessage_hen_MessageIsThreadId_Return()
    {
        // Arrange
        // Для теста не требуется дополнительная подготовка данных, только моковые модели
        
        // Act
        // Вызываем метод ProcessMessage с моковым идентификатором инстанса и сообщением
        await _service.ProcessMessage(ReportServiceTestsMockData.MockInstanceId1, ReportServiceTestsMockData.MockWithMessageThreadIdMessage);

        // Assert
        // Проверяем, что метод AddAsync в репозитории не был вызван
        _repositories.ReportRepositoryMock.Verify(repo => 
            repo.AddAsync(It.IsAny<ReportAggregate>(), It.IsAny<CancellationToken>()), Times.Never());
        
        // Проверяем, что метод UpdateAsync в репозитории не был вызван
        _repositories.ReportRepositoryMock.Verify(repo => 
            repo.UpdateAsync(It.IsAny<ReportAggregate>()), Times.Never());
    }
    
    /// <summary>
    /// Тест проверяет, что метод ProcessMessage возвращает управление, 
    /// когда сообщение является постом в канале (IsChannelPost).
    /// </summary>
    [Fact]
    public async Task ProcessMessage_MessageIsChannelPost_Return()
    {
        // Arrange
        // Для теста не требуется дополнительная подготовка данных, только моковые модели
    
        // Act
        // Вызываем метод ProcessMessage с моковым идентификатором инстанса и сообщением
        await _service.ProcessMessage(ReportServiceTestsMockData.MockInstanceId1, ReportServiceTestsMockData.MockChannelPostMessage);

        // Assert
        // Проверяем, что метод AddAsync в репозитории не был вызван
        _repositories.ReportRepositoryMock.Verify(repo => 
            repo.AddAsync(It.IsAny<ReportAggregate>(), It.IsAny<CancellationToken>()), Times.Never());
    
        // Проверяем, что метод UpdateAsync в репозитории не был вызван
        _repositories.ReportRepositoryMock.Verify(repo => 
            repo.UpdateAsync(It.IsAny<ReportAggregate>()), Times.Never());
    }
    
    /// <summary>
    /// Тест проверяет, что метод ProcessMessage возвращает управление, 
    /// когда сообщение является сообщением в теме форума (IsTopicMessage).
    /// </summary>
    [Fact]
    public async Task ProcessMessage_MessageIsTopicMessage_Return()
    {
        // Arrange
        // Для теста не требуется дополнительная подготовка данных, только моковые модели
    
        // Act
        // Вызываем метод ProcessMessage с моковым идентификатором инстанса и сообщением
        await _service.ProcessMessage(ReportServiceTestsMockData.MockInstanceId1, ReportServiceTestsMockData.MockTopicMessage);

        // Assert
        // Проверяем, что метод AddAsync в репозитории не был вызван
        _repositories.ReportRepositoryMock.Verify(repo => 
            repo.AddAsync(It.IsAny<ReportAggregate>(), It.IsAny<CancellationToken>()), Times.Never());
    
        // Проверяем, что метод UpdateAsync в репозитории не был вызван
        _repositories.ReportRepositoryMock.Verify(repo => 
            repo.UpdateAsync(It.IsAny<ReportAggregate>()), Times.Never());
    }
    
    /// <summary>
    /// Тест проверяет, что создается новый отчёт, если он не найден в кэше.
    /// </summary>
    [Fact]
    public async Task ProcessMessage_ReportNotFounded_CreateNewReport()
    {
        // Arrange
        // Настраиваем поведение репозитория так, чтобы он возвращал пустой список,
        // имитируя отсутствие отчетов в базе данных.
        _repositories.ReportRepositoryMock.Setup(repo => 
                repo.FindAsync(It.IsAny<ISpecification<ReportAggregate, IReportSpecificationVisitor>>(), null, 0, 0))
            .ReturnsAsync(new List<ReportAggregate>());  // Возвращаем пустой список вместо null

        // Настраиваем кэш так, чтобы он возвращал найденный отчет из моков.
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object report) =>
            {
                report = default!;
                return true;
            });
        
        // Act
        // Вызываем метод ProcessMessage, передавая моковый идентификатор инстанса и сообщение.
        await _service.ProcessMessage(ReportServiceTestsMockData.MockInstanceId1, ReportServiceTestsMockData.MockSentMessage);

        // Assert
        // Проверяем, что метод AddAsync был вызван ровно один раз, 
        // что подтверждает создание и сохранение нового отчета.
        _repositories.ReportRepositoryMock.Verify(repo => 
            repo.AddAsync(It.IsAny<ReportAggregate>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    /// <summary>
    /// Тест проверяет, что существующий отчёт обновляется, если он найден в кэше.
    /// </summary>
    [Fact]
    public async Task ProcessMessage_ReportFounded_UpdateExistingReport()
    {
        // Arrange
        // Настраиваем поведение репозитория так, чтобы он возвращал моковый отчёт,
        _repositories.ReportRepositoryMock.Setup(repo => 
                repo.FindAsync(It.IsAny<ISpecification<ReportAggregate, IReportSpecificationVisitor>>(), null, 0, 0))
            .ReturnsAsync(new List<ReportAggregate> { ReportServiceTestsMockData.MockReport });

        // Настраиваем кэш так, чтобы он возвращал найденный отчет из моков.
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object report) =>
            {
                report = ReportServiceTestsMockData.MockReport;
                return true;
            });

        // Act
        // Вызываем метод ProcessMessage, передавая моковый идентификатор инстанса и сообщение.
        await _service.ProcessMessage(ReportServiceTestsMockData.MockInstanceId1, ReportServiceTestsMockData.MockSentMessage);

        // Assert
        // Проверяем, что метод UpdateAsync был вызван ровно один раз, 
        // что подтверждает обновление существующего отчета.
        _repositories.ReportRepositoryMock.Verify(repo => 
            repo.UpdateAsync(It.IsAny<ReportAggregate>()), Times.Once);

        // Проверяем, что отчет был установлен в кэш после обновления.
        _services.MemoryCacheMock.Verify(cache =>
            cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!));
    }
    
    /// <summary>
    /// Тест проверяет, что счетчик отправленных сообщений увеличивается, если сообщение является исходящим.
    /// </summary>
    [Fact]
    public async Task ProcessMessage_MessageIsOutgoing_IncrementSentCounter()
    {
        // Arrange
        // Настраиваем поведение репозитория так, чтобы он возвращал моковый отчёт,
        _repositories.ReportRepositoryMock.Setup(repo => 
                repo.FindAsync(It.IsAny<ISpecification<ReportAggregate, IReportSpecificationVisitor>>(), null, 0, 0))
            .ReturnsAsync(new List<ReportAggregate> { ReportServiceTestsMockData.MockReport });

        // Настраиваем кэш так, чтобы он возвращал найденный отчет из моков.
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object report) =>
            {
                report = ReportServiceTestsMockData.MockReport;
                return true;
            });
        
        // Act
        // Вызываем метод ProcessMessage, передавая моковый идентификатор инстанса и сообщение.
        await _service.ProcessMessage(ReportServiceTestsMockData.MockInstanceId1, ReportServiceTestsMockData.MockSentMessage);
        
        // Assert
        // Проверяем, что счётчик отправленных сообщений увеличился
        Assert.Equal(1, (int)ReportServiceTestsMockData.MockReport.Sent);
    }
    
    /// <summary>
    /// Тест проверяет, что счетчик полученных сообщений увеличивается, если сообщение является входящим.
    /// </summary>
    [Fact]
    public async Task ProcessMessage_MessageIsNotOutgoing_IncrementSentCounter()
    {
        // Arrange
        // Настраиваем поведение репозитория так, чтобы он возвращал моковый отчёт,
        _repositories.ReportRepositoryMock.Setup(repo => 
                repo.FindAsync(It.IsAny<ISpecification<ReportAggregate, IReportSpecificationVisitor>>(), null, 0, 0))
            .ReturnsAsync(new List<ReportAggregate> { ReportServiceTestsMockData.MockReport });

        // Настраиваем кэш так, чтобы он возвращал найденный отчет из моков.
        _services.MemoryCacheMock.Setup(cache =>
                cache.TryGetValue(It.IsAny<object>(), out It.Ref<object>.IsAny!))
            .Returns((object _, out object report) =>
            {
                report = ReportServiceTestsMockData.MockReport;
                return true;
            });
        
        // Act
        // Вызываем метод ProcessMessage, передавая моковый идентификатор инстанса и сообщение.
        await _service.ProcessMessage(ReportServiceTestsMockData.MockInstanceId1, ReportServiceTestsMockData.MockReceivedMessage);
        
        // Assert
        // Проверяем, что счётчик полученных сообщений увеличился
        Assert.Equal(1, (int)ReportServiceTestsMockData.MockReport.Received);
    }
}
