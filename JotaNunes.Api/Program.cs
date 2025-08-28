using JotaNunes.Api.Configuration.ApiConfig;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureStartupApi(builder.Configuration);

var app = builder.Build();

app.UseApiConfiguration(app.Environment);
app.Run();