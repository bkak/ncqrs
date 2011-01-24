using System;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
    [Serializable]
    public class PickListCreated : SourcedEvent
    {
        public Guid PickListId { get; set; }
        public int No { get; set; }
        public string Customer { get; set; }
    }
}
