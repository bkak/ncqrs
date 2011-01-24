using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using Ncqrs.Domain;

namespace Domain
{
    public class ItemStock : AggregateRootMappedByConvention
    {
        private Guid _itemId { get; set; }
        private int _qty { get; set; }
        private Guid _rerId { get; set; }
        public ItemStock ()
        {}
        public ItemStock (Guid item, int qty, Guid refId)
        {
            EventSourceId = item;
            ApplyEvent(new ItemStockCreated() { Item = EventSourceId, Qty = qty });
        }
        public void ReduceItemStock(Guid item, int qty, Guid refId )
        {
            if(_qty >= qty)
               ApplyEvent(new ItemStockReduced() { Item = item, Qty = qty, RefId = refId });
            else
            {
                ApplyEvent(new StockNotFound() {Item = item, QtyAvailable = (_qty - qty), RefId = refId});
            }
        }
        public void OnItemStockCreated(ItemStockCreated evnt)
        {
            _itemId = evnt.Item;
            _qty = evnt.Qty;
        }
        public void OnItemStockReduced(Guid itemId, int qty)
        {
            _qty = qty;
        }
        public void OnStockNotFound()
        {
        }
    }
}
