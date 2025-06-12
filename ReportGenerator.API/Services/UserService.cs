using Bogus;
using ReportGenerator.API.Models;

namespace ReportGenerator.API.Services;

public interface IUserService
{
    List<User> GenerateUsers(int quantity);
}

public class UserService : IUserService
{
    public List<User> GenerateUsers(int quantity)
    {
        var id = 1;
        var faker = new Faker<User>("pt_BR")
            .StrictMode(true)
            .RuleFor(u => u.Id, _ => id++)
            .RuleFor(u => u.Name, f => f.Name.FullName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name).ToLower())
            .RuleFor(u => u.CPF, f => f.Random.Replace("###.###.###-##"))
            .RuleFor(u => u.BirthDate, f => f.Date.Past(40, DateTime.Today.AddYears(-18)))
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("(##) #####-####"))
            .RuleFor(u => u.Address, f => f.Address.StreetAddress())
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.State, f => f.Address.StateAbbr())
            .RuleFor(u => u.Country, _ => "Brasil")
            .RuleFor(u => u.CreatedAt, f => f.Date.Past(2))
            .RuleFor(u => u.IsActive, f => f.Random.Bool(0.9f))

            // Additional data
            .RuleFor(u => u.Gender, f => f.PickRandom("Masculino", "Feminino", "Outro"))
            .RuleFor(u => u.MaritalStatus, f => f.PickRandom("Solteiro", "Casado", "Divorciado", "Viúvo"))
            .RuleFor(u => u.MotherName, f => f.Name.FullName())
            .RuleFor(u => u.FatherName, f => f.Name.FullName())
            .RuleFor(u => u.Nationality, f => f.Address.Country())
            .RuleFor(u => u.Profession, f => f.Name.JobTitle())

            // Bank details
            .RuleFor(u => u.BankName, f => f.Company.CompanyName() + " Bank")
            .RuleFor(u => u.BankAgency, f => f.Random.Replace("####"))
            .RuleFor(u => u.BankAccount, f => f.Finance.Account())
            .RuleFor(u => u.AccountType, f => f.PickRandom("Corrente", "Poupança", "Salário"))

            // Additional document
            .RuleFor(u => u.RG, f => f.Random.Replace("##.###.###-#"))
            .RuleFor(u => u.DocumentIssueDate, f => f.Date.Past(20))

            // Filters and simulation data
            .RuleFor(u => u.EducationLevel, f => f.PickRandom("Fundamental", "Médio", "Superior", "Pós-graduação", "Mestrado", "Doutorado"))
            .RuleFor(u => u.EmploymentStatus, f => f.PickRandom("Empregado", "Desempregado", "Autônomo", "Empreendedor", "Estudante"));

        return faker.Generate(quantity);
    }
}
