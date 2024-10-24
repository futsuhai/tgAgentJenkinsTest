using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Interfaces;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components;

/// <summary>
/// Абстрактные данные сообщения.
/// </summary>
public abstract class TgInputMessageContent
{
    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public abstract Task AcceptAsync(IMessageContentVisitor messageContentVisitor);
}

/// <summary>
/// Данные сообщения типа dice.
/// </summary>
public class TgInputDiceMessage : TgInputMessageContent
{
    /// <summary>
    /// Эмодзи
    /// </summary>
    public required string Emoji { get; init; }
    
    /// <summary>
    /// Указывает, должен ли черновик сообщения в чате быть удален
    /// </summary>
    public bool ClearDraft { get; init; }
    
    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом dice
        return messageContentVisitor.VisitAsync(this);
    }
}

/// <summary>
/// Данные сообщения типа местоположение.
/// </summary>
public class TgInputLocationMessage : TgInputMessageContent
{
    /// <summary>
    /// Широта.
    /// </summary>
    public required double Latitude { get; init; }

    /// <summary>
    /// Долгота.
    /// </summary>
    public required double Longitude { get; init; }

    /// <summary>
    /// Горизонтальная точность.
    /// </summary>
    public double? HorizontalAccuracy { get; init; }

    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом сообщения
        return messageContentVisitor.VisitAsync(this);
    }
}

/// <summary>
/// Данные сообщения с контактом.
/// </summary>
public class TgInputContactMessage : TgInputMessageContent
{
    /// <summary>
    /// Имя.
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    /// Номер телефона пользователя. Если установлено, Chat и Username могут быть не заданы.
    /// </summary>
    public required string PhoneNumber { get; init; }
    
    /// <summary>
    /// Идентификатор пользователя.
    /// При отправке запроса по идентификатору, необходимо быть уверенным, что приложению известен данный пользователь.
    /// </summary>
    public long? Id { get; init; }
    
    /// <summary>
    /// Визитка.
    /// </summary>
    public string? VCard { get; init; }

    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом сообщения
        return messageContentVisitor.VisitAsync(this);
    }
}

/// <summary>
/// Данные сообщения типа текст.
/// </summary>
public class TgInputTextMessage : TgInputMessageContent
{
    /// <summary>
    /// Текст сообщения.
    /// </summary>
    public required string Text { get; init; }

    /// <summary>
    /// Отключить предварительный просмотр URL.
    /// </summary>
    public bool DisableUrlPreview { get; init; }

    /// <summary>
    /// URL предварительного просмотра.
    /// </summary>
    public string? UrlPreview { get; init; }

    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом сообщения
        return messageContentVisitor.VisitAsync(this);
    }
}

/// <summary>
/// Данные сообщения типа аудио.
/// </summary>
public class TgInputAudioMessage : TgInputMessageContent, ITgInputDataWithFile
{
    /// <summary>
    /// Название аудио.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Исполнитель аудио.
    /// </summary>
    public string? Performer { get; init; }
    
    /// <summary>
    /// Файл, содержащий аудио.
    /// </summary>
    public required TgInputFileData File { get; init; }
    
    /// <summary>
    /// Миниатюра изображения файла.
    /// </summary>
    public TgInputThumbnail? AlbumCoverThumbnail { get; init; }
    
    /// <summary>
    /// Подпись к файлу.
    /// </summary>
    public string? Caption { get; init; }

    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом сообщения
        return messageContentVisitor.VisitAsync(this);
    }
}

/// <summary>
/// Данные сообщения типа голосовое сообщение.
/// </summary>
public class TgInputVoiceNoteMessage : TgInputMessageContent, ITgInputDataWithFile
{
    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом сообщения
        return messageContentVisitor.VisitAsync(this);
    }

    /// <summary>
    /// Файл, содержащий голосовое сообщение.
    /// </summary>
    public required TgInputFileData File { get; init; }
    
    /// <summary>
    /// Подпись к файлу.
    /// </summary>
    public string? Caption { get; init; }
}

/// <summary>
/// Данные сообщения типа видео сообщение.
/// </summary>
public class TgInputVideoNoteMessage : TgInputMessageContent, ITgInputDataWithFile
{
    /// <summary>
    /// Файл, содержащий видео сообщение.
    /// </summary>
    public required TgInputFileData File { get; init; }
    
    /// <summary>
    /// Миниатюрное изображение файла.
    /// </summary>
    public TgInputThumbnail? Thumbnail { get; init; }
    
    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом сообщения
        return messageContentVisitor.VisitAsync(this);
    }
}

/// <summary>
/// Данные сообщения типа видео.
/// </summary>
public class TgInputVideoMessage : TgInputMessageContent, ITgInputDataWithFile
{
    /// <summary>
    /// Файл, содержащий видео.
    /// </summary>
    public required TgInputFileData File { get; init; }
    
    /// <summary>
    /// Подпись к файлу.
    /// </summary>
    public string? Caption { get; init; }
    
    /// <summary>
    /// Флаг, необходимо ли показывать активность, что пользователь записывает видео.
    /// </summary>
    public bool NeedShowRecordingActivity { get; init; }
    
    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом сообщения
        return messageContentVisitor.VisitAsync(this);
    }
}

/// <summary>
/// Данные сообщения типа фото.
/// </summary>
public class TgInputPhotoMessage : TgInputMessageContent, ITgInputDataWithFile
{
    /// <summary>
    /// Файл, содержащий фото.
    /// </summary>
    public required TgInputFileData File { get; init; }
    
    /// <summary>
    /// Подпись к файлу.
    /// </summary>
    public string? Caption { get; init; }
    
    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом сообщения
        return messageContentVisitor.VisitAsync(this);
    }
}

/// <summary>
/// Данные сообщения типа документ.
/// </summary>
public class TgInputDocumentMessage : TgInputMessageContent, ITgInputDataWithFile
{
    /// <summary>
    /// Файл, содержащий документ.
    /// </summary>
    public required TgInputFileData File { get; init; }
    
    /// <summary>
    /// Подпись к файлу.
    /// </summary>
    public string? Caption { get; init; }
    
    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом сообщения
        return messageContentVisitor.VisitAsync(this);
    }
}

/// <summary>
/// Данные сообщения типа стикер.
/// </summary>
public class TgInputStickerMessage : TgInputMessageContent, ITgInputDataWithFile
{
    /// <summary>
    /// Файл, содержащий стикер.
    /// </summary>
    public required TgInputFileData File { get; init; }
    
    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом сообщения
        return messageContentVisitor.VisitAsync(this);
    }
}

/// <summary>
/// Данные сообщения типа анимации.
/// </summary>
public class TgInputAnimationMessage : TgInputMessageContent, ITgInputDataWithFile
{
    /// <summary>
    /// Файл, содержащий анимацию.
    /// </summary>
    public required TgInputFileData File { get; init; }
    
    /// <summary>
    /// Истинно, если превью анимации должно быть закрыто.
    /// </summary>
    public bool HasSpoiler { get; init; }
    
    /// <summary>
    /// Подпись к файлу.
    /// </summary>
    public string? Caption { get; init; }
    
    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом сообщения
        return messageContentVisitor.VisitAsync(this);
    }
}

/// <summary>
/// Данные сообщения типа истории(стори).
/// </summary>
public class TgInputStoryMessage : TgInputMessageContent
{
    /// <summary>
    /// Идентификатор чата, в котором опубликована история
    /// </summary>
    public required long StorySenderChatId { get; init; }
    
    /// <summary>
    /// Идентификатор истории
    /// </summary>
    public required int StoryId { get; init; }
    
    /// <summary>
    /// Принять посетителя сообщения.
    /// </summary>
    public override Task AcceptAsync(IMessageContentVisitor messageContentVisitor)
    {
        // Вызываем перегрузку метода Visit у посетителя с данным типом сообщения
        return messageContentVisitor.VisitAsync(this);
    }
}