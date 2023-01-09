using Hangfire.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hangfire.API;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Person> People => Set<Person>();
}
