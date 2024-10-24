using System.Reflection;
using TgInstanceAgent.Domain.Abstractions;

namespace TgInstanceAgent.Infrastructure.Storage.Mappers.StaticMethods;

/// <summary>
/// Статический класс, содержащий информацию о поле идентификатора агрегата.
/// </summary>
internal static class IdFields
{
    /// <summary>
    /// Поле идентификатора агрегата.
    /// </summary>
    public static readonly FieldInfo AggregateId =
        typeof(AggregateRoot).GetField("<Id>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic)!;
}