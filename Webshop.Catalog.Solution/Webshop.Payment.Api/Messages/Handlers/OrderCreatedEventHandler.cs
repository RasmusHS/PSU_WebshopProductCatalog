using Rebus.Bus;
using Rebus.Handlers;
using System.Threading.Tasks;
using Webshop.Payment.Api.Messages.Events;

namespace Webshop.Payment.Api.Messages.Handlers
{
    public class OrderCreatedEventHandler : IHandleMessages<OrderCreatedEvent>
    {
        private readonly IBus _bus;

        public OrderCreatedEventHandler(IBus bus)
        {
            _bus = bus;
        }

        async Task IHandleMessages<OrderCreatedEvent>.Handle(OrderCreatedEvent message)
        {
            await _bus.Publish(new PaymentProcessedEvent()
            {
                OrderId = message.OrderId,
                OrderNumber = message.OrderNumber,
                OrderDate = message.OrderDate,
                CustomerName = message.CustomerName,
                Quantity = message.Quantity,
                Price = message.Price,
                TotalAmount = message.TotalAmount,
                Status = "Payment Complete"
            });

            await Task.CompletedTask;
        }
    }
}
