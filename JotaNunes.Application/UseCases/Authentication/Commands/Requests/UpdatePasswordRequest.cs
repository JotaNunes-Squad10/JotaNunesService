using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Authentication.Commands.Requests;

public class UpdatePasswordRequest : IRequest<DefaultResponse>
{
    public required string Username { get; set; }
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}