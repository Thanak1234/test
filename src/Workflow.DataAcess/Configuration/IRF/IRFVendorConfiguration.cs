using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.IRF;

namespace Workflow.DataAcess.Configuration.IRF {
    public class IRFVendorConfiguration : EntityTypeConfiguration<IRFVendor> {
        public IRFVendorConfiguration() {
            HasKey(t => t.Id);
            ToTable("ITIRF_VENDOR", "IT");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.Vendor).HasColumnName("VENDOR");
            Property(t => t.Address).HasColumnName("ADDRESS");
            Property(t => t.ContactNo).HasColumnName("CONTACT_NUMBER");
            Property(t => t.Email).HasColumnName("EMAIL");
        }
    }
}
