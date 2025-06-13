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
        var worksheet = workbook.Worksheets.Add("Relatório de Usuários");

        // Cabeçalhos
        worksheet.Cell(1, 1).Value = "ID";
        worksheet.Cell(1, 2).Value = "Nome";
        worksheet.Cell(1, 3).Value = "Email";
        worksheet.Cell(1, 4).Value = "CPF";
        worksheet.Cell(1, 5).Value = "Telefone";
        worksheet.Cell(1, 6).Value = "Data de Nascimento";
        worksheet.Cell(1, 7).Value = "Cidade";
        worksheet.Cell(1, 8).Value = "Estado";
        worksheet.Cell(1, 9).Value = "País";

        for (int i = 0; i < users.Count; i++)
        {
            var user = users[i];
            worksheet.Cell(i + 2, 1).Value = user.Id;
            worksheet.Cell(i + 2, 2).Value = user.Name;
            worksheet.Cell(i + 2, 3).Value = user.Email;
            worksheet.Cell(i + 2, 4).Value = user.CPF;
            worksheet.Cell(i + 2, 5).Value = user.PhoneNumber;
            worksheet.Cell(i + 2, 6).Value = user.BirthDate.ToShortDateString();
            worksheet.Cell(i + 2, 7).Value = user.City;
            worksheet.Cell(i + 2, 8).Value = user.State;
            worksheet.Cell(i + 2, 9).Value = user.Country;
        }

        worksheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}
