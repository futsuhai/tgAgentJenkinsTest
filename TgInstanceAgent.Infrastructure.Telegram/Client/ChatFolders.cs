using TdLib;

namespace TgInstanceAgent.Infrastructure.Telegram.Client;

public class ChatFolders
{
    /// <summary>
    /// Список папок чатов
    /// </summary>
    private TdApi.ChatFolderInfo[] _folders = [];

    /// <summary>
    /// Список папок чатов
    /// </summary>
    public IEnumerable<TdApi.ChatFolderInfo> Folders => _folders;
    
    /// <summary>
    /// Позиция основного списка чатов среди папок чатов, 0-based
    /// </summary>
    public int MainChatListPosition { get; private set; }

    /// <summary>
    /// True, если теги папок включены
    /// </summary>
    public bool AreTagsEnabled { get; private set; }

    /// <summary>
    /// Обрабатывает обновления папок чатов.
    /// </summary>
    /// <param name="update">Обновление.</param>
    public void ProcessUpdate(TdApi.Update update)
    {
        if (update is not TdApi.Update.UpdateChatFolders updateChatFolders) return;

        _folders = updateChatFolders.ChatFolders;
        
        MainChatListPosition = updateChatFolders.MainChatListPosition;
        
        AreTagsEnabled = updateChatFolders.AreTagsEnabled;
    }
}