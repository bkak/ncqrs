using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using Ncqrs.Domain;

namespace Domain
{
    public class NoteDailySummary : AggregateRootMappedByConvention
    {
        private DateTime _date;
        private int _newCount;
        private int _editCount;
        public NoteDailySummary()
        {
        }

        public NoteDailySummary(Guid summaryId, DateTime date, int newCount, int editCount) : base(summaryId)
        {
            ApplyEvent(new NoteDailySummaryCreated(){ Date = date, EditCount = editCount, NewCount = newCount });
        }

        public void UpdateDailySummary(int newCount,int editCount)
        {
            editCount = _editCount + editCount;
            newCount = _newCount + newCount;
            ApplyEvent(new NoteDailySummaryUpdated() { EditCount = editCount, NewCount = newCount });
        }

        public void OnNoteDailySummaryCreated(NoteDailySummaryCreated evnt)
        {
            _date = evnt.Date;
            _newCount = evnt.NewCount;
            _editCount = evnt.EditCount;
        }
        public void OnNoteDailySummaryUpdated(NoteDailySummaryUpdated evnt)
        {
           
            _newCount = evnt.NewCount;
            _editCount = evnt.EditCount;
        }

    }
}
