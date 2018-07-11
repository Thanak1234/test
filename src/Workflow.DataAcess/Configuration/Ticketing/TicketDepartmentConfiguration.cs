using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketDepartmentConfiguration  : AbstractModelConfigurationLevel2<TicketDepartment>
    {
        public TicketDepartmentConfiguration() : base()
        {
            this.ToTable("DEPARTMENT", "TICKET");
            this.Property(t => t.DeptName).HasColumnName("DEPT_NAME");
            this.Property(t => t.HoDId).HasColumnName("HOD_ID");
            this.Property(t => t.DefaultItemId).HasColumnName("DEFAULT_ITEM_ID");
            this.Property(t => t.InternalUsed).HasColumnName("INTERNAL_USED");
            this.Property(t => t.AutomationEmail).HasColumnName("AUTOMATION_EMIAL");
            this.Property(t => t.DeptSignature).HasColumnName("DEPT_SIGNATURE");
            this.Property(t => t.IsDefault).HasColumnName("IS_DEFAULT");
            this.Property(t => t.Status).HasColumnName("STATUS");
            //this.Ignore(t => t.IsDefault);
        }
    }
}
