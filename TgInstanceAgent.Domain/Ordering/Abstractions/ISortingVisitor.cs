namespace TgInstanceAgent.Domain.Ordering.Abstractions;

/// <summary>
/// Интерфейс посетителя для сортировки.
/// </summary>
/// <typeparam name="TVisitor">Посетитель.</typeparam>
/// <typeparam name="T">Обобщенный тип.</typeparam>
public interface ISortingVisitor<TVisitor, T> where TVisitor : ISortingVisitor<TVisitor, T>
{
    /// <summary>
    /// Посещает объект сортировки в порядке убывания.
    /// </summary>
    /// <param name="order">Сортировка.</param>
    void Visit(DescendingOrder<T, TVisitor> order);
    
    /// <summary>
    /// Посещает объект сортировки "затем по".
    /// </summary>
    /// <param name="order">Сортировка.</param>
    void Visit(ThenByOrder<T, TVisitor> order);
    
    /// <summary>
    /// Посещает объект случайной сортировки.
    /// </summary>
    /// <param name="order">Сортировка.</param>
    void Visit(RandomOrder<T, TVisitor> order);
}