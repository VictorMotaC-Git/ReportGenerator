using CsvHelper;
using CsvHelper.Configuration;
using ReportGenerator.API.Models;
using System.Globalization;
using System.Text;

namespace ReportGenerator.API.Services;

public interface IReportCsvService
{
    byte[] Generate(List<User> users);
}

public class ReportCsvService : IReportCsvService
{
    public byte[] Generate(List<User> users)
    {
        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
        using var csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture));

        csvWriter.WriteRecords(users);
        streamWriter.Flush();

        return memoryStream.ToArray();
    }
}