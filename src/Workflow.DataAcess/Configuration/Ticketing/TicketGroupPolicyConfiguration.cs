using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketGroupPolicyConfiguration : AbstractModelConfigurationLevel2<TicketGroupPolicy>
    {
        public TicketGroupPolicyConfiguration() : base()
        {
            this.ToTable("GROUP_POLICY", "TICKET");
            this.Property(t => t.GroupName).HasColumnName("GROUP_NAME");
            this.Property(t => t.Status).HasColumnName("STATUS");
            this.Property(t => t.CreateTicket).HasColumnName("CREATE_TICKET");
            this.Property(t => t.EditTicket).HasColumnName("EDIT_TICKET");
            this.Property(t => t.EditRequestor).HasColumnName("EDIT_REQUESTOR");
            this.Property(t => t.PostTicket).HasColumnName("POST_TICKET");
            this.Property(t => t.CloseTicket).HasColumnName("CLOSE_TICKET");
            this.Property(t => t.AssignTicket).HasColumnName("ASSIGN_TICKET");
            this.Property(t => t.MergeTicket).HasColumnName("MERGE_TICKET");
            this.Property(t => t.DeleteTicket).HasColumnName("DELETE_TICKET");
            this.Property(t => t.DeptTransfer).HasColumnName("DEPT_TRANSFER");
            this.Property(t => t.ChangeStatus).HasColumnName("CHANGE_STATUS");
            this.Property(t => t.LimitAccess).HasColumnName("LIMIT_ACCESS");

            this.Property(t => t.NewTicketNotify).HasColumnName("NEW_TICKET_NOTIFY");
            this.Property(t => t.AssignedNotify).HasColumnName("ASSIGNED_NOTIFY");
            this.Property(t => t.ReplyNotify).HasColumnName("REPLY_NOTIFY");
            this.Property(t => t.ChangeStatusNotify).HasColumnName("CHANGE_STATUS_NOTIFY");
            this.Property(t => t.SubTicket).HasColumnName("CREATE_SUB_TICKET");
            this.Property(t => t.ReportAccess).HasColumnName("REPORT_ACCESS");
        }
    }
}
