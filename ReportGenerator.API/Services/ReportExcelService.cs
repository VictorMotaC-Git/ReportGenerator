using ClosedXML.Excel;
using ReportGenerator.API.Models;

namespace ReportGenerator.API.Services;

public interface IReportExcelService
{
    byte[] Generate(List<User> users);
}

public class ReportExcelService : IReportExcelService
{
    public byte[] Generate(List<User> users)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Usuários");

        // Cabeçalhos
        worksheet.Cell(1, 1).Value = "Nome";
        worksheet.Cell(1, 2).Value = "Email";
        worksheet.Cell(1, 3).Value = "CPF";
        worksheet.Cell(1, 4).Value = "Telefone";

        // Conteúdo
        for (int i = 0; i < users.Count; i++)
        {
            var user = users[i];
            worksheet.Cell(i + 2, 1).Value = user.Name;
            worksheet.Cell(i + 2, 2).Value = user.Email;
            worksheet.Cell(i + 2, 3).Value = user.CPF;
            worksheet.Cell(i + 2, 4).Value = user.PhoneNumber;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
