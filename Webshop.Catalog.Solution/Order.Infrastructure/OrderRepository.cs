using Microsoft.EntityFrameworkCore;
using Order.Application.Dto;
using Order.Application.Interfaces;
using Order.Domain;
using Order.Infrastructure.Messages.Events;
using Rebus.Bus;

namespace Order.Infrastructure;

public class OrderRepository : IOrderRepository
{
    private readonly IApplicationDbContext _db;
    private readonly IBus _bus;

    public OrderRepository(IApplicationDbContext db, IBus bus)
    {
        _db = db;
        _bus = bus;
    }

    public async void CreateOrder(OrderEntity order)
    {
        _db.Orders.AddAsync(order).GetAwaiter();
        await _db.SaveChangesAsync();
        await _bus.Publish(new OrderCreatedEvent()
        {
            OrderId = order.OrderId,
            OrderNumber = order.OrderNumber,
            OrderDate = order.OrderDate,
            CustomerName = order.CustomerName,
            Quantity = order.Quantity,
            Price = order.Price,
            TotalAmount = order.TotalAmount,
            Status = order.Status
        });
    }

    public IEnumerable<OrderQueryDto> GetAllOrders()
    {
        foreach(var entity in _db.Orders.AsNoTracking().ToList())
            yield return new OrderQueryDto()
            {
                OrderId = entity.OrderId,
                OrderNumber = entity.OrderNumber,
                OrderDate = entity.OrderDate,
                CustomerName = entity.CustomerName,
                Quantity = entity.Quantity,
                Price = entity.Price,
                TotalAmount = entity.TotalAmount,
                Status = entity.Status
            };
    }

    public OrderQueryDto GetOrderById(int id)
    {
        var entity = _db.Orders.AsNoTracking().FirstOrDefault(x => x.OrderId == id);
        if (entity == null) throw new Exception("Order not found");

        return new OrderQueryDto()
        {
            OrderId = entity.OrderId,
            OrderNumber = entity.OrderNumber,
            OrderDate = entity.OrderDate,
            CustomerName = entity.CustomerName,
            Quantity = entity.Quantity,
            Price = entity.Price,
            TotalAmount = entity.TotalAmount,
            Status = entity.Status
        };
    }

    public OrderEntity LoadOrder(int id)
    {
        var entity = _db.Orders.AsNoTracking().FirstOrDefault(x => x.OrderId == id);
        if (entity == null) throw new Exception("Order not found");

        return entity;
    }

    public void UpdateOrder(OrderEntity order)
    {
        _db.Orders.Update(order);
        _db.SaveChangesAsync(new CancellationToken());
    }

    public void DeleteOrder(OrderEntity order)
    {
        _db.Orders.Remove(order);
        _db.SaveChangesAsync(new CancellationToken());
    }
}