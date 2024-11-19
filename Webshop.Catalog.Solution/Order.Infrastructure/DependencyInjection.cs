using Microsoft.Extensions.DependencyInjection;
using Order.Application.Interfaces;
using Order.Crosscut.Implementation;
using Order.Crosscut;
using Order.Persistence;

namespace Order.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>(p =>
        {
            var db = p.GetRequiredService<ApplicationDbContext>();
            return new UnitOfWork(db);
        });
        return services;
    }
}