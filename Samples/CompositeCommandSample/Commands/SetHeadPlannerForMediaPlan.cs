using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Commands
{
    [MapsToAggregateRootMethod("Domain.MediaPlan, Domain", "SetHeadPlanner")]
    public class SetHeadPlannerForMediaPlan : CommandBase    
    {
        [AggregateRootId]
        public Guid MediaPlanId { get; set; }
        public Guid  HeadPlannerId { get; set; }
    }
}
