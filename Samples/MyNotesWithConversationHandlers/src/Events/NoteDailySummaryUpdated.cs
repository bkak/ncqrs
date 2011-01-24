using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
   [Serializable]
    public class NoteDailySummaryUpdated : SourcedEvent
    {
        public Guid SummaryId { get {return EventSourceId;} }
        public int NewCount { get; set; }
        public int EditCount { get; set; }
    }
}
