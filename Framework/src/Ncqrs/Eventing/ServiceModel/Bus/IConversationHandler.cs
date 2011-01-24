using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ncqrs.Eventing.ServiceModel.Bus
{
    public interface IConversationHandler<TEvent> where TEvent : IEvent
    {
        void Handle(TEvent evnt);
    }
}
