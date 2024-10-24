using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Reports.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Reports.Validators;

/// <summary>
/// Валидатор для GetReportByDateInputModel
/// </summary>
public class GetReportByDateInputModelValidator : AbstractValidator<GetReportByDateInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public GetReportByDateInputModelValidator()
    {
        // Правило для Date
        RuleFor(x => x.Date)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Дата обязательный параметр");
    }
}