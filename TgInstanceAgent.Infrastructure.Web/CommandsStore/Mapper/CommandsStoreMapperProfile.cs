using TgInstanceAgent.Application.Abstractions.Queries.CommandsStore;
using TgInstanceAgent.Infrastructure.Web.CommandsStore.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Mapper;

namespace TgInstanceAgent.Infrastructure.Web.CommandsStore.Mapper;

/// <summary>
/// Класс для маппинга входных моделей в команды
/// </summary>
public class CommandsStoreMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public CommandsStoreMapperProfile()
    {
        // Карта для преобразования данных из модели ввода GetCommandsHistoryInputModel в команду GetInstanceCommandsHistoryQuery.
        // Включает вызов метода MapInstanceId для добавления InstanceId в команду.
        CreateMap<GetCommandsHistoryInputModel, GetCommandsHistoryQuery>().MapInstanceId();
    }
}