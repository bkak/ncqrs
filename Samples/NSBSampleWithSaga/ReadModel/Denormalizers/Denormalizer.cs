using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ReadModel.Denormalizers
{
    public class PickListDenormalizer : IEventHandler<PickListCreated>, IEventHandler<PickListItemAdded>, IEventHandler<ItemCreated>,  IEventHandler<ItemStockCreated>
    {

        public void Handle(PickListCreated evnt)
        {
            throw new NotImplementedException();
        }

        public void Handle(PickListItemAdded evnt)
        {
            throw new NotImplementedException();
        }

        public void Handle(ItemCreated evnt)
        {
            throw new NotImplementedException();
        }

        //public void Handle(ItemStockAdded evnt)
        //{
        //    throw new NotImplementedException();
        //}

        public void Handle(ItemStockCreated evnt)
        {
            throw new NotImplementedException();
        }
    }
}
