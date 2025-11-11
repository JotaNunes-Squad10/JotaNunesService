using JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Requests;
using JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Styles;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Documents;

public class DocumentoEmpreendimento(DocumentoEmpreendimentoRequest empreendimento) : IDocument
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
                
                page.Header().Height(60).Row(row =>
                {
                    row.RelativeItem(1)
                        .AlignLeft()
                        .AlignMiddle()
                        .Image(GetImage("footer.png"))
                        .FitWidth();

                    row.RelativeItem(4);
                });

                page.Content()
                    .Column(column =>
                    {
                        column.Item().Text("ESPECIFICAÇÃO TÉCNICA").Style(Typography.Title).AlignCenter();
                        column.Item().Text($"Empreendimento: {empreendimento.Nome}.").Style(Typography.Normal);
                        column.Item().Text($"Localização: {empreendimento.Localizacao}.").Style(Typography.Normal);
                        column.Item().Text($"Descrição: {empreendimento.Descricao}").Style(Typography.Normal);
                        column.Item().Text("").Style(Typography.Normal);

                        empreendimento.EmpreendimentoTopicos.OrderBy(et => et.Posicao).ToList().ForEach(et =>
                        {
                            AddListTopic(column, 0, $"{et.Posicao}.", et.Topico.Nome, Typography.Topic);

                            if (et.TopicoId != 3) 
                            {
                                et.TopicoAmbientes.OrderBy(ta => ta.Posicao).ToList().ForEach(ta =>
                                {
                                    column.Item().PaddingTop(10);
                                    AddListTopic(column, 1, $"{et.Posicao}.{ta.Posicao}.", ta.Ambiente.Nome, Typography.Topic);
                                    column.Item().Text("").Style(Typography.Normal);

                                    column.Item().Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(3);
                                            columns.RelativeColumn(7);
                                        });

                                        if (ta.AmbienteItens.Count > 0)
                                        {
                                            table.Cell().Element(Table.HeaderCell).Text("Item");
                                            table.Cell().Element(Table.HeaderCell).Text("Descrição");
                                        }

                                        ta.AmbienteItens.OrderBy(ai => ai.Item.Nome).ToList().ForEach(ai =>
                                        {
                                            table.Cell().Element(Table.BodyCell).Text(ai.Item.Nome);
                                            table.Cell().Element(Table.BodyCell).Text(ai.Item.Descricao);
                                        });
                                    });
                                });
                            }
                            else
                            {
                                column.Item().PaddingTop(10);
                                column.Item().Table(table =>
                                {
                                    if (et.TopicoMateriais.Count > 0)
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(3);
                                            columns.RelativeColumn(7);
                                        });
                                        table.Cell().Element(Table.HeaderCell).Text("Item");
                                        table.Cell().Element(Table.HeaderCell).Text("Descrição");
                                    }

                                    et.TopicoMateriais.OrderBy(tm => tm.MarcaMaterial.Material.Nome).ToList().ForEach(tm =>
                                    {
                                        table.Cell().Element(Table.BodyCell).Text(tm.MarcaMaterial.Material.Nome);
                                        table.Cell().Element(Table.BodyCell).Text(tm.MarcaMaterial.Marca.Nome);
                                    });
                                });
                            }

                            column.Item().Text("").Style(Typography.Normal);
                        });
                    });
                
                page.Footer().Height(40).Row(row =>
                {
                    row.RelativeItem(1);

                    row.RelativeItem(2)
                        .AlignCenter()
                        .AlignMiddle()
                        .Text("CNPJ: 16.202.491/0001-93  Jotanunes Construtora Ltda.")
                        .FontSize(9)
                        .FontColor("#CCCCCC"); // Cor cinza claro para efeito marca d'água

                    // Numeração de página à direita
                    row.RelativeItem(1)
                        .AlignRight()
                        .AlignMiddle()
                        .Text(text =>
                        {
                            text.Span("Página ").Style(Typography.Small);
                            text.CurrentPageNumber().Style(Typography.Small);
                            text.Span(" de ").Style(Typography.Small);
                            text.TotalPages().Style(Typography.Small);
                        });
                });
            });
    }

    private void AddListTopic(ColumnDescriptor column, int nestingLevel, string bulletText, string text, TextStyle style)
    {
        column.Item().Row(row =>
        {
            row.ConstantItem(NestingSize * nestingLevel);
            row.AutoItem().PaddingRight(4).Text(bulletText).Style(style);
            row.AutoItem().Text(text).Style(style);
        });
    }

    private string GetImage(string image)
        => Path.Combine(AppContext.BaseDirectory, "Services", "QuestPdf", "Assets", "Images", image);
}