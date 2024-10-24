namespace TgInstanceAgent.Infrastructure.Web.Components.Interfaces;

/// <summary>
/// Интерфейс для моделей с входными данными, представляющие информацию о файле миниатюры.
/// </summary>
public interface IWithInputThumbnail
{
    /// <summary>
    /// Превью альбома
    /// </summary>
    public IFormFile Thumbnail { get; init; }
}