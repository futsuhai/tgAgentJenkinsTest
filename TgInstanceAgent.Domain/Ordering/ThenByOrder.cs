using TgInstanceAgent.Domain.Ordering.Abstractions;

namespace TgInstanceAgent.Domain.Ordering;

/// <summary>
/// Класс для сортировки данных с последующей сортировкой.
/// </summary>
public class ThenByOrder<T, TVisitor> : IOrderBy<T, TVisitor>
    where TVisitor : ISortingVisitor<TVisitor, T>
{
    /// <summary>
    /// Левая часть для сортировки.
    /// </summary>
    public IOrderBy<T, TVisitor> Left { get; }
    /// <summary>
    /// Правая часть для сортировки.
    /// </summary>
    public IOrderBy<T, TVisitor> Right { get; }

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="left">Левая часть для сортировки.</param>
    /// <param name="right">Правая часть для сортировки.</param>
    public ThenByOrder(IOrderBy<T, TVisitor> left, IOrderBy<T, TVisitor> right)
    {
        Left = left;
        Right = right;
    }

    /// <summary>
    /// Сортирует коллекцию данных с помощью левой и правой частей.
    /// </summary>
    /// <param name="items">Коллекция данных для сортировки.</param>
    ///<returns>Отсортированная коллекция данных.</returns>
    public IEnumerable<T> Order(IEnumerable<T> items)
    {
        var list = Left.Divide(items);
        return list.SelectMany(Right.Order);
    }

    /// <summary>
    /// Разделяет коллекцию данных с помощью левой и правой частей и сортирует их.
    /// </summary>
    /// <param name="items">Коллекция данных для разделения и сортировки.</param>
    ///<returns>Список разделенных и отсортированных элементов коллекции.</returns>
    public IList<IEnumerable<T>> Divide(IEnumerable<T> items)
    {
        var list = Left.Divide(items);
        return list.Select(Right.Order).ToList();
    }

    /// <summary>
    /// Принимает посетителя для сортировки данных.
    /// </summary>
    public void Accept(TVisitor visitor) => visitor.Visit(this);
}