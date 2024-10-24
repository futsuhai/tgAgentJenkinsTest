using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TgInstanceAgent.Infrastructure.CommandsStore.Context;

namespace TgInstanceAgent.Infrastructure.CommandsStore.DatabaseInitialization;

/// <summary>
/// Класс для инициализации начальных данных в базу данных
/// </summary>
public class CommandsDatabaseInitializer
{
    /// <summary>
    /// Инициализация начальных данных в базу данных
    /// </summary>
    /// <param name="scopeServiceProvider">Определяет механизм для извлечения объекта службы,
    /// т. е. объекта, обеспечивающего настраиваемую поддержку для других объектов.</param>
    public static async Task InitAsync(IServiceProvider scopeServiceProvider)
    {
        // Получаем контекст базы данных
        var commandsDbContext = scopeServiceProvider.GetRequiredService<CommandsDbContext>();

        //обновляем базу данных
        await commandsDbContext.Database.MigrateAsync();
    }
}