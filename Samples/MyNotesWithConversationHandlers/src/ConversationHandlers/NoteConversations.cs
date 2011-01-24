using Domain;
using Events;
using Ncqrs.Domain;
using Ncqrs.Eventing.ServiceModel.Bus;
using System;

namespace ConversationHandlers
{
    public class NoteConversations : IConversationHandler<NewNoteAdded>,IConversationHandler<NoteTextChanged>
    {

        public void Handle(NewNoteAdded evnt)
        {
            if(evnt.SummaryId.Equals(Guid.Empty))
            {
                AggregateRoot aggregateRoot = new NoteDailySummary(Guid.NewGuid(), evnt.CreationDate, 1, 0);
                UnitOfWork.Current.RegisterDirtyInstance(aggregateRoot);
            }
            else
            {
                var noteDailySummary = UnitOfWork.Current.GetById(typeof(NoteDailySummary), evnt.SummaryId) as NoteDailySummary;
                noteDailySummary.UpdateDailySummary(1, 0);
                UnitOfWork.Current.RegisterDirtyInstance(noteDailySummary);
            }
        }
        public void Handle(NoteTextChanged evnt)
        {
            if (evnt.SummaryId.Equals(Guid.Empty))
            {
                AggregateRoot aggregateRoot = new NoteDailySummary(Guid.NewGuid(), System.DateTime.UtcNow, 0, 1);
                UnitOfWork.Current.RegisterDirtyInstance(aggregateRoot);
            }
            else
            {
                var noteDailySummary = UnitOfWork.Current.GetById(typeof(NoteDailySummary), evnt.SummaryId) as NoteDailySummary;
                noteDailySummary.UpdateDailySummary(0, 1);
                UnitOfWork.Current.RegisterDirtyInstance(noteDailySummary);
            }
        }
    }
}