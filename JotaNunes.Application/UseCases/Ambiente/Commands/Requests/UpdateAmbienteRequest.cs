using JotaNunes.Application.UseCases.Base.Commands;

namespace JotaNunes.Application.UseCases.Ambiente.Commands.Requests;

public class UpdateAmbienteRequest : BaseRequest
{
    public string? Nome { get; set; }
}
