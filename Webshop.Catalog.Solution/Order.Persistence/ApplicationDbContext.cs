using System.Data;
using Microsoft.EntityFrameworkCore;
using Order.Application.Interfaces;
using Order.Domain;

namespace Order.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<OrderEntity> Orders { get; set; }
    public IDbConnection Connection => Database.GetDbConnection();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}