using Order.Application.DTO.Queries;
using Order.Application.Interfaces;
using Order.Application.Interfaces.Queries;

namespace Order.Application.QueryHandlers;

public class GetOrderQueryHandler : IGetOrderQuery
{
    private readonly IOrderRepository _repository;

    public GetOrderQueryHandler(IOrderRepository repository)
    {
        _repository = repository;
    }
    
    OrderQueryDto IGetOrderQuery.GetOrderById(Guid id)
    {
        return _repository.GetOrderById(id);
    }
}