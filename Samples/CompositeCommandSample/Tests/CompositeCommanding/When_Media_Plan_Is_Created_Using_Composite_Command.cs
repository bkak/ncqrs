using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Commands;
using CommonServiceLocator.WindsorAdapter;
using Domain;
using FluentAssertions;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.CompositeCommanding;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;
using Ncqrs.Eventing.Storage.NoDB;
using Ncqrs.Eventing.Storage.SQL;
using Ncqrs.Spec;
using Rhino.Commons;
using NS = Ncqrs.Spec;
using Events;



namespace Ncqrs.Tests.CompositeCommanding
{
    [Specification]
    public class When_Media_Plan_Is_Created_Using_Composite_Command : NS.CompositeCommandTestFixture<CompositeCommandBase>
    {
        private readonly Guid _mediaPlanId = Guid.NewGuid();
        protected override ICommandService BuildCommandService()
        {
            var service = new CommandService();
            var executor = Map.
                Command<CreateMediaPlan>().
                ToAggregateRoot<MediaPlan>().
                CreateNew(c => new MediaPlan(c.MediaPlanId, c.PlanNo));

            service.RegisterExecutor(executor);

            var executor1 = Map.
                Command<SetBudgetForMediaPlan>().
                ToAggregateRoot<MediaPlan>().WithId(c => c.MediaPlanId)
                .ToCallOn((c, b) => b.SetBudget(c.Budget));

            service.RegisterExecutor(executor1);

           
            return service;
        }

        protected override void SetupDependencies()
        {
            IWindsorContainer container = InitializeServiceLocator();

            container.AddComponent("IAggregateRootCreationStrategy", typeof(IAggregateRootCreationStrategy), typeof(SimpleAggregateRootCreationStrategy));
            container.AddComponent("EventBus", typeof(IEventBus), typeof(InProcessEventBus));
            container.Register(Component.For<IEventStore>().Instance(InitializeEventStore()));
            container.Register(Component.For<ISnapshotStore>().Instance(InitializeSnapShotStore()));
            container.AddComponent("DomainRepository", typeof(IDomainRepository), typeof(DomainRepository));
            container.Register(Component.For<IUnitOfWorkContext>()
                                    .ImplementedBy<UnitOfWork>()
                                    .LifeStyle.Is(LifestyleType.Thread));
            IoC.Initialize(container);
        }
        private static IEventStore InitializeEventStore()
        {
            var store = new InMemoryEventStore();
            return store;
        }
        private static ISnapshotStore InitializeSnapShotStore()
        {
            var store = new NoDBSnapshotStore("D:\\");
            return store;
        }
        private static IWindsorContainer InitializeServiceLocator()
        {
            IWindsorContainer container = new WindsorContainer();
            return container;
        }
        protected override CompositeCommandBase WhenExecutingCommand()
        {
            var com = new CompositeCommandBase();
            com.AddCommand(new CreateMediaPlan() { MediaPlanId = _mediaPlanId, PlanNo = 1 });
            com.AddCommand(new SetBudgetForMediaPlan() { MediaPlanId = _mediaPlanId, Budget = 10000 });
            return com;
        }
        [Then]
        public void Then_published_events_count_should_be_one()
        {
            PublishedEvents.Should().HaveCount(2);
            PublishedEvents.First().Should().BeOfType<MediaPlanCreated>();

            var theEvent = PublishedEvents.First().As<MediaPlanCreated>();
            theEvent.PlanNo.Should().Be(1);

            var budgetSet = PublishedEvents.ElementAt(1).As<BudgetForMediaPlanSet>();
            budgetSet.Budget.Should().Be(10000);
            
        }

        //[And]
        //public void And_should_be_of_type_MediaPlanCreated()
        //{
            

        //}
        //[And]
        //public void And_the_event_should_contain_the_correct_details()
        //{
            
        //}
    }
}
