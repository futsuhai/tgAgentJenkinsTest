using TgInstanceAgent.Application.Abstractions.Commands.Instances;

namespace TgInstanceAgent.Infrastructure.CommandsStore.Entities;

/// <summary>
/// Модель команды.
/// </summary>
public class CommandModel
{
    /// <summary>
    /// Идентификатор команды
    /// </summary>
    public Guid CommandId { get; init; }
    
    /// <summary>
    /// Уникальный идентификатор инстанса.
    /// </summary>
    public Guid InstanceId { get; set; }
    
    /// <summary>
    /// Время, в которое была выполнена команда
    /// </summary>
    public DateTime TimeStamp  { get; init; }
    
    /// <summary>
    /// Логи AddInstanceCommand
    /// </summary>
    public AddInstanceCommand? AddInstanceCommand  { get; set; }
    
    /// <summary>
    /// Логи AddWebhookUrlCommand
    /// </summary>
    public AddWebhookUrlCommand? AddWebhookUrlCommand  { get; set; }
    
    /// <summary>
    /// Логи RemoveForwardEntryCommand
    /// </summary>
    public RemoveForwardEntryCommand? RemoveForwardEntryCommand  { get; set; }
    
    /// <summary>
    /// Логи RemoveWebhookUrlCommand
    /// </summary>
    public RemoveWebhookUrlCommand? RemoveWebhookUrlCommand  { get; set; }
    
    /// <summary>
    /// Логи UpdateWebhookSettingCommand
    /// </summary>
    public UpdateWebhookSettingCommand? UpdateWebhookSettingCommand  { get; set; }
    
    /// <summary>
    /// Логи AddForwardEntryCommand
    /// </summary>
    public AddForwardEntryCommand? AddForwardEntryCommand  { get; set; }
    
    /// <summary>
    /// Логи StartInstanceCommand
    /// </summary>
    public StartInstanceCommand? StartInstanceCommand  { get; set; }
    
    /// <summary>
    /// Логи StopInstanceCommand
    /// </summary>
    public StopInstanceCommand? StopInstanceCommand  { get; set; }
}