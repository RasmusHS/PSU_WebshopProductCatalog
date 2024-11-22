using Order.Application.Interfaces;
using Rebus.Handlers;
using Shared.Messages.Events;

namespace Order.Application.CommandHandlers.EventHandlers;

public class PaymentProcessedEventHandler : IHandleMessages<PaymentProcessedEvent>
{
    private readonly IOrderRepository _repository;

    public PaymentProcessedEventHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    Task IHandleMessages<PaymentProcessedEvent>.Handle(PaymentProcessedEvent message)
    {
        Console.WriteLine("PaymentProcessedEvent received");
        // Read
        var model = _repository.LoadOrder(message.OrderId);

        // DoIt
        model.Edit(message.CustomerName, message.Quantity, message.Price, message.Status);

        // Save
        _repository.UpdateOrder(model);

        return Task.CompletedTask;
    }
}
