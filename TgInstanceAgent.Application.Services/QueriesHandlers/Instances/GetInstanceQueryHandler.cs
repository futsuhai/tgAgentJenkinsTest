using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.DTOs.Instances;
using TgInstanceAgent.Application.Abstractions.Queries.Instances;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;

namespace TgInstanceAgent.Application.Services.QueriesHandlers.Instances;

/// <summary>
/// Обработчик запроса на получение данных инстанса.
/// </summary>
/// <param name="repository">Репозиторий для работы с инстансами.</param>
/// <param name="cache">Кэш.</param> 
public class GetInstanceQueryHandler(IInstanceRepository repository, IMemoryCache cache)
    : IRequestHandler<GetInstanceQuery, InstanceDto>
{
    /// <summary>
    /// Обрабатывает запрос на получение данных инстанса.
    /// </summary>
    /// <param name="request">Запрос на получение данных инстанса.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Данные инстанса.</returns>
    public async Task<InstanceDto> Handle(GetInstanceQuery request, CancellationToken cancellationToken)
    {
        // Получаем инстанс
        var instance = await cache.GetInstanceAsync(request.InstanceId, repository, cancellationToken);

        // Маппим данные в InstanceDto и возвращаем
        return new InstanceDto
        {
            Id = instance.Id,
            ExpirationTimeUtc = instance.ExpirationTimeUtc,
            State = instance.State,
            Enabled = instance.Enabled,
            Proxy = instance.Proxy,
            Restrictions = instance.Restrictions,
            WebhookSetting = instance.WebhookSetting,
            ForwardEntries = instance.ForwardEntries.ToList(),
            WebhookUrls = instance.WebhookUrls.ToList()
        };
    }
}