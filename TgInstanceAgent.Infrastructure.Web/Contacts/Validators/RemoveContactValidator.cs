using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Contacts.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Contacts.Validators;

/// <summary>
/// Валидатор для RemoveContactInputModel
/// </summary>
public class RemoveContactValidator : AbstractValidator<RemoveContactInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public RemoveContactValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputUserValidator());
    }
}