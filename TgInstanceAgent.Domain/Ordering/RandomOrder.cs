using TgInstanceAgent.Domain.Ordering.Abstractions;

namespace TgInstanceAgent.Domain.Ordering;

/// <summary>
/// Класс для случайной сортировки данных.
/// </summary>
public class RandomOrder<T, TVisitor> : IOrderBy<T, TVisitor>
    where TVisitor : ISortingVisitor<TVisitor, T>
{
    /// <summary>
    /// Случайным образом сортирует коллекцию данных.
    /// </summary>
    /// <param name="items">Коллекция данных для сортировки.</param>
    ///<returns>Отсортированная коллекция данных.</returns>
    public IEnumerable<T> Order(IEnumerable<T> items) => items.OrderBy(_ => Guid.NewGuid());

    /// <summary>
    /// Разделяет коллекцию данных на отдельные элементы.
    /// </summary>
    /// <param name="items">Коллекция данных для разделения.</param>
    ///<returns>Список разделенных элементов коллекции.</returns>
    public IList<IEnumerable<T>> Divide(IEnumerable<T> items) =>
        Order(items).Select(x => (IEnumerable<T>)new List<T> { x }).ToList();

    /// <summary>
    /// Принимает посетителя для сортировки данных.
    /// </summary>
    public void Accept(TVisitor visitor) => visitor.Visit(this);
}
