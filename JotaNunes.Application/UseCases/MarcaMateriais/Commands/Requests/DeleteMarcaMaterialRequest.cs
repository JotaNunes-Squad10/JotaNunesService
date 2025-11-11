using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.MarcaMateriais.Commands.Requests;

public class DeleteMarcaMaterialRequest : IRequest<DefaultResponse>
{
    public long Id { get; set; }
}