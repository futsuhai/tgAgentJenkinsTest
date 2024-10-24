using TdLib;

namespace TgInstanceAgent.Infrastructure.Telegram.Client;

/// <summary>
/// Класс для управления количеством непрочитанных сообщений.
/// </summary>
public class UnreadMessagesCount
{
    /// <summary>
    /// Приватное поле для хранения массива обновлений количества непрочитанных сообщений.
    /// </summary>
    private TdApi.Update.UpdateUnreadMessageCount[] _unreadMessagesCount = [];

    /// <summary>
    /// Коллекция непрочитанных сообщений.
    /// </summary>
    public IEnumerable<TdApi.Update.UpdateUnreadMessageCount> ChatLists => _unreadMessagesCount;

    /// <summary>
    /// Обрабатывает обновления.
    /// </summary>
    /// <param name="update">Обновление.</param>
    public void ProcessUpdate(TdApi.Update update)
    {
        // Если обновление не является обновлением количества непрочитанных сообщений, выходим из метода.
        if (update is not TdApi.Update.UpdateUnreadMessageCount updateUnreadMessageCount) return;

        // Создаем новый список на основе текущего массива обновлений количества непрочитанных сообщений.
        var newUnreadMessagesCount = _unreadMessagesCount.ToList();

        // Перебираем все элементы в текущем массиве обновлений количества непрочитанных сообщений.
        foreach (var chatCount in _unreadMessagesCount)
        {
            // Если тип данных списка чатов не совпадает с типом данных списка чатов в обновлении, продолжаем цикл.
            if (chatCount.ChatList.DataType != updateUnreadMessageCount.ChatList.DataType) continue;

            // Если список чатов является папкой чатов, проверяем совпадение идентификаторов папок.
            if (chatCount.ChatList is TdApi.ChatList.ChatListFolder pl && updateUnreadMessageCount.ChatList is TdApi.ChatList.ChatListFolder rl)
            {
                // Если идентификаторы папок не совпадают, продолжаем цикл.
                if (pl.ChatFolderId != rl.ChatFolderId) continue;
            }

            // Удаляем текущий элемент из нового списка обновлений количества непрочитанных сообщений.
            newUnreadMessagesCount.Remove(chatCount);

            // Прерываем цикл, так как нашли и обработали нужный элемент.
            break;
        }

        // Добавляем новое обновление количества непрочитанных сообщений в новый список.
        newUnreadMessagesCount.Add(updateUnreadMessageCount);

        // Обновляем массив обновлений количества непрочитанных сообщений новым списком.
        _unreadMessagesCount = newUnreadMessagesCount.ToArray();
    }
}
