using JotaNunes.Domain.Models.Public;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Documents;

public static class Typography
{
    public static TextStyle Title => TextStyle
        .Default
        .FontFamily("Calibri")
        .FontColor("#000000")
        .FontSize(14)
        .LineHeight(2)
        .Bold();

    public static TextStyle Topic => TextStyle
        .Default
        .FontFamily("Calibri")
        .FontColor("#000000")
        .FontSize(12)
        .Bold();

    public static TextStyle Normal => TextStyle
        .Default
        .FontFamily("Calibri")
        .FontColor("#000000")
        .FontSize(12)
        .LineHeight(2);
}

public class DocumentoEmpreendimento(Empreendimento model) : IDocument
{
    public Empreendimento Model { get; } = model;

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
                        column.Item().Text($"Empreendimento: {model.Nome}.").Style(Typography.Normal);
                        column.Item().Text($"Localização: {model.Localizacao}.").Style(Typography.Normal);
                        column.Item().Text($"Descrição: {model.Descricao}").Style(Typography.Normal);

                        AddListItem(column, 0, "1.", "UNIDADES PRIVATIVAS", Typography.Topic);
                        AddListItem(column, 1, "1.1", "Sala de Estar / Jantar", Typography.Topic);
                    });

                page.Footer().Height(50).Background(Colors.Grey.Lighten1);
            });
    }

    void AddListItem(ColumnDescriptor column, int nestingLevel, string bulletText, string text, TextStyle style)
    {

        column.Item().Row(row =>
        {
            row.ConstantItem(NestingSize * nestingLevel);
            row.ConstantItem(NestingSize).Text(bulletText);
            row.RelativeItem().Text(text).Style(style);
        });
    }

    private string GetImage(string image)
        => Path.Combine(AppContext.BaseDirectory, "Services", "QuestPdf", "Assets", image);
}