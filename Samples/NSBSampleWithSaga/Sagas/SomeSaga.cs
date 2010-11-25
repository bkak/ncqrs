using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing;
using NServiceBus;
using NServiceBus.Saga;
using Events;
using Ncqrs.NServiceBus;
using Commands;

namespace SagasNew
{
    //public class SomeSaga<T> : Saga<SagaData>,
    //    IAmStartedByMessages<Ncqrs.NServiceBus.EventMessage<T>>,
    //    IHandleMessages<Ncqrs.NServiceBus.EventMessage<T>>
    //    where T : SomeDomainObjectCreatedEvent

    public class SomeSaga : Saga<SagaData>,
        IAmStartedByMessages<Ncqrs.NServiceBus.EventMessage<ItemCreated>>,
        IHandleMessages<Ncqrs.NServiceBus.EventMessage<ItemCreated>>,
        IHandleMessages<Ncqrs.NServiceBus.EventMessage<PickListItemAdded>>,
        IHandleMessages<Ncqrs.NServiceBus.EventMessage<StockNotFound>>
    {
       
         public override void ConfigureHowToFindSaga()
        {
           // ConfigureMapping<Ncqrs.NServiceBus.EventMessage<SomeDomainObjectCreatedEvent>> (s => s.Id, m=> m.Payload.ObjectId);
           // ConfigureMapping<Ncqrs.NServiceBus.EventMessage<SomeDomainObjectInvalidated>>(s => s.Id, m => m.Payload.Id);
        }
        
        //public void Handle(EventMessage<T> message)
        //{
           
        //}
        public override void Timeout(object state)
        {
            Complete();
        }

        private void Complete()
        {
            //throw new NotImplementedException();
        }

        public void Handle(EventMessage<SomeDomainObjectCreatedEvent> message)
        {
            //throw new NotImplementedException();
        }

        public void Handle(EventMessage<ItemCreated> message)
        {
            Bus.Send("ServerQueue",
                     new CommandMessage()
                         {Payload = new CreateItemStock() {Item = message.Payload.Item, Qty = message.Payload.Qty}});
        }
        public void Handle(EventMessage<PickListItemAdded> message)
        {
            Bus.Send("ServerQueue",
                     new CommandMessage() { Payload = new ReduceItemStock() {Item= message.Payload.Item,   Qty = message.Payload.Qty , RefId = message.Payload.EventSourceId} });
        }

        public void Handle(EventMessage<StockNotFound> message)
        {
            Bus.Send("ServerQueue",
                     new CommandMessage() { Payload = new ChangeQtyOfPickListItem() { Item = message.Payload.Item, NewQty = message.Payload.QtyAvailable, PickListId = message.Payload.RefId } });
        }
    }
}
