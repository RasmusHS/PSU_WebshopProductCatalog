using Order.Application.Dto;
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
    
    OrderQueryDto IGetOrderQuery.GetOrderById(int id)
    {
        return _repository.GetOrderById(id);
    }
}