using Abp.IdentityServer4;
using Abp.Organizations;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Contable.Authorization.Delegation;
using Contable.Authorization.Roles;
using Contable.Authorization.Users;
using Contable.Chat;
using Contable.Editions;
using Contable.Friendships;
using Contable.MultiTenancy;
using Contable.MultiTenancy.Accounting;
using Contable.MultiTenancy.Payments;
using Contable.Storage;
using Contable.Application;
using Contable.Application.Reporting.Dto;
using Abp.Application.Services.Dto;
using Contable.Application.Portal.Dto;

namespace Contable.EntityFrameworkCore
{
    public class ContableDbContext : AbpZeroDbContext<Tenant, Role, User, ContableDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        //Application
        public virtual DbSet<Department> Departments { get; set; }

        public virtual DbSet<Province> Provinces { get; set; }

        public virtual DbSet<District> Districts { get; set; }

        public virtual DbSet<ResponsibleActor> ResponsibleActors { get; set; }

        public virtual DbSet<ResponsibleSubActor> ResponsibleSubActors { get; set; }

        public virtual DbSet<Record> Record { get; set; }

        public virtual DbSet<RecordResource> RecordResources { get; set; }

        public virtual DbSet<SocialConflictLocation> SocialConflictLocations { get; set; }
		
        public virtual DbSet<SocialConflict> SocialConflicts { get; set; }

        public virtual DbSet<TerritorialUnit> TerritorialUnits { get; set; }

        public virtual DbSet<TerritorialUnitDepartment> TerritorialUnitDepartments { get; set; }

        public virtual DbSet<ParameterCategory> ParameterCategories { get; set; }

        public virtual DbSet<Parameter> Parameters { get; set; }

        public virtual DbSet<Compromise> Compromises { get; set; }

        public virtual DbSet<TaskManagement> TaskManagements { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<TaskManagemetExtend> TaskManagemetExtends { get; set; }

        public virtual DbSet<CompromiseLocation> CompromiseLocations { get; set; }

        public virtual DbSet<Situation> Situations { get; set; }

        public virtual DbSet<SituationResource> SituationResources { get; set; }

        public virtual DbSet<PIPMEF> PIPMEFs { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<CompromiseInvolved> CompromiseInvolveds { get; set; }

        //Portal Entities

        public virtual DbSet<PortalPipMefItemDto> PortalPipMef { get; set; }

        public virtual DbSet<ReportSummaryDto> ReportSummary { get; set; }

        public virtual DbSet<ReportStatusDto> ReportStatus { get; set; }

        public virtual DbSet<ReportResponsibleDto> ReportResponsible { get; set; }

        public virtual DbSet<EntityDto> EntityDtos { get; set; }

        public virtual DbSet<ReportSummaryStatusDto> ReportSummaryStatus { get; set; }

        public virtual DbSet<ReportResponsibleStatusDto> ReportResponsibleStatus { get; set; }

        #region Portar Social Conflict 

        public virtual DbSet<ReportSocialConflictRiskDto> ReportSocialConflictRisks { get; set; }

        public virtual DbSet<ReportSocialConflictGeographycTypeDto> ReportSocialConflictGeographycTypes { get; set; }

        public virtual DbSet<ReportSocialConflictLocationDto> ReportSocialConflictLocations { get; set; }

        #endregion

        #region Portar Social Conflict Alerts 

        public virtual DbSet<ReportSocialConflictAlertRiskDto> ReportSocialConflictAlertRisks { get; set; }

        public virtual DbSet<ReportSocialConflictAlertSectorDto> ReportSocialConflictAlertSectors { get; set; }

        public virtual DbSet<ReportSocialConflictAlertStateDto> ReportSocialConflictAlertStates { get; set; }

        public virtual DbSet<ReportSocialConflictAlertLocationDto> ReportSocialConflictAlertTerritorialUnits { get; set; }

        public virtual DbSet<ReportSocialConflictAlertTypologyDto> ReportSocialConflictAlertTypologies { get; set; }

        #endregion

        #region Portar Social Sensible 

        public virtual DbSet<ReportSocialConflictSensibleRiskDto> ReportSocialConflictSensibleRisks { get; set; }

        public virtual DbSet<ReportSocialConflictSensibleGeographycTypeDto> ReportSocialConflictSensibleGeographycTypes { get; set; }

        public virtual DbSet<ReportSocialConflictSensibleLocationDto> ReportSocialConflictSensibleLocations { get; set; }

        #endregion

        //Management
        public virtual DbSet<DinamicVariable> DinamicVariables { get; set; }

        public virtual DbSet<DinamicVariableDetail> DinamicVariableDetails { get; set; }

        public virtual DbSet<StaticVariable> StaticVariables { get; set; }

        public virtual DbSet<StaticVariableOption> StaticVariableOptions { get; set; }

        public virtual DbSet<StaticVariableOptionDetail> StaticVariableOptionDetails { get; set; }

        public virtual DbSet<ProspectiveRisk> ProspectiveRisks { get; set; }

        public virtual DbSet<ProspectiveRiskDetail> ProspectiveRiskDetails { get; set; }

        public virtual DbSet<Level> Levels { get; set; }

        public virtual DbSet<ProjectStage> ProjectStages { get; set; }

        public virtual DbSet<ProjectStageDetail> ProjectStageDetails { get; set; }

        public virtual DbSet<ProjectRisk> ProjectRisks { get; set; }

        public virtual DbSet<ProjectRiskDetail> ProjectRiskDetails { get; set; }

        public virtual DbSet<ProspectiveRiskHistory> ProspectiveRiskHistories { get; set; }

        public virtual DbSet<ProspectiveRiskHistoryDetail> ProspectiveRiskHistoryDetails { get; set; }

        public virtual DbSet<ProjectRiskHistory> ProjectRiskHistories { get; set; }

        public virtual DbSet<ProjectRiskHistoryDetail> ProjectRiskHistoryDetails { get; set; }

        public virtual DbSet<Risk> Risks { get; set; }

        public virtual DbSet<ActorType> ActorType { get; set; }

        public virtual DbSet<ActorMovement> ActorMovements { get; set; }

        public virtual DbSet<Typology> Typologies { get; set; }

        public virtual DbSet<SubTypology> SubTypologies { get; set; }

        public virtual DbSet<Fact> Facts { get; set; }

        public virtual DbSet<Sector> Sectors { get; set; }

        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<SocialConflictActor> SocialConflictActors { get; set; }

        public virtual DbSet<SocialConflictGeneralFact> SocialConflictGeneralFacts { get; set; }

        public virtual DbSet<SocialConflictSugerence> SocialConflictSugerences { get; set; }

        public virtual DbSet<Management> Managements { get; set; }

        public virtual DbSet<SocialConflictManagement> SocialConflictManagements { get; set; }

        public virtual DbSet<SocialConflictManagementResource> SocialConflictManagementResources { get; set; }

        public virtual DbSet<SocialConflictState> SocialConflictStates { get; set; }

        public virtual DbSet<SocialConflictViolenceFact> SocialConflictViolenceFacts { get; set; }

        public virtual DbSet<SocialConflictViolenceFactLocation> SocialConflictViolenceFactLocations { get; set; }

        public virtual DbSet<SocialConflictRisk> SocialConflictRisks { get; set; }

        public virtual DbSet<SocialConflictCondition> SocialConflictConditions { get; set; }

        public virtual DbSet<SocialConflictAlert> SocialConflictAlerts { get; set; }

        public virtual DbSet<SocialConflictAlertLocation> SocialConflictAlertLocations { get; set; }

        public virtual DbSet<SocialConflictAlertRisk> SocialConflictAlertRisks { get; set; }

        public virtual DbSet<SocialConflictAlertSector> SocialConflictAlertSectors { get; set; }

        public virtual DbSet<SocialConflictAlertState> SocialConflictAlertStates { get; set; }

        public virtual DbSet<SocialConflictAlertSeal> SocialConflictAlertSeals { get; set; }
        
        public virtual DbSet<AlertRisk> AlertRisks { get; set; }

        public virtual DbSet<AlertSector> AlertSectors { get; set; }

        public virtual DbSet<AlertSeal> AlertSeals { get; set; }

        public virtual DbSet<AlertDemand> AlertDemands { get; set; }

        public virtual DbSet<AlertResponsible> AlertResponsibles { get; set; }

        public virtual DbSet<Region> Regions { get; set; }

        public virtual DbSet<SocialConflictUser> SocialConflictUsers { get; set; }

        public virtual DbSet<SocialConflictResource> SocialConflictResources { get; set; }

        public virtual DbSet<SocialConflictNote> SocialConflictNotes { get; set; }

        public virtual DbSet<SocialConflictAlertResource> SocialConflictAlertResources { get; set; }

        public virtual DbSet<SocialConflictSensible> SocialConflictSensibles { get; set; }

        public virtual DbSet<SocialConflictSensibleCondition> SocialConflictSensibleConditions { get; set; }

        public virtual DbSet<SocialConflictSensibleGeneralFact> SocialConflictSensibleGeneralFacts { get; set; }

        public virtual DbSet<SocialConflictSensibleLocation> SocialConflictSensibleLocations { get; set; }

        public virtual DbSet<SocialConflictSensibleManagement> SocialConflictSensibleManagements { get; set; }

        public virtual DbSet<SocialConflictSensibleManagementResource> SocialConflictSensibleManagementResources { get; set; }

        public virtual DbSet<SocialConflictSensibleRisk> SocialConflictSensibleRisks { get; set; }

        public virtual DbSet<SocialConflictSensibleState> SocialConflictSensibleStates { get; set; }

        public virtual DbSet<SocialConflictSensibleSugerence> SocialConflictSensibleSugerences { get; set; }

        public virtual DbSet<SocialConflictAlertHistory> Histories { get; set; }

        public virtual DbSet<Report> Reports { get; set; }

        public virtual DbSet<TaskManagementPerson> TaskManagementPersons { get; set; }

        public virtual DbSet<DirectoryGovernment> DirectoryGovernments { get; set; }

        public virtual DbSet<DirectoryGovernmentSector> DirectoryGovernmentSectors { get; set; }

        public virtual DbSet<DirectorySector> DirectorySectors { get; set; }

        public virtual DbSet<DirectoryResponsible> DirectoryResponsibles { get; set; }

        public virtual DbSet<DirectoryDialog> DirectoryDialogs { get; set; }

        public virtual DbSet<DirectoryIndustry> DirectoryIndustries { get; set; }

        public virtual DbSet<SocialConflictTaskManagement> SocialConflictTaskManagements { get; set; }

        public virtual DbSet<SocialConflictTaskManagementComment> SocialConflictTaskManagementComments { get; set; }

        public virtual DbSet<SocialConflictTaskManagementPerson> SocialConflictTaskManagementPersons { get; set; }

        public virtual DbSet<SocialConflictTaskManagementExtend> SocialConflictTaskManagemetExtends { get; set; }

        public virtual DbSet<TerritorialUnitCoordinator> TerritorialUnitCoordinators { get; set; }

        public virtual DbSet<DirectoryConflictType> DirectoryConflictTypes { get; set; }

        public virtual DbSet<DirectoryGovernmentLevel> DirectoryGovernmentLevels { get; set; }

        public virtual DbSet<HelpMemory> HelpMemories { get; set; }

        public virtual DbSet<HelpMemoryResource> HelpMemoryResources { get; set; }

        public virtual DbSet<SocialConflictTaskManagementResource> SocialConflictTaskManagementResources { get; set; }

        public virtual DbSet<SocialConflictVerificationHistory> SocialConflictVerificationHistories { get; set; }

        public virtual DbSet<SocialConflictSensibleVerificationHistory> SocialConflictSensibleVerificationHistories { get; set; }
        
        public virtual DbSet<SocialConflictSensibleNote> SocialConflictSensibleNotes { get; set; }

        public virtual DbSet<SocialConflictSensibleResource> SocialConflictSensibleResources { get; set; }

        public virtual DbSet<SocialConflictTaskManagementHistory> SocialConflictTaskManagementHistories { get; set; }

        public virtual DbSet<TaskManagementHistory> TaskManagementHistories { get; set; }
        
        public virtual DbSet<CompromiseTimeLine> CompromiseTimeLines { get; set; }

        public virtual DbSet<InterventionPlan> InterventionPlans { get; set; }

        public virtual DbSet<InterventionPlanLocation> InterventionPlanLocations { get; set; }

        public virtual DbSet<InterventionPlanActor> InterventionPlanActors { get; set; }

        public virtual DbSet<InterventionPlanState> InterventionPlanStates { get; set; }

        public virtual DbSet<InterventionPlanRisk> InterventionPlanRisks { get; set; }

        public virtual DbSet<InterventionPlanMethodology> InterventionPlanMethodologys { get; set; }

        public virtual DbSet<InterventionPlanOption> InterventionPlanOptions { get; set; }

        public virtual DbSet<InterventionPlanSolution> InterventionPlanSolution { get; set; }

        public virtual DbSet<InterventionPlanActivity> InterventionPlanActivities { get; set; }

        public virtual DbSet<InterventionPlanEntity> InterventionPlanEntities { get; set; }

        public virtual DbSet<InterventionPlanSchedule> InterventionPlanSchedules { get; set; }       

        public virtual DbSet<InterventionPlanRole> InterventionPlanRoles { get; set; }

        public virtual DbSet<InterventionPlanTeam> InterventionPlanTeams { get; set; }

        public virtual DbSet<CrisisCommittee> CrisisCommittees { get; set; }

        public virtual DbSet<CrisisCommitteeTeam> CrisisCommitteeTeams { get; set; }

        public virtual DbSet<CrisisCommitteeJob> CrisisCommitteeJobs { get; set; }

        public virtual DbSet<CrisisCommitteePlan> CrisisCommitteePlans { get; set; }

        public virtual DbSet<CrisisCommitteeAction> CrisisCommitteeActions { get; set; }

        public virtual DbSet<CrisisCommitteeMessage> CrisisCommitteeMessages { get; set; }

        public virtual DbSet<CrisisCommitteeChannel> CrisisCommitteeChannels { get; set; }

        public virtual DbSet<CrisisCommitteeSector> CrisisCommitteeSectors { get; set; }

        public virtual DbSet<CrisisCommitteeTask> CrisisCommitteeTasks { get; set; }

        public virtual DbSet<CrisisCommitteeAgreement> CrisisCommitteeAgreements { get; set; }

        public virtual DbSet<ResponsibleType> ResponsibleTypes { get; set; }

        public virtual DbSet<ResponsibleSubType> ResponsibleSubTypes { get; set; }

        public virtual DbSet<CompromiseLabel> CompromiseLabels { get; set; }

        public virtual DbSet<DirectoryGovernmentType> DirectoryGovernmentTypes { get; set; }

        public virtual DbSet<QuizState> QuizStates { get; set; }

        public virtual DbSet<QuizForm> QuizForms { get; set; }

        public virtual DbSet<QuizFormOption> QuizFormOptions { get; set; }

        public virtual DbSet<QuizComplete> QuizCompletes { get; set; }

        public virtual DbSet<QuizCompleteForm> QuizCompleteForms { get; set; }

        public virtual DbSet<QuizCompleteOption> QuizCompleteOptions { get; set; }

        public virtual DbSet<QuizCompleteResource> QuizCompleteResources { get; set; }

        public virtual DbSet<RecordResourceType> RecordResourceTypes { get; set; }

        public virtual DbSet<CompromiseResponsible> CompromiseResponsibles { get; set; }

        public virtual DbSet<CompromiseState> CompromiseStates { get; set; }

        public virtual DbSet<CompromiseSubState> CompromiseSubStates { get; set; }

        public virtual DbSet<SectorMeet> SectorMeets { get; set; }

        public virtual DbSet<SectorMeetSession> SectorMeetSessions { get; set; }

        public virtual DbSet<SectorMeetSessionAction> SectorMeetSessionActions { get; set; }

        public virtual DbSet<SectorMeetSessionAgreement> SectorMeetSessionAgreements { get; set; }

        public virtual DbSet<SectorMeetSessionCriticalAspect> SectorMeetSessionCriticalAspects { get; set; }

        public virtual DbSet<SectorMeetSessionSchedule> SectorMeetSessionSchedules { get; set; }

        public virtual DbSet<SectorMeetSessionSummary> SectorMeetSessionSummaries { get; set; }

        public virtual DbSet<SectorMeetSessionResource> SectorMeetSessionResources { get; set; }

        public virtual DbSet<SectorMeetSessionLeader> SectorMeetSessionLeaders { get; set; }

        public virtual DbSet<SectorMeetSessionTeam> SectorMeetSessionTeams { get; set; }

        public virtual DbSet<DialogSpace> DialogSpaces { get; set; }

        public virtual DbSet<DialogSpaceLocation> DialogSpaceLocations { get; set; }

        public virtual DbSet<DialogSpaceLeader> DialogSpaceLeaders { get; set; }

        public virtual DbSet<DialogSpaceTeam> DialogSpaceTeams { get; set; }

        public virtual DbSet<DialogSpaceDocument> DialogSpaceDocuments { get; set; }

        public virtual DbSet<DialogSpaceDocumentType> DialogSpaceDocumentTypes { get; set; }

        public virtual DbSet<DialogSpaceHoliday> DialogSpaceHolidays { get; set; }

        public virtual DbSet<DialogSpaceType> DialogSpaceTypes { get; set; }

        public virtual DbSet<DialogSpaceDocumentSituation> DialogSpaceDocumentSituationes { get; set; }

        public virtual DbSet<DialogSpaceDocumentResource> DialogSpaceDocumentResources { get; set; }

        public ContableDbContext(DbContextOptions<ContableDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique();
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });

            modelBuilder.Entity<DinamicVariableDetail>(b =>
            {
                b.HasIndex(e => new { e.DinamicVariableId, e.ProvinceId });
            });

            modelBuilder.Entity<ProspectiveRisk>(b =>
            {
                b.HasIndex(e => new { e.ProvinceId });
            });

            modelBuilder.Entity<ProspectiveRiskDetail>(b =>
            {
                b.HasIndex(e => new { e.ProspectiveRiskId, e.StaticVariableOptionId });
            });

            modelBuilder.Entity<ProjectRiskDetail>(b =>
            {
                b.HasIndex(e => new { e.ProjectRiskId, e.ProjectStageDetailId, e.StaticVariableOptionId });
            });

            modelBuilder.Entity<ProspectiveRiskHistoryDetail>(b =>
            {
                b.HasIndex(e => new { e.ProspectiveRiskHistoryId, e.StaticVariableOptionId });
            });

            modelBuilder.Entity<ProjectRiskHistoryDetail>(b =>
            {
                b.HasIndex(e => new { e.ProjectRiskHistoryId, e.ProjectStageDetailId, e.StaticVariableOptionId });
            });

            modelBuilder.Entity<EntityDto>().HasIndex(b => new { b.Id });

            modelBuilder.Entity<ReportSummaryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ReportStatusDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ReportResponsibleDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ReportSummaryStatusDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ReportResponsibleStatusDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<PortalPipMefItemDto>().HasNoKey().ToView(null);

            //Portal Social Conflict

            modelBuilder.Entity<ReportSocialConflictRiskDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ReportSocialConflictLocationDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ReportSocialConflictGeographycTypeDto>().HasNoKey().ToView(null);

            //Portal Social Conflict Alerts

            modelBuilder.Entity<ReportSocialConflictAlertRiskDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ReportSocialConflictAlertSectorDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ReportSocialConflictAlertStateDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ReportSocialConflictAlertLocationDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ReportSocialConflictAlertTypologyDto>().HasNoKey().ToView(null);

            //Portal Social Conflict Sensible

            modelBuilder.Entity<ReportSocialConflictSensibleRiskDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ReportSocialConflictSensibleGeographycTypeDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ReportSocialConflictSensibleLocationDto>().HasNoKey().ToView(null);

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}

