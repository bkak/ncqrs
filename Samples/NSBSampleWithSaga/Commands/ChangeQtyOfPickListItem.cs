using System;
using Ncqrs.Commanding;

using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace Commands
{
    [Serializable]
    [MapsToAggregateRootMethod("Domain.PickList, Domain", "ChangeQtyOfPickListItem")]
    public class ChangeQtyOfPickListItem : CommandBase
    {
        [AggregateRootIdAttribute]
        public Guid PickListId { get; set; }
        public Guid Item { get; set; }
        public int NewQty { get; set; }
    }
}
