namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

/// <summary>
/// Представляет исключение, которое возникает, если не указано ни одно свойство, необходимое для настройки приватности истории
/// </summary>
public class InvalidStoryPrivacySettingsException()
    : Exception("None of the mandatory parameters are specified for privacy story settings exception");
