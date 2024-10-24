using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.Specifications;

/// <summary>
/// Класс для операции "ИЛИ" с условиями.
/// </summary>
/// <param name="left">Левое условие.</param>
/// <param name="right">Правое условие.</param>
/// <typeparam name="T">Тип объекта, на который применяется спецификация.</typeparam>
/// <typeparam name="TVisitor">Тип посетителя спецификации.</typeparam>
public class OrSpecification<T, TVisitor>(ISpecification<T, TVisitor> left, ISpecification<T, TVisitor> right)
    : ISpecification<T, TVisitor>
    where TVisitor : ISpecificationVisitor<TVisitor, T>
{
    /// <summary>
    /// Левое условие.
    /// </summary>
    public ISpecification<T, TVisitor> Left { get; } = left;
    
    /// <summary>
    /// Правое условие.
    /// </summary>
    public ISpecification<T, TVisitor> Right { get; } = right;

    /// <summary>
    /// Принимает посетителя для проверки условий.
    /// </summary>
    /// <param name="visitor">Посетитель спецификации.</param>
    public void Accept(TVisitor visitor) => visitor.Visit(this);
    
    /// <summary>
    /// Проверяет, удовлетворяет ли объект хотя бы одному из условий "ИЛИ".
    /// </summary>
    /// <param name="obj">Объект для проверки условий.</param>
    ///<returns>Результат проверки условий.</returns>
    public bool IsSatisfiedBy(T obj) => Left.IsSatisfiedBy(obj) || Right.IsSatisfiedBy(obj);
}