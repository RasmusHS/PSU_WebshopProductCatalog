using Order.Application.Dto;
using Order.Application.Interfaces;
using Order.Application.Interfaces.Commands;
using Order.Crosscut;
using Order.Domain;
using System.Data;

namespace Order.Application.CommandHandlers;

public class CreateOrderCommandHandler : ICreateOrderCommand
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _uow;

    public CreateOrderCommandHandler(IOrderRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    void ICreateOrderCommand.Create(CreateOrderDto dto)
    {
        try
        {
            _uow.BeginTransaction(IsolationLevel.ReadCommitted);

            var order = new OrderEntity(dto.CustomerName, dto.Quantity, dto.Price, dto.Status);
            _repository.CreateOrder(order);

            _uow.Commit();
        }
        catch (Exception ex)
        {
            _uow.Rollback();
            throw new Exception("Error creating order", ex);
        }
    }
}