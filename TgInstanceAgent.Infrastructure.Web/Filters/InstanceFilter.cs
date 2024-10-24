using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using TgInstanceAgent.Application.Abstractions.Commands.Instances;

namespace TgInstanceAgent.Infrastructure.Web.Filters;

/// <summary>
/// Фильтр для обработки действий с экземплярами объектов.
/// </summary>
public class InstanceFilter : ActionFilterAttribute
{
    /// <summary>
    /// Выполняет асинхронное действие перед выполнением действия.
    /// </summary>
    /// <param name="context">Контекст выполнения действия.</param>
    /// <param name="next">Делегат для выполнения следующего действия в цепочке.</param>
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // Получаем экземпляр сервиса ISender из контекста запроса
        var mediator = context.HttpContext.RequestServices.GetRequiredService<ISender>();

        // Извлекаем значение параметра "instanceId" из запроса и преобразуем его в строку
        var instanceId = context.HttpContext.Request.RouteValues["instanceId"]?.ToString() ??
                         throw new NullReferenceException();

        // Извлекаем идентификатор пользователя из контекста запроса по ClaimTypes.NameIdentifier, если его нет, выбрасываем исключение NullReferenceException
        var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                     throw new NullReferenceException();

        // Отправляем команду CheckInstanceOwnershipCommand через медиатор для проверки владения инстансом
        await mediator.Send(new CheckInstanceOwnershipCommand
        {
            InstanceId = Guid.Parse(instanceId),
            UserId = Guid.Parse(userId)
        });

        // Вызываем следующий middleware в конвейере обработки запроса
        await next();
    }
}