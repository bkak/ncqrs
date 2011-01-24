using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Eventing.Sourcing;

namespace Events
{
    [Serializable]
    public class SomeDomainObjectInvalidated :   SourcedEvent
    {
        public Guid Id;
    }
}
