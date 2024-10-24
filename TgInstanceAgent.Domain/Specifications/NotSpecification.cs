using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.Specifications;

/// <summary>
/// Класс для операции "НЕ" с условием.
/// </summary>
/// <param name="specification">Условие для отрицания.</param>
/// <typeparam name="T">Тип объекта, на который применяется спецификация.</typeparam>
/// <typeparam name="TVisitor">Тип посетителя спецификации.</typeparam>
public class NotSpecification<T, TVisitor>(ISpecification<T, TVisitor> specification) : ISpecification<T, TVisitor>
    where TVisitor : ISpecificationVisitor<TVisitor, T>
{
    /// <summary>
    /// Условие для отрицания.
    /// </summary>
    public ISpecification<T, TVisitor> Specification { get; } = specification;

    /// <summary>
    /// Принимает посетителя для проверки условия.
    /// </summary>
    /// <param name="visitor">Посетитель спецификации.</param>
    public void Accept(TVisitor visitor) => visitor.Visit(this);
    
    /// <summary>
    /// Проверяет, удовлетворяет ли объект условию "НЕ".
    /// </summary>
    /// <param name="obj">Объект для проверки условия.</param>
    ///<returns>Результат проверки условия.</returns>
    public bool IsSatisfiedBy(T obj) => !Specification.IsSatisfiedBy(obj);
}