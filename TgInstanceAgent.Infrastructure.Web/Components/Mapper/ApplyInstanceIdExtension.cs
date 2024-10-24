using AutoMapper;
using TgInstanceAgent.Application.Abstractions.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Mapper;

/// <summary>
/// Класс, содержащий extension метод для упрощения маппинга InstanceId в AutoMapper.
/// </summary>
public static class ApplyInstanceIdExtension
{
    /// <summary>
    /// Маппинг свойста InstanceId целевого типа.
    /// </summary>
    /// <param name="mappingExpression">Экземпляр IMappingExpression.</param>
    /// <typeparam name="TSource">Тип исходного объекта.</typeparam>
    /// <typeparam name="TDestination">Тип целевого объекта, реализующего интерфейс IWithInstanceId.</typeparam>
    /// <returns></returns>
    public static void MapInstanceId<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> mappingExpression)
        where TDestination : IWithInstanceId
    {
        // Устанавливаем поведения маппинга для InstanceId из словаря Items
        mappingExpression.ForMember(dest => dest.InstanceId,
            opt => opt.MapFrom((_, _, _, context) => context.Items["InstanceId"]));
    }
}
