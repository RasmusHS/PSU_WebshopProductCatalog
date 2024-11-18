using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Order.Domain;

namespace Order.Application.Interfaces;

public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }
    public DbSet<OrderEntity> Orders { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}