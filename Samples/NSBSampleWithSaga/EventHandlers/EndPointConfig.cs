using NServiceBus;

namespace EventHandlers
{
    public class EndPointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
        public static System.Guid AggregateId;

        public void Init()
        {
            Configure.With()
                .DefaultBuilder()
                .BinarySerializer();
                
        }
    }
    
}
