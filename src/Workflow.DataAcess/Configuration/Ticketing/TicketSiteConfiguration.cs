using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.DataAcess.Configuration.Base;
using Workflow.Domain.Entities.Ticket;

namespace Workflow.DataAcess.Configuration.Ticketing
{
    public class TicketSiteConfiguration : AbstractModelConfigurationLevel2<TicketSite>
    {
        public TicketSiteConfiguration():base(){
            // Table & Column Configurationpings
            this.ToTable("SITE", "TICKET");
            this.Property(t => t.SiteName).HasColumnName("SITE_NAME");
        }
    }
}
