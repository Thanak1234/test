using System.Data.Entity.ModelConfiguration;
using Workflow.Domain.Entities.ITCR;

namespace Workflow.DataAcess.Configuration.ITCR
{
    public class RequestFormDataConfiguration : EntityTypeConfiguration<RequestFormData>
    {
        public RequestFormDataConfiguration()
        {
            HasKey(t => t.Id);
            ToTable("REQUEST_FORM_DATA", "ITCR");
            Property(t => t.Id).HasColumnName("ID");
            Property(t => t.RequestHeaderId).HasColumnName("REQUEST_HEADER_ID");
            Property(t => t.ActualResult).HasColumnName("ACTUAL_RESULT");
            Property(t => t.AdditionalNotes).HasColumnName("ADDITIONAL_NOTES");
            Property(t => t.ChangeType).HasColumnName("CHANGE_TYPE");
            Property(t => t.DateRequest).HasColumnName("DATE_REQUEST");
            Property(t => t.DireedResult).HasColumnName("DIREED_RESULT");
            Property(t => t.Failback).HasColumnName("FAILBACK");
            Property(t => t.Intervention).HasColumnName("INTERVENTION");
            Property(t => t.Implmentation).HasColumnName("IMPLMENTATION");
            Property(t => t.Justification).HasColumnName("JUSTIFICATION");
            Property(t => t.RequestChange).HasColumnName("REQUEST_CHANGE");
            Property(t => t.RestorationLavel).HasColumnName("RESTORATION_LAVEL");
            Property(t => t.Session).HasColumnName("SESSION");
            Property(t => t.TargetDate).HasColumnName("TARGET_DATE");
            Property(t => t.TestParameters).HasColumnName("TEST_PARAMETERS");
            Property(t => t.AkResult).HasColumnName("AK_RESULT");
            Property(t => t.AkRemark).HasColumnName("AK_REMARK");
        }
    }
}
