namespace TgInstanceAgent.Domain.Specifications.Abstractions;

/// <summary>
/// Определяет интерфейс спецификации.
/// </summary>
/// <typeparam name="T">Тип объекта, на который применяется спецификация.</typeparam>
/// <typeparam name="TVisitor">Тип посетителя спецификации.</typeparam>
public interface ISpecification<in T, in TVisitor> where TVisitor : ISpecificationVisitor<TVisitor, T>
{
    /// <summary>
    /// Проверяет, удовлетворяет ли объект условию.
    /// </summary>
    /// <param name="item">Объект для проверки условия.</param>
    /// <returns>Результат проверки условия.</returns>
    bool IsSatisfiedBy(T item);
    
    /// <summary>
    /// Принимает посетителя для проверки условия.
    /// </summary>
    /// <param name="visitor">Посетитель спецификации.</param>
    void Accept(TVisitor visitor);
}