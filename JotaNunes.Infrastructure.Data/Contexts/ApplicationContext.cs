using JotaNunes.Infrastructure.CrossCutting.Commons.Providers;
using Microsoft.EntityFrameworkCore;

namespace JotaNunes.Infrastructure.Data.Contexts;

public class ApplicationContext(DbContextOptions<ApplicationContext> options, ApplicationProvider appProvider)
    : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(appProvider.DataBase,
                npgsqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);
                    sqlOptions.CommandTimeout(60);
                });

        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging(false);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
    }
}