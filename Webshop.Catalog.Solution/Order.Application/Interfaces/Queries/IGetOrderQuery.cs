using Order.Application.DTO.Queries;

namespace Order.Application.Interfaces.Queries;

public interface IGetOrderQuery
{
    OrderQueryDto GetOrderById(Guid id);
}