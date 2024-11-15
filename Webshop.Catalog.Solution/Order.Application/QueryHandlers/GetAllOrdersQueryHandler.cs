using Order.Application.Dto;
using Order.Application.Interfaces;
using Order.Application.Interfaces.Queries;

namespace Order.Application.QueryHandlers;

public class GetAllOrdersQueryHandler : IGetAllOrdersQuery
{
    private readonly IOrderRepository _repository;

    public GetAllOrdersQueryHandler(IOrderRepository repository)
    {
        _repository = repository;
    }
    
    IEnumerable<OrderQueryDto> IGetAllOrdersQuery.GetAllOrders()
    {
        throw new System.NotImplementedException();
    }
}