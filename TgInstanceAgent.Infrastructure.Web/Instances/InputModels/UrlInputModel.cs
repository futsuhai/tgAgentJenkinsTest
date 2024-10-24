namespace TgInstanceAgent.Infrastructure.Web.Instances.InputModels;

/// <summary>
/// Модель ввода для представления данных URL.
/// Используется для передачи URL в различных операциях, таких как добавление или удаление ссылок.
/// </summary>
public class UrlInputModel
{
    /// <summary>
    /// URL-адрес, который будет использоваться в операциях.
    /// Обязательное поле для задания URL, который будет передан в команды или запросы.
    /// </summary>
    public required string Url { get; init; }
}