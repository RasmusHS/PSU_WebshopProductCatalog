using Microsoft.Extensions.DependencyInjection;
using Order.Application.CommandHandlers;
using Order.Application.Interfaces.Commands;
using Order.Application.Interfaces.Queries;
using Order.Application.QueryHandlers;

namespace Order.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateOrderCommand, CreateOrderCommandHandler>();
        services.AddScoped<IGetAllOrdersQuery, GetAllOrdersQueryHandler>();
        services.AddScoped<IGetOrderQuery, GetOrderQueryHandler>();
        services.AddScoped<IUpdateOrderCommand, UpdateOrderCommandHandler>();
        services.AddScoped<IDeleteOrderCommand, DeleteOrderCommandHandler>();
        
        return services;
    }
}