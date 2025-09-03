using JotaNunes.Api.Configuration.HealthChecks.Checks;
using JotaNunes.Api.Configuration.HealthChecks.Models;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace JotaNunes.Api.Configuration.HealthChecks;

    public static class HealthCheckSetup
    {
        private static readonly string _healthChecksResource = "/healthchecks";

        public static void AddHealthCheckSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddHealthChecks()
                .AddCheck<AppHealthCheck>(nameof(AppHealthCheck), tags: new[] { AppDataProvider.HealthResource })
                .AddCheck<DatabaseHealthCheck>(nameof(DatabaseHealthCheck), tags: new[] { _healthChecksResource });
        }

        public static void UseAppHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks(AppDataProvider.HealthResource, GetHealthOptions());
            app.UseHealthChecks(_healthChecksResource, GetHealthChecksOptions());
        }

        private static HealthCheckOptions GetHealthOptions()
        {
            return new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains(AppDataProvider.HealthResource),
                AllowCachingResponses = false,
                ResponseWriter = GetResponseWriter()
            };
        }

        private static HealthCheckOptions GetHealthChecksOptions()
        {
            return new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains(_healthChecksResource),
                AllowCachingResponses = false,
                ResponseWriter = GetResponseWriter()
            };
        }

        private static Func<HttpContext, HealthReport, Task> GetResponseWriter()
        {
            return async (c, r) =>
            {
                c.Response.ContentType = "application/json";

                var results = r.Entries.Select(pair =>
                {
                    return KeyValuePair.Create(pair.Key, new ResponseResults
                    {
                        Status = pair.Value.Status.ToString(),
                        Description = pair.Value.Description,
                        Duration = pair.Value.Duration.TotalSeconds.ToString() + "s",
                        ExceptionMessage = pair.Value.Exception != null ? pair.Value.Exception.Message : null,
                        Data = pair.Value.Data
                    });
                }).ToDictionary(p => p.Key, p => p.Value);

                var result = new ResponseHealthCheck
                {
                    Status = r.Status.ToString(),
                    TotalDuration = r.TotalDuration.TotalSeconds.ToString() + "s",
                    Results = results
                };

                await c.Response.WriteAsync(JsonConvert.SerializeObject(result, new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    FloatFormatHandling = FloatFormatHandling.DefaultValue,
                    FloatParseHandling = FloatParseHandling.Decimal,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    Converters = new[] { new IsoDateTimeConverter { DateTimeStyles = System.Globalization.DateTimeStyles.AssumeLocal } }
                }));
            };
        }
    }
