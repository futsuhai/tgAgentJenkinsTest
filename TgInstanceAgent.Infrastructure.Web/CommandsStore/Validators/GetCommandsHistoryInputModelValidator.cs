using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.CommandsStore.InputModels;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

namespace TgInstanceAgent.Infrastructure.Web.CommandsStore.Validators;

/// <summary>
/// Валидатор для GetCommandsHistoryInputModel.
/// </summary>
public class GetCommandsHistoryInputModelValidator : AbstractValidator<GetCommandsHistoryInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public GetCommandsHistoryInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputLimitValidator());
    }
}