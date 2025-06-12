using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using ReportGenerator.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Serviços necessários
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

QuestPDF.Settings.License = LicenseType.Community;


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API de Relatórios",
        Description = "API para geração de relatórios com exemplos.",
        Contact = new OpenApiContact
        {
            Name = "Victor Mota",
            Email = "victor.conceicao-mota@ext.prosegur.com",
            Url = new Uri("https://github.com/VictorMotaC")
        }
    });
});


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReportPdfService, ReportPdfService>();
builder.Services.AddScoped<IReportCsvService, ReportCsvService>();
builder.Services.AddScoped<IReportExcelService, ReportExcelService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm", policy =>
    {
        policy.WithOrigins("https://localhost:7179")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials() 
              .WithExposedHeaders("Content-Disposition");
    });
});


var app = builder.Build();

app.UseCors("AllowBlazorWasm");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Relatórios v1");
        options.RoutePrefix = string.Empty;
        options.DocumentTitle = "Documentação API de Relatórios";
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
