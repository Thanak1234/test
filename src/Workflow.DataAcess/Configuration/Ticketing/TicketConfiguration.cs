using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketConfiguration : AbstractModelConfigurationLevel2<Workflow.Domain.Entities.Ticket.Ticket>
    {
        public TicketConfiguration() : base()
        {
            this.ToTable("TICKETING", "TICKET");

            this.Property(t=>t.TicketNo).HasColumnName("TICKET_NO");

            this.Property(t => t.DeptId).HasColumnName("DEPT_ID");

            this.Property(t => t.TicketTypeId).HasColumnName("TICKET_TYPE_ID");
            
            this.Property(t => t.Subject).HasColumnName("SUBJECT");

            this.Property(t => t.SubmitterId).HasColumnName("SUBMITTER_ID");
            this.Property(t => t.RequestorId).HasColumnName("REQUESTOR_ID");
            this.Property(t => t.SourceId).HasColumnName("SOURCE_ID");

            this.Property(t => t.TicketItemId).HasColumnName("TICKET_TYPE_ID");
            this.Property(t => t.ImpactId).HasColumnName("IMPACT_ID");
            this.Property(t => t.UrgentcyId).HasColumnName("URGENCY_ID");
            this.Property(t => t.PriorityId).HasColumnName("PRIORITY_ID");

            this.Property(t => t.SiteId).HasColumnName("SITE_ID");
            this.Property(t => t.TicketItemId).HasColumnName("ITEM_ID");
           
            this.Property(t => t.StatusId).HasColumnName("STATUS_ID");
            this.Property(t => t.LastAction).HasColumnName("LAST_ACTION");

            this.Property(t => t.EstimatedHours).HasColumnName("ESTIMATED_HOURS");

            this.Property(t => t.actualMinutes).HasColumnName("ACTUAL_MINUTES");
            this.Property(t => t.completedDate).HasColumnName("COMPLETED_DATE");
            

            this.Property(t => t.LastActionBy).HasColumnName("LAST_ACTION_BY");
            this.Property(t => t.LastActionDate).HasColumnName("LAST_ACTION_DATE");
            this.Property(t => t.WaitActionedBy).HasColumnName("WAIT_ACTIONED_BY");

            this.Property(t => t.LastAssEmpId).HasColumnName("LAST_ASSIGNED_EMP_ID");
            this.Property(t => t.LastAssTeamId).HasColumnName("LAST_ASSIGNED_TEAM_ID");
            
            this.Property(t => t.DueDate).HasColumnName("DUE_DATE");

            this.Property(t => t.FirstResponse).HasColumnName("FIRT_RESPONSE");

            this.Property(t => t.FirstResponseDate).HasColumnName("FIRST_RESPONSE_DATE");

            this.Property(t => t.LockedBy).HasColumnName("LOCKED_BY");

            this.Property(t => t.SlaId).HasColumnName("SLA_ID");

            this.Property(t => t.RootCauseId).HasColumnName("ROOT_CAUSE_ID");

            this.Property(t => t.RefType).HasColumnName("REF_TYPE");

            this.Property(t => t.Reference).HasColumnName("REFERENCE");

        }
    }
}
