using TgInstanceAgent.Domain.Ordering.Abstractions;

namespace TgInstanceAgent.Domain.Ordering;

/// <summary>
/// Класс для сортировки данных по убыванию.
/// </summary>
public class DescendingOrder<T, TVisitor> : IOrderBy<T, TVisitor>
    where TVisitor : ISortingVisitor<TVisitor, T>
{
    /// <summary>
    /// Данные для сортировки.
    /// </summary>
    public IOrderBy<T, TVisitor> OrderData { get; }

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="orderData">Данные для сортировки.</param>
    public DescendingOrder(IOrderBy<T, TVisitor> orderData) => OrderData = orderData;

    /// <summary>
    /// Сортирует коллекцию данных по убыванию.
    /// </summary>
    /// <param name="items">Коллекция данных для сортировки.</param>
    ///<returns>Отсортированная коллекция данных.</returns>
    public IEnumerable<T> Order(IEnumerable<T> items) => OrderData.Order(items).Reverse();

    /// <summary>
    /// Разделяет коллекцию данных на группы и сортирует их по убыванию.
    /// </summary>
    /// <param name="items">Коллекция данных для разделения и сортировки.</param>
    ///<returns>Список групп отсортированных данных.</returns>
    public IList<IEnumerable<T>> Divide(IEnumerable<T> items)
    {
        var data = OrderData.Divide(items);
        return data.Select(x => x.Reverse()).ToList();
    }

    /// <summary>
    /// Принимает посетителя для сортировки данных.
    /// </summary>
    public void Accept(TVisitor visitor) => visitor.Visit(this);
}