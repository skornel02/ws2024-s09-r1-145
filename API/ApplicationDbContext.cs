using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserRecord> Users { get; set; }
    public DbSet<RouteRecord> Routes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
}
