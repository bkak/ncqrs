using System;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Commands
{
    [Serializable]
    [MapsToAggregateRootConstructor("Domain.ItemStock, Domain")]
    public class CreateItemStock : CommandBase
    {
        public Guid Item { get; set; }
        public int Qty { get; set; }
        public Guid RefId { get; set; }
    }
}
