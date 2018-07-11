using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.DataAcess.Configuration.Base
{
    public abstract class AbstractModelConfigurationLevel2<T> : EntityTypeConfiguration<T> where T : AbstractEntityLevel2
    {
        public AbstractModelConfigurationLevel2()
        {
            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings

            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.Description).HasColumnName("DESCRIPTION");
            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");
            this.Property(t => t.ModifiedDate).HasColumnName("MODIFIED_DATE");
        }
    }
}
