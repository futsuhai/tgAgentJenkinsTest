using AutoMapper;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Enums;
using static TdLib.TdApi;

namespace TgInstanceAgent.Infrastructure.Telegram.Mapper.Converters;

/// <summary>
/// Конвертер для преобразования enum TgMessageFilterType в объект SearchMessagesFilter.
/// </summary>
public class SearchMessagesFilterConverter : ITypeConverter<TgMessageFilterType, SearchMessagesFilter>
{
    /// <summary>
    /// Преобразует enum TgMessageFilterType в объект SearchMessagesFilter.
    /// </summary>
    /// <param name="source">Объект TgMessageFilterType для преобразования.</param>
    /// <param name="destination">Целевой объект SearchMessagesFilter.</param>
    /// <param name="context">Контекст преобразования.</param>
    /// <returns>Преобразованный объект SearchMessagesFilter.</returns>
    public SearchMessagesFilter Convert(TgMessageFilterType source, SearchMessagesFilter destination, ResolutionContext context)
    {
        // Преобразуем объект TgMessageFilterType в объект SearchMessagesFilter с помощью оператора switch
        return source switch
        {
            TgMessageFilterType.Animation => new SearchMessagesFilter.SearchMessagesFilterAnimation(),
            TgMessageFilterType.Audio => new SearchMessagesFilter.SearchMessagesFilterAudio(),
            TgMessageFilterType.ChatPhoto => new SearchMessagesFilter.SearchMessagesFilterPhoto(),
            TgMessageFilterType.Document => new SearchMessagesFilter.SearchMessagesFilterDocument(),
            TgMessageFilterType.FailedToSend => new SearchMessagesFilter.SearchMessagesFilterFailedToSend(),
            TgMessageFilterType.Mention => new SearchMessagesFilter.SearchMessagesFilterMention(),
            TgMessageFilterType.Photo => new SearchMessagesFilter.SearchMessagesFilterPhoto(),
            TgMessageFilterType.PhotoAndVideo => new SearchMessagesFilter.SearchMessagesFilterPhotoAndVideo(),
            TgMessageFilterType.Pinned => new SearchMessagesFilter.SearchMessagesFilterPinned(),
            TgMessageFilterType.UnreadMention => new SearchMessagesFilter.SearchMessagesFilterUnreadMention(),
            TgMessageFilterType.UnreadReaction => new SearchMessagesFilter.SearchMessagesFilterUnreadReaction(),
            TgMessageFilterType.Url => new SearchMessagesFilter.SearchMessagesFilterUrl(),
            TgMessageFilterType.Video => new SearchMessagesFilter.SearchMessagesFilterVideo(),
            TgMessageFilterType.VideoNote => new SearchMessagesFilter.SearchMessagesFilterVideoNote(),
            TgMessageFilterType.VoiceAndVideoNote => new SearchMessagesFilter.SearchMessagesFilterVoiceAndVideoNote(),
            TgMessageFilterType.VoiceNote => new SearchMessagesFilter.SearchMessagesFilterVoiceNote(),
            TgMessageFilterType.Empty => new SearchMessagesFilter.SearchMessagesFilterEmpty(),
            _ => throw new ArgumentOutOfRangeException(nameof(source), source, null)
        };
    }
}