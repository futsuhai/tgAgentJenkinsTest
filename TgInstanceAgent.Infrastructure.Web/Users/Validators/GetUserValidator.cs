using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Users.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Users.Validators;

/// <summary>
/// Валидатор для GetUserInputModel
/// </summary>
public class GetUserValidatorValidator : AbstractValidator<GetUserInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public GetUserValidatorValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputUserValidator());
    }
}