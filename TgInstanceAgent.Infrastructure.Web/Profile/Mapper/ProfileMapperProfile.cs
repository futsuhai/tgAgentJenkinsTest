using TgInstanceAgent.Application.Abstractions.Commands.TgProfile;
using TgInstanceAgent.Application.Abstractions.Interfaces;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;
using TgInstanceAgent.Infrastructure.Web.Components.Mapper;
using TgInstanceAgent.Infrastructure.Web.Profile.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Profile.Mapper;

/// <summary>
/// Класс для маппинга входных моделей для работы с профилем в команды
/// </summary>
public class ProfileMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public ProfileMapperProfile()
    {
        // Карта для BackgroundGradientInputModel в BackgroundGradientFillData
        CreateMap<BackgroundGradientInputModel, BackgroundGradientData>();

        // Карта для SetPictureProfilePhotoInputModel в SetPictureProfilePhotoCommand
        CreateMap<SetPictureProfilePhotoInputModel, SetPictureProfilePhotoCommand>().MapInstanceId();

        // Карта для SetEmojiProfilePhotoInputModel в SetEmojiProfilePhotoCommand
        CreateMap<SetEmojiProfilePhotoInputModel, SetEmojiProfilePhotoCommand>().MapInstanceId();

        // Карта для SetStickerProfilePhotoInputModel в SetStickerProfilePhotoCommand
        CreateMap<SetStickerProfilePhotoInputModel, SetStickerProfilePhotoCommand>().MapInstanceId();

        // Карта для SetAnimationProfilePhotoInputModel в SetAnimationProfilePhotoCommand
        CreateMap<SetAnimationProfilePhotoInputModel, SetAnimationProfilePhotoCommand>().MapInstanceId();

        // Карта для SetUserBioInputModel в SetUserBioCommand
        CreateMap<SetUserBioInputModel, SetUserBioCommand>().MapInstanceId();
    }
}