using System.Threading.Channels;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

namespace TgInstanceAgent.Infrastructure.Telegram.Structs;

/// <summary>
/// Класс для ожидания аутентификации по QR-коду.
/// </summary>
public class QrAuthenticationWaitData : IDisposable
{
    public Channel<string> QrChannel { get; } = Channel.CreateUnbounded<string>();

    /// <summary>
    /// Установка кода аутентификации
    /// </summary>
    /// <param name="code">Код аутентификации</param>
    public async void SetCode(string code)
    {
        // Отправка первого кода QR в канал
        await QrChannel.Writer.WriteAsync(code);
    }

    /// <summary>
    /// Устанавливает завершение процесса с указанием подсказки пароля.
    /// </summary>
    public void SetCompleted() => QrChannel.Writer.Complete();

    /// <summary>
    /// Устанавливает завершение процесса с указанием подсказки пароля.
    /// </summary>
    /// <param name="hint">Подксазка для пароля</param>
    public void SetCompletedWithPassword(string? hint)
    {
        // Закрываем канал
        QrChannel.Writer.Complete(new PasswordNeededException(hint));
    }

    public void Dispose()
    {
        // Отменяет вызов финализатора для текущего объекта при следующем сборе мусора
        GC.SuppressFinalize(this);

        // Завершает канал, чтобы предотвратить отправку новых данных в него
        QrChannel.Writer.TryComplete();
    }
}