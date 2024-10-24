namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое возникает, если не указано ни одно свойство, необходимое для установки типа прокси сервера
/// </summary>
public class InvalidProxyTypeException()
    : Exception("None of the mandatory parameters are specified for proxy type");
