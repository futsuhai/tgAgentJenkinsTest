namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;

/// <summary>
/// Описывает тип чата
/// </summary>
public enum TgInputChatType
{
    /// <summary>
    /// Приватный чат
    /// </summary>
    PrivateChat,
    
    /// <summary>
    /// Базовый групповой чат
    /// </summary>
    BasicGroupChat,
    
    /// <summary>
    /// Супергруппа
    /// </summary>
    SuperGroupChat,
    
    /// <summary>
    /// Секретный чат
    /// </summary>
    SecretChat
}