using System;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Commands
{
    [Serializable]
    [MapsToAggregateRootConstructor("Domain.PickList, Domain")]
    public class CreatePickList : CommandBase
    {
        public int No { get; set; }
        public string Customer { get; set; }
    }
}
