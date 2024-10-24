using TgInstanceAgent.Application.Abstractions.Queries.TgUsers;
using TgInstanceAgent.Infrastructure.Web.Components.Mapper;
using TgInstanceAgent.Infrastructure.Web.Users.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Users.Mapper;

/// <summary>
/// Класс для маппинга входных моделей пользователя в команды
/// </summary>
public class UserMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public UserMapperProfile()
    {
        // Карта для GetUserInputModel в GetUserQuery
        CreateMap<GetUserInputModel, GetUserQuery>().MapInstanceId();
        
        // Карта для GetUserInputModel в GetUserFullInfoQuery
        CreateMap<GetUserInputModel, GetUserFullInfoQuery>().MapInstanceId();
        
        // Карта для GetUserProfilePhotosInputModel в GetUserProfilePhotosQuery
        CreateMap<GetUserProfilePhotosInputModel, GetUserProfilePhotosQuery>().MapInstanceId();
    }
}