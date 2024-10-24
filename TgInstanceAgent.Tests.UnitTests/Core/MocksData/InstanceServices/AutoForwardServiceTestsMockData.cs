using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Domain.Instances.ValueObjects;

namespace TgInstanceAgent.Tests.UnitTests.Core.MocksData.InstanceServices;

/// <summary>
/// Класс, содержащий моковые данные для тестирования сервиса автоматической пересылки сообщений.
/// </summary>
public static class AutoForwardServiceTestsMockData
{
    /// <summary>
    /// Моковый идентификатор инстанса
    /// </summary>
    public static readonly Guid MockInstanceId = Guid.NewGuid();
    
    /// <summary>
    /// Моковая пересылка сообщений
    /// </summary>
    public static readonly ForwardEntry MockForwardEntry = new(1, 10, false);

    /// <summary>
    /// Моковое исключение клиента
    /// </summary>
    public static readonly ClientException MockClientException = new(400, "Bad Request");
    
    /// <summary>
    /// Моковый агрегат инстанса
    /// </summary>
    public static InstanceAggregate MockInstanceAggregate = new(new Guid(), DateTime.Now.AddHours(24), new Guid());
    
    /// <summary>
    /// Моковое исходящее сообщение.
    /// </summary>
    public static readonly TgMessage MockMessageOutgoing = new()
    {
        ChatId = 1,
        SenderId = new TgMessageSenderUser { UserId = 1 },
        Id = 1001,
        IsOutgoing = true,
        CanBeEdited = false,
        CanBeForwarded = true,
        CanBeDeletedForAllUsers = true,
        IsPinned = false,
        CanGetAddedReactions = true,
        CanGetStatistics = true,
        CanGetReadDate = true,
        CanGetViewers = true,
        IsChannelPost = false,
        IsTopicMessage = false,
        MessageThreadId = 1,
        Date = DateTime.Today,
        Content = new TgTextMessage { Text = new TgFormatedText { Text = "Test Message!" } }
    };
    
    /// <summary>
    /// Моковое сообщение, не доступное для пересылки.
    /// </summary>
    public static readonly TgMessage MockMessageNotForward = new()
    {
        ChatId = 1,
        SenderId = new TgMessageSenderUser { UserId = 1 },
        Id = 1001,
        IsOutgoing = false,
        CanBeEdited = false,
        CanBeForwarded = false,
        CanBeDeletedForAllUsers = true,
        IsPinned = false,
        CanGetAddedReactions = true,
        CanGetStatistics = true,
        CanGetReadDate = true,
        CanGetViewers = true,
        IsChannelPost = false,
        IsTopicMessage = false,
        MessageThreadId = 1,
        Date = DateTime.Today,
        Content = new TgTextMessage { Text = new TgFormatedText { Text = "Test Message!" } }
    };
    
    /// <summary>
    /// Моковое сообщение
    /// </summary>
    public static readonly TgMessage MockMessage = new()
    {
        ChatId = 1,
        SenderId = new TgMessageSenderUser { UserId = 1 },
        Id = 1001,
        IsOutgoing = false,
        CanBeEdited = false,
        CanBeForwarded = true,
        CanBeDeletedForAllUsers = true,
        IsPinned = false,
        CanGetAddedReactions = true,
        CanGetStatistics = true,
        CanGetReadDate = true,
        CanGetViewers = true,
        IsChannelPost = false,
        IsTopicMessage = false,
        MessageThreadId = 1,
        Date = DateTime.Today,
        Content = new TgTextMessage { Text = new TgFormatedText { Text = "Test Message!" } }
    };

    // Метод для сброса состояния моковых моделей
    public static void ResetMockData()
    {
        // Сбрасываем состояние инстанса
        MockInstanceAggregate = new InstanceAggregate(new Guid(), DateTime.Now.AddHours(24), new Guid());
    }
}