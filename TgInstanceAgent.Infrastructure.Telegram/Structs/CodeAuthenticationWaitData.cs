using TgInstanceAgent.Application.Abstractions.TelegramClient.Exceptions;

namespace TgInstanceAgent.Infrastructure.Telegram.Structs;

/// <summary>
/// Класс для ожидания аутентификации по коду.
/// </summary>
public class CodeAuthenticationWaitData : IDisposable
{
    // Ожидание завершения задачи по сканированию QR-кода
    private readonly TaskCompletionSource _codeResumed = new();

    /// <summary>
    /// Возвращает задачу ожидания.
    /// </summary>
    public Task Waiter => _codeResumed.Task;

    /// <summary>
    /// Устанавливает завершение процесса с указанием подсказки пароля.
    /// </summary>
    public void SetCompleted()
    {
        // Резолвим таск
        _codeResumed.SetResult();
    }
    
    /// <summary>
    /// Устанавливает завершение процесса с указанием подсказки пароля.
    /// </summary>
    /// <param name="hint">Подксазка для пароля</param>
    public void SetCompletedWithPassword(string? hint)
    {
        // Резолвим таск
        _codeResumed.SetException(new PasswordNeededException(hint));
    }

    public virtual void Dispose()
    {
        // Отменяет вызов финализатора для текущего объекта при следующем сборе мусора
        GC.SuppressFinalize(this);
        
        // Устанавливает флаг отмены для объекта TaskCompletionSource, связанного с кодом возобновления
        _codeResumed.TrySetCanceled();
    }
}