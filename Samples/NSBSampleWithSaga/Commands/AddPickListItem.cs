using System;
using Ncqrs.Commanding;

using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Commands
{
    [Serializable]
    [MapsToAggregateRootMethod("Domain.PickList, Domain", "AddPickListItem")]
    public class AddPickListItem : CommandBase
    {
        [AggregateRootIdAttribute]
        public Guid PickListId { get; set; }
        public Guid Item { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
