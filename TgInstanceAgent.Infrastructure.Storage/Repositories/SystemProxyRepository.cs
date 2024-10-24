using Microsoft.EntityFrameworkCore;
using TgInstanceAgent.Domain.Abstractions.Repositories;
using TgInstanceAgent.Domain.Ordering.Abstractions;
using TgInstanceAgent.Domain.Specifications.Abstractions;
using TgInstanceAgent.Domain.SystemProxy;
using TgInstanceAgent.Domain.SystemProxy.Ordering.Visitor;
using TgInstanceAgent.Domain.SystemProxy.Specifications.Visitor;
using TgInstanceAgent.Infrastructure.Storage.Context;
using TgInstanceAgent.Infrastructure.Storage.Entities.SystemProxies;
using TgInstanceAgent.Infrastructure.Storage.Mappers.Abstractions;
using TgInstanceAgent.Infrastructure.Storage.Visitors.Sorting;
using TgInstanceAgent.Infrastructure.Storage.Visitors.Specifications;

namespace TgInstanceAgent.Infrastructure.Storage.Repositories;

public class SystemProxyRepository(
    IDbContextFactory<ApplicationDbContext> contextFactory,
    IAggregateMapperUnit<SystemProxyAggregate, SystemProxyModel> aggregateMapper,
    IModelMapperUnit<SystemProxyModel, SystemProxyAggregate> modelMapper) : ISystemProxyRepository
{
    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно добавляет новый агрегат. 
    /// </summary> 
    public async Task AddAsync(SystemProxyAggregate entity, CancellationToken cancellationToken)
    {
        // Создание нового контекста базы данных асинхронно
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        // Преобразование сущности и добавление ее в контекст
        var systemProxy = await modelMapper.MapAsync(entity, context);

        // Асинхронное добавление сущности в контекст и сохранение изменений
        await context.AddAsync(systemProxy, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно обновляет информацию о агрегата. 
    /// </summary> 
    public async Task UpdateAsync(SystemProxyAggregate entity)
    {
        // Создание нового контекста базы данных асинхронно
        await using var context = await contextFactory.CreateDbContextAsync();

        // Преобразование сущности и добавление ее в контекст
        await modelMapper.MapAsync(entity, context);

        // Асинхронное сохранение изменений в контексте
        await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно удаляет агрегат по ее ключу. 
    /// </summary> 
    public async Task DeleteAsync(Guid id)
    {
        // Создание нового контекста базы данных асинхронно
        await using var context = await contextFactory.CreateDbContextAsync();

        // Удаление первого объекта соответствующего условию из контекста
        context.Remove(context.Instances.First(instance => instance.Id == id));

        // Асинхронное сохранение изменений в контексте
        await context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно получает агрегат по ее ключу. 
    /// </summary> 
    public async Task<SystemProxyAggregate?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        // Создание нового контекста базы данных асинхронно
        await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

        // Получение сущности из контекста вместе с зависимым объектом
        var systemProxy = await context.SystemProxies
            .FirstOrDefaultAsync(systemProxy => systemProxy.Id == id, cancellationToken: cancellationToken);

        // Возвращение объекта, отображенного на агрегат, если он существует
        return systemProxy == null ? null : await aggregateMapper.MapAsync(systemProxy, context);
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно выполняет поиск агрегатов, удовлетворяющих указанной спецификации, с возможностью сортировки, пропуска и взятия определенного количества. 
    /// </summary> 
    public async Task<IReadOnlyCollection<SystemProxyAggregate>> FindAsync(
        ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>? specification,
        IOrderBy<SystemProxyAggregate, ISystemProxySortingVisitor>? orderBy = null, int? skip = null, int? take = null)
    {
        // Создание нового контекста базы данных асинхронно
        await using var context = await contextFactory.CreateDbContextAsync();

        // Получение запроса на выборку из базы данных
        var query = context.SystemProxies.AsQueryable();

        // Если задана спецификация
        if (specification != null)
        {
            // Создаем посетитель спецификаций системных прокси
            var visitor = new SystemProxyVisitor();

            // Посещаем спецификацию
            specification.Accept(visitor);

            // Добавляем к запросу полученную выборку
            if (visitor.Expr != null) query = query.Where(visitor.Expr);
        }

        // Применение сортировки к запросу, если она есть
        if (orderBy != null)
        {
            // Создаем посетитель сортировки инстанса
            var visitor = new SystemProxySortingVisitor();

            // Посещаем сортировку
            orderBy.Accept(visitor);

            // Получаем первое поле сортировки
            var firstQuery = visitor.SortItems.First();

            // Формируем первую сортирову используя первое поле сортировки (order by)
            var orderedQuery = firstQuery.IsDescending
                ? query.OrderByDescending(firstQuery.Expr)
                : query.OrderBy(firstQuery.Expr);

            // Формируем последующие сортировки используя остальные поля сортировки (then by)
            query = visitor.SortItems.Skip(1)
                .Aggregate(orderedQuery, (current, sort) => sort.IsDescending
                    ? current.ThenByDescending(sort.Expr)
                    : current.ThenBy(sort.Expr));
        }

        // Если установлено значение пропускаемых записей - устанавливаем в запрос
        if (skip.HasValue) query = query.Skip(skip.Value);

        // Если установлено значение получаемых записей - устанавливаем в запрос
        if (take.HasValue) query = query.Take(take.Value);

        // Выполняем запрос и получаем модели ef
        var models = await query.ToArrayAsync();

        // Создаем массив агрегатов нужной длинны
        var proxies = new SystemProxyAggregate[models.Length];

        // Перебираем все полученные модели EF и маппим модели в агрегаты
        for (var i = 0; i < models.Length; i++) proxies[i] = await aggregateMapper.MapAsync(models[i], context);

        // Возвращаем массив
        return proxies;
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Возвращает количество агрегатов, удовлетворяющих указанной спецификации. 
    /// </summary> 
    public async Task<int> CountAsync(ISpecification<SystemProxyAggregate, ISystemProxySpecificationVisitor>? specification)
    {
        // Создание нового контекста базы данных асинхронно
        await using var context = await contextFactory.CreateDbContextAsync();

        // Получение запроса на выборку из базы данных
        var query = context.SystemProxies.AsQueryable();

        // Если спецификация не задана, возврат общего числа записей в запросе
        if (specification == null) return await query.CountAsync();

        // Создаем посетитель спецификаций отчетов
        var visitor = new SystemProxyVisitor();

        // Посещаем спецификацию
        specification.Accept(visitor);

        // Добавляем к запросу полученную выборку
        if (visitor.Expr != null) query = query.Where(visitor.Expr);

        // Выполняем запрос и возвращаем результат
        return await query.CountAsync();
    }
}