using AutoMapper;
using TgInstanceAgent.Application.Abstractions.Proxies.DataModels;
using TgInstanceAgent.Domain.SystemProxy.Enums;
using TgInstanceAgent.Infrastructure.Proxy.Objects;

namespace TgInstanceAgent.Infrastructure.Proxy.Mapper;

/// <summary>
/// Класс для маппинга объектов 
/// </summary>
public class ProxyMapperProfile : Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public ProxyMapperProfile()
    {
        // Карта для ProxyClientData в ProxyData 
        CreateMap<ProxyResponse, ProxyData>()
            .ForMember(dest => dest.Port, opt => opt.MapFrom(src => ushort.Parse(src.Port)))
            .ForMember(dest => dest.PurchaseTime, opt => opt.MapFrom(src => src.PurchaseTime.ToUniversalTime()))
            .ForMember(dest => dest.ExpirationTime, opt => opt.MapFrom(src => src.ExpirationTime.ToUniversalTime()))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type == "http" ? ProxyType.Https : (ProxyType)Enum.Parse(typeof(ProxyType), src.Type, true)));
        // Карта для ProlongResponse в ProxyProlong 
        CreateMap<ProlongResponse, ExtendResult>();
    }
}