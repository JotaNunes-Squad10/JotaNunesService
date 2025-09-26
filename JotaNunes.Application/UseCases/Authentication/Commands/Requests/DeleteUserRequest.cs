using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Authentication.Commands.Requests;

public class DeleteUserRequest : IRequest<DefaultResponse>
{
    public required Guid Id { get; set; }
}