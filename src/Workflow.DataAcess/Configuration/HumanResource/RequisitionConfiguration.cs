using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;
using Workflow.Domain.Entities.HumanResource;

namespace Workflow.DataAcess.Configuration.HumanResource
{
    public class RequisitionConfiguration : EntityTypeConfiguration<Requisition>
    {
        public RequisitionConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);
            
            // Table & Column Mappings
            this.ToTable("EMPLOYEE_REQUISITION", "HR");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            this.Property(t => t.ReferenceNo).HasColumnName("REFERENCE_NUMBER");
            this.Property(t => t.Position).HasColumnName("POSITION");
            this.Property(t => t.ReportingTo).HasColumnName("REPORTING_TO");
            this.Property(t => t.SalaryRange).HasColumnName("SALARY_RANG");
            this.Property(t => t.RequestTypeId).HasColumnName("REQUEST_TYPE_ID");
            this.Property(t => t.ShiftTypeId).HasColumnName("SHIFT_TYPE_ID");
            this.Property(t => t.LocationTypeId).HasColumnName("LOCATION_TYPE_ID");
            this.Property(t => t.Private).HasColumnName("PRIVATE");
            this.Property(t => t.RequisitionNumber).HasColumnName("REQUISITION_NUMBER");
            this.Property(t => t.Justification).HasColumnName("JUSTIFICATION");


            this.HasRequired<RequestHeader>(p => p.RequestHeader).WithMany().HasForeignKey(p => p.RequestHeaderId);
            this.HasRequired<Lookup>(p => p.RequestType).WithMany().HasForeignKey(p => p.RequestTypeId);
            this.HasRequired<Lookup>(p => p.ShiftType).WithMany().HasForeignKey(p => p.ShiftTypeId);
            this.HasRequired<Lookup>(p => p.LocationType).WithMany().HasForeignKey(p => p.LocationTypeId);

            /****** Script for SelectTopNRows command from SSMS  ******/
        }
    }
}