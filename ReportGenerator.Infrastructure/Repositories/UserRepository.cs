using Microsoft.EntityFrameworkCore;
using ReportGenerator.Domain.Entities;
using ReportGenerator.Infrastructure.Data;

namespace ReportGenerator.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUsersAsync(int limit);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync(int limit)
        {
            return await _context.Users
                .OrderBy(u => u.Name)
                .Take(limit)
                .ToListAsync();
        }
    }
}
