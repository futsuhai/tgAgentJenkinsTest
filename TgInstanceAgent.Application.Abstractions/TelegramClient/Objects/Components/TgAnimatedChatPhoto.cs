namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Components;

/// <summary>
/// Представляет анимацию, установленную в виде фотографии профиля
/// </summary>
public class TgAnimatedChatPhoto
{
    /// <summary>
    /// Ширина и высота анимации.
    /// </summary>
    public required int Length { get; init; }

    /// <summary>
    /// Файл анимации.
    /// </summary>
    public required TgFile File { get; init; }

    /// <summary>
    /// Временная метка кадра, используемого в качестве статической фотографии в чате.
    /// </summary>
    public double? MainFrameTimestamp { get; init; }
}