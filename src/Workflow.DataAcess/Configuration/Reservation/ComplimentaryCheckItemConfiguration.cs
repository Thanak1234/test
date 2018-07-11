using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.DataAcess.Configuration.Reservation
{
    public class ComplimentaryCheckItemConfiguration : EntityTypeConfiguration<ComplimentaryCheckItem>
    {

        public ComplimentaryCheckItemConfiguration()
        {
            //Primary Key
            this.HasKey(t => t.Id);

            //Table & Column Mapping
            this.ToTable("COMPLIMENTARY_EXPENSE_CHECKLIST", "RESERVATION");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");            
            this.Property(t => t.ExplId).HasColumnName("EXPLID");
            this.Property(t => t.CreatedDate).HasColumnName("CREATEDATE");

            //this.Ignore(t => t.CheckName);
            //this.Ignore(t => t.Check);
            //this.Ignore(t => t.Uncheck);
        }
                
    }
}
