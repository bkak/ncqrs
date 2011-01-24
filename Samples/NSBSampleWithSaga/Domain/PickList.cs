using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using Ncqrs.Domain;

namespace Domain
{
    public class PickList : AggregateRootMappedByConvention
    {
        private Guid _pickListId;
        private int _no;
        private string _customer;
        private Dictionary<Guid, PickListItem> _items;
        private Dictionary<Guid, PickListItem> Items
        {
            get { return _items ?? new Dictionary<Guid, PickListItem>(); }
            set { _items = value; }
        }
        public PickList ()
        {}
        public PickList (int no, string customer)
        {
            ApplyEvent(new PickListCreated(){ Customer = customer, No = no, PickListId = EventSourceId });
        }
        public void OnPickListCreated(PickListCreated evnt)
        {
            _no = evnt.No;
            _customer = evnt.Customer;
        }
        public void AddPickListItem(Guid item, int qty, decimal price)
        {
           
            ApplyEvent(new PickListItemAdded(){Item = item, Price= price, Qty = qty});
        }
        public void OnPickListItemAdded(PickListItemAdded evnt)
        {
            if (_items == null)
                _items = new Dictionary<Guid, PickListItem>();
            _items.Add(evnt.Item, new PickListItem(){Item = evnt.Item, Price = evnt.Price, Qty = evnt.Qty});
        }
        public void ChangeQtyOfPickListItem(Guid item, int newQty)
        {
            ApplyEvent(new ItemQtyInPickListChanged(){Item = item, Qty = newQty});
        }
        public void OnItemQtyInPickListChanged(ItemQtyInPickListChanged evnt)
        {
            _items[evnt.Item].Qty = evnt.Qty;
        }
    }
}
