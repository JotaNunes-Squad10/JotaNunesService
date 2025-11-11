using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.MarcaMateriais.Commands.Requests;

public class UpdateMarcaMaterialRequest : IRequest<DefaultResponse>
{
    public required long Id { get; set; }
    public long? MaterialId { get; set; }
    public long? MarcaId { get; set; }
}