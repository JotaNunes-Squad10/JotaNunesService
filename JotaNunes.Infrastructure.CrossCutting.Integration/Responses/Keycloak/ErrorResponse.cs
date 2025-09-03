using System.Text.Json.Serialization;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Responses.Keycloak;

public class ErrorResponse
{
    [JsonPropertyName("error")]
    public required string Error { get; set; }
    
    [JsonPropertyName("error_description")]
    public required string ErrorDescription { get; set; }
    
    [JsonPropertyName("errorMessage")]
    public required string ErrorMessage { get; set; }
}