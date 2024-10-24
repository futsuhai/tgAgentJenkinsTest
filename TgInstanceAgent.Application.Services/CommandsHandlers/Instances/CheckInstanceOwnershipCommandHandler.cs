using MediatR;
using Microsoft.Extensions.Caching.Memory;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;
using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Services.Extensions;
using TgInstanceAgent.Domain.Abstractions.Repositories;

namespace TgInstanceAgent.Application.Services.CommandsHandlers.Instances;

/// <summary>
/// Обработчик команды для проверки владения инстансом.
/// </summary>
/// <param name="repository">Репозиторий, предоставляющий методы для работы с инстансами.</param>
public class CheckInstanceOwnershipCommandHandler(IInstanceRepository repository, IMemoryCache cache)
    : IRequestHandler<CheckInstanceOwnershipCommand>
{
    /// <summary>
    /// Обрабатывает команду проверки владения инстансом.
    /// </summary>
    /// <param name="request">Команда с параметрами для проверки владения инстансом.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию проверки владения инстансом.</returns>
    public async Task Handle(CheckInstanceOwnershipCommand request, CancellationToken cancellationToken)
    {
        // Получаем инстанс из кэша/репозитория по указанному идентификатору асинхронно
        var instance = await cache.GetInstanceAsync(request.InstanceId, repository, cancellationToken);
        
        // Проверяем, что идентификатор пользователя инстанса соответствует идентификатору пользователя из запроса,
        // иначе выбрасываем исключение InstanceNotBelongToUserException
        if (instance.UserId != request.UserId) throw new InstanceNotBelongToUserException();
    }
}