using FluentValidation;
using JotaNunes.Api.Configuration.AutoMapper;
using JotaNunes.Api.Configuration.HealthChecks;
using JotaNunes.Api.Configuration.Swagger;
using JotaNunes.Application.UseCases.Base.Queries;
using JotaNunes.Domain.Extensions;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Interfaces.Base;
using JotaNunes.Domain.Models.Keycloak;
using JotaNunes.Domain.Services;
using JotaNunes.Domain.Services.Base;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;
using JotaNunes.Infrastructure.CrossCutting.Integration.Interfaces;
using JotaNunes.Infrastructure.Data.Contexts;
using JotaNunes.Infrastructure.Data.Repositories.Base;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using KeycloakService = JotaNunes.Infrastructure.CrossCutting.Integration.Services.KeycloakService;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace JotaNunes.Api.Configuration.ApiConfig;

public static class ApiConfig
{
    public static IServiceCollection ConfigureStartupApi(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddSwaggerConfiguration();
        services.AddApiConfiguration();
        services.AddMediatorConfiguration();
        services.AddAutoMapperConfiguration();
        services.RegisterApiVersion();
        services.RegisterAppSettings(configuration);
        services.RegisterPatterns();
        services.RegisterIoC();
        services.AddHttpClient();
        services.AddCustomAuth(configuration);

        return services;
    }

    private static void AddApiConfiguration(this IServiceCollection services)
    {
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

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });

        services.AddHealthCheckSetup();
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

        var connectionString = configuration.GetConnectionString("AppConnectionString");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Invalid ApplicationProvider configuration: AppConnectionString is missing.");

        appProvider.DataBase = Encoding.UTF8.GetString(Convert.FromBase64String(connectionString));

        services.AddSingleton(appProvider);
        services.AddContexts();
    }

    private static void AddContexts(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationContext>();
        services.AddScoped<DbContext, ApplicationContext>();
    }

    private static void AddCustomAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUser, User>();

        var keycloakSection = configuration.GetSection("ApplicationProvider:ExternalServices:KeycloakService");

        if (keycloakSection == null)
            throw new InvalidOperationException("Invalid ApplicationProvider configuration: ExternalServices.KeycloakService is missing.");

        if (string.IsNullOrWhiteSpace(keycloakSection["Url"]))
            throw new InvalidOperationException("Invalid ApplicationProvider configuration: ExternalServices.KeycloakService.Url is missing.");

        if (string.IsNullOrWhiteSpace(keycloakSection["ClientId"]))
            throw new InvalidOperationException("Invalid ApplicationProvider configuration: ExternalServices.KeycloakService.ClientId is missing.");

        services.AddKeycloakWebApiAuthentication(
            keycloakOptions =>
            {
                keycloakOptions.AuthServerUrl = keycloakSection["Url"];
                keycloakOptions.Realm = "JotaNunes";
                keycloakOptions.Resource = keycloakSection["ClientId"]!;
            },
            jwtBearerOptions =>
            {
                jwtBearerOptions.RequireHttpsMetadata = false;
            });

        services.AddAuthorization(options =>
        {
            options.AddGroupPolicies();
        }).AddKeycloakAuthorization();
    }

    private static void AddMediatorConfiguration(this IServiceCollection services)
    {
        var assembly = AppDataProvider.GetApplication();

        AssemblyScanner
            .FindValidatorsInAssembly(assembly)
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

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

        app.UseCors("AllowAll");

        app.UseAppHealthChecks();

        app.UseStaticFiles();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwaggerConfiguration();
    }

    private static void RegisterIoC(this IServiceCollection services)
    {
        services.AddScoped<IDomainService, DomainService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IKeycloakService, KeycloakService>();
        services.RegisterTypes(AppDataProvider.GetIntegration(), typeof(BaseHttpService));
        services.RegisterTypes(AppDataProvider.GetApplication(), typeof(BaseQueries<,,>));
        services.RegisterTypes(AppDataProvider.GetData(), typeof(BaseRepository<>));
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
