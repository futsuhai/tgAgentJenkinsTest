using Microsoft.EntityFrameworkCore;
using TgInstanceAgent.Infrastructure.Storage.Entities;
using TgInstanceAgent.Infrastructure.Storage.Entities.Instances;
using TgInstanceAgent.Infrastructure.Storage.Entities.Reports;
using TgInstanceAgent.Infrastructure.Storage.Entities.SystemProxies;

namespace TgInstanceAgent.Infrastructure.Storage.Context;

/// <summary> 
/// Класс контекста базы данных приложения. 
/// </summary> 
/// <param name="options">Параметры контекста базы данных.</param> 
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{ 
    /// <summary> 
    /// Коллекция инстансов модели InstanceModel. 
    /// </summary> 
    public DbSet<InstanceModel> Instances { get; set; } = null!; 
 
    /// <summary> 
    /// Коллекция инстансов модели ProxyModel. 
    /// </summary> 
    public DbSet<ProxyModel> Proxies { get; set; } = null!;
    
    /// <summary> 
    /// Коллекция инстансов модели ForwardEntryModel. 
    /// </summary> 
    public DbSet<ForwardEntryModel> ForwardEntries { get; set; } = null!;

    /// <summary> 
    /// Коллекция инстансов модели Restrictions. 
    /// </summary> 
    public DbSet<RestrictionsModel> Restrictions { get; set; } = null!;
    
    /// <summary>
    /// Коллекция URL-адресов вебхуков модели <see cref="WebhookUrlModel"/>.
    /// Хранит данные о URL-адресах, связанных с вебхуками для инстансов.
    /// </summary>
    public DbSet<WebhookUrlModel> WebhookUrls { get; set; } = null!;

    /// <summary>
    /// Коллекция настроек вебхуков модели <see cref="WebhookSettingModel"/>.
    /// Хранит данные о настройках вебхуков, таких как флаги для обработки сообщений, чатов и групп.
    /// </summary>
    public DbSet<WebhookSettingModel> WebhookSettings { get; set; } = null!;
    
    /// <summary> 
    /// Коллекция отчетов модели ReportModel. 
    /// </summary> 
    public DbSet<ReportModel> Reports { get; set; } = null!; 
    
    /// <summary> 
    /// Коллекция инстансов модели SystemProxyModel. 
    /// </summary> 
    public DbSet<SystemProxyModel> SystemProxies { get; set; } = null!; 

    /// <summary> 
    /// Настраивает модель базы данных при создании контекста. 
    /// </summary> 
    /// <param name="modelBuilder">Построитель модели базы данных.</param> 
    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    { 
        // Определяем отношение один к одному между моделями InstanceModel и ProxyModel. 
        modelBuilder.Entity<InstanceModel>()
            .HasOne(x => x.Proxy)
            .WithOne()
            .HasForeignKey<ProxyModel>(x => x.InstanceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Определяем отношение один к одному между моделями InstanceModel и RestrictionsModel. 
        modelBuilder.Entity<InstanceModel>()
            .HasOne(x => x.Restrictions)
            .WithOne()
            .HasForeignKey<RestrictionsModel>(x => x.InstanceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Определяем отношение один ко многим между моделями InstanceModel и WebhookUrlModel.
        modelBuilder.Entity<InstanceModel>()
            .HasMany(x => x.WebhookUrls)
            .WithOne()
            .HasForeignKey(x => x.InstanceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Определяем отношение один к одному между моделями InstanceModel и WebhookSettingModel.
        modelBuilder.Entity<InstanceModel>()
            .HasOne(x => x.WebhookSetting)
            .WithOne()
            .HasForeignKey<WebhookSettingModel>(x => x.InstanceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Определяем отношение один к многим между моделями InstanceModel и SystemProxyModel. 
        modelBuilder.Entity<InstanceModel>()
            .HasOne<SystemProxyModel>()
            .WithMany(p => p.Instances)
            .HasForeignKey(x => x.SystemProxyId)
            .OnDelete(DeleteBehavior.SetNull);

        // Определяем ключ для модели WebhookUrlModel.
        modelBuilder.Entity<WebhookUrlModel>()
            .HasKey(u => new { u.Url, u.InstanceId });

        // Определяем ключ для модели WebhookSettingModel.
        modelBuilder.Entity<WebhookSettingModel>()
            .HasKey(w => new { w.InstanceId });

        // Определяем ключ для модели RestrictionsModel.
        modelBuilder.Entity<RestrictionsModel>()
            .HasKey(w => new { w.InstanceId });

        // Определяем ключ для модели ProxyModel.
        modelBuilder.Entity<ProxyModel>()
            .HasKey(w => new { w.InstanceId });
        
        // Определяем уникальный индекс на поля InstanceId и Date в таблице Reports.
        modelBuilder.Entity<ReportModel>()
            .HasIndex(r => new { r.InstanceId, r.Date })
            .IsUnique();

        // Вызов базовой реализации метода OnModelCreating для применения настроек модели.
        base.OnModelCreating(modelBuilder);
    } 
}