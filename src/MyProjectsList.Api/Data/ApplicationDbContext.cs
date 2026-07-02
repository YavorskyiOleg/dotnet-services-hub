using Microsoft.EntityFrameworkCore;
using MyProjectsList.Api.Models;
namespace MyProjectsList.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }
        public DbSet<Project> Projects => Set<Project>();
    }
}