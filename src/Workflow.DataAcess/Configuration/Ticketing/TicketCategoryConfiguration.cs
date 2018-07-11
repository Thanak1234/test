using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketCategoryConfiguration : AbstractModelConfigurationLevel2<TicketCategory>
    {
         // Table & Column Configurationpings

        public TicketCategoryConfiguration() : base()
        {
            this.ToTable("CATEGORY", "TICKET");
            this.Property(t => t.DeptId).HasColumnName("DEPT_ID");
            this.Property(t => t.CateName).HasColumnName("CATE_NAME");
            this.Property(t => t.Status).HasColumnName("STATUS");
        }

            
    }
}
