using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Commands
{
    [MapsToAggregateRootConstructor("Domain.MediaPlan, Domain")]
    public class CreateMediaPlan : CommandBase
    {
        public Guid MediaPlanId { get; set; }
        public int PlanNo { get; set; }
    }
}
