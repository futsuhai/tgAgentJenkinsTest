using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Users.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Users.Validators;

/// <summary>
/// Валидатор для GetUserProfilePhotosInputModel
/// </summary>
public class GetUserProfilePhotosValidator : AbstractValidator<GetUserProfilePhotosInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public GetUserProfilePhotosValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputUserValidator());

        // Правило для смещения
        RuleFor(x => x.Offset)
            
            // Больше нуля
            .GreaterThanOrEqualTo(0)
            
            // С сообщением
            .WithMessage("Offset cannot be less than 0");

        // Правило для лимита
        RuleFor(x => x.Limit)
            
            // Между 1 и 100 включительно
            .InclusiveBetween(1, 100)
            
            // С сообщением
            .WithMessage("The value must be in the range from 1 to 100.");
    }
}