using System.Linq.Expressions;
using TgInstanceAgent.Domain.Specifications;
using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Infrastructure.Storage.Visitors.Specifications;

/// <inheritdoc cref="ISpecificationVisitor{TVisitor,T}"/>
/// <summary>
/// Реализация посетителя спецификации.
/// </summary>
public abstract class BaseVisitor<TEntity, TVisitor, TItem> where TVisitor : ISpecificationVisitor<TVisitor, TItem>
{
    /// <summary>
    /// Выражение для запроса к ef.
    /// </summary>
    public Expression<Func<TEntity, bool>>? Expr { get; protected set; }

    /// <summary>
    /// Конвертирует спецификацию в Expression.
    /// </summary>
    /// <param name="spec">Спецификация</param>
    protected abstract Expression<Func<TEntity, bool>> ConvertSpecToExpression(ISpecification<TItem, TVisitor> spec);

    /// <inheritdoc cref="ISpecificationVisitor{TVisitor,T}"/>
    /// <summary>
    /// Посещает объект с условием "И".
    /// </summary>
    public void Visit(AndSpecification<TItem, TVisitor> spec)
    {
        // Преобразование левой спецификации в выражение
        var leftExpr = ConvertSpecToExpression(spec.Left);

        // Преобразование правой спецификации в выражение
        var rightExpr = ConvertSpecToExpression(spec.Right);

        // Получение параметра левого выражения
        var param = leftExpr.Parameters.Single();

        // Создание тела выражения с оператором AndAlso
        var exprBody = Expression.AndAlso(leftExpr.Body, Expression.Invoke(rightExpr, param));

        // Создание лямбда выражения для типа TEntity
        Expr = Expression.Lambda<Func<TEntity, bool>>(exprBody, param);
    }

    /// <inheritdoc cref="ISpecificationVisitor{TVisitor,T}"/>
    /// <summary>
    /// Посещает объект с условием "ИЛИ".
    /// </summary>
    public void Visit(OrSpecification<TItem, TVisitor> spec)
    {
        // Преобразование левой спецификации в выражение
        var leftExpr = ConvertSpecToExpression(spec.Left);

        // Преобразование правой спецификации в выражение
        var rightExpr = ConvertSpecToExpression(spec.Right);

        // Получение параметра левого выражения
        var param = leftExpr.Parameters.Single();

        // Создание тела выражения с оператором Or
        var exprBody = Expression.Or(leftExpr.Body, Expression.Invoke(rightExpr, param));

        // Создание лямбда выражения для типа TEntity
        Expr = Expression.Lambda<Func<TEntity, bool>>(exprBody, param);
    }

    /// <inheritdoc cref="ISpecificationVisitor{TVisitor,T}"/>
    /// <summary>
    /// Посещает объект с условием "НЕ".
    /// </summary>
    public void Visit(NotSpecification<TItem, TVisitor> spec)
    {
        // Преобразование спецификации в выражение
        var specExpr = ConvertSpecToExpression(spec.Specification);

        // Создание тела выражения с оператором Not
        var exprBody = Expression.Not(specExpr.Body);

        // Создание лямбда выражения для типа TEntity
        Expr = Expression.Lambda<Func<TEntity, bool>>(exprBody, specExpr.Parameters.Single());
    }
}