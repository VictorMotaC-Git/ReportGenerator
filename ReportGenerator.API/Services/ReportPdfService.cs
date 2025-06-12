using QuestPDF.Fluent;
using QuestPDF.Helpers;
using ReportGenerator.API.Models;

namespace ReportGenerator.API.Services;

public interface IReportPdfService
{
    byte[] Generate(List<User> users);
}

public class ReportPdfService : IReportPdfService
{
    public byte[] Generate(List<User> users)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);
                page.Size(PageSizes.A4);

                page.Header().Text("Relatório de Usuários").FontSize(20).Bold();

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                        columns.RelativeColumn(1);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("Nome").Bold();
                        header.Cell().Text("Email").Bold();
                        header.Cell().Text("CPF").Bold();
                        header.Cell().Text("Telefone").Bold();
                    });

                    foreach (var user in users)
                    {
                        table.Cell().Text(user.Name);
                        table.Cell().Text(user.Email);
                        table.Cell().Text(user.CPF);
                        table.Cell().Text(user.PhoneNumber);
                    }
                });

                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Gerado em: ");
                    x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")).SemiBold();
                });
            });
        });

        return document.GeneratePdf();
    }
}