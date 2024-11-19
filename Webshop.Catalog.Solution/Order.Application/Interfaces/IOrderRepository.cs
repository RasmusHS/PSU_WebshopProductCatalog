using Order.Application.DTO.Queries;
using Order.Domain;

namespace Order.Application.Interfaces;

public interface IOrderRepository
{
    void CreateOrder(OrderEntity order);
    IEnumerable<OrderQueryDto> GetAllOrders();
    OrderQueryDto GetOrderById(int id);
    OrderEntity LoadOrder(int id);
    void UpdateOrder(OrderEntity order);
    void DeleteOrder(OrderEntity order);
}