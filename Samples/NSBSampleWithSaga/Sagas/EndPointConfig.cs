using NServiceBus;
using NServiceBus.Grid.MessageHandlers;
using NServiceBus.Sagas.Impl;

namespace SagasNew
{
    //public class EndpointConfigBootstrapper : IWantToRunAtStartup
    //{
    //    private readonly IBus _bus;

    //    public EndpointConfigBootstrapper(IBus bus)
    //    {
    //        _bus = bus;
    //    }

    //    public void Run()
    //    {
    //        _bus.Subscribe<Ncqrs.NServiceBus.EventMessage<Events.SomeDomainObjectCreatedEvent>>();
    //    }

    //    public void Stop()
    //    {
    //    }
    //}

    public class EndpointConfig : IConfigureThisEndpoint, IWantCustomInitialization
                                    //,ISpecifyMessageHandlerOrdering
    {
        public void Init()
        {
            //var bus = NServiceBus.Configure.With()
            //    .DefaultBuilder()
            //    .BinarySerializer()
            //    .MsmqTransport()
            //    .IsTransactional(true)
            //    .PurgeOnStartup(false)
            //    .UnicastBus()
            //    .LoadMessageHandlers()
            //    .ImpersonateSender(false)
            //    .Sagas()
            //    .CreateBus()
            //    .Start(); 

            NServiceBus.Configure c = NServiceBus.Configure.With()
                // .StructureMapBuilder(_container)
                .DefaultBuilder()
                .BinarySerializer()
                .MsmqSubscriptionStorage()
                .MsmqTransport()
                .IsTransactional(true)
                .PurgeOnStartup(true)
                .UnicastBus()
                .LoadMessageHandlers()
                .Sagas();
              // .SetSagaPersister(_container);

            //NServiceBus.Configure.With()
            //    .DefaultBuilder()
            //    .BinarySerializer()
            //    .Sagas();
        }
        public void SpecifyOrder(Order order)
        {
            order.Specify(First<GridInterceptingMessageHandler>
              .Then<SagaMessageHandler>());
        }
    }
}
