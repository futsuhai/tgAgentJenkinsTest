using TdLib;

namespace TgInstanceAgent.Infrastructure.Telegram.Client;

/// <summary>
/// Класс для управления пользователями.
/// </summary>
public class Users
{
    /// <summary>
    /// Приватное поле для хранения словаря пользователей, где ключ - идентификатор пользователя, а значение - объект пользователя.
    /// </summary>
    private readonly Dictionary<long, TdApi.User> _users = [];

    /// <summary>
    /// Свойство для получения списка всех пользователей.
    /// </summary>
    public IEnumerable<TdApi.User> UsersList => _users.Values;

    /// <summary>
    /// Обрабатывает обновления.
    /// </summary>
    /// <param name="update">Обновление.</param>
    public void ProcessUpdate(TdApi.Update update)
    {
        // Обрабатываем различные типы обновлений с помощью switch-case.
        switch (update)
        {
            // Обновление информации о пользователе.
            case TdApi.Update.UpdateUser updateUser:

                // Добавляем пользователя в коллекцию пользователей.
                _users[updateUser.User.Id] = updateUser.User;
                break;

            // Обновление информации о пользователе.
            case TdApi.Update.UpdateUserStatus updateUser:

                // Если пользователь не найден в коллекции, выходим из метода.
                if (!_users.TryGetValue(updateUser.UserId, out var user)) return;

                // Обновляем статус пользователя.
                user.Status = updateUser.Status;
                break;

            // Обновление файла.
            case TdApi.Update.UpdateFile updateFile:

                // Перебираем всех пользователей в коллекции.
                foreach (var userValue in _users.Values)
                {
                    // Проверяем и обновляем маленькое фото профиля пользователя.
                    Files.CheckAndUpdateFile(updateFile.File, userValue.ProfilePhoto,
                        nameof(userValue.ProfilePhoto.Small));

                    // Проверяем и обновляем большое фото профиля пользователя.
                    Files.CheckAndUpdateFile(updateFile.File, userValue.ProfilePhoto,
                        nameof(userValue.ProfilePhoto.Big));
                }

                break;
        }
    }

    /// <summary>
    /// Получаем пользователей затронутых в событии сообщения
    /// <param name="message">Сообщение</param>
    /// </summary>
    /// <returns>Коллекция пользователей</returns>
    public IEnumerable<TdApi.User> GetUsersForMessage(TdApi.Message message)
    {
        // Идентификаторы пользователей, которые фигурируют в специальных сообщениях
        var specialMessageMemberIds = new List<long>();

        switch (message.Content)
        {
            // Если сообщение об исключении пользователя
            case TdApi.MessageContent.MessageChatDeleteMember deletedMember:
                specialMessageMemberIds.Add(deletedMember.UserId);
                break;

            // Если сообщение о добавлении пользователей
            case TdApi.MessageContent.MessageChatAddMembers addedMembers:
                specialMessageMemberIds.AddRange(addedMembers.MemberUserIds);
                break;

            // Если сообщение это контакт
            case TdApi.MessageContent.MessageContact contact:
                specialMessageMemberIds.Add(contact.Contact.UserId);
                break;
        }

        if (message.SenderId is TdApi.MessageSender.MessageSenderUser senderUser)
        {
            specialMessageMemberIds.Add(senderUser.UserId);
        }

        // Выбираем пользователей
        return _users.Values
            .Where(u => specialMessageMemberIds.Contains(u.Id))
            .ToArray();
    }

    /// <summary>
    /// Получаем пользователей затронутых в обновлении чата
    /// </summary>
    /// <param name="chats">Чаты</param>
    /// <returns>Коллекция пользователей</returns>
    public IReadOnlyCollection<TdApi.User> GetUsersForChats(TdApi.Chat[] chats)
    {
        // Идентификаторы пользователей с которыми ведётся приватный чат
        var privateChatsUserIds = new List<long>();

        // Идентификаторы пользователей, которые фигурируют в специальных сообщениях
        var specialMessageMemberIds = new List<long>();

        // Идентификаторы отправителей последних сообщений
        var lastMessageSenderIds = new List<long>();

        // Получаем идентификаторы пользователей с которыми ведётся приватный чат
        foreach (var chat in chats)
        {
            // Если чат приватный
            if (chat.Type is TdApi.ChatType.ChatTypePrivate)
            {
                // Записываем идентификатор пользователя с которым ведётся приватный чат
                privateChatsUserIds.Add(chat.Id);
            }

            // Если последнее сообщение не null
            if (chat.LastMessage != null)
            {
                // Получаем идентификатор отправителя последнего сообщения
                if (chat.LastMessage.SenderId is TdApi.MessageSender.MessageSenderUser senderUser)
                {
                    lastMessageSenderIds.Add(senderUser.UserId);
                }

                // Проверяем тип контента последнего сообщения
                switch (chat.LastMessage.Content)
                {
                    // Если сообщение об исключении пользователя
                    case TdApi.MessageContent.MessageChatDeleteMember deletedMember:
                        specialMessageMemberIds.Add(deletedMember.UserId);
                        break;

                    // Если сообщение о добавлении пользователей
                    case TdApi.MessageContent.MessageChatAddMembers addedMembers:
                        specialMessageMemberIds.AddRange(addedMembers.MemberUserIds);
                        break;
                }
            }
        }

        // Выбираем пользователей
        return _users.Values
            .Where(u =>
                // Включаем пользователей, инициаторов последнего сообщения
                lastMessageSenderIds.Contains(u.Id) ||

                // Включаем пользователей, которые фигурируют в специальных сообщениях
                specialMessageMemberIds.Contains(u.Id) ||

                // Включаем пользователей, с которыми ведётся приватный чат
                privateChatsUserIds.Contains(u.Id)
            )
            .ToArray();
    }
}