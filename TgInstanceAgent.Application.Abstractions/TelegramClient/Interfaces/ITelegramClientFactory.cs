namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Interfaces;

/// <summary>
/// Фабрика для создания клиентов Telegram
/// </summary>
public interface ITelegramClientFactory
{
    /// <summary>
    /// Метод для создания клиента Telegram
    /// </summary>
    /// <param name="filePath">Путь к директории с файлами</param>
    /// <returns>Экземпляр сервиса клиента Telegram</returns>
    ITelegramClient FactoryMethod(string filePath);
}