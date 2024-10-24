using Microsoft.EntityFrameworkCore;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Infrastructure.CommandsStore.Entities;

namespace TgInstanceAgent.Infrastructure.CommandsStore.Context;

/// <summary> 
/// Класс контекста базы данных логгирования команд. 
/// </summary> 
/// <param name="options">Параметры контекста базы данных.</param> 
public class CommandsDbContext(DbContextOptions<CommandsDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Коллекция команд.
    /// </summary>
    public DbSet<CommandModel> Commands { get; set; } = null!;
    
    /// <summary>
    /// Коллекция команд AddInstanceCommand
    /// </summary>
    public DbSet<AddInstanceCommand> AddInstanceCommands  { get; set; } = null!;
    
    /// <summary>
    /// Коллекция команд AddWebhookUrlCommand
    /// </summary>
    public DbSet<AddWebhookUrlCommand> AddWebhookUrlCommands  { get; set; } = null!;
    
    /// <summary>
    /// Коллекция команд RemoveForwardEntryCommand
    /// </summary>
    public DbSet<RemoveForwardEntryCommand> RemoveForwardEntryCommands  { get; set; } = null!;
    
    /// <summary>
    /// Коллекция команд RemoveWebhookUrlCommand
    /// </summary>
    public DbSet<RemoveWebhookUrlCommand> RemoveWebhookUrlCommands  { get; set; } = null!;
    
    /// <summary>
    /// Коллекция команд UpdateWebhookSettingCommand
    /// </summary>
    public DbSet<UpdateWebhookSettingCommand> UpdateWebhookSettingCommands  { get; set; } = null!;
    
    /// <summary>
    /// Коллекция команд AddForwardEntryCommand
    /// </summary>
    public DbSet<AddForwardEntryCommand> AddForwardEntryCommands  { get; set; } = null!;
    
    /// <summary>
    /// Коллекция команд StartInstanceCommand.
    /// </summary>
    public DbSet<StartInstanceCommand> StartInstanceCommands { get; set; } = null!;
    
    /// <summary>
    /// Коллекция команд StopInstanceCommand.
    /// </summary>
    public DbSet<StopInstanceCommand> StopInstanceCommands { get; set; } = null!;
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommandModel>()
            .HasKey(c => c.CommandId);
        
        // Определяем отношение один к одному между моделями CommandModel и AddInstanceCommand. 
        modelBuilder.Entity<CommandModel>()
            .HasOne(c => c.AddInstanceCommand)
            .WithOne()
            .HasForeignKey<AddInstanceCommand>(c => c.CommandId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Определяем отношение один к одному между моделями CommandModel и AddWebhookUrlCommand. 
        modelBuilder.Entity<CommandModel>()
            .HasOne(c => c.AddWebhookUrlCommand)
            .WithOne()
            .HasForeignKey<AddWebhookUrlCommand>(c => c.CommandId)
            .OnDelete(DeleteBehavior.Cascade);
                
        // Определяем отношение один к одному между моделями CommandModel и RemoveForwardEntryCommand. 
        modelBuilder.Entity<CommandModel>()
            .HasOne(c => c.RemoveForwardEntryCommand)
            .WithOne()
            .HasForeignKey<RemoveForwardEntryCommand>(c => c.CommandId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Определяем отношение один к одному между моделями CommandModel и RemoveWebhookUrlCommand. 
        modelBuilder.Entity<CommandModel>()
            .HasOne(c => c.RemoveWebhookUrlCommand)
            .WithOne()
            .HasForeignKey<RemoveWebhookUrlCommand>(c => c.CommandId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Определяем отношение один к одному между моделями CommandModel и UpdateWebhookSettingCommand. 
        modelBuilder.Entity<CommandModel>()
            .HasOne(c => c.UpdateWebhookSettingCommand)
            .WithOne()
            .HasForeignKey<UpdateWebhookSettingCommand>(c => c.CommandId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Определяем отношение один к одному между моделями CommandModel и AddForwardEntryCommand. 
        modelBuilder.Entity<CommandModel>()
            .HasOne(c => c.AddForwardEntryCommand)
            .WithOne()
            .HasForeignKey<AddForwardEntryCommand>(c => c.CommandId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Определяем отношение один к одному между моделями CommandModel и StartInstanceCommand. 
        modelBuilder.Entity<CommandModel>()
            .HasOne(c => c.StartInstanceCommand)
            .WithOne()
            .HasForeignKey<StartInstanceCommand>(c => c.CommandId)
            .OnDelete(DeleteBehavior.Cascade);

        // Определяем отношение один к одному между моделями CommandModel и StopInstanceCommand. 
        modelBuilder.Entity<CommandModel>()
            .HasOne(c => c.StopInstanceCommand)
            .WithOne()
            .HasForeignKey<StopInstanceCommand>(c => c.CommandId)
            .OnDelete(DeleteBehavior.Cascade); 
        
        // Определяем ключ для модели AddInstanceCommand.
        modelBuilder.Entity<AddInstanceCommand>()
            .Ignore(c => c.InstanceId)
            .HasKey(c => new { c.CommandId });
        
        // Определяем ключ для модели AddWebhookUrlCommand.
        modelBuilder.Entity<AddWebhookUrlCommand>()
            .Ignore(c => c.InstanceId)
            .HasKey(c => new { c.CommandId });
        
        // Определяем ключ для модели RemoveForwardEntryCommand.
        modelBuilder.Entity<RemoveForwardEntryCommand>()
            .Ignore(c => c.InstanceId)
            .HasKey(c => new { c.CommandId });
        
        // Определяем ключ для модели RemoveWebhookUrlCommand.
        modelBuilder.Entity<RemoveWebhookUrlCommand>()
            .Ignore(c => c.InstanceId)
            .HasKey(c => new { c.CommandId });
        
        // Определяем ключ для модели UpdateWebhookSettingCommand.
        modelBuilder.Entity<UpdateWebhookSettingCommand>()
            .Ignore(c => c.InstanceId)
            .HasKey(c => new { c.CommandId });
        
        // Определяем ключ для модели AddForwardEntryCommand.
        modelBuilder.Entity<AddForwardEntryCommand>()
            .Ignore(c => c.InstanceId)
            .HasKey(c => new { c.CommandId });
        
        // Определяем ключ для модели StartInstanceCommand.
        modelBuilder.Entity<StartInstanceCommand>()
            .Ignore(c => c.InstanceId)
            .HasKey(c => new { c.CommandId });
        
        // Определяем ключ для модели StopInstanceCommand.
        modelBuilder.Entity<StopInstanceCommand>()
            .Ignore(c => c.InstanceId)
            .HasKey(c => new { c.CommandId });
        
        // Вызов базовой реализации метода OnModelCreating для применения настроек модели.
        base.OnModelCreating(modelBuilder);
    }
}