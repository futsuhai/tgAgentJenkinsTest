using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Reports.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Reports.Validators;

/// <summary>
/// Валидатор для GetReportsByDateRangeInputModel
/// </summary>
public class GetReportsByDatesRangeInputModelValidator : AbstractValidator<GetReportsByDatesRangeInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public GetReportsByDatesRangeInputModelValidator()
    {
        // Правило для StartDate
        RuleFor(x => x.StartDate)

            // Не null
            .NotEmpty().

            // С сообщением
            WithMessage("Стартовая дата диапазона обязательна");

        // Правило для EndDate
        RuleFor(x => x.EndDate)

            // Не null
            .NotEmpty().

            // С сообщением
            WithMessage("Конечная дата диапазона обязательна.")

            // Дата страта не равна дате конца диапазона
            .GreaterThanOrEqualTo(x => x.StartDate).

            // С сообщением
            WithMessage("Дата страта диапазона не может быть равна дате конца диапазона")

            // Диапазон не более 6 месяцев
            .Must((model, _) => BeWithinSixMonths(model))

            // С сообщением
            .WithMessage("Диапазон не более 6 месяцев");
    }

    /// <summary>
    /// Проверка диапазона дат
    /// </summary>
    /// <param name="model">Входная модель</param>
    /// <returns></returns>
    private static bool BeWithinSixMonths(GetReportsByDatesRangeInputModel model)
    {
        var startDateDayNumber = model.StartDate.DayNumber;
        var endDateDayNumber = model.EndDate.DayNumber;
        var dateDifference = endDateDayNumber - startDateDayNumber;
        return dateDifference <= 183;
    }
}