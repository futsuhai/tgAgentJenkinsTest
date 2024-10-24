using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

namespace TgInstanceAgent.Infrastructure.Web.Components.Validatiors;

/// <summary>
/// Валидатор для классов, реализующих интерфейс IWithInputReaction
/// </summary>
public class WithInputSendOptionValidator : AbstractValidator<IWithInputSendOption>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public WithInputSendOptionValidator()
    {
        // Правило для SendDate
        RuleFor(x => x.SendOnTime)

            // UNIX-время должно быть меньше или равно текущему + 367 дней или null
            .Must(x => x <= GetUnixTimeStamp(367))

            // Когда указано
            .When(x => x.SendOnTime.HasValue)

            // С сообщением
            .WithMessage("SendDate must be a UNIX timestamp within the next 367 days or null");
    }


    /// <summary>
    /// Метод для получения даты в Unix 
    /// </summary>
    /// <param name="daysOffset">Смещение по дням</param>
    /// <returns>дата в unix</returns>
    private static int GetUnixTimeStamp(int daysOffset = 0)
    {
        var date = DateTimeOffset.UtcNow.AddDays(daysOffset);
        return (int)Math.Floor((decimal)date.ToUnixTimeSeconds());
    }
}