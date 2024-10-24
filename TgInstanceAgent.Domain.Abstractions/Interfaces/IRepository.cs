using TgInstanceAgent.Domain.Ordering.Abstractions;
using TgInstanceAgent.Domain.Specifications.Abstractions;

namespace TgInstanceAgent.Domain.Abstractions.Interfaces;

/// <summary> 
/// Интерфейс репозитория для работы с сущностями. 
/// </summary> 
/// <typeparam name="T">Тип агрегата.</typeparam> 
/// <typeparam name="TK">Тип ключа агрегата.</typeparam> 
/// <typeparam name="TX">Тип посетителя спецификации для агрегата.</typeparam> 
/// <typeparam name="TM">Тип посетителя сортировки для агрегата.</typeparam> 
public interface IRepository<T, in TK, out TX, out TM> where T : class 
    where TX : ISpecificationVisitor<TX, T> 
    where TM : ISortingVisitor<TM, T> 
{ 
    /// <summary> 
    /// Асинхронно добавляет новый агрегат. 
    /// </summary> 
    /// <param name="entity">Агрегат для добавления.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    Task AddAsync(T entity, CancellationToken cancellationToken = default); 
 
    /// <summary> 
    /// Асинхронно обновляет информацию о агрегата. 
    /// </summary> 
    /// <param name="entity">Агрегат для обновления.</param> 
    Task UpdateAsync(T entity); 
 
    /// <summary> 
    /// Асинхронно удаляет агрегат по ее ключу. 
    /// </summary> 
    /// <param name="id">Ключ агрегата для удаления.</param> 
    Task DeleteAsync(TK id); 
 
    /// <summary> 
    /// Асинхронно получает агрегат по ее ключу. 
    /// </summary> 
    /// <param name="id">Ключ агрегата для получения.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns>агрегат с указанным ключом.</returns> 
    Task<T?> GetAsync(TK id, CancellationToken cancellationToken);

    /// <summary> 
    /// Асинхронно выполняет поиск агрегатов, удовлетворяющих указанной спецификации, с возможностью сортировки, пропуска и взятия определенного количества. 
    /// </summary>
    /// <param name="specification">Спецификация для поиска агрегатов.</param>
    /// <param name="orderBy">Объект для сортировки агрегатов.</param>
    /// <param name="skip">Количество пропускаемых агрегатов.</param>
    /// <param name="take">Количество выбираемых агрегатов.</param>
    /// <returns>Список агрегатов, удовлетворяющих условиям поиска.</returns> 
    Task<IReadOnlyCollection<T>> FindAsync(ISpecification<T, TX>? specification, IOrderBy<T, TM>? orderBy = null, int? skip = null, int? take = null); 
 
    /// <summary> 
    /// Возвращает количество агрегатов, удовлетворяющих указанной спецификации. 
    /// </summary> 
    /// <param name="specification">Спецификация для поиска агрегатов.</param> 
    /// <returns>Количество агрегатов, удовлетворяющих условиям поиска.</returns> 
    Task<int> CountAsync(ISpecification<T, TX>? specification); 
}