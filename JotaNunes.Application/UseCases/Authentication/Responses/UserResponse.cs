namespace JotaNunes.Application.UseCases.Authentication.Responses;

public class UserResponse
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
}