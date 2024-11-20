using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.CommandHandlers;
using Order.Application.CommandHandlers.EventHandlers;
using Order.Application.Interfaces.Commands;
using Order.Application.Interfaces.Queries;
using Order.Application.QueryHandlers;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Shared.Messages.Events;

namespace Order.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ICreateOrderCommand, CreateOrderCommandHandler>();
        services.AddScoped<IGetAllOrdersQuery, GetAllOrdersQueryHandler>();
        services.AddScoped<IGetOrderQuery, GetOrderQueryHandler>();
        services.AddScoped<IUpdateOrderCommand, UpdateOrderCommandHandler>();
        services.AddScoped<IDeleteOrderCommand, DeleteOrderCommandHandler>();
        //services.AutoRegisterHandlersFromAssemblyOf<PaymentProcessedEventHandler>();

        services.AddRebus(
            rebus => rebus
                //.Routing(r =>
                //    r.TypeBased().Map<OrderCreatedEvent>("OrderQueue"))
                .Transport(t =>
                t.UseRabbitMq(
                        configuration.GetConnectionString("MessageBroker"),
                        "OrderQueue"))
                //.Subscriptions(s => s.StoreInPostgres(configuration.GetConnectionString("Database"), "OrderSubNames"))
                //.Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.PureJson))
                //.Options(o => o.Decorate<ISerializer>(c => new CustomMessageDeserializer(c.Get<ISerializer>())))
                ,
            onCreated: async bus =>
            {
                //await bus.Advanced.Topics.Subscribe("OrderCreatedEvent"); // PaymentProcessedEvent
                await bus.Subscribe<PaymentProcessedEvent>();
                //await bus.Advanced.Topics.Subscribe("PaymentProcessedEvent");
            });
        services.AddRebusHandler<PaymentProcessedEventHandler>();
        //services.AddScoped<typeof(PaymentProcessedEvent), PaymentProcessedEventHandler>();

        return services;
    }
}