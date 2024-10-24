using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TgInstanceAgent.Infrastructure.Storage.Entities.Abstractions;

/// <summary>
/// Интерфейс аггрегата
/// </summary>
public interface IAggregateModel
{
    /// <summary>
    /// Идентификатор, сгенерированной базой данных
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }
}