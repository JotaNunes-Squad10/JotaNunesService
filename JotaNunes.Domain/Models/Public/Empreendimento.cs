using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class Empreendimento : BaseAuditEntity
{
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Localizacao { get; set; }
    public required long TamanhoArea { get; set; }
    public required long Versao { get; set; }
    public required long Padrao { get; set; }
    public required long Status { get; set; }

    public required Padrao EmpreendimentoPadrao { get; set; }
    public required EmpreendimentoStatus EmpreendimentoStatus { get; set; }
    
    public required List<EmpreendimentoTopico> EmpreendimentoTopicos { get; set; }
    public required List<EmpreendimentoAmbiente> EmpreendimentoAmbientes { get; set; }
    public required List<EmpreendimentoItem> EmpreendimentoItens { get; set; }
    public required List<EmpreendimentoMaterial> EmpreendimentoMaterials { get; set; }
}