using System.Text.RegularExpressions;

namespace TgInstanceAgent.Infrastructure.Telegram.Extensions;

/// <summary>
/// Класс, содержащий методы расширения для строк
/// </summary>
public static partial class StringExtensions
{
    /// <summary>
    /// Метод для нахождения числа из строки с ошибкой 
    /// </summary>
    /// <param name="phrase">Текст</param>
    /// <returns>Число</returns>
    public static int? FindNumber(this string phrase)
    {
        // Получаем число из строки с ошибкой
        var match = MyRegex().Match(phrase);

        // Преобразуем число в int
        return match.Success ? int.Parse(match.Value) : null;
    }

    /// <summary>
    /// Разбивает текст на слова
    /// </summary>
    /// <param name="phrase">Текст</param>
    /// <returns>Массив слов</returns>
    public static string[] ExtractWords(this string phrase)
    {
        // Получаем слова
        var matches = MyRegex1().Matches(phrase);

        // Преобразовывываем в массив
        return matches.Select(m => m.Value).ToArray();
    }

    public static string? NullIfEmpty(this string? str)
    {
        return string.IsNullOrEmpty(str) ? null : str;
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex MyRegex();

    [GeneratedRegex(@"\b[\w'-]+\b")]
    private static partial Regex MyRegex1();
}