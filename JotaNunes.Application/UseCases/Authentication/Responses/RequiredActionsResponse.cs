namespace JotaNunes.Application.UseCases.Authentication.Responses;

public class RequiredActionsResponse
{
    public required string Username { get; set; }
    public required List<string> RequiredActions { get; set; }
}