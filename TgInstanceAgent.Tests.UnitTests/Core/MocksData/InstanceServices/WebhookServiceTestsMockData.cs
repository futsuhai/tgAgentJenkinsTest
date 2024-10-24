using TgInstanceAgent.Application.Abstractions.TelegramClient.Events;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Domain.Instances.ValueObjects;

namespace TgInstanceAgent.Tests.UnitTests.Core.MocksData.InstanceServices;

/// <summary>
/// Класс, содержащий моковые данные для тестирования сервиса отправки вебхуков
/// </summary>
public static class WebhookServiceTestsMockData
{
    /// <summary>
    /// Моковый идентификатор инстанса
    /// </summary>
    public static readonly Guid InstanceIdMock = Guid.NewGuid();

    /// <summary>
    /// Моковая модель инстанса
    /// </summary>
    public static readonly InstanceAggregate InstanceWithoutWeebhooksMock =
        new(Guid.NewGuid(), DateTime.Now.AddHours(24), Guid.NewGuid())
        {
            WebhookSetting = new WebhookSetting
            {
                Chats = false,
                Messages = false,
                Files = false,
                Users = false,
                Stories = false,
                Other = false
            }
        };
    
    /// <summary>
    /// Моковая модель инстанса
    /// </summary>
    public static readonly InstanceAggregate InstanceWithWeebhooksMock =
        new(Guid.NewGuid(), DateTime.Now.AddHours(24), Guid.NewGuid())
        {
            WebhookSetting = new WebhookSetting
            {
                Chats = true,
                Messages = true,
                Files = true,
                Users = true,
                Stories = true,
                Other = true
            }
        };

    /// <summary>
    /// Моковая модель события
    /// </summary>
    public static readonly TgAuthenticatedEvent TgAuthenticatedEventMock = new();

    /// <summary>
    /// Моковая модель события
    /// </summary>
    public static readonly TgUpdateChatMessageAutoDeleteTimeEvent TgUpdateChatMessageAutoDeleteTimeEventMock = new()
    {
        ChatId = 1,
        MessageAutoDeleteTime = 1,
    };
}