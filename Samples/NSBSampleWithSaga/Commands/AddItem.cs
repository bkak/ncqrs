using System;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Commands
{
    [Serializable]
    [MapsToAggregateRootConstructor("Domain.Item, Domain")]
    public class AddItem : CommandBase
    {
        public string Name { get; set; }
        public int Qty { get; set; }
    }
}
