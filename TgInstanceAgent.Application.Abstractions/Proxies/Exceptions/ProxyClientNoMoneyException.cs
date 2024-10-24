namespace TgInstanceAgent.Application.Abstractions.Proxies.Exceptions;

/// <summary>
/// Ошибка баланса. На вашем балансе отсутствуют средства, либо их не хватает для покупки запрашиваемого кол-ва прокси
/// </summary>
public class ProxyClientNoMoneyException(int errorId, string message) : ProxyClientException(errorId, message);