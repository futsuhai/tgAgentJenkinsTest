using TgInstanceAgent.Application.Abstractions.Commands.TgProxies;
using TgInstanceAgent.Infrastructure.Web.Components.Mapper;
using TgInstanceAgent.Infrastructure.Web.Proxies.InputModels;
using TgInstanceAgent.Infrastructure.Web.Proxies.InputModels.Components;

namespace TgInstanceAgent.Infrastructure.Web.Proxies.Mapper;

/// <summary>
/// Класс для маппинга входных моделей для работы с прокси в команды
/// </summary>
public class ProxiesMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public ProxiesMapperProfile()
    {
        // Карта для ProxyTypeHttpInputModel в ProxyTypeHttp
        CreateMap<ProxyTypeHttpInputModel, ProxyTypeHttp>();

        // Карта для ProxyTypeSocksInputModel в ProxyTypeSocks
        CreateMap<ProxyTypeSocksInputModel, ProxyTypeSocks>();

        // Карта для ProxyTypeMtprotoInputModel в ProxyTypeMtproto
        CreateMap<ProxyTypeMtprotoInputModel, ProxyTypeMtproto>();
        
        // Карта для SetProxyInputModel в SetProxyCommand
        CreateMap<SetProxyInputModel, SetProxyCommand>()
            .ForMember(dest => dest.ProxyTypeHttp, opt => opt.MapFrom(src => src.ProxyTypeHttpInputModel))
            .ForMember(dest => dest.ProxyTypeSocks, opt => opt.MapFrom(src => src.ProxyTypeSocksInputModel))
            .ForMember(dest => dest.ProxyTypeMtproto, opt => opt.MapFrom(src => src.ProxyTypeMtprotoInputModel))
            .MapInstanceId();
    }
}