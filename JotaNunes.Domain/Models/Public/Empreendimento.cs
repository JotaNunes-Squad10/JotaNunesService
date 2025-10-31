using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class Empreendimento : BaseAuditEntity
{
    public required Guid Guid { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Localizacao { get; set; }
    public required int TamanhoArea { get; set; }
    public required long Padrao { get; set; }
    public required long Versao { get; set; }

    public required EmpreendimentoBase EmpreendimentoBase { get; set; }
    public required Padrao EmpreendimentoPadrao { get; set; }

    public required List<EmpreendimentoTopico> EmpreendimentoTopicos { get; set; }
}