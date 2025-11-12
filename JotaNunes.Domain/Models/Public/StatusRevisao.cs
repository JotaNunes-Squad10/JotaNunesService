using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Domain.Models.Public;

public class StatusRevisao : BaseEntity
{
    public required string Descricao { get; set; }
    public bool Excluido { get; set; }

    public required List<RevisaoAmbiente> RevisoesAmbiente { get; set; }
    public required List<RevisaoItem> RevisoesItem { get; set; }
    public required List<RevisaoMaterial> RevisoesMaterial { get; set; }
    public required List<RevisaoTopico> RevisoesTopico { get; set; }
}

public enum StatusRevisaoEnum
{
    Aprovado = 1,
    Pendente = 2,
    Cancelado = 3
}