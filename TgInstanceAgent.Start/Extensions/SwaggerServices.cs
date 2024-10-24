using Microsoft.OpenApi.Models;
using TgInstanceAgent.Application.Abstractions.TelegramClient.Objects.Users;
using TgInstanceAgent.Infrastructure.Web.Authentication.Hubs;
using TgInstanceAgent.Start.SwaggerFilters.Enums;
using TgInstanceAgent.Start.SwaggerFilters.Localization;

namespace TgInstanceAgent.Start.Extensions;

/// <summary>
/// Класс для настройки Swagger в приложении.
/// </summary>
public static class SwaggerServices
{
    /// <summary>
    /// Добавляет настройки Swagger в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            // Название файла с документацией контроллеров и моделей
            var xmlWebFile = $"{typeof(QrAuthenticationHub).Assembly.GetName().Name}.xml";
            
            // Название файла с документацией сущностей TelegramClient
            var xmlAbstractionsFile = $"{typeof(TgUser).Assembly.GetName().Name}.xml";

            // Включение human-friendly описаний для операций, параметров и схем на
            // основе файлов комментариев XML
            foreach (var file in new [] {xmlWebFile, xmlAbstractionsFile})
            {
                // Добавление комментариев в файлы
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, file));
                
                //Добавление фильтра схемы для вывода полей перечислений в описание
                options.SchemaFilter<EnumTypeSchemaFilter>(Path.Combine(AppContext.BaseDirectory, file));
            }
            
            // Добавление фильтра документа для локализации
            options.DocumentFilter<LocalizationDocumentFilter>();
            
            // Добавление фильтра документа для добавления описаний перечислений из схемы документа
            options.DocumentFilter<EnumTypeDocumentFilter>();
    
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            
            // Определите схему OAuth2 для Swagger
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri("https://localhost:10001/connect/authorize"),
                        TokenUrl = new Uri("https://localhost:10001/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "TgInstanceAgent", "Доступ к инстансам" }
                        }
                    }
                }
            });

            // Используйте схему OAuth2 для всех операций в Swagger
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                        Scheme = "oauth2",
                        Name = "oauth2",
                        In = ParameterLocation.Header
                    },
                    new[] { "TgInstanceAgent" }
                }
            });
        });
    }
}