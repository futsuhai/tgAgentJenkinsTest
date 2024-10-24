using IntegrationEvents.Instances;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;

namespace TgInstanceAgent.Infrastructure.Bus.Instances.Mapper;

/// <summary>
/// Класс для маппинга событий интеграции в команды
/// </summary>
public class InstancesMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public InstancesMapperProfile()
    {
        // Карта для InstanceCreatedIntegrationEvent в AddInstanceCommand
        CreateMap<InstanceCreatedIntegrationEvent, AddInstanceCommand>();
    }
}