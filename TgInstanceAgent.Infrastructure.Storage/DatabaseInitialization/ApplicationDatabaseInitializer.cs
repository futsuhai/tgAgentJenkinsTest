using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TgInstanceAgent.Infrastructure.Storage.Context;

namespace TgInstanceAgent.Infrastructure.Storage.DatabaseInitialization;

/// <summary>
/// Класс для инициализации начальных данных в базу данных
/// </summary>
public static class ApplicationDatabaseInitializer
{
    /// <summary>
    /// Инициализация начальных данных в базу данных
    /// </summary>
    /// <param name="scopeServiceProvider">Определяет механизм для извлечения объекта службы,
    /// т. е. объекта, обеспечивающего настраиваемую поддержку для других объектов.</param>
    public static async Task InitAsync(IServiceProvider scopeServiceProvider)
    {
        // Получаем контекст базы данных
        var applicationDbContext = scopeServiceProvider.GetRequiredService<ApplicationDbContext>();

        //обновляем базу данных
        await applicationDbContext.Database.MigrateAsync();
    }
}