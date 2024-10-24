using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TgInstanceAgent.Domain.SystemProxy.Enums;
using TgInstanceAgent.Infrastructure.Storage.Entities.Abstractions;
using TgInstanceAgent.Infrastructure.Storage.Entities.Instances;

namespace TgInstanceAgent.Infrastructure.Storage.Entities.SystemProxies;

/// <summary>
/// Модель системного прокси в EntityFramework.
/// </summary>
public class SystemProxyModel : IAggregateModel
{
    /// <summary>
    /// Идентификатор системного прокси.
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор прокси во внешней системе
    /// </summary>
    [MaxLength(60)]
    public string IdInProviderSystem { get; set; } = null!;
    
    /// <summary>
    /// IP адрес прокси
    /// </summary>
    [MaxLength(60)]
    public string Ip { get; set; } = null!;
    
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
    public string Login { get; set; } = null!;

    /// <summary>
    /// Пароль для аутентификации на прокси.
    /// </summary>
    [MaxLength(60)]
    public string Password { get; set; } = null!;

    /// <summary>
    /// Время истечения срока действия в формате UTC.
    /// </summary>
    public DateTime ExpirationTimeUtc { get; set; }

    /// <summary>
    /// Инстансы.
    /// </summary>
    public List<InstanceModel> Instances { get; set; } = [];
    
    /// <summary>
    /// Тип прокси.
    /// </summary>
    public ProxyType Type { get; set; }
    
    /// <summary>
    /// Версия прокси
    /// </summary>
    public IpVersion IpVersion { get; set; }
}