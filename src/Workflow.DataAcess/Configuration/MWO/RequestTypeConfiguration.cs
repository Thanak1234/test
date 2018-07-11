using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.MWO;

namespace Workflow.DataAcess.Configuration.MWO {
    public class RequestTypeConfiguration : EntityTypeConfiguration<RequestType> {

        public RequestTypeConfiguration() {
            ToTable("MWO_REQUEST_TYPE", "MAINTENANCE");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.Name).HasColumnName("NAME");
            Property(t => t.Sequence).HasColumnName("SEQUENCE");
        }
    }
}
