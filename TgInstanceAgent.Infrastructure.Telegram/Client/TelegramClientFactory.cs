using AutoMapper;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Interfaces;
using TgInstanceAgent.Infrastructure.Telegram.Structs;

namespace TgInstanceAgent.Infrastructure.Telegram.Client;

/// <summary>
/// Реализация фабрики телеграм клиента
/// </summary>
/// <param name="telegramApp">Данные приложения телеграм</param>
public class TelegramClientFactory(TelegramApp telegramApp, IMapper mapper) : ITelegramClientFactory
{
    /// <inheritdoc/>
    /// <summary>
    /// Метод для создания клиента Telegram
    /// </summary>
    public ITelegramClient FactoryMethod(string filePath)
    {
        // Создаем и возвращаем новый клиент телеграм
        return new TelegramClient(telegramApp, filePath, mapper);
    }
}