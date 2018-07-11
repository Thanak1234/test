/**
*@author : Yim Samaune
*/
using System;
using System.Linq;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.Domain.Entities;

namespace Workflow.DataAcess.Repositories
{
    public class ActivityHistoryRepository : RepositoryBase<ActivityHistory> , IActivityHistoryRepository
    {
        public ActivityHistoryRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public ActivityHistory GetLastActivityHistory(int requestId, string activityName, string actionBy)
        {
            var dbSet = DbContext.Set<ActivityHistory>();
            try
            {
                return dbSet.Where(p => 
                        p.RequestHeaderId == requestId && 
                        p.Activity == activityName
                        //p.Approver.Replace("K2:", string.Empty).ToUpper() == actionBy.Replace("K2:", string.Empty).ToUpper()
                    )
                    .OrderByDescending(p => p.CreatedDate)
                    .SingleOrDefault();
            }
            catch (Exception)
            {
                return new ActivityHistory();
            }
        }
    }
}
