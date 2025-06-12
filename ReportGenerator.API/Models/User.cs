namespace ReportGenerator.API.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string CPF { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string Country { get; set; } = "Brasil";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    // Additional data
    public string Gender { get; set; } = default!;
    public string MaritalStatus { get; set; } = default!;
    public string MotherName { get; set; } = default!;
    public string FatherName { get; set; } = default!;
    public string Nationality { get; set; } = default!;
    public string Profession { get; set; } = default!;

    // Bank details
    public string BankName { get; set; } = default!;
    public string BankAgency { get; set; } = default!;
    public string BankAccount { get; set; } = default!;
    public string AccountType { get; set; } = default!; // Corrente, Poupança, etc.

    // Additional document
    public string RG { get; set; } = default!;
    public DateTime DocumentIssueDate { get; set; }

    // Data for filters and simulations
    public string EducationLevel { get; set; } = default!;
    public string EmploymentStatus { get; set; } = default!; // Empregado, Desempregado, Autônomo, etc.
}
