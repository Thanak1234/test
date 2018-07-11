/**
*@author : Phanny
*/
namespace Workflow.DataAcess
{
    using System.Data.Entity;
    using Domain.Entities;
    using Configuration;
    using Domain.Entities.IT;
    using Configuration.IT;
    using Microsoft.AspNet.Identity.EntityFramework;
    using App;
    using Domain.Entities.Core;
    using Domain.Entities.AV;
    using Configuration.AV;
    using Configuration.BPMDATA;
    using BCJ = Domain.Entities.BCJ;
    using Domain.Entities.PBF;
    using Reservation = Domain.Entities.Reservation;
    using MWO = Domain.Entities.MWO;
    using MTF = Domain.Entities.MTF;
    using N2MWO = Domain.Entities.N2MWO;

    using HumanResourceConfig = Configuration.HumanResource;
    using ReservationConfig = Configuration.Reservation;
    using BCJConfig = Configuration.BCJ;
    using PBFConfig = Configuration.PBF;
    using MWOConfig = Configuration.MWO;
    using N2MWOConfig = Configuration.N2MWO;
    using EOMConfig = Configuration.EOM;
    
    using Configuration.Ticketing;
    using Configuration.Email;
    using Domain.Entities.Email;
    using Configuration.HumanResource;
    using Domain.Entities.Scheduler;
    using Domain.Entities.EOMRequestForm;

    using Domain.Entities.INCIDENT;
    using Configuration.Incident;
    using Domain.Entities.EGM;
    using Configuration.EGMMachine;
    using Configuration.EGMAttandance;
    using Domain.Entities.Ticket;
    using Configuration.Scheduler;
    using Configuration.ADMCPPForm;
    using Domain.Entities.ADMCPPForm;
    using Configuration.ITApp;
    using Domain.Entities.Core.ITApp;
    using Domain.Entities.Queue;
    using Configuration.Queue;
    using Domain.Entities.VAF;
    using Configuration.VAF;
    using Configuration.VoucherRequest;
    using Domain.Entities.VoucherRequest;
    using Domain.Entities.Core.CCR;
    using Configuration.CCR;
    using Domain.Entities.RAC;
    using Configuration.RAC;
    using Domain.Entities.Finance;
    using Domain.Entities.Forms;
    using System.Data.Entity.Infrastructure;
    using Domain.Entities.OSHA;
    using Configuration.Admsr;
    using Domain.Entities.Admsr;
    using Configuration.ITAD;
    using Domain.Entities.ITAD;
    using Domain.Entities.ITEIRQ;
    using Domain.Entities.RMD;
    using Configuration.IRF;
    using Workflow.Domain.Entities.Security;

    public partial class WorkflowContext : IdentityDbContext<ApplicationUser>
    {

        private static WorkflowContext workfFlowContext = null;

        public WorkflowContext() : base("name=Workflow")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;
            //Database.SetInitializer(new CreateDatabaseIfNotExists<WorkflowContext>());
            //this.Configuration.LazyLoadingEnabled = false;
            //this.Configuration.ProxyCreationEnabled = false;
        }
        public DbSet<Profiler> Profilers { get; set; }
        public DbSet<Document> AttachmentDocuments { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<Lookup> Lookups { get; set; }
        public DbSet<RequestHeader> RequestHeaders { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<ActivityHistory> ActivityHistory { get; set; }

        public DbSet<RequestUser> RequestUser { get; set; }
        public DbSet<Domain.Entities.Department> Department { get; set; }
        public DbSet<RequestItem> RequestItem { get; set; }
        public DbSet<EmployeeView> EmployeeViews { get; set; }
        public DbSet<RequestApplication> RequestApplications { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<AvbRequestItem> AvbRequestItem { get; set; }
        public DbSet<DeptAccessRight> DeptAccessRight { get; set; }

        public DbSet<AvbJobHistory> AvbJobHistory { get; set; }
        public DbSet<AvbItem> AvbItem { get; set; }
        public DbSet<AvbItemType> AvbItemType { get; set; }
        public DbSet<BpmUserRole> UserRoles { get; set; }
        public DbSet<DeptApprovalRole> DeptApprovalRoles { get; set; }
        public DbSet<ActivityRoleRight> ActivityRoleRights { get; set; }

        public DbSet<BCJ.ProjectDetail> BCJProjectDetails { get; set; }
        public DbSet<BCJ.BcjRequestItem> BCJRequestItems { get; set; }
        public DbSet<BCJ.AnalysisItem> BCJAnalysisItems { get; set; }
        public DbSet<BCJ.CapexCategory> BCJCapexCategories { get; set; }
        public DbSet<BCJ.PurchaseOrder> PurchaseOrder { get; set; }

        public DbSet<Reservation.Complimentary> Complimentaries { get; set; }
        public DbSet<Reservation.Guest> Guests { get; set; }
        public DbSet<Reservation.Booking> Bookings { get; set; }
        public DbSet<Reservation.RoomCategory> RoomCategories { get; set; }

        public DbSet<MWO.Mode> Modes { get; set; }
        public DbSet<MWO.RequestType> RequestTypes { get; set; }
        public DbSet<MWO.WorkType> WorkTypes { get; set; }
        public DbSet<MWO.DepartmentChargable> DepartmentChargables { get; set; }
        public DbSet<MWO.RequestInformation> RequestInformations { get; set; }
        public DbSet<MWO.MaintenanceDepartment> MaintenanceDepartments { get; set; }
        public DbSet<MWO.MWOInformation> MWOInformations { get; set; }

        public DbSet<N2MWO.N2Mode> N2Modes { get; set; }
        public DbSet<N2MWO.N2RequestType> N2RequestTypes { get; set; }
        public DbSet<N2MWO.N2WorkType> N2WorkTypes { get; set; }
        public DbSet<N2MWO.N2DepartmentChargable> N2DepartmentChargables { get; set; }
        public DbSet<N2MWO.N2RequestInformation> N2RequestInformations { get; set; }
        public DbSet<N2MWO.N2MaintenanceDepartment> N2MaintenanceDepartments { get; set; }
        public DbSet<N2MWO.N2MWOInformation> N2MWOInformations { get; set; }


        public DbSet<ProjectBrief> ProjectBriefs { get; set; }
        public DbSet<Specification> Specifications { get; set; }

        //Ticket

        public DbSet<TicketType> TicketTypes { get; set; }


        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<TicketSource> TicketSources { get; set; }
        public DbSet<TicketImpact> TicketImpacts { get; set; }
        public DbSet<TicketUrgency> TicketUrgencies { get; set; }
        public DbSet<TicketPriority> TicketPriorities { get; set; }
        public DbSet<TicketSite> TicketSites { get; set; }
        public DbSet<TicketSLA> TicketSLAs { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<TicketSubCategory> TicketSubCategories { get; set; }
        public DbSet<TicketItem> TicketItems { get; set; }
        public DbSet<TicketDepartment> TicketDepartments { get; set; }
        public DbSet<TicketTeam> TicketTeams { get; set; }
        public DbSet<TicketAgent> TicketAgents { get; set; }
        public DbSet<TicketSubTkLink> TicketSubTkLinks { get; set; }

        public DbSet<TicketNoneReqEmp> TicketNoneReqEmps { get; set; }

        public DbSet<TicketSLAMapping> TicketSLAMappings { get; set; }

        // AVDR & AVIR

        public DbSet<Avdr> Avdrs { get; set; }
        public DbSet<Avir> Avirs { get; set; }

        // Scheduler

        public DbSet<Job> Jobs { get; set; }
        public DbSet<EmailContent> EmailContents { get; set; }
        public DbSet<Recipient> Recipients { get; set; }


        public DbSet<EOMDetail> EOMs { get; set; }
        public DbSet<MTF.Medicine> Medicines { get; set; }
        public DbSet<MTF.Prescription> Prescriptions { get; set; }
        public DbSet<MTF.Treatment> Treatments { get; set; }
        public DbSet<MTF.UnfitToWork> UnfitToWorks { get; set; }
        public DbSet<MTF.QueuePatient> QueuePatients { get; set; }
        

        //INCIDENT
        public DbSet<IncidentEmployee> IncidentEmployee { get; set; }

        //Machine
        public DbSet<MachineEmployee> MachineEmployee {get;set;}

        // Email
        public DbSet<EmailItem> EmailItems { get; set; }
        public DbSet<Domain.Entities.Email.FileAttachement> FileAttachements { get; set; }
        public DbSet<MailList> MailLists { get; set; }

        public DbSet<TicketRouting> TicketRoutings { get; set; }
        public DbSet<TicketAssignment> TicketAssignments { get; set; }
        


        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketActivity> TicketActivitys { get; set; }
        public DbSet<TicketChangeActivity> TicketChangeActivities { get; set; }

        public DbSet<TicketFileUpload> TicketFileUploads { get; set; }
        public DbSet<TicketGroupPolicy> TicketGroupPolics { get; set; }
        public DbSet<TicketMerged> TicketMergeds { get; set; }

        // ADMCPP

        public DbSet<ADMCPP> AdmCpps { get; set; }

        // IT Software Development

        public DbSet<ItappProjectApproval> ItappProjectApprovals { get; set; }
        public DbSet<ItappProjectDev> ItappProjectDevs { get; set; }
        public DbSet<ItappProjectInit> ItappProjectInits { get; set; }

        // Ticket

        public DbSet<TicketNotification> TicketNotifications { get; set; }

        // Queue

        public DbSet<FingerPrint> FingerPrints { get; set; }
        public DbSet<FingerPrintMachine> FingerPrintMachines { get; set; }

        // VAF

        public DbSet<Information> Informations { get; set; }
        public DbSet<Outline> Outlines { get; set; }

        //VR
        public DbSet<RequestData> RequestDatas { get; set; }

        //CCR
        public DbSet<ContractDraft> ContractDrafts { get; set; }

        //FAD
        public DbSet<AssetDisposal> AssetDisposals { get; set; }
        public DbSet<AssetDisposalDetail> AssetDisposalDetails { get; set; }
        public DbSet<AssetControlDetail> AssetControlDetails { get; set; }

        //ITEIRQ
        public DbSet<EventInternet> EventInternet { get; set; }
        public DbSet<Quotation> Quotation { get; set; }

        //FAT
        public DbSet<AssetTransfer> AssetTransfers { get; set; }
        public DbSet<AssetTransferDetail> AssetTransferDetails { get; set; }

        //RAC
        public DbSet<AccessCard> AccessCards { get; set; }
        //RAC
        public DbSet<AdditionalTimeWorked> AdditionalTimeWorkeds { get; set; }

        //EOMBP
        public DbSet<BestPerformance> BestPerformances { get; set; }
        public DbSet<BestPerformanceDetail> BestPerformanceDetails { get; set; }

        //TAS
        public DbSet<CourseEmployee> CourseEmployees { get; set; }
        public DbSet<CourseRegistration> CourseRegistrations { get; set; }

        //OSHA
        public DbSet<OSHAEmployee> OSHAEmployees { get; set; }
        public DbSet<OSHAInformation> OSHAInformations { get; set; }

        //ADMSR
        public DbSet<AdmsrCompany> AdmsrCompanys { get; set; }
        public DbSet<AdmsrInformation> AdmsrInformations { get; set; }

        //IT AD
        public DbSet<ITADEmployee> ITADEmployees { get; set; }

        //RMD
        public DbSet<RiskAssessment> RiskAssessments { get; set; }
        public DbSet<Worksheet1> Worksheet1 { get; set; }
        public DbSet<Worksheet2> Worksheet2 { get; set; }
        public DbSet<Worksheet3> Worksheet3 { get; set; }

        //JRAM
        public DbSet<RamClear> RamClear { get; set; }

        //GMU RAM
        public DbSet<GmuRamClear> GmuRamClear { get; set; }

        //HGVR
        public DbSet<VoucherHotel> VoucherHotel { get; set; }
        public DbSet<VoucherHotelDetail> VoucherHotelDetail { get; set; }
        public DbSet<VoucherHotelFinance> VoucherHotelFinance { get; set; }

        public DbSet<UserAuth> UserAuth { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new MenuConfiguration());
            modelBuilder.Configurations.Add(new ActivityConfiguration());

            modelBuilder.Configurations.Add(new AvbRequestItemConfiguration());
            modelBuilder.Configurations.Add(new AvbItemConfiguraiton());
            modelBuilder.Configurations.Add(new AvbJobHistoryConfiguration());
            modelBuilder.Configurations.Add(new AvbItemTypeConfiguration());

            /* Business Case Justification entity configuration */
            modelBuilder.Configurations.Add(new BCJConfig.ProjectDetailConfiguration());
            modelBuilder.Configurations.Add(new BCJConfig.RequestItemConfiguration());
            modelBuilder.Configurations.Add(new BCJConfig.AnalysisItemConfiguration());
            modelBuilder.Configurations.Add(new BCJConfig.CapexCategoryConfiguration());

            /* Reservation entity configuration */
            modelBuilder.Configurations.Add(new ReservationConfig.ComplimentaryConfiguration());
            modelBuilder.Configurations.Add(new ReservationConfig.GuestConfiguration());
            modelBuilder.Configurations.Add(new ReservationConfig.BookingConfiguration());
            modelBuilder.Configurations.Add(new ReservationConfig.RoomCategoryConfiguration());
            modelBuilder.Configurations.Add(new ReservationConfig.ComplimentaryItemConfiguration());
            modelBuilder.Configurations.Add(new ReservationConfig.ComplimentaryCheckItemConfiguration());

            /* Maintenance Work Order configuration */
            modelBuilder.Configurations.Add(new MWOConfig.ModeConfiguration());
            modelBuilder.Configurations.Add(new MWOConfig.RequestTypeConfiguration());
            modelBuilder.Configurations.Add(new MWOConfig.WorkTypeConfiguration());
            modelBuilder.Configurations.Add(new MWOConfig.DepartmentChargableConfiguration());
            modelBuilder.Configurations.Add(new MWOConfig.RequestInformationConfiguration());
            modelBuilder.Configurations.Add(new MWOConfig.MaintenanceDepartmentConfiguration());
            modelBuilder.Configurations.Add(new MWOConfig.InformationConfiguration());

            /* N2 Maintenance Work Order configuration */
            modelBuilder.Configurations.Add(new N2MWOConfig.N2ModeConfiguration());
            modelBuilder.Configurations.Add(new N2MWOConfig.N2RequestTypeConfiguration());
            modelBuilder.Configurations.Add(new N2MWOConfig.N2WorkTypeConfiguration());
            modelBuilder.Configurations.Add(new N2MWOConfig.N2DepartmentChargableConfiguration());
            modelBuilder.Configurations.Add(new N2MWOConfig.N2RequestInformationConfiguration());
            modelBuilder.Configurations.Add(new N2MWOConfig.N2MaintenanceDepartmentConfiguration());
            modelBuilder.Configurations.Add(new N2MWOConfig.N2InformationConfiguration());

            /* Human Resource schema configuration */
            modelBuilder.Configurations.Add(new HumanResourceConfig.RequisitionConfiguration());

            /* Event - Project Brief */
            modelBuilder.Configurations.Add(new PBFConfig.ProjectBriefConfiguration());
            modelBuilder.Configurations.Add(new PBFConfig.SpecificationConfiguration());


            modelBuilder.Configurations.Add(new LookupConfiguration());
            modelBuilder.Configurations.Add(new RequestHeaderConfiguration());
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new AttachementConfiguration());
            modelBuilder.Configurations.Add(new ActivityHistoryConfiguration());
            modelBuilder.Configurations.Add(new RequestUserConfiguration());
            modelBuilder.Configurations.Add(new DepartmentConfiguration());
            modelBuilder.Configurations.Add(new RequestItemConfiguration());
            modelBuilder.Configurations.Add(new ItemTypeConfiguration());
            modelBuilder.Configurations.Add(new ItemRoleConfiguration());
            modelBuilder.Configurations.Add(new ItemConfiguration());
            modelBuilder.Configurations.Add(new SessionConfiguration());
            modelBuilder.Configurations.Add(new ActivityRoleRightConfiguration());
            modelBuilder.Configurations.Add(new RihgtConfiguration());
            modelBuilder.Configurations.Add(new RoleRightConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());
            modelBuilder.Configurations.Add(new WorkflowRoleConfiguration());
            modelBuilder.Configurations.Add(new NotificationUserSessionConfiguration());
            modelBuilder.Configurations.Add(new CheckPointConfiguration());
            modelBuilder.Configurations.Add(new DeptAccessRightConfiguration());
            modelBuilder.Configurations.Add(new DeptApprovalRoleConfiguration());
            modelBuilder.Configurations.Add(new DeptHistoryConfiguration());
            modelBuilder.Configurations.Add(new EscalationConfiguration());
            modelBuilder.Configurations.Add(new RequestApplicationConfiguration());
            modelBuilder.Configurations.Add(new RequestEmployeeConfiguration());
            modelBuilder.Configurations.Add(new RequestExportedConfiguration());
            modelBuilder.Configurations.Add(new WorkflowRightConfiguration());
            modelBuilder.Configurations.Add(new FixDeptConfiguration());
            modelBuilder.Configurations.Add(new DeptGroupApprovalConfiguration());
            modelBuilder.Configurations.Add(new MtfFyiRoleConfiguration());
            modelBuilder.Configurations.Add(new AssetCategoryConfiguration());
            modelBuilder.Configurations.Add(new ExpenseAccountConfiguration());
            modelBuilder.Configurations.Add(new ExpenseClaimHeaderConfiguration());
            modelBuilder.Configurations.Add(new ExpenseClaimItemDetailConfiguration());
            modelBuilder.Configurations.Add(new AdGroupConfiguration());
            modelBuilder.Configurations.Add(new BackupEmployeeConfiguration());
            //modelBuilder.Configurations.Add(new MedicineConfiguration());
            //modelBuilder.Configurations.Add(new PrescriptionConfiguration());
            //modelBuilder.Configurations.Add(new TreatmentConfiguration());
            //modelBuilder.Configurations.Add(new UnfitToWorkConfiguration());
            modelBuilder.Configurations.Add(new EmployeeViewConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfig());

            //ticket
            modelBuilder.Configurations.Add(new TicketTypeConfiguration());
            modelBuilder.Configurations.Add(new TicketStatusConfiguration());
            modelBuilder.Configurations.Add(new TicketSourceConfiguration());
            modelBuilder.Configurations.Add(new TicketImpactConfiguration());
            modelBuilder.Configurations.Add(new TicketUrgencyConfiguration());
            modelBuilder.Configurations.Add(new TicketPriorityConfiguration());
            modelBuilder.Configurations.Add(new TicketSiteConfiguration());
            modelBuilder.Configurations.Add(new TicketSLAConfiguration());
            modelBuilder.Configurations.Add(new TicketSLAMappingConfiguration());
            modelBuilder.Configurations.Add(new TicketCategoryConfiguration());
            modelBuilder.Configurations.Add(new TicketSubCategoryConfiguration());
            modelBuilder.Configurations.Add(new TicketItemConfiguration());
            modelBuilder.Configurations.Add(new TicketDepartmentConfiguration());
            modelBuilder.Configurations.Add(new TicketTeamConfiguration());
            modelBuilder.Configurations.Add(new TicketAgentConfiguration());
            modelBuilder.Configurations.Add(new TicketSubTkLinkConfiguration());

            modelBuilder.Configurations.Add(new TravelDetailConfiguration());
            modelBuilder.Configurations.Add(new DestinationConfiguration());
            modelBuilder.Configurations.Add(new FlightDetailConfiguration());

            modelBuilder.Configurations.Add(new TicketNotificationConfiguration());

            modelBuilder.Configurations.Add(new TicketNoneReqEmpConfiguration());
            modelBuilder.Configurations.Add(new TicketFormIntegratedConfiguration());
            
            // AVDR & AVIR
            modelBuilder.Configurations.Add(new AvdrConfiguration());
            modelBuilder.Configurations.Add(new AvirConfiguration());

            modelBuilder.Configurations.Add(new JobConfiguration());
            modelBuilder.Configurations.Add(new EmailContentConfiguration());
            modelBuilder.Configurations.Add(new RecipientConfiguration());

            // Employee Of Month
            modelBuilder.Configurations.Add(new EOMConfig.EOMConfiguration());


            //Incident
            modelBuilder.Configurations.Add(new IncidentConfiguration());
            modelBuilder.Configurations.Add(new EmployeeListConfiguration());

            //Machine
            modelBuilder.Configurations.Add(new MachineConfiguration());
            modelBuilder.Configurations.Add(new MachineEmployeeListConfiguration());
            modelBuilder.Configurations.Add(new MachineIssueTypeConfiguration());

            //EGM - Attandance
            modelBuilder.Configurations.Add(new AttandanceConfiguration());
            modelBuilder.Configurations.Add(new AttandanceDetailTypeConfiguration());

            //Email
            modelBuilder.Configurations.Add(new EmailItemConfiguration());
            modelBuilder.Configurations.Add(new FileAttachementConfiguration());
            modelBuilder.Configurations.Add(new MailListConfiguration());
            modelBuilder.Configurations.Add(new TicketConfiguration());
            modelBuilder.Configurations.Add(new TicketActivityConfiguration());
            modelBuilder.Configurations.Add(new TicketRoutingConfiguration());
            modelBuilder.Configurations.Add(new TicketAssignmentConfiguration());
            modelBuilder.Configurations.Add(new TicketFileUploadConfiguration());
            modelBuilder.Configurations.Add(new TicketChangeActivityConfiguration());
            modelBuilder.Configurations.Add(new TicketGroupPolicyConfiguration());
            modelBuilder.Configurations.Add(new TicketMergedConfiguration());
            modelBuilder.Configurations.Add(new TicketTeamAgentAssignConfiguration());
            modelBuilder.Configurations.Add(new TicketGroupPolicyTeamAssignConfiguration());
            modelBuilder.Configurations.Add(new TicketGroupPolicyReportAssignConfiguration());            

            // ADMCPP
            modelBuilder.Configurations.Add(new ADMCPPConfiguration());

            // IT Software Development
            modelBuilder.Configurations.Add(new ITAppProjectApprovalConfiguration());
            modelBuilder.Configurations.Add(new ITAppProjectDevConfiguration());
            modelBuilder.Configurations.Add(new ITAppProjectInitConfiguration());

            // Queue
            modelBuilder.Configurations.Add(new FingerPrintConfiguration());
            modelBuilder.Configurations.Add(new FingerPrintMachineConfiguration());

            // VAF
            modelBuilder.Configurations.Add(new InformationConfiguration());        
            modelBuilder.Configurations.Add(new OutlineConfiguration());       
            
            // VR
            modelBuilder.Configurations.Add(new RequestDataConfiguration());

            // CCR
            modelBuilder.Configurations.Add(new ContractDraftConfiguration());

            // RAC
            modelBuilder.Configurations.Add(new AccessCardConfiguration());

            // OSHA
            modelBuilder.Configurations.Add(new Configuration.OSHA.InformationConfiguration());
            modelBuilder.Configurations.Add(new Configuration.OSHA.EmployeeConfiguration());

            // ADMSR
            modelBuilder.Configurations.Add(new AdmsrInformationConfiguration());
            modelBuilder.Configurations.Add(new AdmsrCompnayConfiguration());

            // IT AD
            modelBuilder.Configurations.Add(new ITADEmployeeConfiguration());

            // IT Change Request Form
            modelBuilder.Configurations.Add(new Configuration.ITCR.RequestFormDataConfiguration());

            // IT Item Repair Form
            modelBuilder.Configurations.Add(new IRFRequestItemConfiguration());
            modelBuilder.Configurations.Add(new IRFVendorConfiguration());
        }

        public static WorkflowContext Create()
        {
            if (workfFlowContext == null)
            {
                workfFlowContext = new WorkflowContext();
            }

            return workfFlowContext;
        }
    }
}
