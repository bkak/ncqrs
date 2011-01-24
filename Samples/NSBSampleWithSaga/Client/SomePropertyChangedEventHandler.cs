using System;
using Events;
using Ncqrs.NServiceBus;
using NServiceBus;

namespace Client
{
    public class SomePropertyChangedEventHandler : IMessageHandler<EventMessage<SomePropertyChangedEvent>>, IMessageHandler<EventMessage<SomeDomainObjectCreatedEvent>>
   {
      public void Handle(EventMessage<SomePropertyChangedEvent> message)
      {
         ClientEndpoint.AggregateId = message.Payload.EventSourceId;
         Console.WriteLine("Aggregate with ID={0} property changed to {1}", message.Payload.EventSourceId, message.Payload.Value);         
      }
      public void Handle(EventMessage<SomeDomainObjectCreatedEvent> message)
      {
          ClientEndpoint.AggregateId = message.Payload.EventSourceId;
          Console.WriteLine("Aggregate with ID={0} created to {1}", message.Payload.EventSourceId, message.Payload.ObjectId);
      }
   }
}