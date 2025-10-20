using JotaNunes.Domain.Models.Public;
using JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Styles;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Documents;

public class DocumentoEmpreendimento(Empreendimento empreendimento) : IDocument
{
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public DocumentSettings GetSettings() => DocumentSettings.Default;

    private const float NestingSize = 20;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);

                page.Header().Height(100).Row(row =>
                {
                    row.RelativeItem(4);
                    row.RelativeItem(1).Image(GetImage("logo.png"));
                });

                page.Content()
                    .Column(column =>
                    {
                        column.Item().Text("ESPECIFICAÇÃO TÉCNICA").Style(Typography.Title).AlignCenter();
                        column.Item().Text("").Style(Typography.Normal);
                        column.Item().Text($"Empreendimento: {empreendimento.Nome}.").Style(Typography.Normal);
                        column.Item().Text($"Localização: {empreendimento.Localizacao}.").Style(Typography.Normal);
                        column.Item().Text($"Descrição: {empreendimento.Descricao}").Style(Typography.Normal);
                        column.Item().Text("").Style(Typography.Normal);

                        empreendimento.EmpreendimentoTopicos.OrderBy(et => et.Posicao).ToList().ForEach(et =>
                        {
                            AddListItem(column, 0, $"{et.Posicao}.", et.Topico.Nome, Typography.Topic);
                            
                            et.TopicoAmbientes.OrderBy(ta => ta.Posicao).ToList().ForEach(ta =>
                            {
                                column.Item().Text("").Style(Typography.Normal);
                                AddListItem(column, 1, $"{et.Posicao}.{ta.Posicao}. ", ta.Ambiente.Nome, Typography.Topic);
                                column.Item().Text("").Style(Typography.Normal);

                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(3); // 30%
                                        columns.RelativeColumn(7); // 70%
                                    });
                                    
                                    table.Cell().Element(Table.HeaderCell).Text("Item");
                                    table.Cell().Element(Table.HeaderCell).Text("Descrição");
                                    
                                    ta.AmbienteItens.OrderBy(ai => ai.Item.Nome).ToList().ForEach(ai =>
                                    {
                                        table.Cell().Element(Table.BodyCell).Text(ai.Item.Nome);
                                        table.Cell().Element(Table.BodyCell).Text(ai.Item.Descricao);
                                    });
                                });
                            });
                            column.Item().Text("").Style(Typography.Normal);
                        });
                        
                    });

                page.Footer().Height(40).Row(row =>
                {
                    row.RelativeItem().AlignCenter().PaddingTop(12).Image(GetImage("footer.png")).FitHeight();
                });
            });
    }

    private void AddListItem(ColumnDescriptor column, int nestingLevel, string bulletText, string text, TextStyle style)
    {
        column.Item().Row(row =>
        {
            row.ConstantItem(NestingSize * nestingLevel);
            row.ConstantItem(NestingSize).Text(bulletText).Style(style);
            row.RelativeItem().Text(text).Style(style);
        });
    }

    private string GetImage(string image)
        => Path.Combine(AppContext.BaseDirectory, "Services", "QuestPdf", "Assets", image);
}