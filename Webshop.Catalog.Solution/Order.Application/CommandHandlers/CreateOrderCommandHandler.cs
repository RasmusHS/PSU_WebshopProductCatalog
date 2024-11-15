using Order.Application.Dto;
using Order.Application.Interfaces;
using Order.Application.Interfaces.Commands;

namespace Order.Application.CommandHandlers;

public class CreateOrderCommandHandler : ICreateOrderCommand
{
    private readonly IOrderRepository _repository;

    public CreateOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }
    void ICreateOrderCommand.Create(CreateOrderDto dto)
    {
        throw new System.NotImplementedException();
    }
}