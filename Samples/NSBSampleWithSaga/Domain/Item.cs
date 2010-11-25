using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using Ncqrs.Domain;
namespace Domain
{
    public class Item : AggregateRootMappedByConvention
    {
        private int _qty { get; set; }
        private string _name { get; set; }
        public Item ()
        {}
        public Item(string name, int qty)
        {
            ApplyEvent(new ItemCreated(){Item = EventSourceId, Name = name, Qty = qty});
        }
        public void OnItemCreated(ItemCreated evnt)
        {
            _name = evnt.Name;
            _qty = evnt.Qty;
        }
    }
}
