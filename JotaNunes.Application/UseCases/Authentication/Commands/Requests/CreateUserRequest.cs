using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Authentication.Commands.Requests;

public class CreateUserRequest : IRequest<DefaultResponse>
{
    public required string Username { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required int Profile { get; set; }
    public required string Password { get; set; }
}