using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using TgInstanceAgent.Infrastructure.Web.Authentication.Validatiors;

namespace TgInstanceAgent.Start.Extensions;

///<summary>
/// Статический класс сервисов валидации.
///</summary>
public static class ValidationServices
{
    ///<summary>
    /// Расширяющий метод для добавления сервисов валидации в коллекцию служб.
    ///</summary>
    ///<param name="services">Коллекция служб.</param>
    public static void AddValidationServices(this IServiceCollection services)
    {
        // Добавляем все валидаторы из Assembly (для получения Assembly передаем один из валидаторов) 
        services.AddValidatorsFromAssemblyContaining<SetPhoneNumberInputModelValidator>();
        
        // Добавляем интеграцию валидаторов с валидацией ASP NET
        services.AddFluentValidationAutoValidation();
    }
}