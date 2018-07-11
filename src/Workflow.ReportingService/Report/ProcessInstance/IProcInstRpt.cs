using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.ReportingService.Report;
using Workflow.DataObject;
using Workflow.DataObject.Reports;
using Workflow.Domain.Entities;

namespace Workflow.ReportingService.Report
{
    public interface IProcInstRpt : IReport<ProcInst, ProcInstParam>
    {
        
    }
}
