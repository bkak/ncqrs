using System;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
    public class ItemStockReduced : SourcedEvent
    {
        public Guid Item { get; set; }
        public int Qty { get; set; }
        public Guid RefId { get; set; }
    }
}
