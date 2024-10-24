using TgInstanceAgent.Application.Abstractions.Commands.TgAuth;
using TgInstanceAgent.Infrastructure.Web.Authentication.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Mapper;

namespace TgInstanceAgent.Infrastructure.Web.Authentication.Mapper;

/// <summary>
/// Класс для маппинга входных моделей, используемых для авторизации в команды
/// </summary>
public class AuthenticationMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public AuthenticationMapperProfile()
    {
        CreateMap<SetPhoneNumberInputModel, SetPhoneNumberCommand>().MapInstanceId();
        
        CreateMap<CheckCodeInputModel, SetCodeCommand>().MapInstanceId();
        
        CreateMap<CheckPasswordInputModel, SetPasswordCommand>().MapInstanceId();
    }
}