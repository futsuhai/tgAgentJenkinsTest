using TdLib;

namespace TgInstanceAgent.Infrastructure.Telegram.Client;

/// <summary>
/// Класс для управления количеством непрочитанных чатов.
/// </summary>
public class UnreadChatsCount
{
    /// <summary>
    /// Приватное поле для хранения массива обновлений количества непрочитанных чатов.
    /// </summary>
    private TdApi.Update.UpdateUnreadChatCount[] _unreadChatsCount = [];

    /// <summary>
    /// Коллекция непрочитанных чатов.
    /// </summary>
    public IEnumerable<TdApi.Update.UpdateUnreadChatCount> ChatLists => _unreadChatsCount;

    /// <summary>
    /// Обрабатывает обновления папок чатов.
    /// </summary>
    /// <param name="update">Обновление.</param>
    public void ProcessUpdate(TdApi.Update update)
    {
        // Если обновление не является обновлением количества непрочитанных чатов, выходим из метода.
        if (update is not TdApi.Update.UpdateUnreadChatCount updateUnreadChatCount) return;

        // Создаем новый список на основе текущего массива обновлений количества непрочитанных чатов.
        var newUnreadChatsCount = _unreadChatsCount.ToList();

        // Перебираем все элементы в текущем массиве обновлений количества непрочитанных чатов.
        foreach (var chatCount in _unreadChatsCount)
        {
            // Если тип данных списка чатов не совпадает с типом данных списка чатов в обновлении, продолжаем цикл.
            if (chatCount.ChatList.DataType != updateUnreadChatCount.ChatList.DataType) continue;

            // Если список чатов является папкой чатов, проверяем совпадение идентификаторов папок.
            if (chatCount.ChatList is TdApi.ChatList.ChatListFolder pl && updateUnreadChatCount.ChatList is TdApi.ChatList.ChatListFolder rl)
            {
                // Если идентификаторы папок не совпадают, продолжаем цикл.
                if (pl.ChatFolderId != rl.ChatFolderId) continue;
            }

            // Удаляем текущий элемент из нового списка обновлений количества непрочитанных чатов.
            newUnreadChatsCount.Remove(chatCount);

            // Прерываем цикл, так как нашли и обработали нужный элемент.
            break;
        }

        // Добавляем новое обновление количества непрочитанных чатов в новый список.
        newUnreadChatsCount.Add(updateUnreadChatCount);

        // Обновляем массив обновлений количества непрочитанных чатов новым списком.
        _unreadChatsCount = newUnreadChatsCount.ToArray();
    }
}
