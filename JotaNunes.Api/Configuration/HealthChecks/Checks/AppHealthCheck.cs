using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace JotaNunes.Api.Configuration.HealthChecks.Checks;

public class AppHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}