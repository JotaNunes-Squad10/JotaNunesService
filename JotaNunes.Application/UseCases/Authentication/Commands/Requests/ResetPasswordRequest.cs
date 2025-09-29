using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Authentication.Commands.Requests;

public class ResetPasswordRequest : IRequest<DefaultResponse>
{
    public required Guid UserId { get; set; }
    public required string Password { get; set; }
}