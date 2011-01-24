using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
    [Serializable]
    public class NoteDailySummaryCreated : SourcedEvent
    {
        public Guid SummaryId { get { return EventSourceId; }
        }
        public  DateTime Date { get; set;}
        public int NewCount { get; set;}
        public int EditCount { get; set; }
    }
}
