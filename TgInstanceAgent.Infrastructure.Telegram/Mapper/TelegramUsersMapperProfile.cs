using AutoMapper;
using TdLib;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;
using TgInstanceAgent.Infrastructure.Telegram.Extensions;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper;

/// <summary>
/// Класс для маппинга объектов Telegram в пользователей
/// </summary>
public class TelegramUsersMapperProfile : Profile
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public TelegramUsersMapperProfile()
    {
        // Карта для TdApi.User в TgUser
        CreateMap<TdApi.User, TgUser>()
            .ForMember(p => p.FirstName, opt => opt.MapFrom(s => s.FirstName.NullIfEmpty()))
            .ForMember(p => p.LastName, opt => opt.MapFrom(s => s.LastName.NullIfEmpty()))
            .ForMember(p => p.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber.NullIfEmpty()))
            .ForMember(p => p.Username,
                opt => opt.MapFrom(s => s.Usernames == null ? null : s.Usernames.EditableUsername.NullIfEmpty()));

        // Карта для TdApi.UserFullInfo в TgUserFullInfo
        CreateMap<TdApi.UserFullInfo, TgUserFullInfo>()
            .ForMember(p => p.IsStoriesBlocked,
                opt => opt.MapFrom(s => s.BlockList is TdApi.BlockList.BlockListStories))
            .ForMember(p => p.IsBlocked, opt => opt.MapFrom(s => s.BlockList is TdApi.BlockList.BlockListMain));


        // Карта для TdApi.ProfilePhoto в TgProfilePhoto
        CreateMap<TdApi.ProfilePhoto, TgProfilePhoto>()
            .ForMember(p => p.MiniThumbnail, opt => opt.MapFrom(s => s.Minithumbnail));


        // Карты для TdApi.UserStatus в TgUserStatus
        // Включает производные типы
        CreateMap<TdApi.UserStatus.UserStatusEmpty, TgUserStatusEmpty>();
        CreateMap<TdApi.UserStatus.UserStatusOffline, TgUserStatusOffline>();
        CreateMap<TdApi.UserStatus.UserStatusOnline, TgUserStatusOnline>();
        CreateMap<TdApi.UserStatus.UserStatusRecently, TgUserStatusRecently>();
        CreateMap<TdApi.UserStatus.UserStatusLastWeek, TgUserStatusLastWeek>();
        CreateMap<TdApi.UserStatus.UserStatusLastMonth, TgUserStatusLastMonth>();
        CreateMap<TdApi.UserStatus, TgUserStatus>()
            .Include<TdApi.UserStatus.UserStatusEmpty, TgUserStatusEmpty>()
            .Include<TdApi.UserStatus.UserStatusOffline, TgUserStatusOffline>()
            .Include<TdApi.UserStatus.UserStatusOnline, TgUserStatusOnline>()
            .Include<TdApi.UserStatus.UserStatusRecently, TgUserStatusRecently>()
            .Include<TdApi.UserStatus.UserStatusLastWeek, TgUserStatusLastWeek>()
            .Include<TdApi.UserStatus.UserStatusLastMonth, TgUserStatusLastMonth>();
    }
}