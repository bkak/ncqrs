using System;
using Ncqrs.Eventing.Sourcing;


namespace Events
{
     [Serializable]
    public class StockNotFound : SourcedEvent
    {
        public Guid Item { get; set; }
        public int QtyAvailable { get; set; }
        public Guid RefId { get; set; }
    }
}
