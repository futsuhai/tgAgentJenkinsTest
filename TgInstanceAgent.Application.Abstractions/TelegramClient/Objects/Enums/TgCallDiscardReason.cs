namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;

public enum TgCallDiscardReason
{
    // Пропущенный
    Missed,
    
    // Повесили трубку
    HungUp,
    
    // Пустой
    Empty,
    
    // Разъединенный
    Disconnected,
    
    // Отклоненный
    Declined
}