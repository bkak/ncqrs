using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;
using NServiceBus.Saga;

namespace SagasNew
{
    public class SagaData :  IContainSagaData
    {
        public virtual Guid Id { get; set; }
        public virtual int No { get; set; }
        public virtual string OriginalMessageId { get; set; }
        public virtual string Originator { get; set; }
    }
}
