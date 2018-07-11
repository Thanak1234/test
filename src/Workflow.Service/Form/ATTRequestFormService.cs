using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Business.ATTRequestForm;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.HumanResource;
using Workflow.Domain.Entities.BatchData;
using Workflow.Service.Interfaces;

namespace Workflow.Service
{
    public class ATTRequestFormService : AbstractRequestFormService<IATTRequestFormBC, ATTRequestWorkflowInstance>, IATTRequestFormService
    {
        private IFlightDetailRepository _FlightDetailRepository = null;

        private IDbFactory workflow;

        public ATTRequestFormService()
        {
            workflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.Workflow);
            var docWorkflow = DbContextSelector.getInstance().getDbFactory(DbContextSelector.DbName.WorkflowDoc);
            BC = new ATTRequestFormBC(workflow, docWorkflow);
            _FlightDetailRepository = new FlightDetailRepository(workflow);
        }

        public int GetTakenYears(int requestorId, int purposeId) {            
            return _FlightDetailRepository.GetTakenYears(requestorId, purposeId);
        }
    }
}
