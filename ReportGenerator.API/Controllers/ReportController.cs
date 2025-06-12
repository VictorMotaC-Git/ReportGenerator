using Microsoft.AspNetCore.Mvc;
using ReportGenerator.API.Services;

namespace ReportGenerator.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IReportPdfService _pdfService;
    private readonly IReportCsvService _csvService;
    private readonly IReportExcelService _excelService;

    public ReportController(IUserService userService, IReportPdfService pdfService, IReportCsvService csvService, IReportExcelService excelService)
    {
        _userService = userService;
        _pdfService = pdfService;
        _csvService = csvService;
        _excelService = excelService;
    }

    [HttpGet("pdf")]
    public IActionResult GetPdf([FromQuery] int quantidade)
    {
        var users = _userService.GenerateUsers(quantidade);
        var pdfBytes = _pdfService.Generate(users);
        return File(pdfBytes, "application/pdf", "relatorioPDF.pdf");
    }

    [HttpGet("csv")]
    public IActionResult GetCsv([FromQuery] int quantidade)
    {
        var users = _userService.GenerateUsers(quantidade);
        var csvBytes = _csvService.Generate(users);
        return File(csvBytes, "text/csv", "relatorioCSV.csv");
    }

    [HttpGet("excel")]
    public IActionResult GetExcel([FromQuery] int quantidade)
    {
        var users = _userService.GenerateUsers(quantidade);
        var excelBytes = _excelService.Generate(users);
        return File(excelBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "relatorioUsuarios.xlsx");
    }
}