using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Mapper;

/// <summary>
/// Класс для маппинга входных моделей в команды
/// </summary>
public class ComponentsMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public ComponentsMapperProfile()
    {
        CreateMap<IFormFile, FileData>()
            .ForMember(f => f.Stream, opt => opt.MapFrom(form => form.OpenReadStream()))
            .ForMember(f => f.Name, opt => opt.MapFrom(form => form.FileName));
        
        CreateMap<IFormFile, ThumbnailData>()
            .ForMember(f => f.Stream, opt => opt.MapFrom(form => form.OpenReadStream()))
            .ForMember(f => f.Name, opt => opt.MapFrom(form => form.FileName));

    }
}