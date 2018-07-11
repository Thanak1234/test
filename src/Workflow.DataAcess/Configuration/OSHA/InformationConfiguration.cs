using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.OSHA;

namespace Workflow.DataAcess.Configuration.OSHA {
    public class InformationConfiguration : EntityTypeConfiguration<OSHAInformation> {

        public InformationConfiguration() {
            ToTable("INFORMATION", "OSHA");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.ACNR).HasColumnName("ACNR");
            Property(t => t.AT).HasColumnName("AT");
            Property(t => t.CA).HasColumnName("CA");
            Property(t => t.DF).HasColumnName("DF");
            Property(t => t.DIEGSD).HasColumnName("DIEGSD");
            Property(t => t.DTA).HasColumnName("DTA");
            Property(t => t.E1).HasColumnName("E1");
            Property(t => t.E2).HasColumnName("E2");
            Property(t => t.G3).HasColumnName("G3");
            Property(t => t.G4).HasColumnName("G4");
            Property(t => t.G5).HasColumnName("G5");
            Property(t => t.HCAT).HasColumnName("HCAT");
            Property(t => t.HSC).HasColumnName("HSC");
            Property(t => t.LAI).HasColumnName("LAI");
            Property(t => t.NODONE).HasColumnName("NO_DONE");
            Property(t => t.NTA).HasColumnName("NTA");
            Property(t => t.YESDONE).HasColumnName("YES_DONE");
        }
    }
}
