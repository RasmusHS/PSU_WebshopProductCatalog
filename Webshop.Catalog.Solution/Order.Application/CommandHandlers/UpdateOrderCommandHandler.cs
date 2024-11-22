using Order.Application.DTO.Commands;
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
        // Read
        var model = _repository.LoadOrder(dto.OrderId);

        // DoIt
        model.Edit(dto.CustomerName, dto.Quantity, dto.Price, dto.Status);

        // Save
        _repository.UpdateOrder(model);
    }
}