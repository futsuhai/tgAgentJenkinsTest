namespace TgInstanceAgent.Application.Abstractions.TelegramClient.Functions.Components.Visitors;

/// <summary>
/// Интерфейс посетителя типов прокси
/// </summary>
public interface IProxyVisitor
{
    /// <summary>
    /// Метод посещения типа прокси Http
    /// </summary>
    /// <param name="tgInputProxyTypeHttp">Тип прокси Http</param>
    void Visit(TgInputProxyTypeHttp tgInputProxyTypeHttp);

    /// <summary>
    /// Метод посещения типа прокси Socks
    /// </summary>
    /// <param name="tgInputProxyTypeSocks">Тип прокси Socks</param>
    /// <returns></returns>
    void Visit(TgInputProxyTypeSocks tgInputProxyTypeSocks);

    /// <summary>
    /// Метод посещения типа прокси Mtproto
    /// </summary>
    /// <param name="tgInputProxyTypeMtproto">Тип прокси Mtproto</param>
    /// <returns></returns>
    void Visit(TgInputProxyTypeMtproto tgInputProxyTypeMtproto);
}