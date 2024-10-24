namespace TgInstanceAgent.Application.Abstractions.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда не удается найти настройки пересылки.
/// </summary>
public class ForwardEntryNotFoundException() : Exception("Forward entry not found");