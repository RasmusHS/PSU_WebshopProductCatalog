using Order.Application.Dto;

namespace Order.Application.Interfaces.Queries;

public interface IGetOrderQuery
{
    OrderQueryDto GetOrderById(int id);
}