using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;
using Ncqrs.Domain;

namespace Domain
{
    public class MediaPlan : AggregateRootMappedByConvention
    {
        private int _budget;
        private Guid _headPlannerId;
        private int _planNo;
        
        public MediaPlan (Guid mediaPlanId, int planNo) : base(mediaPlanId)
        {
            ApplyEvent(new MediaPlanCreated(){PlanNo = planNo});
        }
        #region Commands
        public void SetBudget(int budget)
        {
            ApplyEvent(new BudgetForMediaPlanSet(){Budget = budget});
        }
        public void SetHeadPlanner(Guid headPlannerId)
        {
            ApplyEvent(new HeadPlannerForMediaPlanSet() { HeadPlannerId= headPlannerId});
        }
        #endregion Commands

        #region Events
        public void OnMediaPlanCreated(MediaPlanCreated evnt)
        {
            _planNo = evnt.PlanNo;
        }
        public void OnBudgetForMediaPlanSet(BudgetForMediaPlanSet evnt)
        {
            _budget = evnt.Budget;
        }
        public void OnHeadPlannerForMediaPlanSet(HeadPlannerForMediaPlanSet evnt)
        {
            _headPlannerId = evnt.HeadPlannerId;
        }
        #endregion Events
    }
}
