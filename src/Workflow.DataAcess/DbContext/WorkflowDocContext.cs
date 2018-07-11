namespace Workflow.DataAcess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Domain.Entities;
    using Configuration;
    using Domain.Entities.IT;
    using Configuration.IT;
    using Domain.Entities.Attachement;
    using Domain.Entities.AV;
    using Configuration.AV;
    using BCJ = Domain.Entities.BCJ;
    using Domain.Entities.PBF;
    using BCJConfig = Configuration.BCJ;
    using Reservation = Domain.Entities.Reservation;
    using ReservationConfig = Configuration.Reservation;
    using Configuration.Attachment;
    using Domain.Entities.Attachment;
    using Domain.Entities.MWO;
    using Configuration.MWO;
    using Configuration.EOM;
    using Domain.Entities.EOMRequestForm;
    using Configuration.Incident;
    using Configuration.EGMMachine;
    using Configuration.EGMAttandance;

    public partial class WorkflowDocContext : DbContext
    {
        public WorkflowDocContext() : base("name=WorkflowDoc")
        {
        }

        public DbSet<DocumentFile> DocumentFiles { get; set; }
        public DbSet<FileTemp> Documents { get; set; }
        public DbSet<ItRequestFiles> ItRequestFiles { get; set; }
        public DbSet<AvbUploadFile> AVJBRequestFiles { get; set; }
        public DbSet<BCJ.BcjAttachment> BCJAttachments { get; set; }
        public DbSet<Reservation.FnFAttachment> FnFAttachments { get; set; }
        public DbSet<Complimentary> Complimentaries { get; set; }
        public DbSet<Requisition> Requisitions { get; set; }
        public DbSet<Domain.Entities.Attachment.ProjectBrief> ProjectBriefs { get; set; }
        public DbSet<TravelDetail> TravelDetails { get; set; }
        public DbSet<EOMUploadFile> EOMUploadFiles { get; set; }
        public DbSet<UploadFile> UploadFiles { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new AttachementConfiguration());
            modelBuilder.Configurations.Add(new ItRequestFilesConfiguration());
            modelBuilder.Configurations.Add(new AVJBRequestFilesConfiguration());
            modelBuilder.Configurations.Add(new BCJConfig.AttachmentConfiguration());
            modelBuilder.Configurations.Add(new ReservationConfig.AttachmentConfiguration());
            modelBuilder.Configurations.Add(new ComplimentaryConfiguration());
            modelBuilder.Configurations.Add(new RequisitionConfiguration());
            modelBuilder.Configurations.Add(new ProjectBriefConfiguration());
            modelBuilder.Configurations.Add(new DocumentConfiguration());
            modelBuilder.Configurations.Add(new TravelDetailConfiguration());
            modelBuilder.Configurations.Add(new EOMUploadFileConfiguration());            
            modelBuilder.Configurations.Add(new IncidentRequestFileConfiguration());
            modelBuilder.Configurations.Add(new MachineAttachmentConfiguration());
            modelBuilder.Configurations.Add(new AttandanceAttachmentConfiguration());
            modelBuilder.Configurations.Add(new SimpleFileUploadConfiguration());
            
        }

    }
}
