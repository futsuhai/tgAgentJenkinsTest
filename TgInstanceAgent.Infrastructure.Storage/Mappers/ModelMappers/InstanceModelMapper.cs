using Microsoft.EntityFrameworkCore;
using TgInstanceAgent.Domain.Instances;
using TgInstanceAgent.Infrastructure.Storage.Context;
using TgInstanceAgent.Infrastructure.Storage.Entities;
using TgInstanceAgent.Infrastructure.Storage.Entities.Instances;
using TgInstanceAgent.Infrastructure.Storage.Extensions;
using TgInstanceAgent.Infrastructure.Storage.Mappers.Abstractions;

namespace TgInstanceAgent.Infrastructure.Storage.Mappers.ModelMappers;

/// <summary>
/// Класс для преобразования InstanceAggregate в InstanceModel
/// </summary>
public class InstanceModelMapper : IModelMapperUnit<InstanceModel, InstanceAggregate>
{
    /// <summary>
    /// Преобразуем InstanceAggregate в InstanceModel
    /// </summary>
    /// <param name="aggregate">Аггрегат InstanceAggregate</param>
    /// <param name="context">Контекст базы данных</param>
    /// <returns>Модель инстанса</returns>
    public async Task<InstanceModel> MapAsync(InstanceAggregate aggregate, ApplicationDbContext context)
    {
        // Получаем модель инстанса из базы данных
        var model = await context.Instances
            .LoadDependencies()
            .FirstOrDefaultAsync(x => x.Id == aggregate.Id);

        // Если модели null, то создаем новую модель, так как это новый инстанс 
        model ??= new InstanceModel { Id = aggregate.Id, UserId = aggregate.UserId };

        // Задаем время истечения инстанса
        model.ExpirationTimeUtc = aggregate.ExpirationTimeUtc;

        // Задаем состояние инстанса
        model.State = aggregate.State;

        // Задаем включен ли инстанс
        model.Enabled = aggregate.Enabled;
        
        // Если системное прокси в агрегате не null
        if (aggregate.SystemProxy != null)
        {
            // Задаем идентификатор системного прокси
            model.SystemProxyId = aggregate.SystemProxy.ProxyId;
        
            //Задаем время установки системного прокси
            model.SystemProxySetTime = aggregate.SystemProxy.SetTime;
        }

        // Преобразуем коллекцию WebhookUrls
        ProcessWebhookUrls(aggregate, model);

        // Преобразуем коллекцию ForwardEntries
        ProcessForwardEntries(aggregate, model);

        // Преобразуем WebhookSettings
        ProcessWebhookSettings(aggregate, model);

        // Преобразуем Restrictions
        ProcessRestrictions(aggregate, model);

        // Преобразуем Proxy
        ProcessProxy(aggregate, model);

        // Возвращаем модель инстанса в EF
        return model;
    }

    /// <summary>
    /// Обрабатывает пересылки сообщений для указанного экземпляра.
    /// Удаляет устаревшие пересылки из модели и добавляет новые пересылки в модель.
    /// </summary>
    /// <param name="aggregate">Экземпляр агрегата.</param>
    /// <param name="model">Модель экземпляра.</param>
    private static void ProcessForwardEntries(InstanceAggregate aggregate, InstanceModel model)
    {
        // Удаляем из модели пересылки, которых нет в агрегате
        model.ForwardEntries.RemoveAll(x =>
            aggregate.ForwardEntries.All(m => m.To != x.To || m.For != x.For || m.SendCopy != x.SendCopy));

        // Получаем новые пересылки, которых нет в модели
        var newEntries = aggregate.ForwardEntries
            .Where(x => model.ForwardEntries.All(m => m.To != x.To || m.For != x.For || m.SendCopy != x.SendCopy))
            .Select(x => new ForwardEntryModel
            {
                For = x.For,
                To = x.To,
                SendCopy = x.SendCopy
            });

        // Добавляем новые пересылки в модель
        model.ForwardEntries.AddRange(newEntries);
    }

    /// <summary>
    /// Обрабатывает Urls для указанного экземпляра.
    /// Удаляет устаревшие Urls из модели и добавляет новые Urls в модель.
    /// </summary>
    /// <param name="aggregate">Экземпляр агрегата.</param>
    /// <param name="model">Модель экземпляра.</param>
    private static void ProcessWebhookUrls(InstanceAggregate aggregate, InstanceModel model)
    {
        // Удаляем из модели URL, которых нет в агрегате
        model.WebhookUrls.RemoveAll(x => aggregate.WebhookUrls.All(m => m != x.Url));

        // Получаем новые URL, которых нет в модели
        var newUrls = aggregate.WebhookUrls
            .Where(x => model.WebhookUrls.All(m => m.Url != x))
            .Select(x => new WebhookUrlModel
            {
                Url = x,
                InstanceId = aggregate.Id,
            });

        // Добавляем новые URL в модель
        model.WebhookUrls.AddRange(newUrls);
    }

    /// <summary>
    /// Обрабатывает WebhookSettings для указанного инстанса.
    /// Удаляет устаревшие WebhookSettings из модели и добавляет новые WebhookSettings в модель.
    /// </summary>
    /// <param name="aggregate">Агрегат инстанса.</param>
    /// <param name="model">Модель инстанса.</param>
    private static void ProcessWebhookSettings(InstanceAggregate aggregate, InstanceModel model)
    {
        // Если WebhookSetting в модели не инициализирован, инициализируем его с InstanceId из aggregate
        model.WebhookSetting ??= new WebhookSettingModel { InstanceId = aggregate.Id };

        // Устанавливаем InstanceId в WebhookSetting модели из aggregate
        model.WebhookSetting.InstanceId = aggregate.Id;

        // Копируем значение свойства Messages из WebhookSetting агрегата в WebhookSetting модели
        model.WebhookSetting.Messages = aggregate.WebhookSetting.Messages;

        // Копируем значение свойства Chats из WebhookSetting агрегата в WebhookSetting модели
        model.WebhookSetting.Chats = aggregate.WebhookSetting.Chats;

        // Копируем значение свойства Users из WebhookSetting агрегата в WebhookSetting модели
        model.WebhookSetting.Users = aggregate.WebhookSetting.Users;

        // Копируем значение свойства Files из WebhookSetting агрегата в WebhookSetting модели
        model.WebhookSetting.Files = aggregate.WebhookSetting.Files;

        // Копируем значение свойства Stories из WebhookSetting агрегата в WebhookSetting модели
        model.WebhookSetting.Stories = aggregate.WebhookSetting.Stories;

        // Копируем значение свойства Other из WebhookSetting агрегата в WebhookSetting модели
        model.WebhookSetting.Other = aggregate.WebhookSetting.Other;
    }

    /// <summary>
    /// Обрабатывает прокси-настройки для указанного инстанса.
    /// Удаляет устаревшие данные прокси из модели и добавляет новые данные в модель.
    /// </summary>
    /// <param name="aggregate">Агрегат инстанса.</param>
    /// <param name="model">Модель инстанса.</param>
    private static void ProcessProxy(InstanceAggregate aggregate, InstanceModel model)
    {
        // Если у агрегата нет прокси
        if (aggregate.Proxy == null)
        {
            // Удаляем прокси из модели
            model.Proxy = null;

            // Не продолжаем
            return;
        }
        
        // Убираем идентификатор системной прокси в ProxyModel
        //model.SystemProxyId = null;
        
        // Убираем время установки системной прокси в ProxyModel
        //model.SystemProxySetTime = null;

        // Если Proxy в модели не инициализирован, инициализируем его с InstanceId из aggregate
        model.Proxy ??= new ProxyModel { InstanceId = aggregate.Id };

        // Копируем значение свойства Host из прокси-настроек агрегата в ProxyModel
        model.Proxy.Host = aggregate.Proxy.Host;

        // Копируем значение свойства Port из прокси-настроек агрегата в ProxyModel
        model.Proxy.Port = aggregate.Proxy.Port;

        // Копируем значение свойства Login из прокси-настроек агрегата в ProxyModel
        model.Proxy.Login = aggregate.Proxy.Login;

        // Копируем значение свойства Password из прокси-настроек агрегата в ProxyModel
        model.Proxy.Password = aggregate.Proxy.Password;

        // Копируем значение свойства Type из прокси-настроек агрегата в ProxyModel
        model.Proxy.Type = aggregate.Proxy.Type;

        // Копируем значение свойства ExpirationTimeUtc из прокси-настроек агрегата в ProxyModel
        model.Proxy.ExpirationTimeUtc = aggregate.Proxy.ExpirationTimeUtc;
    }


    /// <summary>
    /// Обрабатывает ограничения для указанного инстанса.
    /// Удаляет устаревшие данные ограничений из модели и добавляет новые данные в модель.
    /// </summary>
    /// <param name="aggregate">Агрегат инстанса.</param>
    /// <param name="model">Модель инстанса.</param>
    private static void ProcessRestrictions(InstanceAggregate aggregate, InstanceModel model)
    {
        // Если Restrictions в модели не инициализирован, инициализируем его с InstanceId из aggregate
        model.Restrictions ??= new RestrictionsModel { InstanceId = aggregate.Id };

        // Копируем значение свойства FileDownloadCount из ограничений агрегата в RestrictionsModel
        model.Restrictions.FileDownloadCount = aggregate.Restrictions.FileDownloadCount;

        // Копируем значение свойства MessageCount из ограничений агрегата в RestrictionsModel
        model.Restrictions.MessageCount = aggregate.Restrictions.MessageCount;

        // Копируем значение свойства CurrentDate из ограничений агрегата в RestrictionsModel
        model.Restrictions.CurrentDate = aggregate.Restrictions.CurrentDate;
    }
}