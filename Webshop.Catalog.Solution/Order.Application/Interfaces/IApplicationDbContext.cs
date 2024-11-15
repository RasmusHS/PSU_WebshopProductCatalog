using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Order.Domain;

namespace Order.Application.Interfaces;

public interface IApplicationDbContext
{
    public IDbConnection Connection { get; }
    DatabaseFacade Database { get; }
    public DbSet<OrderEntity> Orders { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}