using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Rebus.Extensions;
using Rebus.Messages;
using Rebus.Serialization;
using System;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Payment.Api.Services;

public class CustomMessageDeserializer //: ISerializer
{
    //static readonly ConcurrentDictionary<string, Type> KnownTypes = new ConcurrentDictionary<string, Type>
    //{
    //    ["Order.Application.Messages.Events.OrderCreatedEvent, Order.Application"] = typeof(OrderCreatedEvent),
    //    ["Webshop.Payment.Api.Messages.Events.OrderCreatedEvent, Webshop.Payment.Api"] = typeof(OrderCreatedEvent),
    //    ["Order.Application.Messages.Events.PaymentProcessedEvent, Order.Application"] = typeof(PaymentProcessedEvent),
    //    ["Webshop.Payment.Api.Messages.Events.PaymentProcessedEvent, Webshop.Payment.Api"] = typeof(PaymentProcessedEvent)
    //};

    //readonly ISerializer _serializer;

    //public CustomMessageDeserializer(ISerializer serializer) => _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

    //public Task<TransportMessage> Serialize(Message message) => _serializer.Serialize(message);

    //public async Task<Message> Deserialize(TransportMessage transportMessage)
    //{
    //    var headers = transportMessage.Headers.Clone();
    //    var json = Encoding.UTF8.GetString(transportMessage.Body);
    //    var typeName = headers.GetValue(Headers.Type);

    //    // if we don't know the type, just deserialize the message into a JObject
    //    if (!KnownTypes.TryGetValue(typeName, out var type))
    //    {
    //        return new Message(headers, JsonConvert.DeserializeObject<JObject>(json));
    //    }

    //    var body = JsonConvert.DeserializeObject(json, type);

    //    return new Message(headers, body);
    //}
}
