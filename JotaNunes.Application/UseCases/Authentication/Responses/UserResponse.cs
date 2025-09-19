namespace JotaNunes.Application.UseCases.Authentication.Responses;

public class UserResponse
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}