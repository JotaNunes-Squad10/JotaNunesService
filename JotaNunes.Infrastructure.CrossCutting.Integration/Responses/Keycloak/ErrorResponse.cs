using System.Text.Json.Serialization;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Responses.Keycloak;

public class ErrorResponse
{
    [JsonPropertyName("errorMessage")]
    public required string ErrorMessage { get; set; }
}