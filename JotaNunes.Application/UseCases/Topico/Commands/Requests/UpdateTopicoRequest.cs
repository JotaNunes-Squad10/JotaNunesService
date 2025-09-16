using JotaNunes.Application.UseCases.Base.Commands;

namespace JotaNunes.Application.UseCases.Topico.Commands.Requests;

public class UpdateTopicoRequest : BaseRequest
{
    public string? Nome { get; set; }
}
