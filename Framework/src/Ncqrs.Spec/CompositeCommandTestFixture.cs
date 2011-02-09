using System;
using System.Collections.Generic;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.CompositeCommanding;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace Ncqrs.Spec
{
    [Specification]
    [TestFixture] // TODO: Testdriven.net debug runner doesn't recognize inhiret attributes. Use native for now.
    public abstract class CompositeCommandTestFixture<TCommand>
        where TCommand : ICompositeCommand
    {
        protected Exception CaughtException { get; private set; }

        protected IEnumerable<ISourcedEvent> PublishedEvents { get; private set; }

        protected TCommand ExecutedCommand { get; private set; }

        protected abstract TCommand WhenExecutingCommand();

        protected virtual void SetupDependencies() { }
        protected virtual void Finally() { }

        [Given]
        [SetUp] // TODO: Testdriven.net debug runner doesn't recognize inhiret attributes. Use native for now.
        public void Setup()
        {
            SetupDependencies();
            var commandService = BuildCommandService();
            PublishedEvents = new SourcedEvent[0];

        
            try
            {
                var command = WhenExecutingCommand();

                using (var context = new EventContext())
                {
                    commandService.Execute(command);
                    
                    ExecutedCommand = command;
                    PublishedEvents = context.Events;
                }
            }
            catch (Exception exception)
            {
                CaughtException = exception;
            }
            finally
            {
                Finally();
            }
        }

        protected abstract ICommandService BuildCommandService();
    }
}
