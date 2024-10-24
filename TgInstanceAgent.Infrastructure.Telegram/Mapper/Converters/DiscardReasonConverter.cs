using AutoMapper;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;
using static TdLib.TdApi;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper.Converters;

/// <summary>
/// Конвертер для преобразования объекта CallDiscardReason в объект TgCallDiscardReason.
/// </summary>
public class DiscardReasonConverter : ITypeConverter<CallDiscardReason?, TgCallDiscardReason?>
{
    /// <summary>
    /// Преобразует объект CallDiscardReason в объект TgCallDiscardReason.
    /// </summary>
    /// <param name="source">Объект CallDiscardReason для преобразования.</param>
    /// <param name="destination">Целевой объект TgCallDiscardReason.</param>
    /// <param name="context">Контекст преобразования.</param>
    /// <returns>Преобразованный объект TgCallDiscardReason.</returns>
    public TgCallDiscardReason? Convert(CallDiscardReason? source, TgCallDiscardReason? destination, ResolutionContext context)
    {
        // Преобразуем объект CallDiscardReason в объект TgCallDiscardReason с помощью оператора switch
        return source switch
        {
            // Если CallDiscardReasonEmpty -> Empty
            CallDiscardReason.CallDiscardReasonEmpty => TgCallDiscardReason.Empty,

            // Если CallDiscardReasonDeclined -> Declined
            CallDiscardReason.CallDiscardReasonDeclined => TgCallDiscardReason.Declined,

            // Если CallDiscardReasonDisconnected -> Disconnected
            CallDiscardReason.CallDiscardReasonDisconnected => TgCallDiscardReason.Disconnected,

            // Если CallDiscardReasonHungUp -> HungUp
            CallDiscardReason.CallDiscardReasonHungUp => TgCallDiscardReason.HungUp,

            // Если CallDiscardReasonMissed -> Missed
            CallDiscardReason.CallDiscardReasonMissed => TgCallDiscardReason.Missed,

            // По умолчанию null
            _ => null
        };
    }
}