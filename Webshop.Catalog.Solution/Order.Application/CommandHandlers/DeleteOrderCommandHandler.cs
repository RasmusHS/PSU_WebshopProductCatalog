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
    
    void IDeleteOrderCommand.Delete(Guid id)
    {
        // Read
        var model = _repository.LoadOrder(id);

        // DoIt
        _repository.DeleteOrder(model);
    }
}