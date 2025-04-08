using Blazor8Auth.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor8Auth.Database.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

    }
}
