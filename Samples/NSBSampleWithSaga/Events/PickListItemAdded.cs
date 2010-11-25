using System;
using Ncqrs.Eventing.Sourcing;


namespace Events
{
     [Serializable]
    public class PickListItemAdded : SourcedEvent
    {
         public Guid Item { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
