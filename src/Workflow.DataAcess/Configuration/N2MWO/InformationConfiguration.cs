using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.N2MWO;

namespace Workflow.DataAcess.Configuration.N2MWO
{
    public class N2InformationConfiguration : EntityTypeConfiguration<N2MWOInformation> {

        public N2InformationConfiguration() {
            ToTable("MWO_INFORMATION", "N2_MAIN");
            HasKey(t => t.Id);
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.CcdId).HasColumnName("CCD_ID");
            Property(t => t.Instruction).HasColumnName("INSTRUCTION");
            Property(t => t.JaDate).HasColumnName("JA_DATE");
            Property(t => t.JaTechnician).HasColumnName("JA_TECHNICIAN");
            Property(t => t.Location).HasColumnName("LOCATION");
            Property(t => t.Mode).HasColumnName("MODE");
            Property(t => t.ReferenceNumber).HasColumnName("REFERENCE_NUMBER");
            Property(t => t.Remark).HasColumnName("REMARK");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.RequestType).HasColumnName("REQUEST_TYPE");
            Property(t => t.SubLocation).HasColumnName("SUB_LOCATION");
            Property(t => t.TcDesc).HasColumnName("TC_DESC");
            Property(t => t.WorkType).HasColumnName("WORK_TYPE");
            Property(t => t.Wrjd).HasColumnName("WRJD");
        }
    }
}
