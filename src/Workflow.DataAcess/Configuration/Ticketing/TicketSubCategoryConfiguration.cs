using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketSubCategoryConfiguration : AbstractModelConfigurationLevel2<TicketSubCategory>
    {
        public TicketSubCategoryConfiguration() : base()
        {
            this.ToTable("SUB_CATEGORY", "TICKET");
            this.Property(t => t.CateId).HasColumnName("CATE_ID");
            this.Property(t => t.SubCateName).HasColumnName("SUB_CATE_NAME");
            this.Property(t => t.Status).HasColumnName("STATUS");
        }
    }
}
