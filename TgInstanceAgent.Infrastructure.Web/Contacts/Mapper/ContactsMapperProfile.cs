using TgInstanceAgent.Application.Abstractions.Commands.TgContacts;
using TgInstanceAgent.Application.Abstractions.Queries.TgUsers;
using TgInstanceAgent.Infrastructure.Web.Components.Mapper;
using TgInstanceAgent.Infrastructure.Web.Contacts.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Contacts.Mapper;

/// <summary>
/// Класс для маппинга входных моделей для работы с контактами в команды
/// </summary>
public class ContactsMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public ContactsMapperProfile()
    {
        // Карта для ImportContactInputModel в ImportContact
        CreateMap<ImportContactInputModel, ImportContact>();

        // Карта для GetContactsInputModel в GetContactsQuery
        CreateMap<GetContactsInputModel, GetContactsQuery>().MapInstanceId();
        
        // Карта для AddContactInputModel в AddContactCommand
        CreateMap<AddContactInputModel, AddContactCommand>().MapInstanceId();

        // Карта для RemoveContactInputModel в RemoveContactCommand
        CreateMap<RemoveContactInputModel, RemoveContactCommand>().MapInstanceId();
        
        // Карта для SearchContactsInputModel в SearchContactsQuery
        CreateMap<SearchContactsInputModel, SearchContactsQuery>().MapInstanceId();
    }
}