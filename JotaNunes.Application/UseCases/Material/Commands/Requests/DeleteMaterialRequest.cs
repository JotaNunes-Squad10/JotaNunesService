using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Material.Commands.Requests;

public class DeleteMaterialRequest : IRequest<DefaultResponse>
{
    public long Id { get; set; }
}