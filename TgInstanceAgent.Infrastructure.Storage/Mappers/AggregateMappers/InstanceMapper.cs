using System.Reflection;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Domain.Instances.ValueObjects;
using TgInstanceAgent.Infrastructure.Storage.Context;
using TgInstanceAgent.Infrastructure.Storage.Entities.Instances;
using TgInstanceAgent.Infrastructure.Storage.Mappers.Abstractions;

namespace TgInstanceAgent.Infrastructure.Storage.Mappers.AggregateMappers;

/// <summary>
/// Класс для преобразования инстансов
/// </summary>
public class InstanceMapper : IAggregateMapperUnit<InstanceAggregate, InstanceModel>
{
    /// <summary>
    /// Статическое readonly поле, представляющее тип InstanceAggregate.
    /// </summary>
    private static readonly Type InstanceAggregateType = typeof(InstanceAggregate);
    
    /// <summary>
    /// Статическое readonly поле, представляющее информацию о поле "_forwardEntries" в типе InstanceAggregate.
    /// Извлекается с помощью рефлексии и использует флаги BindingFlags.Instance и BindingFlags.NonPublic.
    /// </summary>
    private static readonly FieldInfo ForwardEntries =
        InstanceAggregateType.GetField("_forwardEntries", BindingFlags.Instance | BindingFlags.NonPublic)!;

    /// <summary>
    /// Статическое readonly поле, представляющее информацию о поле "_urls" в типе InstanceAggregate.
    /// Извлекается с помощью рефлексии и использует флаги BindingFlags.Instance и BindingFlags.NonPublic.
    /// </summary>
    private static readonly FieldInfo Urls =
        InstanceAggregateType.GetField("_urls", BindingFlags.Instance | BindingFlags.NonPublic)!;
    
    /// <summary>
    /// Статическое readonly поле, представляющее информацию о поле "restrictions" в типе InstanceAggregate.
    /// Извлекается с помощью рефлексии и использует флаги BindingFlags.Instance и BindingFlags.NonPublic.
    /// </summary>
    private static readonly FieldInfo Restrictions =
        InstanceAggregateType.GetField("<Restrictions>k__BackingField",
            BindingFlags.Instance | BindingFlags.NonPublic)!;

    /// <summary>
    /// Преобразует модель инстанса в агрегат инстанса.
    /// </summary>
    /// <param name="model">Модель инстанса.</param>
    /// <param name="context">Контекст базы данных</param>
    /// <returns>Агрегат инстанса.</returns>
    public Task<InstanceAggregate> MapAsync(InstanceModel model, ApplicationDbContext context)
    {
        // Создаем новый экземпляр InstanceAggregate с помощью конструктора, передавая Id, ExpirationTimeUtc и UserId из модели
        var instance = new InstanceAggregate(model.Id, model.ExpirationTimeUtc, model.UserId)
        {
            // Устанавливаем значение свойства State из модели в агрегат инстанса
            State = model.State,

            // Устанавливаем значение свойства Enabled из модели в агрегат инстанса
            Enabled = model.Enabled,
        };
        
        // Если в модели есть WebhookSetting, создаем новый экземпляр WebhookSetting и копируем значения свойств из модели
        if (model.WebhookSetting != null)
        {
            instance.WebhookSetting = new WebhookSetting
            {
                // Копируем значение свойства Messages из модели в агрегат инстанса
                Messages = model.WebhookSetting.Messages,

                // Копируем значение свойства Chats из модели в агрегат инстанса
                Chats = model.WebhookSetting.Chats,

                // Копируем значение свойства Users из модели в агрегат инстанса
                Users = model.WebhookSetting.Users,
                
                // Копируем значение свойства Files из модели в агрегат инстанса
                Files = model.WebhookSetting.Files,
                
                // Копируем значение свойства Stories из модели в агрегат инстанса
                Stories = model.WebhookSetting.Stories,

                // Копируем значение свойства Other из модели в агрегат инстанса
                Other = model.WebhookSetting.Other
            };
        }
        
        // Если установлено SystemProxy
        if (model is { SystemProxyId: not null, SystemProxySetTime: not null })
        {
            // Создаём и устанавливаем системное прокси
            instance.SystemProxy = new SystemProxy
            {
                ProxyId = model.SystemProxyId.Value,
                SetTime = model.SystemProxySetTime.Value
            };
        }

        // Если в модели есть Restrictions, создаем новый экземпляр Restrictions и копируем значения свойств из модели
        if (model.Restrictions != null)
        {
            var restrictions = new Restrictions
            {
                // Копируем значение свойства MessageCount из модели в агрегат инстанса
                MessageCount = model.Restrictions.MessageCount,

                // Копируем значение свойства FileDownloadCount из модели в агрегат инстанса
                FileDownloadCount = model.Restrictions.FileDownloadCount
            };

            // Получаем тип ограничений
            var type = restrictions.GetType();

            // Получаем свойство CurrentDate из типа ограничений с помощью рефлексии
            var prop = type.GetProperty("CurrentDate", BindingFlags.Instance);

            // Если свойство CurrentDate существует, устанавливаем его значение из модели в агрегат инстанса
            prop?.SetValue(restrictions, model.Restrictions.CurrentDate);

            // Устанавливаем значение свойства Restrictions в агрегате инстанса
            Restrictions.SetValue(instance, restrictions);
        }

        // Если в модели есть Proxy, создаем новый экземпляр Proxy и копируем значения свойств из модели
        if (model.Proxy != null)
        {
            var proxy = new Proxy
            {
                // Копируем значение свойства Host из модели в агрегат инстанса
                Host = model.Proxy.Host,

                // Копируем значение свойства Port из модели в агрегат инстанса
                Port = model.Proxy.Port,

                // Копируем значение свойства Login из модели в агрегат инстанса
                Login = model.Proxy.Login,

                // Копируем значение свойства Password из модели в агрегат инстанса
                Password = model.Proxy.Password,

                // Копируем значение свойства Type из модели в агрегат инстанса
                Type = model.Proxy.Type,

                // Копируем значение свойства ExpirationTimeUtc из модели в агрегат инстанса
                ExpirationTimeUtc = model.Proxy.ExpirationTimeUtc
            };

            // Устанавливаем значение свойства Proxy в агрегате инстанса
            instance.Proxy = proxy;
        }
        
        // Преобразуем ForwardEntries из модели в список экземпляров ForwardEntry
        var forwardEntries = model.ForwardEntries
            .Select(fe => new ForwardEntry(fe.For, fe.To, fe.SendCopy))
            .ToList();
        
        // Устанавливаем значение свойства ForwardEntries экземпляра instance
        ForwardEntries.SetValue(instance, forwardEntries);
        
        // Создаем список Urls из модели
        var urls = model.WebhookUrls.Select(u => u.Url).ToList();

        // Устанавливаем WebHookUrls в агрегате инстанса
        Urls.SetValue(instance, urls);

        // Возвращаем агрегат инстанса
        return Task.FromResult(instance);
    }
}