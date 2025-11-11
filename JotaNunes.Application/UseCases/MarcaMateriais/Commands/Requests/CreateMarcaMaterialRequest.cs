using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.MarcaMateriais.Commands.Requests;

public class CreateMarcaMaterialRequest: IRequest<DefaultResponse>
{
    public required long MaterialId { get; set; }
    public required long MarcaId { get; set; }
}