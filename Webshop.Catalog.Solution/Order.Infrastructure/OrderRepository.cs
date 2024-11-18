using Microsoft.EntityFrameworkCore;
using Order.Application.Dto;
using Order.Application.Interfaces;
using Order.Domain;

namespace Order.Infrastructure;

public class OrderRepository : IOrderRepository
{
    private readonly IApplicationDbContext _db;

    public OrderRepository(IApplicationDbContext db)
    {
        _db = db;
    }

    public void CreateOrder(OrderEntity order)
    {
        _db.Orders.AddAsync(order);
        _db.SaveChangesAsync();
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

    public async void UpdateOrder(OrderEntity order)
    {
        await Task.Run(() => _db.Orders.Update(order));
        await _db.SaveChangesAsync(new CancellationToken());
    }

    public async void DeleteOrder(OrderEntity order)
    {
        await Task.Run(() => _db.Orders.Remove(order));
        await _db.SaveChangesAsync(new CancellationToken());
    }
}