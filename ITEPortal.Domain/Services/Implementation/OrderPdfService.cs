using ITEPortal.Domain.Dto;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ITEPortal.Domain.Services.Implementation
{
    public class OrderPdfService : IDocument
    {
        public OrderPdfService(OrderPdfModel model)
        {
            Model = model;
        }

        public OrderPdfModel Model { get; }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container
            .Page(page =>
            {
                page.Margin(50);
                page.DefaultTextStyle(x => x.FontFamily("Inter"));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);


                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
        }


        private void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Orange.Medium);

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Order #{Model.OrderId}").Style(titleStyle);
                    column.Item().Text($"{Model.ExhibitionName}").SemiBold();

                    column.Item().Text(text =>
                    {
                        text.Span("Order date: ").SemiBold();
                        text.Span($"{Model.OrderDate:dd.MM.yyyy}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("From: ").SemiBold();
                        text.Span($"{Model.ExhibitorEmail}");
                    });
                });

                row.ConstantItem(100).Height(50).Placeholder();
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().Element(ComposeTable);
                column.Item().AlignRight().Text($"Order total: {Model.OrderTotal} {Model.Currency}");
            });
        }

        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                // Define columns
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(25);
                    columns.RelativeColumn(3);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                // Define header
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Product");
                    header.Cell().Element(CellStyle).AlignRight().Text("Unit price");
                    header.Cell().Element(CellStyle).AlignRight().Text("Quantity");
                    header.Cell().Element(CellStyle).AlignRight().Text("Total");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // Fill in the table
                foreach (var item in Model.OrderItems)
                {
                    table.Cell().Element(CellStyle).Text($"{Model.OrderItems.IndexOf(item) + 1}");
                    table.Cell().Element(CellStyle).Text(item.Name);
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Price} {item.Currency}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Quantity}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Total} {item.Currency}");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }
    }
}
