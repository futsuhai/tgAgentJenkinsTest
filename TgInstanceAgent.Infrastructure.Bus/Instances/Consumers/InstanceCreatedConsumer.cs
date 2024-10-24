using AutoMapper;
using IntegrationEvents.Instances;
using MassTransit;
using MediatR;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;

namespace TgInstanceAgent.Infrastructure.Bus.Instances.Consumers;

/// <summary>
/// Обработчик интеграционного события InstanceCreatedIntegrationEvent
/// </summary>
/// <param name="mediator">Медиатор</param>
/// <param name="mapper">Маппер</param>
public class InstanceCreatedConsumer(ISender mediator, IMapper mapper) : IConsumer<InstanceCreatedIntegrationEvent>
{
    /// <summary>
    /// Метод обработчик 
    /// </summary>
    /// <param name="context">Контекст сообщения</param>
    public async Task Consume(ConsumeContext<InstanceCreatedIntegrationEvent> context)
    {
        // Мапим команду из события
        var command = mapper.Map<AddInstanceCommand>(context.Message);

        // Отправляем команду на обработку события
        await mediator.Send(command, context.CancellationToken);

        // Отдаем ответ об успешности операции
        await context.RespondAsync(new InstanceAddedToServerIntegrationEvent
        {
            Id = context.Message.InstanceId
        });
    }
}