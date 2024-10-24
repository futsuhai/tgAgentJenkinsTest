using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TgInstanceAgent.Infrastructure.CommandsStore.DatabaseInitialization;
using TgInstanceAgent.Infrastructure.Storage.DatabaseInitialization;
using TgInstanceAgent.Infrastructure.Webhook.Converters;
using TgInstanceAgent.Start.Extensions;
using TgInstanceAgent.Start.Middlewares;

// Создание экземпляра объекта builder с использованием переданных аргументов
var builder = WebApplication.CreateBuilder(args);
builder.Environment.EnvironmentName = Environments.Development;

// Добавляем файл appsettings.json в конфигурацию
builder.Configuration.AddJsonFile("Configuration/appsettings.json");

// Добавляем файл proxies.json в конфигурацию
builder.Configuration.AddJsonFile("Configuration/proxies.json");

// Регистрируем сервисы логгирования
builder.AddLoggingServices();

// Добавление служб для валидации
builder.Services.AddValidationServices();

// Добавление служб для мапинга
builder.Services.AddMappingServices();

// Добавление служб для хранилища
builder.Services.AddStorageServices(builder.Configuration);

// Добавление служб для работы с инстансами
builder.Services.AddInstancesServices(builder.Configuration, builder.Environment);

// Добавление служб для работы с прокси
builder.Services.AddProxyServices(builder.Configuration, builder.Environment);

// Добавление служб для работы с отчётами
builder.Services.AddReportServices(builder.Configuration);

// Добавление служб для отправки веб-хуков
builder.Services.AddWebhookServices(builder.Configuration);

// Добавление кэширования
builder.Services.AddMemoryCache();

// Добавление служб авторизации
builder.Services.AddJwtAuthorization(builder.Configuration);

// Добавление служб локализации
builder.Services.AddLocalizationServices();

// Добавление служб медиатора
builder.Services.AddMediatorServices();

// Добавление служб MassTransit
//builder.Services.AddMassTransitServices(builder.Configuration); todo:

// Добавление сервисов swagger
builder.Services.AddSwaggerServices();

// Добавление поддержки NewtonsowtJson для swagger
builder.Services.AddSwaggerGenNewtonsoftSupport();

// Регистрация контроллеров с поддержкой сериализации JSON
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.Converters.Add(new TypeNameSerializationConverter());
        options.SerializerSettings.Formatting = Formatting.Indented;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    }
);

// Добавление служб SignalR
builder.Services.AddSignalR().AddNewtonsoftJsonProtocol(options =>
    {
        options.PayloadSerializerSettings.Converters.Add(new StringEnumConverter());
        options.PayloadSerializerSettings.Converters.Add(new TypeNameSerializationConverter());
        options.PayloadSerializerSettings.Formatting = Formatting.Indented;
        options.PayloadSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    }
);

// Добавление служб для работы с CORS
builder.Services.AddCorsServices();

// Создание приложения на основе настроек builder
await using var app = builder.Build();

// Создаем область для инициализации баз данных
using (var scope = app.Services.CreateScope())
{
    // Инициализация начальных данных в базу данных
    await ApplicationDatabaseInitializer.InitAsync(scope.ServiceProvider);
    
    // Инициализация начальных данных в базу данных
    await CommandsDatabaseInitializer.InitAsync(scope.ServiceProvider);
}

// Добавляем мидлварь обработки ошибок
app.UseMiddleware<ExceptionMiddleware>();

// Включение CORS
app.UseCors("DefaultPolicy");

// Добавляем мидлварь локализации
app.UseRequestLocalization();

// Используется аутентификация для приложения
app.UseAuthentication();

// Используется авторизация для приложения
app.UseAuthorization();

// Используется отправка статических файлов (wwwroot)
app.UseStaticFiles();

// Использование SwaggerServices для обслуживания документации по API
app.UseSwagger();

// Использование SwaggerServices UI для предоставления интерактивной документации по API
app.UseSwaggerUI(c =>
{
    // Определяем путь к html файлу с элементом select для выбора языка 
    var select = Path.Combine(builder.Environment.WebRootPath, "swagger/swagger-language-select.html");

    // Устанавливаем select в верхушку файла swagger
    c.HeadContent = File.ReadAllText(select);

    // Внедряем js файл со скриптом к select в html swagger страницу
    c.InjectJavascript("/swagger/swagger-language-select.js");

    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

    // Настройте Swagger UI для использования OAuth2
    c.OAuthClientId("swagger");
    c.OAuthAppName("Swagger UI");

    // Использование PKCE (Proof Key for Code Exchange) с авторизационным кодом
    c.OAuthUsePkce();
});

// Маппим контроллеры
app.MapControllers();

// Привязка хабов
app.MapHubs();

// Запуск приложения
await app.RunAsync();