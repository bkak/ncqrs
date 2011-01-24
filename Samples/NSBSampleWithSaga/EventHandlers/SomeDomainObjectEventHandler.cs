using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using Ncqrs.NServiceBus;
using NServiceBus;

namespace EventHandlers
{
    public class SomeDomainObjectEventHandler : IMessageHandler<EventMessage<SomePropertyChangedEvent>>, 
        IMessageHandler<EventMessage<SomeDomainObjectCreatedEvent>>
    {
        public void Handle(EventMessage<SomePropertyChangedEvent> message)
        {
            EndPointConfig.AggregateId = message.Payload.EventSourceId;
            Console.WriteLine("Aggregate with ID={0} property changed to {1}", message.Payload.EventSourceId, message.Payload.Value);
        }

        public void Handle(EventMessage<SomeDomainObjectCreatedEvent> message)
        {
            EndPointConfig.AggregateId = message.Payload.EventSourceId;
            Console.WriteLine("Aggregate with ID={0} property changed to {1}", message.Payload.EventSourceId, message.Payload.ObjectId);
        }
    }
}
