namespace JotaNunes.Application.UseCases.Empreendimentos.Commands.Requests;

public class LogStatusRequest
{
    public required Guid EmpreendimentoId { get; set; }
    public required long Status { get; set; }
}