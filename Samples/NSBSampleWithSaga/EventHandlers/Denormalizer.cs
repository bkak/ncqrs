using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.NServiceBus;
using NServiceBus;
using ReadModel;

namespace EventHandlers
{
    public class Denormalizer : IMessageHandler<EventMessage<PickListCreated>>, IMessageHandler<EventMessage<PickListItemAdded>>, 
                                IMessageHandler<EventMessage<ItemCreated>>, IMessageHandler<EventMessage<ItemStockReduced>>, 
                                IMessageHandler<EventMessage<ItemStockCreated>>, IMessageHandler<EventMessage<ItemQtyInPickListChanged>>
    {
        public void Handle(EventMessage<PickListCreated> message)
        {
            using (var context = new MyNotesReadModelEntities())
            {
                var lst = new picklist();
                lst.Number = message.Payload.No;
                lst.PickListId = message.Payload.PickListId;
                lst.Customer = message.Payload.Customer;
                context.picklists.AddObject(lst);
                context.SaveChanges();
            }
        }

        public void Handle(EventMessage<PickListItemAdded> message)
        {
            using (var context = new MyNotesReadModelEntities())
            {
                var item = new pickListItem();
                item.Item= message.Payload.Item;
                item.pickListId = message.Payload.EventSourceId;
                item.Price= message.Payload.Price;
                context.pickListItems.AddObject(item);
                context.SaveChanges();
            }
        }

        public void Handle(EventMessage<ItemCreated> message)
        {
            using (var context = new MyNotesReadModelEntities())
            {
                var item = new Item();
                item.Item1 = message.Payload.Item;
                item.Name  = message.Payload.Name;
                item.Qty = message.Payload.Qty;
                context.Items.AddObject(item);
                context.SaveChanges();
            }
        }

        public void Handle(EventMessage<ItemStockReduced> message)
        {
            using (var context = new MyNotesReadModelEntities())
            {
                var item = context.ItemStocks.SingleOrDefault(x => x.Item == message.Payload.Item);
                item.Qty  = message.Payload.Qty ;
                context.SaveChanges();
            }
        }

        public void Handle(EventMessage<ItemStockCreated> message)
        {
            using (var context = new MyNotesReadModelEntities())
            {
                var item = new ItemStock();
                item.Qty = message.Payload.Qty;
                item.Item = message.Payload.Item;
                item.RefId = message.Payload.RefId;
                context.ItemStocks.AddObject(item);
                context.SaveChanges();
            }
        }

        public void Handle(EventMessage<ItemQtyInPickListChanged> message)
        {
            using (var context = new MyNotesReadModelEntities())
            {
                var item = context.pickListItems.SingleOrDefault(x => x.PickListItemId == message.Payload.Item);
                item.Qty = message.Payload.Qty;
                context.SaveChanges();
            }
        }
    }
}
