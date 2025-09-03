using System.Text.RegularExpressions;
using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace JotaNunes.Api.Configuration.HealthChecks.Checks;

public class DatabaseHealthCheck : IHealthCheck
{
    public static string Name { get { return nameof(DatabaseHealthCheck); } }
    private const string _defaultQuery = "SELECT 1";
    private readonly ApplicationProvider _appProvider;

    public DatabaseHealthCheck(ApplicationProvider appProvider)
    {
        _appProvider = appProvider;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
    {
        var connectionString = _appProvider.DataBase;
        var dataSource = Regex.Match(connectionString, @"host=([A-Za-z0-9_.]+)", RegexOptions.IgnoreCase).Value;
        using var connection = new NpgsqlConnection(connectionString);
        
        try
        {
            await connection.OpenAsync(cancellationToken);

            var command = connection.CreateCommand();
            command.CommandText = _defaultQuery;

            await command.ExecuteNonQueryAsync(cancellationToken);

            return HealthCheckResult.Healthy(dataSource);
        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy(dataSource, e);
        }
    }
}