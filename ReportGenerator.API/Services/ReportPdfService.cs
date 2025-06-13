using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ReportGenerator.API.Models;
using System.IO;

namespace ReportGenerator.API.Services;

public interface IReportPdfService
{
    byte[] Generate(List<User> users);
}

public class ReportPdfService : IReportPdfService
{
    public byte[] Generate(List<User> users)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imgm", "logo.png");
        var logoBytes = File.Exists(logoPath) ? File.ReadAllBytes(logoPath) : null;

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(20);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(10).FontColor(Colors.Black));

                page.Header().Row(row =>
                {
                    if (logoBytes is not null)
                    {
                        row.ConstantItem(80).Image(logoBytes).FitHeight();
                    }

                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("Relatório de Usuários").FontSize(18).Bold().FontColor(Colors.Blue.Medium);
                        col.Item().Text($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(10).Italic();
                    });
                });

                page.Content().Column(col =>
                {
                    foreach (var user in users)
                    {
                        col.Item().Element(userSection =>
                            userSection.ShowEntire().BorderBottom(1).PaddingVertical(10).Column(section =>
                            {
                                section.Spacing(3);

                                section.Item().Text($"Nome: {user.Name}").Bold().FontSize(12);
                                section.Item().Text($"Email: {user.Email}");
                                section.Item().Text($"CPF: {user.CPF}  |  Nascimento: {user.BirthDate:dd/MM/yyyy}");
                                section.Item().Text($"Telefone: {user.PhoneNumber}");

                                section.Item().PaddingTop(4).Text("Endereço").Bold().FontColor(Colors.Grey.Darken1);
                                section.Item().Text($"{user.Address}, {user.City} - {user.State}, {user.Country}");

                                section.Item().PaddingTop(4).Text("Dados Pessoais").Bold().FontColor(Colors.Grey.Darken1);
                                section.Item().Text($"Gênero: {user.Gender}  |  Estado Civil: {user.MaritalStatus}");
                                section.Item().Text($"Profissão: {user.Profession}  |  Nacionalidade: {user.Nationality}");

                                section.Item().PaddingTop(4).Text("Dados Bancários").Bold().FontColor(Colors.Grey.Darken1);
                                section.Item().Text($"Banco: {user.BankName}  |  Agência: {user.BankAgency}  |  Conta: {user.BankAccount} ({user.AccountType})");

                                section.Item().PaddingTop(4).Text("Documentação").Bold().FontColor(Colors.Grey.Darken1);
                                section.Item().Text($"RG: {user.RG}  |  Emissão: {user.DocumentIssueDate:dd/MM/yyyy}");

                                section.Item().PaddingTop(4).Text("Outros").Bold().FontColor(Colors.Grey.Darken1);
                                section.Item().Text($"Escolaridade: {user.EducationLevel}  |  Situação Profissional: {user.EmploymentStatus}");

                                section.Item().AlignRight().Text($"Criado em: {user.CreatedAt:dd/MM/yyyy HH:mm}").FontSize(9).Italic();
                            })
                        );
                    }
                });

                page.Footer().AlignCenter().Text(txt =>
                {
                    txt.CurrentPageNumber().FontSize(9);
                    txt.Span(" / ");
                    txt.TotalPages().FontSize(9);
                });
            });
        });

        return document.GeneratePdf();
    }
}
