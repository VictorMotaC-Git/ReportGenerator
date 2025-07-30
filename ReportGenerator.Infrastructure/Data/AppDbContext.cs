using Microsoft.EntityFrameworkCore;
using ReportGenerator.Domain.Entities;

namespace ReportGenerator.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS"); // Nome da tabela Oracle
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedNever();
                entity.Property(e => e.Name).HasColumnName("NAME");
                entity.Property(e => e.Email).HasColumnName("EMAIL");
                // Continue os mappings conforme necessário
            });
        }
    }
}
