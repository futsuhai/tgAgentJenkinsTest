using System.Linq.Expressions;

namespace TgInstanceAgent.Infrastructure.Storage.Visitors.Sorting.Models;

///<summary>
/// Класс данных сортировки.
///</summary>
/// <param name="expr">Выражение сортировки</param>
/// <param name="isDescending">Флаг сортировки по убыванию</param>
public class SortData<TEntity>(Expression<Func<TEntity, dynamic>> expr, bool isDescending)
{
    ///<summary>
    /// Выражение для сортировки.
    ///</summary>
    public Expression<Func<TEntity, dynamic>> Expr { get; } = expr;
    
    ///<summary>
    /// Флаг порядка сортировки.
    ///</summary>
    public bool IsDescending { get; } = isDescending;
}