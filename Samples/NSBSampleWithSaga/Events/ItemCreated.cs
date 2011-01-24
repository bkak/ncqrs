using System;
using Ncqrs.Eventing.Sourcing;


namespace Events
{
    [Serializable]
    public class ItemCreated : SourcedEvent
    {
        public string Name { get; set; }
        public Guid Item { get; set; }
        public int Qty { get; set; }
    }
}
