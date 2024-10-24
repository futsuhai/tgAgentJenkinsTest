using AutoMapper;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Enums;
using static TdLib.TdApi;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper.Converters;

/// <summary>
/// Конвертер для преобразования объекта ChatList в объект TgChatList.
/// </summary>
public class ChatListConverter : ITypeConverter<ChatList, TgChatList>
{
    /// <summary>
    /// Преобразует объект ChatList в объект TgChatList.
    /// </summary>
    /// <param name="source">Объект ChatList для преобразования.</param>
    /// <param name="destination">Целевой объект TgChatList.</param>
    /// <param name="context">Контекст преобразования.</param>
    /// <returns>Преобразованный объект TgChatList.</returns>
    public TgChatList Convert(ChatList? source, TgChatList destination, ResolutionContext context)
    {
        // Преобразуем объект ChatList в объект TgChatList с помощью оператора switch
        return source switch
        {
            // Возвращаем TgChatList.Archive, если source равен ChatList.ChatListArchive
            ChatList.ChatListArchive => TgChatList.Archive,
            
            // Возвращаем TgChatList.Folder, если source равен ChatList.ChatListFolder
            ChatList.ChatListFolder => TgChatList.Folder,
            
            // Возвращаем TgChatList.Main, если source равен ChatList.ChatListMain
            ChatList.ChatListMain => TgChatList.Main,
            
            // Выбрасываем исключение, если source не равен ни одному из ожидаемых значений
            _ => throw new ArgumentOutOfRangeException(nameof(source))
        };
    }
}