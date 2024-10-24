namespace TgInstanceAgent.Domain.Instances.Exceptions;

/// <summary>
/// Исключение для совпадающих URL вебхуков.
/// </summary>
public class DuplicateWebhookUrlException() : Exception("The same webhook url already exists.");