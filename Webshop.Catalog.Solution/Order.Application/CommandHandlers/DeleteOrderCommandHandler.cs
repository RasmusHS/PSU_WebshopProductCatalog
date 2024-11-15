using Order.Application.Interfaces;
using Order.Application.Interfaces.Commands;

namespace Order.Application.CommandHandlers;

public class DeleteOrderCommandHandler : IDeleteOrderCommand
{
    private readonly IOrderRepository _repository;

    public DeleteOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }
    
    void IDeleteOrderCommand.Delete(int id)
    {
        throw new System.NotImplementedException();
    }
}