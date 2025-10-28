using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Styles;

public static class Typography
{
    public static TextStyle Title => TextStyle
        .Default
        .FontFamily(Fonts.Calibri)
        .FontColor("#000000")
        .FontSize(14)
        .LineHeight(2)
        .Bold();

    public static TextStyle Topic => TextStyle
        .Default
        .FontFamily(Fonts.Calibri)
        .FontColor("#000000")
        .FontSize(12)
        .Bold();

    public static TextStyle Normal => TextStyle
        .Default
        .FontFamily(Fonts.Calibri)
        .FontColor("#000000")
        .FontSize(12)
        .LineHeight(2);
}