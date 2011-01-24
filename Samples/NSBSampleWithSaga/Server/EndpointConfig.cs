﻿using System;
using NServiceBus;

namespace Server
{
   public class EndpointConfig : IConfigureThisEndpoint, AsA_Publisher, IWantCustomInitialization
   {
      public void Init()
      {
         Configure.With()
            .DefaultBuilder()
            .BinarySerializer()
            .MsmqSubscriptionStorage()
            .InstallNcqrs()           
            .MsmqTransport();
            //.PurgeOnStartup(true);
      }
   }
}