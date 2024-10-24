namespace TgInstanceAgent.Domain.Ordering.Abstractions;

/// <summary>
/// Интерфейс для сортировки коллекции данных.
/// </summary>
/// <typeparam name="T">Обобщенный тип.</typeparam>
/// <typeparam name="TVisitor">Посетитель.</typeparam>
public interface IOrderBy<T, in TVisitor> where TVisitor : ISortingVisitor<TVisitor, T>
{
    /// <summary>
    /// Сортирует коллекцию данных.
    /// </summary>
    /// <param name="items">Коллекция данных для сортировки.</param>
    ///<returns>Отсортированная коллекция данных.</returns>
    IEnumerable<T> Order(IEnumerable<T> items);
    
    /// <summary>
    /// Разделяет коллекцию данных на отдельные элементы.
    /// </summary>
    /// <param name="items">Коллекция данных для разделения.</param>
    ///<returns>Список разделенных элементов коллекции.</returns>
    IList<IEnumerable<T>> Divide(IEnumerable<T> items);
    
    /// <summary>
    /// Принимает посетителя для сортировки данных.
    /// </summary>
    /// <param name="visitor">Посетитель.</param>
    void Accept(TVisitor visitor);
}