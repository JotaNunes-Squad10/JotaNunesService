using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class EmpreendimentoStatus : BaseEntity
{
    public required string Descricao { get; set; }
    public bool Excluido { get; set; }

    public required List<EmpreendimentoBase> EmpreendimentosBase { get; set; }
    public required List<LogStatus> LogsStatus { get; set; }
}

public enum Status
{
    Aprovado = 1,
    EmRevisao = 2,
    Pendente = 3,
    Editando = 4,
    Cancelado = 5
}
