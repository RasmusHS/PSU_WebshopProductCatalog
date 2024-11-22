using Order.Application.DTO.Commands;
using Order.Application.Interfaces;
using Order.Application.Interfaces.Commands;
using Order.Domain;
using Rebus.Bus;
using Shared.Messages.Events;

namespace Order.Application.CommandHandlers;

public class CreateOrderCommandHandler : ICreateOrderCommand
{
    private readonly IOrderRepository _repository;
    private readonly IBus _bus;

    public CreateOrderCommandHandler(IOrderRepository repository, IBus bus)
    {
        _repository = repository;
        _bus = bus;
    }

    void ICreateOrderCommand.Create(CreateOrderDto dto)
    {
        Console.WriteLine("CreateOrderCommand received");

        var order = new OrderEntity(dto.CustomerName, dto.Quantity, dto.Price, dto.Status);
        _repository.CreateOrder(order);

        Console.WriteLine("Order created");

        Console.WriteLine("Publishing OrderCreatedEvent");

        _bus.Publish(new OrderCreatedEvent()
        {
            OrderId = order.OrderId,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            CustomerName = order.CustomerName,
            Quantity = order.Quantity,
            Price = order.Price,
            TotalAmount = order.TotalAmount,
            Status = order.Status
        }).Wait();
        
        Console.WriteLine("OrderCreatedEvent published");
    }
}