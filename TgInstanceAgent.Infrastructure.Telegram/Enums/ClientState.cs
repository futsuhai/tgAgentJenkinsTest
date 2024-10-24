namespace TgInstanceAgent.Infrastructure.Telegram.Enums;

/// <summary>
/// Перечисление состояний клиента.
/// </summary>
public enum ClientState
{
    // Не готов
    NotReady,
    
    // Ожидает номер телефона
    WaitPhoneNumber,
    
    // Ожидает подтверждения на другом устройстве
    WaitOtherDeviceConfirmation,
    
    // Ожидает аутентификацию
    WaitAuthenticationCode,
    
    // Ожидает пароль
    WaitPassword,
    
    // Готов
    Ready,
    
    // Не аутентифицирован
    LoggingOut
}