using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ncqrs.Eventing.ServiceModel.Bus
{
    public static class RegisterAllConversationHandlersInAssembly
    {
        private static ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void RegisterAllConverseHandlersInAssembly(this InProcessEventBus target, Assembly asm)
        {
            target.RegisterAllConverseHandlersInAssembly(asm, CreateInstance);
        }

        public static void RegisterAllConverseHandlersInAssembly(this InProcessEventBus target, Assembly asm, Func<Type, object> handlerFactory)
        {
            foreach (var type in asm.GetTypes().Where(ImplementsAtLeastOneIConversationHandlerInterface))
            {
                var handler = handlerFactory(type);

                foreach (var handlerInterfaceType in type.GetInterfaces().Where(IsIConversationHandlerInterface))
                {
                    var eventDataType = handlerInterfaceType.GetGenericArguments().First();
                    RegisterHandlerConverse(handler, eventDataType, target);
                }
            }
        }

        private static object CreateInstance(Type type)
        {
            Contract.Requires<ArgumentNullException>(type != null);
           return Activator.CreateInstance(type);
        }

        private static void RegisterHandlerConverse(object handler, Type eventDataType, InProcessEventBus target)
        {
            var registerHandlerMethod = target.GetType().GetMethods().Single
            (
                m => m.Name == "RegisterConversationHandler" && m.IsGenericMethod && m.GetParameters().Count() == 1
            );

            var targetMethod = registerHandlerMethod.MakeGenericMethod(new[] { eventDataType });
            targetMethod.Invoke(target, new object[] { handler });

            _log.InfoFormat("Registered {0} as event handler for event {1}.", handler.GetType().FullName, eventDataType.FullName);
        }

        private static bool ImplementsAtLeastOneIConversationHandlerInterface(Type type)
        {
            return type.IsClass &&
                   type.GetInterfaces().Any(IsIConversationHandlerInterface);
        }

        private static bool IsIConversationHandlerInterface(Type type)
        {
            return type.IsInterface &&
                   type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(IConversationHandler<>);
        }

    }
}
