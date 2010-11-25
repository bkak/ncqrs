using System;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
     [Serializable]
    public class ItemQtyInPickListChanged : SourcedEvent
    {
        public Guid Item { get; set; }
        public int Qty { get; set; }
    }
}
