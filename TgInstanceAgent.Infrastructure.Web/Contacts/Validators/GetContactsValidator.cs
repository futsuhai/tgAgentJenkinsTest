using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Contacts.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Contacts.Validators;

/// <summary>
/// Валидатор для GetContactsInputModel.
/// </summary>
public class GetContactsValidator : AbstractValidator<GetContactsInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public GetContactsValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputLimitValidator());
    }
}