using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Infrastructure;
using Workflow.Domain.Entities.MWO;

namespace Workflow.DataAcess.Repositories.MWO {

    public class RequestInformationRepository : RepositoryBase<RequestInformation>, IRequestInformationRepository {
        public RequestInformationRepository(IDbFactory dbFactory) : base(dbFactory) { }

        public string GetReferenceNumber(string ccd) {
            var context = DbContext as WorkflowContext;

            if(context != null) {
                var record = context.DepartmentChargables.FirstOrDefault(p => p.CCD == ccd);
                if(record != null) {
                    var requestInfo = context.RequestInformations.Where(p => p.CcdId == record.Id).OrderByDescending(o => o.Id).FirstOrDefault();
                    if(requestInfo != null) {
                        int num = GetRefNumber(requestInfo.ReferenceNumber) + 1;
                        string refNum = string.Format("{0}-{1:D6}", record.CCD, num);
                        return refNum;
                    } else {
                        string refNum = string.Format("{0}-{1:D6}", record.CCD, 1);
                        return refNum;
                    }
                }
            }

            return string.Empty;
        }

        private int GetRefNumber(string refNum) {            
            int num = 0;
            var parts = refNum.Split('-');

            if (parts.Count() == 2) {
                string lastNum = parts[1];
                num = Convert.ToInt16(lastNum);
            }

            return num;
        }
    }

}
