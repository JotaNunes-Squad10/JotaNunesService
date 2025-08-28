using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Authentication.Commands.Requests;

public class CreateUserRequest : IRequest<DefaultResponse>
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}