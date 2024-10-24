namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда клиент не ожидает определенного действия (Например ввод пароля, когда клиент ждёт кода подтверждения).
/// </summary>
public class ClientNotExpectActionException(string clientState) : Exception("The client does not expect this action")
{
    /// <summary>
    /// Состояние клиента
    /// </summary>
    public string ClientState { get; } = clientState;
}