using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
    public class MediaPlanCreated : SourcedEvent 
    {
        public int PlanNo { get; set; }
    }

    public class BudgetForMediaPlanSet : SourcedEvent
    {
        public int Budget { get; set; }
    }

    public class HeadPlannerForMediaPlanSet : SourcedEvent
    {
        public Guid HeadPlannerId { get; set; }
    }
}
