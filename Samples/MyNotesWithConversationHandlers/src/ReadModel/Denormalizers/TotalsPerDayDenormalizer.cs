using System.Linq;
using Events;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ReadModel.Denormalizers
{
    public class TotalsPerDayDenormalizer : IEventHandler<NoteDailySummaryCreated>,
                                            IEventHandler<NoteDailySummaryUpdated>
    {
        public void Handle(NoteDailySummaryCreated evnt)
        {
            using (var context = new ReadModelContainer())
            {
                var noteSummary = new TotalsPerDayItem() {Date = evnt.Date, EditCount = evnt.EditCount, SummaryId = evnt.SummaryId,NewCount = evnt.NewCount };
                context.TotalsPerDayItemSet.AddObject(noteSummary);
                context.SaveChanges();
            }
        }

        public void Handle(NoteDailySummaryUpdated evnt)
        {
            using (var context = new ReadModelContainer())
            {
                var noteSummary = context.TotalsPerDayItemSet.SingleOrDefault(i => i.SummaryId == evnt.SummaryId);
                noteSummary.EditCount = evnt.EditCount;
                noteSummary.NewCount = evnt.NewCount;
                context.SaveChanges();
            }

           
        }
    }
}