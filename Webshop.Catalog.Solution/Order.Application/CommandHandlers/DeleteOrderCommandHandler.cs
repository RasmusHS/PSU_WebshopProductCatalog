using Order.Application.Interfaces;
using Order.Application.Interfaces.Commands;
using Order.Crosscut;
using System.Data;

namespace Order.Application.CommandHandlers;

public class DeleteOrderCommandHandler : IDeleteOrderCommand
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _uow;

    public DeleteOrderCommandHandler(IOrderRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }
    
    void IDeleteOrderCommand.Delete(int id)
    {
        try
        {
            _uow.BeginTransaction(IsolationLevel.ReadCommitted);

            // Read
            var model = _repository.LoadOrder(id);

            // DoIt
            _repository.DeleteOrder(model);

            _uow.Commit();
        }
        catch (Exception ex)
        {
            _uow.Rollback();
            throw new Exception("Error deleting order", ex);
        }
    }
}