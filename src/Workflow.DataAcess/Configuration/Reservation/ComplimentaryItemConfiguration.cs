using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Reservation;

namespace Workflow.DataAcess.Configuration.Reservation
{
    public class ComplimentaryItemConfiguration : EntityTypeConfiguration<ComplimentaryItem>
    {

        public ComplimentaryItemConfiguration()
        {
            //Primary Key
            this.HasKey(t => t.Id);

            //Table & Column Mapping

            this.ToTable("COMPLIMENTARY_EXPENSE_LIST", "RESERVATION");
            this.Property(t => t.Name).HasColumnName("NAME");
            this.Property(t => t.Id).HasColumnName("ID");            
        }

    }
}
