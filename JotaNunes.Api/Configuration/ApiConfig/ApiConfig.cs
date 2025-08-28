using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using FluentValidation;
using JotaNunes.Api.Configuration.Swagger;
using JotaNunes.Domain.Services;
using JotaNunes.Domain.Services.Base;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Notification;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;
using JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;
using JotaNunes.Infrastructure.Data.Settings;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json.Serialization;
using KeycloakService = JotaNunes.Infrastructure.CrossCutting.Integration.Services.KeycloakService;

namespace JotaNunes.Api.Configuration.ApiConfig;

public static class ApiConfig
{
    public static IServiceCollection ConfigureStartupApi(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));
     
        services.AddSwaggerConfiguration();
        services.AddApiConfiguration();
        services.AddMediatorConfiguration();
        services.RegisterApiVersion();
        services.RegisterAppSettings(configuration);
        services.RegisterIoC();
        services.AddHttpClient();
        services.AddAuthenticationConfiguration(configuration);

        return services;
    }
    
    private static void AddApiConfiguration(this IServiceCollection services)
    {
        // services.AddControllersWithViews();
        
        services
            .AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        services
            .AddMvc()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

        // services.AddHealthCheckSetup();
    }

    private static void RegisterAppSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var appProvider = configuration
            .GetSection("ApplicationProvider")
            .Get<ApplicationProvider>();

        if (appProvider == null)
            throw new InvalidOperationException("Invalid ApplicationProvider configuration: section 'ApplicationProvider' was not found or could not be bound.");

        if (appProvider.ExternalServices == null || appProvider.ExternalServices.KeycloakService == null || string.IsNullOrWhiteSpace(appProvider.ExternalServices.KeycloakService.Url))
            throw new InvalidOperationException("Invalid ApplicationProvider configuration: ExternalServices.KeycloakService.Url is missing.");

        services.AddSingleton(appProvider);
        services.AddContexts();
    }

    private static void AddContexts(this IServiceCollection services)
    {
        services.Configure<DatabaseSettings>(options =>
        {
            options.ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")!;
            options.DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME")!;
        });
    }

    private static void AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKeycloakWebApiAuthentication(configuration);
        services.AddAuthorization();
    }

    private static void AddMediatorConfiguration(this IServiceCollection services)
    {
        var assembly = AppDataProvider.GetApplication();

        AssemblyScanner
            .FindValidatorsInAssembly(assembly)
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineBehavior<,>));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
    }

    public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UsePathBase($"/{AppDataProvider.BaseEndpointName}");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("AllowFrontend");

        // app.UseAppHealthChecks();

        app.UseStaticFiles();

        app.UseAuthentication();

        app.UseAuthorization();

        // app.UseCustomAuth();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwaggerConfiguration();
    }

    private static void RegisterIoC(this IServiceCollection services)
    {
        services.AddScoped<IDomainService, DomainService>();
        services.AddScoped<DefaultResponse>();
        services.AddScoped<INotifications, Notifications>();
        services.AddScoped<IKeycloakService, KeycloakService>();
        services.RegisterTypes(AppDataProvider.GetIntegration(), typeof(BaseHttpService));
        // services.RegisterTypes(AppDataProvider.GetApplication(), typeof(BaseQueries<,,>));
        // services.RegisterTypes(AppDataProvider.GetData(), typeof(BaseRepository<>));
    }

    private static void RegisterTypes(this IServiceCollection services, Assembly assembly, Type baseType)
    {
        var types = assembly.DefinedTypes
            .Where(x => x.ImplementedInterfaces.Any() 
                && x is { IsInterface: false, IsAbstract: false, BaseType: not null }
                && x.BaseType.Name.Equals(baseType.Name));

        foreach (var type in types)
            services.AddScoped(type.ImplementedInterfaces.FirstOrDefault(x => x.Name.Contains(type.Name)) ?? throw new InvalidOperationException("Invalid type."), type.UnderlyingSystemType);
    }

    private class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string TransformOutbound(object? value)
            => value != null ? Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower() : null;
    }
}