using JotaNunes.Domain.Models.Public;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Requests;

public class DocumentoEmpreendimentoRequest
{
    public required Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public required string Localizacao { get; set; }
    public int? TamanhoArea { get; set; }
    public required int Versao { get; set; }

    public required List<EmpreendimentoTopico> EmpreendimentoTopicos { get; set; }
}