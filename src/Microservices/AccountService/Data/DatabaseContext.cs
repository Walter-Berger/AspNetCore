using AccountService.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AccountService.Data;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;


    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
