using Order.Application.DTO.Commands;
using Order.Application.Interfaces;
using Order.Application.Interfaces.Commands;
using Order.Crosscut;
using System.Data;

namespace Order.Application.CommandHandlers;

public class UpdateOrderCommandHandler : IUpdateOrderCommand
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _uow;

    public UpdateOrderCommandHandler(IOrderRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }
    
    void IUpdateOrderCommand.Update(UpdateOrderDto dto)
    {
        try
        {
            _uow.BeginTransaction(IsolationLevel.ReadCommitted);

            // Read
            var model = _repository.LoadOrder(dto.OrderId);

            // DoIt
            model.Edit(dto.CustomerName, dto.Quantity, dto.Price, dto.Status);

            // Save
            _repository.UpdateOrder(model);

            _uow.Commit();
        }
        catch (Exception ex)
        {
            _uow.Rollback();
            throw new Exception("Error updating order", ex);
        }
    }
}