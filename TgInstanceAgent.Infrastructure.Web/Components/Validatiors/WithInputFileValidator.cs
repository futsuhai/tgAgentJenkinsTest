using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

/// <summary>
/// Валидатор для классов, реализующих интерфейс IWithFile
/// </summary>
public class WithInputFileValidator : AbstractValidator<IWithInputFile>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public WithInputFileValidator()
    {
        // Правило для Base64
        RuleFor(x => x.LocalId)

            // Не пустое
            .NotEmpty()

            // Когда пустое RemoteId и LocalId
            .When(x => string.IsNullOrWhiteSpace(x.RemoteId) && !x.LocalId.HasValue)

            // С сообщением
            .WithMessage("You need to specify one of the possible file representations: Base64, local Id, remote Id");

        // Правило для LocalId
        RuleFor(x => x.LocalId)

            // Больше чем 0 
            .GreaterThan(0)
            
            // С сообщением
            .WithMessage("File Id cannot be less than 0");
    }
}