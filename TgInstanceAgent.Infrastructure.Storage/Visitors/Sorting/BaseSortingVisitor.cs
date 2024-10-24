using TgInstanceAgent.Domain.Ordering;
using TgInstanceAgent.Domain.Ordering.Abstractions;
using TgInstanceAgent.Infrastructure.Storage.Visitors.Sorting.Models;

namespace TgInstanceAgent.Infrastructure.Storage.Visitors.Sorting;

/// <inheritdoc cref="BaseSortingVisitor{TEntity,TVisitor,TItem}"/>
/// <summary>
/// Реализация посетителя для сортировки.
/// </summary>
public abstract class BaseSortingVisitor<TEntity, TVisitor, TItem> where TVisitor : ISortingVisitor<TVisitor, TItem>
{
    /// <summary>
    /// Список параметров сортировки
    /// </summary>
    public List<SortData<TEntity>> SortItems { get; } = [];

    /// <summary>
    /// Конвертирует сортировку в список параметров для сортировки
    /// </summary>
    /// <param name="order">Сортировка</param>
    /// <returns>Список параметров</returns>
    protected abstract List<SortData<TEntity>> ConvertOrderToList(IOrderBy<TItem, TVisitor> order);

    /// <inheritdoc cref="BaseSortingVisitor{TEntity,TVisitor,TItem}"/>
    /// <summary>
    /// Посещает объект сортировки в порядке убывания.
    /// </summary>
    public void Visit(DescendingOrder<TItem, TVisitor> order)
    {
        // Преобразование данных заказа в список
        var x = ConvertOrderToList(order.OrderData);
        
        // Добавление в список SortItems всех элементов из списка x, кроме последнего
        SortItems.AddRange(x.Take(x.Count - 1));
        
        // Получение последнего элемента из списка x
        var last = x.Last();
        
        // Добавление последнего элемента из списка x в SortItems
        SortItems.Add(new SortData<TEntity>(last.Expr, true));
    }

    /// <inheritdoc cref="BaseSortingVisitor{TEntity,TVisitor,TItem}"/>
    /// <summary>
    /// Посещает объект сортировки "затем по".
    /// </summary>
    public void Visit(ThenByOrder<TItem, TVisitor> order)
    {
        // Преобразование данных из левого подзапроса в список и добавление в SortItems
        var left = ConvertOrderToList(order.Left);
        
        // Добавляем в список элементов сортировки
        SortItems.AddRange(left);
        
        // Преобразование данных из правого подзапроса
        var right = ConvertOrderToList(order.Right);
        
        // Добавляем в список элементов сортировки
        SortItems.AddRange(right);
    }

    /// <inheritdoc cref="BaseSortingVisitor{TEntity,TVisitor,TItem}"/>
    /// <summary>
    /// Посещает объект случайной сортировки.
    /// </summary>
    public void Visit(RandomOrder<TItem, TVisitor> order)
    {
        // Добавление в список SortItems сортировки по уникальному идентификатору в возрастающем порядке для сущности TEntity
        SortItems.Add(new SortData<TEntity>(x => Guid.NewGuid(), false));
    }
}