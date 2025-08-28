namespace JotaNunes.Infrastructure.Data.Settings;

public class DatabaseSettings
{
    public required string ConnectionString { get; set; }

    public required string DatabaseName { get; set; }
}