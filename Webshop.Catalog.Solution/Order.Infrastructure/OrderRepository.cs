using Order.Application.Dto;
using Order.Application.Interfaces;
using Order.Domain;

namespace Order.Infrastructure;

public class OrderRepository : IOrderRepository
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IApplicationReadDbConnection _readDbConnection;
    private readonly IApplicationWriteDbConnection _writeDbConnection;

    public OrderRepository(IApplicationDbContext dbContext, IApplicationReadDbConnection readDbConnection, IApplicationWriteDbConnection writeDbConnection)
    {
        _dbContext = dbContext;
        _readDbConnection = readDbConnection;
        _writeDbConnection = writeDbConnection;
    }

    public void CreateOrder(OrderEntity order)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderQueryDto> GetAllOrders()
    {
        throw new NotImplementedException();
    }

    public OrderQueryDto GetOrderById(int id)
    {
        throw new NotImplementedException();
    }

    public OrderEntity LoadOrder(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateOrder(OrderEntity order)
    {
        throw new NotImplementedException();
    }

    public void DeleteOrder(OrderEntity order)
    {
        throw new NotImplementedException();
    }
}