using System;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;
using Ncqrs.Eventing.Sourcing;


namespace Events
{
      [Serializable]
    public class ItemStockCreated : SourcedEvent
    {
        public Guid Item { get; set; }
        public int Qty { get; set; }
        public Guid RefId { get; set; }
    }
}
