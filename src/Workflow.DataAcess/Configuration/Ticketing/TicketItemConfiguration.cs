using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketItemConfiguration : AbstractModelConfigurationLevel2<TicketItem>
    {
        public TicketItemConfiguration() : base()
        {
            this.ToTable("ITEM", "TICKET");
            this.Property(t => t.SubCateId).HasColumnName("SUB_CATE_ID");
            this.Property(t => t.ItemName).HasColumnName("ITEM_NAME");
            this.Property(t => t.SLAId).HasColumnName("SLA_ID");
            this.Property(t => t.TeamId).HasColumnName("TEAM_ID");
            this.Ignore(t => t.IsActive);
            this.Ignore(t => t.IsDefault);
            this.Ignore(t => t.TicketTypeId);
            this.Property(t => t.Status).HasColumnName("STATUS");
            
        }
    }
}
