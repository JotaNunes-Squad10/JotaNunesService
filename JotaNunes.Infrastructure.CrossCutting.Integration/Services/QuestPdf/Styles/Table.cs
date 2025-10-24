using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace JotaNunes.Infrastructure.CrossCutting.Integration.Services.QuestPdf.Styles;

public class Table
{
    private static IContainer BaseCell(IContainer container, Color color)
        => container.Border(0.5f).BorderColor("#D3D3D3").Background(color).PaddingHorizontal(4).PaddingVertical(2).AlignMiddle();

    public static IContainer HeaderCell(IContainer container) 
        => BaseCell(container, Colors.Grey.Lighten3 );
    
    public static IContainer BodyCell(IContainer container) 
        => BaseCell(container, Colors.White );
}