using System.Text.Json.Serialization;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Responses.Keycloak;

public class ErrorResponse
{
    [JsonPropertyName("error")]
    public string? Error { get; set; }
    
    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; }
    
    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; set; }
}