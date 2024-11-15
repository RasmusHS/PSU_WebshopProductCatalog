using Order.Application.Dto;
using Order.Application.Interfaces;
using Order.Application.Interfaces.Commands;

namespace Order.Application.CommandHandlers;

public class UpdateOrderCommandHandler : IUpdateOrderCommand
{
    private readonly IOrderRepository _repository;

    public UpdateOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }
    
    void IUpdateOrderCommand.Update(UpdateOrderDto dto)
    {
        throw new System.NotImplementedException();
    }
}