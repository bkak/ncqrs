using System;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;


namespace Commands
{
    [Serializable]
    [MapsToAggregateRootMethod("Domain.ItemStock, Domain", "ReduceItemStock")]
    public class ReduceItemStock : CommandBase
    {
        [AggregateRootIdAttribute]
        public Guid Item { get; set; }
        public int Qty { get; set; }
        public Guid RefId { get; set; }
    }
}
