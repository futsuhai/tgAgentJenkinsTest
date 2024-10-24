using TdLib;

namespace TgInstanceAgent.Infrastructure.Telegram.Client;

public class Chats
{
    /// <summary>
    /// Словарь чатов, где ключ - идентификатор чата, а значение - объект чата.
    /// Чаты загружаются по одному через обновления, поэтому при загрузке они добавляются в эту коллекцию,
    /// чтобы потом быть возвращенными пользователю
    /// </summary>
    private readonly Dictionary<long, TdApi.Chat> _chats = [];

    /// <summary>
    /// Список чатов.
    /// </summary>
    public IEnumerable<TdApi.Chat> ChatsList => _chats.Values;

    /// <summary>
    /// Обрабатывает обновления чатов.
    /// </summary>
    /// <param name="update">Обновление.</param>
    public void ProcessUpdate(TdApi.Update update)
    {
        // Обновление информации о новом чате
        switch (update)
        {
            // Обновление информации о чате
            case TdApi.Update.UpdateNewChat updateChat:

                // Добавляем чат в список чатов.
                // Используем идентификатор чата в качестве ключа и сам чат в качестве значения
                _chats[updateChat.Chat.Id] = updateChat.Chat;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление позиции чата
            case TdApi.Update.UpdateChatPosition chatPosition:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(chatPosition.ChatId, out var chat)) return;

                // Удаляем старую позицию чата и добавляем новую позицию, если она имеет порядок, отличный от 0
                var newPositions = RemovePosition(chat.Positions, chatPosition.Position);

                // Если Order не равен 0 (позиция удалена), добавляем новую позицию
                if (chatPosition.Position.Order != 0) newPositions.Add(chatPosition.Position);

                // Обновляем позиции чата
                chat.Positions = newPositions.ToArray();

                // Завершаем обработку текущего обновления.
                break;

            // Обновление последнего сообщения в чате
            case TdApi.Update.UpdateChatLastMessage chatLastMessage:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(chatLastMessage.ChatId, out chat)) return;

                // Обновляем последнее сообщение в чате.
                chat.LastMessage = chatLastMessage.LastMessage;

                // Преобразуем текущие позиции чата в список.
                newPositions = chat.Positions.ToList();

                // Перебираем новые позиции чата из обновления.
                foreach (var chatPosition in chatLastMessage.Positions)
                {
                    // Удаляем старую позицию чата и добавляем новую позицию, если она имеет порядок, отличный от 0.
                    newPositions = RemovePosition(newPositions, chatPosition);
                    if (chatPosition.Order != 0) newPositions.Add(chatPosition);
                }

                // Обновляем позиции чата новым списком позиций.
                chat.Positions = newPositions.ToArray();

                // Завершаем обработку текущего обновления.
                break;


            // Обновление прав доступа в чате
            case TdApi.Update.UpdateChatPermissions chatPermissions:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(chatPermissions.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.Permissions = chatPermissions.Permissions;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление фото чата
            case TdApi.Update.UpdateChatPhoto chatPhoto:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(chatPhoto.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.Photo = chatPhoto.Photo;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление заголовка чата
            case TdApi.Update.UpdateChatTitle chatTitle:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(chatTitle.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.Title = chatTitle.Title;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление статуса эмодзи чата
            case TdApi.Update.UpdateChatEmojiStatus emojiStatus:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(emojiStatus.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.EmojiStatus = emojiStatus.EmojiStatus;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление ожидающих запросов на вступление в чат
            case TdApi.Update.UpdateChatPendingJoinRequests pendingJoinRequests:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(pendingJoinRequests.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.PendingJoinRequests = pendingJoinRequests.PendingJoinRequests;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление списка заблокированных пользователей в чате
            case TdApi.Update.UpdateChatBlockList chatBlockList:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(chatBlockList.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.BlockList = chatBlockList.BlockList;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление доступных реакций в чате
            case TdApi.Update.UpdateChatAvailableReactions availableReactions:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(availableReactions.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.AvailableReactions = availableReactions.AvailableReactions;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление времени автоудаления сообщений в чате
            case TdApi.Update.UpdateChatMessageAutoDeleteTime autoDeleteTime:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(autoDeleteTime.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.MessageAutoDeleteTime = autoDeleteTime.MessageAutoDeleteTime;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление статуса "помечено как непрочитанное" в чате
            case TdApi.Update.UpdateChatIsMarkedAsUnread markedAsUnread:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(markedAsUnread.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.IsMarkedAsUnread = markedAsUnread.IsMarkedAsUnread;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление количества непрочитанных сообщений в чате
            case TdApi.Update.UpdateChatReadInbox readInbox:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(readInbox.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.UnreadCount = readInbox.UnreadCount;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление статуса "просмотр как темы" в чате
            case TdApi.Update.UpdateChatViewAsTopics viewAsTopics:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(viewAsTopics.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.ViewAsTopics = viewAsTopics.ViewAsTopics;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление количества непрочитанных реакций в чате
            case TdApi.Update.UpdateChatUnreadReactionCount unreadReactionCount:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(unreadReactionCount.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.UnreadReactionCount = unreadReactionCount.UnreadReactionCount;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление количества непрочитанных упоминаний в чате
            case TdApi.Update.UpdateChatUnreadMentionCount unreadMentionCount:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(unreadMentionCount.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.UnreadMentionCount = unreadMentionCount.UnreadMentionCount;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление статуса наличия запланированных сообщений в чате
            case TdApi.Update.UpdateChatHasScheduledMessages hasScheduledMessages:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(hasScheduledMessages.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.HasScheduledMessages = hasScheduledMessages.HasScheduledMessages;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление статуса наличия защищенного контента в чате
            case TdApi.Update.UpdateChatHasProtectedContent hasProtectedContent:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(hasProtectedContent.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.HasProtectedContent = hasProtectedContent.HasProtectedContent;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление фона чата
            case TdApi.Update.UpdateChatBackground background:

                // Проверяем, существует ли чат в словаре
                if (!_chats.TryGetValue(background.ChatId, out chat)) return;

                // Обновляем данные чата
                chat.Background = background.Background;

                // Завершаем обработку текущего обновления.
                break;

            // Обновление файла
            case TdApi.Update.UpdateFile updateFile:

                // Перебираем все чаиы в коллекции.
                foreach (var chatsValue in _chats.Values)
                {
                    // Проверяем и обновляем маленькое фото чата.
                    Files.CheckAndUpdateFile(updateFile.File, chatsValue.Photo, nameof(chatsValue.Photo.Small));

                    // Проверяем и обновляем большое фото чата.
                    Files.CheckAndUpdateFile(updateFile.File, chatsValue.Photo, nameof(chatsValue.Photo.Big));

                    // Проверяем и обновляем документ фона чата.
                    Files.CheckAndUpdateFile(updateFile.File, chatsValue.Background?.Background?.Document,
                        nameof(chatsValue.Background.Background.Document.Document_));

                    // Проверяем и обновляем миниатюру документа фона чата.
                    Files.CheckAndUpdateFile(updateFile.File, chatsValue.Background?.Background?.Document?.Thumbnail,
                        nameof(chatsValue.Background.Background.Document.Thumbnail.File));

                    // todo: last message update
                }
                
                // Завершаем обработку текущего обновления.
                break;
        }
    }

    /// <summary>
    /// Удаляет позицию чата из списка позиций.
    /// </summary>
    /// <param name="oldPositions">Старые позиции чата.</param>
    /// <param name="position">Новая позиция чата.</param>
    /// <returns>Список новых позиций чата.</returns>
    private static List<TdApi.ChatPosition> RemovePosition(IReadOnlyCollection<TdApi.ChatPosition> oldPositions,
        TdApi.ChatPosition position)
    {
        // Создаем список новых позиций, первоначально заполняем его старыми позициями
        var newPositions = oldPositions.ToList();

        // Перебираем старые позиции
        foreach (var oldPosition in oldPositions)
        {
            // Если тип перебираемой позиции не равен типу новой позиции - оставляем в списке
            if (oldPosition.List.DataType != position.List.DataType) continue;

            // Если типы позиций совпадают

            // Если это позиции в папке
            if (oldPosition.List is TdApi.ChatList.ChatListFolder pl &&
                position.List is TdApi.ChatList.ChatListFolder rl)
            {
                // Если папка перебираемой позиции не равна папке перебираемой позиции - оставляем в списке
                if (pl.ChatFolderId != rl.ChatFolderId) continue;
            }

            // Убираем старую позицию из списка
            newPositions.Remove(oldPosition);

            // Завершаем обработку позиций, так как удалили нужную.
            break;
        }

        // Возвращаем массив новых позиций
        return newPositions;
    }
}