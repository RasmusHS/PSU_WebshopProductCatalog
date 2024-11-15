using Order.Application.Dto;

namespace Order.Application.Interfaces.Queries;

public interface IGetAllOrdersQuery
{
    IEnumerable<OrderQueryDto> GetAllOrders();
}