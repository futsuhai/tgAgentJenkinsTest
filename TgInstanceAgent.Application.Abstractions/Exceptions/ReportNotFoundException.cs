namespace TgInstanceAgent.Application.Abstractions.Exceptions;

/// <summary>
/// Представляет исключение, которое вызывается, когда не удается найти отчёт.
/// </summary>
public class ReportNotFoundException() : Exception("Отчёт за указанную дату не найден");
