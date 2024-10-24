using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Messages;
using TgInstanceAgent.Domain.Reports;

namespace TgInstanceAgent.Tests.UnitTests.Core.MocksData.ReportService;

/// <summary>
/// Класс, содержащий моковые данные для тестирования сервиса отчётов.
/// </summary>
public static class ReportServiceTestsMockData
{
    /// <summary>
    /// Моковый идентификатор инстанса
    /// </summary>
    public static readonly Guid MockInstanceId1 = Guid.NewGuid();
    
    /// <summary>
    /// Моковый идентификатор инстанса
    /// </summary>
    public static readonly Guid MockInstanceId2 = Guid.NewGuid();
    
    /// <summary>
    /// Моковый отчёт для тестирования
    /// </summary>
    public static ReportAggregate MockReport = new(
        Guid.NewGuid(),
        0,
        0,
        DateOnly.FromDateTime(DateTime.Today),
        MockInstanceId1
    );

    /// <summary>
    /// Моковое сообщение, содержащее идентификатор потока
    /// </summary>
    public static readonly TgMessage MockWithMessageThreadIdMessage = new()
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
        AuthorSignature = null,
        MediaAlbumId = null,
        Date = DateTime.Today,
        EditDate = null,
        ReplyTo = null,
        ForwardInfo = null,
        SchedulingState = null,
        Content = new TgTextMessage { Text = new TgFormatedText { Text = "Test Message!" } }
    };
    
    /// <summary>
    /// Моковое сообщение - пост в канале
    /// </summary>
    public static readonly TgMessage MockChannelPostMessage = new()
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
        IsChannelPost = true,
        IsTopicMessage = false,
        MessageThreadId = null,
        AuthorSignature = null,
        MediaAlbumId = null,
        Date = DateTime.Today,
        EditDate = null,
        ReplyTo = null,
        ForwardInfo = null,
        SchedulingState = null,
        Content = new TgTextMessage { Text = new TgFormatedText { Text = "Test Message!" } }
    };
    
    /// <summary>
    /// Моковое сообщение, представляющее собой тему форума
    /// </summary>
    public static readonly TgMessage MockTopicMessage= new()
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
        IsTopicMessage = true,
        MessageThreadId = null,
        AuthorSignature = null,
        MediaAlbumId = null,
        Date = DateTime.Today,
        EditDate = null,
        ReplyTo = null,
        ForwardInfo = null,
        SchedulingState = null,
        Content = new TgTextMessage { Text = new TgFormatedText { Text = "Test Message!" } }
    };
    
    /// <summary>
    /// Моковое сообщение, представляющее собой отправленное текстовое сообщение
    /// </summary>
    public static readonly TgMessage MockSentMessage = new()
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
        MessageThreadId = null,
        AuthorSignature = null,
        MediaAlbumId = null,
        Date = DateTime.Today,
        EditDate = null,
        ReplyTo = null,
        ForwardInfo = null,
        SchedulingState = null,
        Content = new TgTextMessage { Text = new TgFormatedText { Text = "Test Message!" } }
    };
    
    /// <summary>
    /// Моковое сообщение, представляющее собой полученное текстовое сообщение
    /// </summary>
    public static readonly TgMessage MockReceivedMessage = new()
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
        MessageThreadId = null,
        AuthorSignature = null,
        MediaAlbumId = null,
        Date = DateTime.Today,
        EditDate = null,
        ReplyTo = null,
        ForwardInfo = null,
        SchedulingState = null,
        Content = new TgTextMessage { Text = new TgFormatedText { Text = "Test Message!" } }
    };
    
    // Метод для сброса состояния моковых моделей
    public static void ResetMockData()
    {
        // Сбрасываем состояние отчета
        MockReport = new ReportAggregate(
            Guid.NewGuid(),
            0,
            0,
            DateOnly.FromDateTime(DateTime.Today),
            MockInstanceId1
        );
    }
}