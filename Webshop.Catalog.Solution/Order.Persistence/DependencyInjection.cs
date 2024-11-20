using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Interfaces;
using Order.Crosscut.Implementation;
using Order.Crosscut;

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

        //services.AddRebus(
        //    rebus => rebus
        //        //.Routing(r =>
        //        //    r.TypeBased().Map<OrderCreatedEvent>("OrderQueue"))
        //        .Transport(t => 
        //            t.UseRabbitMq(
        //                configuration.GetConnectionString("MessageBroker"),
        //                "OrderQueue"))
        //        //.Subscriptions(s => s.StoreInPostgres(configuration.GetConnectionString("Database"), "OrderSubNames"))
        //        //.Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.PureJson))
        //        //.Options(o => o.Decorate<ISerializer>(c => new CustomMessageDeserializer(c.Get<ISerializer>())))
        //        ,
        //    onCreated: async bus =>
        //    {
        //        //await bus.Advanced.Topics.Subscribe("OrderCreatedEvent"); // PaymentProcessedEvent
        //        await bus.Subscribe<PaymentProcessedEvent>();
        //        //await bus.Advanced.Topics.Subscribe("PaymentProcessedEvent");
        //    });
        //services.AddRebusHandler<PaymentProcessedEventHandler>();
        
        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());
        
        return services;
    }
}