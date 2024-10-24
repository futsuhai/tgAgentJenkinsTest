using TgInstanceAgent.Domain.Instances.Enums;
using TgInstanceAgent.Domain.Instances.ValueObjects;

namespace TgInstanceAgent.Application.Abstractions.DTOs.Instances;

/// <summary>
/// Класс, представляющий данные инстанса.
/// </summary>
public class InstanceDto
{
    /// <summary>
    /// Идентификатор инстанса.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Время истечения срока действия в формате UTC.
    /// </summary>
    public required DateTime ExpirationTimeUtc { get; init; }

    /// <summary>
    /// Состояние инстанса.
    /// </summary>
    public required State State { get; init; }

    /// <summary>
    /// Включен ли инстанс.
    /// </summary>
    public required bool Enabled { get; init; }
    
    /// <summary>
    /// Прокси-сервер.
    /// </summary>
    public Proxy? Proxy { get; init; }

    /// <summary>
    /// Ограничения.
    /// </summary>
    public required Restrictions Restrictions { get; init; }
    
    /// <summary>
    /// Настройки вебхука.
    /// </summary>
    public required WebhookSetting WebhookSetting { get; init; }
    
    /// <summary>
    /// Пересылки сообщений
    /// </summary>
    public required List<ForwardEntry> ForwardEntries { get; init; }
    
    /// <summary>
    /// Коллекция ссылок
    /// </summary>
    public required List<Uri> WebhookUrls { get; init; } 
}