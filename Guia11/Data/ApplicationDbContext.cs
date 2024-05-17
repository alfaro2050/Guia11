using Microsoft.EntityFrameworkCore;
using Guia11.Models;

namespace Guia11.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
    }
}