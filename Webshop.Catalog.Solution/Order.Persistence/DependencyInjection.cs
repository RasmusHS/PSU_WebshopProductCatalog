using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Interfaces;
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
        
        services.AddRebus(
            rebus => rebus
                .Routing(r => 
                    r.TypeBased())
                .Transport(t => 
                    t.UseRabbitMq(
                        configuration.GetConnectionString("RabbitMQ"),
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