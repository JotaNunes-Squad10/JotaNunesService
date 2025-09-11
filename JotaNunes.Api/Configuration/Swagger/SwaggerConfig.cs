using System.Reflection;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;
using Microsoft.OpenApi.Models;

namespace JotaNunes.Api.Configuration.Swagger;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        var appName = AppDataProvider.GetName();

        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = $"{appName} API",
                Version = "v1",
                Contact = new OpenApiContact
                    { Name = appName, Url = new Uri("https://github.com/JotaNunes-Squad10/JotaNunesService?tab=readme-ov-file") }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Authorization header using the Bearer scheme.",
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    public static void UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        var appName = AppDataProvider.GetName();
        
        app.UseSwagger(options =>
        {
            options.RouteTemplate = "docs/{documentName}/swagger.json";
        });
        app.UseSwaggerUI(options =>
        {
            options.ConfigObject.TryItOutEnabled = true;
            options.DocumentTitle = $"{appName} API";
            options.RoutePrefix = "docs";
            options.SwaggerEndpoint("/docs/v1/swagger.json", $"{appName} API v1");
        });
    }
}