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
    public class TravelDetailConfiguration : EntityTypeConfiguration<TravelDetail>
    {
        public TravelDetailConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            
            // Table & Column Mappings
            this.ToTable("TRAVEL_DETAIL", "HR");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.ClassTravelEntitlement).HasColumnName("CLASS_TRAVEL_ENTITLEMENT");
            this.Property(t => t.ClassTravelRequest).HasColumnName("CLASS_TRAVEL_REQUEST");
            this.Property(t => t.ReasonException).HasColumnName("REASON_EXCEPTION");
            this.Property(t => t.PurposeTravel).HasColumnName("PURPOSE_TRAVEL");
            this.Property(t => t.EstimatedCostTicket).HasColumnName("ESTIMATED_COST_TICKET");
            this.Property(t => t.costTicket).HasColumnName("COST_TICKET");
            this.Property(t => t.ReasonTravel).HasColumnName("REASON_TRAVEL");
            this.Property(t => t.NoRequestTaken).HasColumnName("NO_REQUEST_TAKEN");
            this.Property(t => t.ExtraCharge).HasColumnName("EXTRA_CHARGE");
            this.Property(t => t.Remark).HasColumnName("REMARK");

        }
    }
}