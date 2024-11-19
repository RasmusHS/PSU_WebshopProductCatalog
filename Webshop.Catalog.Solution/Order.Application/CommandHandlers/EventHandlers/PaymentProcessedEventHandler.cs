using Order.Application.Interfaces;
using Order.Application.Messages.Events;
using Order.Crosscut;
using Rebus.Handlers;
using System.Data;

namespace Order.Application.CommandHandlers.EventHandlers;

public class PaymentProcessedEventHandler : IHandleMessages<PaymentProcessedEvent>
{
    private readonly IOrderRepository _repository;
    private readonly IUnitOfWork _uow;

    public PaymentProcessedEventHandler(IOrderRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    Task IHandleMessages<PaymentProcessedEvent>.Handle(PaymentProcessedEvent message)
    {
        try
        { 
            _uow.BeginTransaction(IsolationLevel.ReadCommitted);

            // Read
            var model = _repository.LoadOrder(message.OrderId);

            // DoIt
            model.Edit(message.CustomerName, message.Quantity, message.Price, message.Status);

            // Save
            _repository.UpdateOrder(model);

            _uow.Commit();

            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            throw new Exception("Error processing payment", ex);
        }
    }
}
