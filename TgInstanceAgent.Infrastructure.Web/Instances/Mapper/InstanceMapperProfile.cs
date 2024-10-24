using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Infrastructure.Web.Components.Mapper;
using TgInstanceAgent.Infrastructure.Web.Instances.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Instances.Mapper;

/// <summary>
/// Класс для маппинга входных моделей в команды
/// </summary>
public class InstanceMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public InstanceMapperProfile()
    {
        // Карта для преобразования данных из модели ввода UrlInputModel в команду AddWebhookUrlCommand.
        // Преобразует строку URL в объект Uri и включает InstanceId в команду.
        CreateMap<UrlInputModel, AddWebhookUrlCommand>()
            .ForMember(m => m.Url,
                opt => opt.MapFrom(s => new Uri(s.Url))) 
            .MapInstanceId();

        // Карта для преобразования данных из модели ввода UrlInputModel в команду RemoveWebhookUrlCommand.
        // Преобразует строку URL в объект Uri и включает InstanceId в команду.
        CreateMap<UrlInputModel, RemoveWebhookUrlCommand>()
            .ForMember(m => m.Url,
                opt => opt.MapFrom(s => new Uri(s.Url)))  
            .MapInstanceId();
        
        // Карта для преобразования данных из модели ввода UpdateWebhookSettingInputModel в команду UpdateWebhookSettingCommand.
        // Включает вызов метода MapInstanceId для добавления InstanceId в команду.
        CreateMap<UpdateWebhookSettingInputModel, UpdateWebhookSettingCommand>().MapInstanceId();
        
        // Карта для преобразования данных из модели ввода AddForwardEntryInputModel в команду AddForwardEntryCommand.
        // Включает вызов метода MapInstanceId для добавления InstanceId в команду.
        CreateMap<AddForwardEntryInputModel, AddForwardEntryCommand>().MapInstanceId();
        
        // Карта для преобразования данных из модели ввода RemoveForwardEntryInputModel в команду RemoveForwardEntryCommand.
        // Включает вызов метода MapInstanceId для добавления InstanceId в команду.
        CreateMap<RemoveForwardEntryInputModel, RemoveForwardEntryCommand>().MapInstanceId();
    }
}