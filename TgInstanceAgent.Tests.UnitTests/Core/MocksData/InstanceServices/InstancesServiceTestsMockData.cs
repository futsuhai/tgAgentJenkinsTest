using TgInstanceAgent.Domain.Instances;

namespace TgInstanceAgent.Tests.UnitTests.Core.MocksData.InstanceServices;

/// <summary>
/// Класс, содержащий моковые данные для тестирования сервиса инстансов.
/// </summary>
public class InstancesServiceTestsMockData
{
    /// <summary>
    /// Моковый список инстансов
    /// </summary>
    public static readonly List<InstanceAggregate> InstancesMock = 
    [
        new InstanceAggregate(Guid.NewGuid(), DateTime.Now.AddHours(24), Guid.NewGuid()),
        new InstanceAggregate(Guid.NewGuid(), DateTime.Now.AddHours(24), Guid.NewGuid())
    ];

    /// <summary>
    /// Моковая модель истекшего инстанса
    /// </summary>
    public static readonly InstanceAggregate ExpiredInstanceMock =
        new(Guid.NewGuid(), DateTime.Now.AddHours(-24), Guid.NewGuid());
    
    /// <summary>
    /// Моковая модель инстанса
    /// </summary>
    public static readonly InstanceAggregate InstanceMock =
        new(Guid.NewGuid(), DateTime.Now.AddHours(24), Guid.NewGuid());
    
    /// <summary>
    /// Моковый идентификатор инстанса
    /// </summary>
    public static readonly Guid InstanceIdMock = Guid.NewGuid();
}