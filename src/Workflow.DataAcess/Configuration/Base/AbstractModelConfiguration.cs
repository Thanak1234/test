using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Base;

namespace Workflow.DataAcess.Configuration.Base
{
    public class AbstractModelConfiguration<T> : EntityTypeConfiguration<T> where T : AbstractBaseEntity
    {
        public AbstractModelConfiguration()
        {
            // Primary Key

            this.HasKey(t => t.Id);

            // Table & Column Mappings

            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.CreatedDate).HasColumnName("CREATED_DATE");
        }
    }
}
