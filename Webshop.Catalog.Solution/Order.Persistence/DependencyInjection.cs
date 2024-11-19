using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Interfaces;
using Order.Application.Messages.Events;
using Order.Crosscut.Implementation;
using Order.Crosscut;
using Rebus.Config;
using Rebus.Routing.TypeBased;

namespace Order.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseNpgsql(configuration.GetConnectionString("Database"))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork, UnitOfWork>(p =>
        {
            var db = p.GetRequiredService<ApplicationDbContext>();
            return new UnitOfWork(db);
        });

        services.AddRebus(
            rebus => rebus
                .Routing(r => 
                    r.TypeBased())
                .Transport(t => 
                    t.UseRabbitMq(
                        configuration.GetConnectionString("MessageBroker"),
                        inputQueueName: "OrderQueue")),
            onCreated: async bus =>
            {
                await bus.Subscribe<OrderCreatedEvent>();
            });
        
        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());
        
        return services;
    }
}