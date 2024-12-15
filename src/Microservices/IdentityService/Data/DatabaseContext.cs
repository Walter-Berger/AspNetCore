using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data;

public class DatabaseContext : IdentityDbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
}
