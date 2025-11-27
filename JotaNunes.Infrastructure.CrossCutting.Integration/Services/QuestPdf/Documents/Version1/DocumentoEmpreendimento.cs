using JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Documents.Version1.Styles;
using JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Requests;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Documents.Version1;

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

                page.Header().Height(100).Row(row =>
                {
                    row.RelativeItem(4);
                    row.RelativeItem().Image(GetImage("logo.png"));
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
                            AddListTopic(column, 0, $"{et.Posicao}.", et.Topico.Nome, Typography.Topic);

                            if (et.TopicoId != 3) // Diferente de Marcas
                            {
                                et.TopicoAmbientes?.OrderBy(ta => ta.Posicao).ToList().ForEach(ta =>
                                {
                                    column.Item().Text("").Style(Typography.Normal);
                                    AddListTopic(column, 1, $"{et.Posicao}.{ta.Posicao}.", ta.Ambiente.Nome, Typography.Topic);
                                    column.Item().Text("").Style(Typography.Normal);

                                    column.Item().Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(3);
                                            columns.RelativeColumn(7);
                                        });

                                        if (ta.AmbienteItens?.Count > 0)
                                        {
                                            table.Cell().Element(Table.HeaderCell).Text("Item");
                                            table.Cell().Element(Table.HeaderCell).Text("Descrição");
                                        }

                                        ta.AmbienteItens?.OrderBy(ai => ai.Item.Nome).ToList().ForEach(ai =>
                                        {
                                            table.Cell().Element(Table.BodyCell).Text(ai.Item.Nome);
                                            table.Cell().Element(Table.BodyCell).Text(ai.Item.Descricao);
                                        });
                                    });
                                });
                            }

                            else
                            {
                                column.Item().Text("").Style(Typography.Normal);
                                column.Item().Table(table =>
                                {
                                    if (et.TopicoMateriais?.Count > 0)
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(3);
                                            columns.RelativeColumn(7);
                                        });
                                        table.Cell().Element(Table.HeaderCell).Text("Item");
                                        table.Cell().Element(Table.HeaderCell).Text("Descrição");
                                    }

                                    et.TopicoMateriais?.GroupBy(tm => tm.MarcaMaterial.MaterialId)
                                        .ToList().ForEach(group =>
                                        {
                                            var material = group.First().MarcaMaterial.Material.Nome;
                                            var marcas = string.Join(", ", group
                                                .Select(x => x.MarcaMaterial.Marca.Nome)
                                                .Distinct()
                                                .OrderBy(n => n));

                                            table.Cell().Element(Table.BodyCell).Text(material);
                                            table.Cell().Element(Table.BodyCell).Text(marcas);
                                        });
                                });
                            }

                            column.Item().Text("").Style(Typography.Normal);
                        });
                    });

                page.Footer().Height(40).Row(row =>
                {
                    row.RelativeItem().AlignCenter().PaddingTop(12).Image(GetImage("footer.png")).FitHeight();
                });
            });
    }

    private static void AddListTopic(ColumnDescriptor column, int nestingLevel, string bulletText, string text, TextStyle style)
    {
        column.Item().Row(row =>
        {
            row.ConstantItem(NestingSize * nestingLevel);
            row.AutoItem().PaddingRight(4).Text(bulletText).Style(style);
            row.AutoItem().Text(text).Style(style);
        });
    }

    private static string GetImage(string image)
        => Path.Combine(AppContext.BaseDirectory, "Services", "QuestPdf", "Assets", "Images", image);
}