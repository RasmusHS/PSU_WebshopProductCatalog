using Order.Application.DTO.Queries;

namespace Order.Application.Interfaces.Queries;

public interface IGetAllOrdersQuery
{
    IEnumerable<OrderQueryDto> GetAllOrders();
}