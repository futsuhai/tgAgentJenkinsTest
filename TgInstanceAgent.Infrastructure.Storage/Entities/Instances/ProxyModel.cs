using System.ComponentModel.DataAnnotations;
using TgInstanceAgent.Domain.Instances.Enums;
using TgInstanceAgent.Domain.SystemProxy.Enums;

namespace TgInstanceAgent.Infrastructure.Storage.Entities.Instances;

/// <summary>
/// Модель прокси в EntityFramework.
/// </summary>
public class ProxyModel
{
    /// <summary>
    /// Хост прокси.
    /// </summary>
    [MaxLength(60)]
    public string Host { get; set; } = null!;

    /// <summary>
    /// Порт прокси.
    /// </summary>
    public ushort Port { get; set; }

    /// <summary>
    /// Логин для аутентификации на прокси.
    /// </summary>
    [MaxLength(60)]
    public string? Login { get; set; }

    /// <summary>
    /// Пароль для аутентификации на прокси.
    /// </summary>
    [MaxLength(60)]
    public string? Password { get; set; }

    /// <summary>
    /// Тип прокси.
    /// </summary>
    public ProxyType Type { get; set; }

    /// <summary>
    /// Время истечения в формате UTC.
    /// </summary>
    public DateTime? ExpirationTimeUtc { get; set; }

    /// <summary>
    /// Идентификатор инстанса.
    /// </summary>
    public Guid InstanceId { get; set; }
}