namespace TgInstanceAgent.Domain.Instances.ValueObjects;

/// <summary>
/// Пересылка сообщений.
/// </summary>
public class ForwardEntry
{
    /// <summary>
    /// Идентификатор чата, из которого будут пересылка сообщения.
    /// </summary>
    public long For { get; }
    
    /// <summary>
    /// Идентификатор чата, в который будут пересылка сообщения.
    /// Данный чат должен быть известен TdLib, иначе сообщение не будет доставлено.
    /// </summary>
    public long To { get; }
    
    /// <summary>
    /// Флаг, определяющий переслать сообщение или отправить копию.
    /// </summary>
    public bool SendCopy { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="forChatId">Идентификатор чата, из которого будут пересылка сообщения.</param>
    /// <param name="toChatId">Идентификатор чата, в который будут пересылка сообщения.</param>
    /// <param name="sendCopy">Флаг, определяющий переслать сообщение или отправить копию.</param>
    public ForwardEntry(long forChatId, long toChatId, bool sendCopy)
    {
        if (forChatId == toChatId) throw new ArgumentException("Chat to forward and chat from forward are same.");
        For = forChatId;
        To = toChatId;
        SendCopy = sendCopy;
    }
    
    /// <summary>
    /// Определяет, равен ли указанный объект текущему экземпляру.
    /// </summary>
    /// <param name="obj">Объект для сравнения с текущим экземпляром.</param>
    /// <returns>true, если указанный объект равен текущему экземпляру; в противном случае false.</returns>
    public override bool Equals(object? obj)
    {
        // Проверяем, что объект не null и имеет тот же тип
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        var other = (ForwardEntry)obj;
        
        // Сравниваем свойства For и To текущего объекта с другим объектом
        return For == other.For && To == other.To;
    }
    
    /// <summary>
    /// Возвращает хэш-код для текущего экземпляра.
    /// </summary>
    /// <returns>Хэш-код для текущего экземпляра.</returns>
    public override int GetHashCode()
    {
        unchecked
        {
            // Генерируем хеш-код на основе свойств For и To
            return (For.GetHashCode() * 397) ^ To.GetHashCode();
        }
    }
}