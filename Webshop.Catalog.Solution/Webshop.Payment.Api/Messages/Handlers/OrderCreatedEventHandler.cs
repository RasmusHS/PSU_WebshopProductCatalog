using Rebus.Bus;
using Rebus.Handlers;
using Shared.Messages.Events;
using System;
using System.Threading.Tasks;

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
            Console.WriteLine("OrderCreatedEvent received");
            
            await _bus.Publish( new PaymentProcessedEvent()
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
            Console.WriteLine("PaymentProcessedEvent published");

            await Task.CompletedTask;
        }
    }
}
