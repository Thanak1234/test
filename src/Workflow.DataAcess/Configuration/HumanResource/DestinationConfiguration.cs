using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.HR;
using Workflow.Domain.Entities.HumanResource;

namespace Workflow.DataAcess.Configuration.HumanResource
{
    public class DestinationConfiguration : EntityTypeConfiguration<Destination>
    {
        public DestinationConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            
            // Table & Column Mappings
            this.ToTable("DESTINATION", "HR");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.FromDestination).HasColumnName("FROM_DESTINATION");
            this.Property(t => t.ToDestination).HasColumnName("TO_DESTINATION");
            this.Property(t => t.Date).HasColumnName("DATE");
            this.Property(t => t.Time).HasColumnName("TIME");
            
            
        }
    }
}