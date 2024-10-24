namespace TgInstanceAgent.Domain.Specifications.Abstractions;

/// <summary>
/// Интерфейс посетителя спецификации.
/// </summary>
/// <typeparam name="TVisitor">Тип посетителя спецификации.</typeparam>
/// <typeparam name="T">Тип объекта, на который применяется спецификация.</typeparam>
public interface ISpecificationVisitor<TVisitor, T> where TVisitor : ISpecificationVisitor<TVisitor, T>
{
    /// <summary>
    /// Посещает объект с условием "И".
    /// </summary>
    /// <param name="spec">Спецификация "И".</param>
    void Visit(AndSpecification<T, TVisitor> spec);
    
    /// <summary>
    /// Посещает объект с условием "ИЛИ".
    /// </summary>
    /// <param name="spec">Спецификация "ИЛИ".</param>
    void Visit(OrSpecification<T, TVisitor> spec);
    
    /// <summary>
    /// Посещает объект с условием "НЕ".
    /// </summary>
    /// <param name="spec">Спецификация "НЕ".</param>
    void Visit(NotSpecification<T, TVisitor> spec);
}