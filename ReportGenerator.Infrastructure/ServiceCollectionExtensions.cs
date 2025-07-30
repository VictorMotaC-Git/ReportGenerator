using Microsoft.Extensions.DependencyInjection;
using ReportGenerator.Infrastructure.Data;
using ReportGenerator.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ReportGenerator.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string oracleConnectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseOracle(oracleConnectionString));

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
