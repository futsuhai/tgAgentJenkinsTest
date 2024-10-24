using System.Diagnostics;
using System.Net;
using TgInstanceAgent.Application.Abstractions.Exceptions;
using TgInstanceAgent.Application.Abstractions.InstancesService.Exceptions;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;
using TgInstanceAgent.Domain.Instances.Exceptions;

namespace TgInstanceAgent.Start.Middlewares;

/// <summary>
/// Промежуточное ПО для обработки исключений.
/// </summary>
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    /// <summary>
    /// Выполнение обработки запроса.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
    /// <returns>Асинхронная задача.</returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Вызов следующего промежуточного ПО в конвейере обработки запроса.
            await next(context);
        }
        catch (Exception exception)
        {
            // Сообщение об ошибке
            string? message;

            // Статус код по умолчанию (400)
            var statusCode = (int)HttpStatusCode.BadRequest;

            // Секция в стандарте
            var section = "15.5.1.";

            // Формируем текст ошибки в зависимости от типа ошибки
            switch (exception)
            {
                // Не удалось найти настройки пересылки
                case ForwardEntryNotFoundException: 
                    message = "Не удалось найти настройки пересылки.";
                    break;
                // Не указан способ доступа к файлу
                case InvalidFileRequestException: 
                    message = "Не указан способ доступа к файлу.";
                    break;
                // Ошибка при заполнении фона профиля
                case InvalidBackgroundFillException: 
                    message = "Не указаны параметры для заполнения фона фотографии профиля.";
                    break;
                // Ошибка при установке типа прокси-сервера
                case InvalidProxyTypeException: 
                    message = "Не указаны обязательные параметры для установки типа прокси-сервера.";
                    break;
                // Ошибка при запросе реакции
                case InvalidReactionRequestException: 
                    message = "Не указаны обязательные параметры для реакции.";
                    break;
                // Ошибка при настройках приватности истории
                case InvalidStoryPrivacySettingsException: 
                    message = "Не указаны обязательные параметры для настройки приватности истории.";
                    break;
                // Пользователь не найден
                case UserNotFoundException: 
                    message = "Пользователь не найден по указанным параметрам.";
                    break;
                // Дублирующаяся запись пересылки
                case DuplicateForwardEntryExсeption: 
                    message = "Пересылка сообщений с таким идентификатором уже существует.";
                    break;
                // Дублирующийся URL вебхука
                case DuplicateWebhookUrlException: 
                    message = "Уже существует вебхук с таким URL.";
                    break;
                // Не указаны параметры идентификации пользователя
                case InvalidUserRequestException: 
                    message = "Не указаны параметры идентификации пользователя.";
                    break;
                // Отчёт не найден
                case ReportNotFoundException: 
                    message = "Отчёт за указанную дату не найден.";
                    break;
                // URL вебхука не найден
                case WebhookUrlNotFoundException: 
                    message = "Не удалось найти URL вебхука.";
                    break;
                // Инстанс не принадлежит пользователю
                case InstanceNotBelongToUserException: 
                    message = "Инстанс не принадлежит пользователю.";
                    break;
                // Сервис инстанса еще не запущен
                case InstanceServiceNotReadyException:
                    message = "Сервер в процессе запуска. Попробуйте через несколько минут.";
                    break;
                // Чат не найден
                case ChatNotFoundException:
                    message = "Чат не найден.";
                    break;
                // Не указано ни одно свойство, необходимое для идентификации чата
                case InvalidChatRequestException: 
                    message = "Не указано ни одно свойство, необходимое для идентификации чата.";
                    break;
                // Состояние телеграм клиента не позволяет выполнить действие
                case ClientNotExpectActionException ex:
                    message = $"Инстанс не может обработать это действие сейчас. Состояние инстанса: {ex.ClientState}.";
                    break;
                // Файл уже загружен
                case FileAlreadyDownloadedException: 
                    message = "Файл уже загружен на сервер и может быть получен.";
                    break;
                // Файл не загружен
                case FileNotDownloadedException ex: 
                    message = $"Файл не загружен на сервер и не может быть получен. Процесс загрузки: {ex.Progress:F}%.";
                    break;
                // Пользователь телеграм с таким номером не найден
                case InvalidPhoneNumberException: 
                    message = "Этот номер телефона не зарегистрирован в телеграм.";
                    break;
                // Неверный код аутентификации
                case InvalidCodeException: 
                    message = "Неверный код.";
                    break;
                // Неверный пароль от аккаунта телеграм
                case InvalidPasswordException: 
                    message = "Неверный пароль.";
                    break;
                // Сообщение не найдено
                case MessageNotFoundException: 
                    message = "Сообщение не найдено.";
                    break;
                // Временная блокировка телеграма
                case TooManyRequestsException ex:
                    var messageContinue = ex.Seconds.HasValue
                        ? $"До окончания блокировки {ex.Seconds} секунд"
                        : "Попробуйте позже.";
                    message = $"Телеграм аккаунт был временно заблокирован. {messageContinue}.";
                    break;
                // Неожиданный тип файла
                case UnexpectedFileTypeException ex: 
                    message = $"Ожидаемый тип сообщения {ex.ExpectedType}, но в запросе указан {ex.FileType}.";
                    break;
                // Ошибка телеграм клиента
                case ClientException ex: 
                    message = $"Ошибка выполнения действия: {ex.Error} ({ex.Code}).";
                    break;
                // Сообщение не содержит файла
                case MessageNotContainFileException: 
                    message = "Сообщение не содержит какого-либо файла.";
                    break;
                // Для продолжения аутентификации нужен пароль
                case PasswordNeededException ex: 
                    message = "Для продолжения аутентификации укажите пароль.";
                    if (!string.IsNullOrEmpty(ex.Hint)) message += $" Подсказка: {ex.Hint}.";
                    break;
                // Инстанс не найден
                case InstanceNotFoundException:
                    message = "Инстанс не найден.";
                    break;
                // Срок действия инстанса истек
                case InstanceExpiredException: 
                    message = "Срок действия инстанса истек.";
                    break;
                // Срок действия прокси истек
                case ProxyExpiredException: 
                    message = "Срок действия прокси истек.";
                    break;
                // Лимит по отправке сообщений
                case MessageLimitException: 
                    message = "Вы исчерпали лимит отправки сообщений и не можете отправить больше сообщений в текущий день.";
                    break;
                // Лимит по загрузке файлов
                case DownloadLimitException:
                    message = "Вы исчерпали лимит загрузки файлов и не можете больше загружать файлы в текущий день.";
                    break;
                // Действие по умолчанию
                default: 

                    // Устанавливаем статус код 500
                    statusCode = (int)HttpStatusCode.InternalServerError;

                    // Устанавливаем секцию на этот статус код в стандарте
                    section = "15.6.1.";

                    // Устанавливаем сообщение ошибки
                    message = exception.Message;

                    // Логгируем исключение
                    logger.LogError(exception, "Возникла ошибка при обработке запроса");
                    break;
            }

            // Установка статуса кода ответа.
            context.Response.StatusCode = statusCode;

            // Формирование объекта ошибки для отправки клиенту.
            var errorResponse = new
            {
                // Ошибка
                errors = new { Error = message },

                // Ссылка на стандарт
                type = $"https://tools.ietf.org/html/rfc9110#section-{section}",

                // Заголовок
                title = "One or more errors occurred.",

                // Статус запроса
                status = context.Response.StatusCode,

                // Уникальный идентификатор запроса
                traceId = Activity.Current?.Id ?? context.TraceIdentifier
            };

            // Отправка объекта ошибки в формате JSON.
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}