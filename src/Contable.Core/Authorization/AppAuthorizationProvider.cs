using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Contable.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Conflict, L("UserConflicts"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Lock, L("LockUser"));
            
            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            //TENANT-SPECIFIC PERMISSIONS

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            //Maintenance
            var maintenance = pages.CreateChildPermission(AppPermissions.Pages_Maintenance, L("Maintenance"), multiTenancySides: MultiTenancySides.Tenant);

            var ubigeos = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_Ubigeo, L("Ubigeos"), multiTenancySides: MultiTenancySides.Tenant);
            ubigeos.CreateChildPermission(AppPermissions.Pages_Maintenance_Ubigeo_Create, L("CreatingNewUbigeo"), multiTenancySides: MultiTenancySides.Tenant);
            ubigeos.CreateChildPermission(AppPermissions.Pages_Maintenance_Ubigeo_Edit, L("EditingUbigeos"), multiTenancySides: MultiTenancySides.Tenant);
            ubigeos.CreateChildPermission(AppPermissions.Pages_Maintenance_Ubigeo_Delete, L("DeletingUbigeos"), multiTenancySides: MultiTenancySides.Tenant);

            var responsibleActors = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_Responsible_Actor, L("ResponsibleActor"), multiTenancySides: MultiTenancySides.Tenant);
            responsibleActors.CreateChildPermission(AppPermissions.Pages_Maintenance_Responsible_Actor_Create, L("CreatingNewResponsibleActor"), multiTenancySides: MultiTenancySides.Tenant);
            responsibleActors.CreateChildPermission(AppPermissions.Pages_Maintenance_Responsible_Actor_Edit, L("EditingResponsibleActor"), multiTenancySides: MultiTenancySides.Tenant);
            responsibleActors.CreateChildPermission(AppPermissions.Pages_Maintenance_Responsible_Actor_Delete, L("DeletingResponsibleActor"), multiTenancySides: MultiTenancySides.Tenant);

            var responsibleSubActors = responsibleActors.CreateChildPermission(AppPermissions.Pages_Maintenance_Responsible_Actor_SubActor, L("ResponsibleSubActor"), multiTenancySides: MultiTenancySides.Tenant);
            responsibleSubActors.CreateChildPermission(AppPermissions.Pages_Maintenance_Responsible_Actor_SubActor_Create, L("CreatingNewResponsibleSubActor"), multiTenancySides: MultiTenancySides.Tenant);
            responsibleSubActors.CreateChildPermission(AppPermissions.Pages_Maintenance_Responsible_Actor_SubActor_Edit, L("EditingResponsibleSubActor"), multiTenancySides: MultiTenancySides.Tenant);
            responsibleSubActors.CreateChildPermission(AppPermissions.Pages_Maintenance_Responsible_Actor_SubActor_Delete, L("DeletingResponsibleSubActor"), multiTenancySides: MultiTenancySides.Tenant);

            var territorialUnits = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_TerritorialUnit, L("TerritorialUnits"), multiTenancySides: MultiTenancySides.Tenant);
            territorialUnits.CreateChildPermission(AppPermissions.Pages_Maintenance_TerritorialUnit_Create, L("CreatingNewTerritorialUnit"), multiTenancySides: MultiTenancySides.Tenant);
            territorialUnits.CreateChildPermission(AppPermissions.Pages_Maintenance_TerritorialUnit_Edit, L("EditingTerritorialUnit"), multiTenancySides: MultiTenancySides.Tenant);
            territorialUnits.CreateChildPermission(AppPermissions.Pages_Maintenance_TerritorialUnit_Delete, L("DeletingTerritorialUnit"), multiTenancySides: MultiTenancySides.Tenant);

            var actorTypes = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_ActorType, L("SocialConflictActorType"), multiTenancySides: MultiTenancySides.Tenant);
            actorTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_ActorType_Create, L("CreatingNewSocialConflictActorType"), multiTenancySides: MultiTenancySides.Tenant);
            actorTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_ActorType_Edit, L("EditingSocialConflictActorType"), multiTenancySides: MultiTenancySides.Tenant);
            actorTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_ActorType_Delete, L("DeletingSocialConflictActorType"), multiTenancySides: MultiTenancySides.Tenant);

            var actorMovements = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_ActorMovement, L("SocialConflictActorMovement"), multiTenancySides: MultiTenancySides.Tenant);
            actorMovements.CreateChildPermission(AppPermissions.Pages_Maintenance_ActorMovement_Create, L("CreatingNewSocialConflictActorMovement"), multiTenancySides: MultiTenancySides.Tenant);
            actorMovements.CreateChildPermission(AppPermissions.Pages_Maintenance_ActorMovement_Edit, L("EditingSocialConflictActorMovement"), multiTenancySides: MultiTenancySides.Tenant);
            actorMovements.CreateChildPermission(AppPermissions.Pages_Maintenance_ActorMovement_Delete, L("DeletingSocialConflictActorMovement"), multiTenancySides: MultiTenancySides.Tenant);

            var typology = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_Typology, L("SocialConflictTypology"), multiTenancySides: MultiTenancySides.Tenant);
            typology.CreateChildPermission(AppPermissions.Pages_Maintenance_Typology_Create, L("CreatingNewSocialConflictTypology"), multiTenancySides: MultiTenancySides.Tenant);
            typology.CreateChildPermission(AppPermissions.Pages_Maintenance_Typology_Edit, L("EditingSocialConflictTypology"), multiTenancySides: MultiTenancySides.Tenant);
            typology.CreateChildPermission(AppPermissions.Pages_Maintenance_Typology_Delete, L("DeletingSocialConflictTypology"), multiTenancySides: MultiTenancySides.Tenant);

            var subTypology = typology.CreateChildPermission(AppPermissions.Pages_Maintenance_Typology_SubTypology, L("SocialConflictSubTypology"), multiTenancySides: MultiTenancySides.Tenant);
            subTypology.CreateChildPermission(AppPermissions.Pages_Maintenance_Typology_SubTypology_Create, L("CreatingNewSocialConflictSubTypology"), multiTenancySides: MultiTenancySides.Tenant);
            subTypology.CreateChildPermission(AppPermissions.Pages_Maintenance_Typology_SubTypology_Edit, L("EditingSocialConflictSubTypology"), multiTenancySides: MultiTenancySides.Tenant);
            subTypology.CreateChildPermission(AppPermissions.Pages_Maintenance_Typology_SubTypology_Delete, L("DeletingSocialConflictSubTypology"), multiTenancySides: MultiTenancySides.Tenant);
            
            var risks = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_Risk, L("SocialConflictRisk"), multiTenancySides: MultiTenancySides.Tenant);
            risks.CreateChildPermission(AppPermissions.Pages_Maintenance_Risk_Create, L("CreatingNewSocialConflictRisk"), multiTenancySides: MultiTenancySides.Tenant);
            risks.CreateChildPermission(AppPermissions.Pages_Maintenance_Risk_Edit, L("EditingSocialConflictRisk"), multiTenancySides: MultiTenancySides.Tenant);
            risks.CreateChildPermission(AppPermissions.Pages_Maintenance_Risk_Delete, L("DeletingSocialConflictRisk"), multiTenancySides: MultiTenancySides.Tenant);

            var sector = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_Sector, L("SocialConflictSector"), multiTenancySides: MultiTenancySides.Tenant);
            sector.CreateChildPermission(AppPermissions.Pages_Maintenance_Sector_Create, L("CreatingNewSocialConflictSector"), multiTenancySides: MultiTenancySides.Tenant);
            sector.CreateChildPermission(AppPermissions.Pages_Maintenance_Sector_Edit, L("EditingSocialSocialConflictSector"), multiTenancySides: MultiTenancySides.Tenant);
            sector.CreateChildPermission(AppPermissions.Pages_Maintenance_Sector_Delete, L("DeletingSocialConflictSector"), multiTenancySides: MultiTenancySides.Tenant);

            var facts = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_Fact, L("SocialConflictFact"), multiTenancySides: MultiTenancySides.Tenant);
            facts.CreateChildPermission(AppPermissions.Pages_Maintenance_Fact_Create, L("CreatingNewSocialConflictFact"), multiTenancySides: MultiTenancySides.Tenant);
            facts.CreateChildPermission(AppPermissions.Pages_Maintenance_Fact_Edit, L("EditingSocialConflictFact"), multiTenancySides: MultiTenancySides.Tenant);
            facts.CreateChildPermission(AppPermissions.Pages_Maintenance_Fact_Delete, L("DeletingSocialConflictFact"), multiTenancySides: MultiTenancySides.Tenant);

            var coordinators = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_Coordinator, L("SocialConflictCoordinator"), multiTenancySides: MultiTenancySides.Tenant);
            coordinators.CreateChildPermission(AppPermissions.Pages_Maintenance_Coordinator_Create, L("CreatingNewSocialConflictCoordinator"), multiTenancySides: MultiTenancySides.Tenant);
            coordinators.CreateChildPermission(AppPermissions.Pages_Maintenance_Coordinator_Edit, L("EditingSocialConflictCoordinator"), multiTenancySides: MultiTenancySides.Tenant);
            coordinators.CreateChildPermission(AppPermissions.Pages_Maintenance_Coordinator_Delete, L("DeletingSocialConflictCoordinator"), multiTenancySides: MultiTenancySides.Tenant);

            var managers = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_Manager, L("SocialConflictManager"), multiTenancySides: MultiTenancySides.Tenant);
            managers.CreateChildPermission(AppPermissions.Pages_Maintenance_Manager_Create, L("CreatingNewSocialConflictManager"), multiTenancySides: MultiTenancySides.Tenant);
            managers.CreateChildPermission(AppPermissions.Pages_Maintenance_Manager_Edit, L("EditingSocialConflictManager"), multiTenancySides: MultiTenancySides.Tenant);
            managers.CreateChildPermission(AppPermissions.Pages_Maintenance_Manager_Delete, L("DeletingSocialConflictManager"), multiTenancySides: MultiTenancySides.Tenant);

            var managements = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_Management, L("SocialConflictManagement"), multiTenancySides: MultiTenancySides.Tenant);
            managements.CreateChildPermission(AppPermissions.Pages_Maintenance_Management_Create, L("CreatingNewSocialConflictManagement"), multiTenancySides: MultiTenancySides.Tenant);
            managements.CreateChildPermission(AppPermissions.Pages_Maintenance_Management_Edit, L("EditingSocialConflictManagement"), multiTenancySides: MultiTenancySides.Tenant);
            managements.CreateChildPermission(AppPermissions.Pages_Maintenance_Management_Delete, L("DeletingSocialConflictManagement"), multiTenancySides: MultiTenancySides.Tenant);

            var analysts = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_Analyst, L("SocialConflictAnalyst"), multiTenancySides: MultiTenancySides.Tenant);
            analysts.CreateChildPermission(AppPermissions.Pages_Maintenance_Analyst_Create, L("CreatingNewSocialConflictAnalyst"), multiTenancySides: MultiTenancySides.Tenant);
            analysts.CreateChildPermission(AppPermissions.Pages_Maintenance_Analyst_Edit, L("EditingSocialConflictAnalyst"), multiTenancySides: MultiTenancySides.Tenant);
            analysts.CreateChildPermission(AppPermissions.Pages_Maintenance_Analyst_Delete, L("DeletingSocialConflictAnalyst"), multiTenancySides: MultiTenancySides.Tenant);

            var alertRisks = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertRisk, L("SocialConflictAlertRisk"), multiTenancySides: MultiTenancySides.Tenant);
            alertRisks.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertRisk_Create, L("CreatingNewSocialConflictAlertRisk"), multiTenancySides: MultiTenancySides.Tenant);
            alertRisks.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertRisk_Edit, L("EditingSocialConflictAlertRisk"), multiTenancySides: MultiTenancySides.Tenant);
            alertRisks.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertRisk_Delete, L("DeletingSocialConflictAlertRisk"), multiTenancySides: MultiTenancySides.Tenant);

            var alertSectors = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertSector, L("SocialConflictAlertSector"), multiTenancySides: MultiTenancySides.Tenant);
            alertSectors.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertSector_Create, L("CreatingNewSocialConflictAlertSector"), multiTenancySides: MultiTenancySides.Tenant);
            alertSectors.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertSector_Edit, L("EditingSocialSocialConflictAlertSector"), multiTenancySides: MultiTenancySides.Tenant);
            alertSectors.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertSector_Delete, L("DeletingSocialConflictAlertSector"), multiTenancySides: MultiTenancySides.Tenant);

            var alertSeals = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertSeal, L("SocialConflictAlertSeal"), multiTenancySides: MultiTenancySides.Tenant);
            alertSeals.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertSeal_Create, L("CreatingNewSocialConflictAlertSeal"), multiTenancySides: MultiTenancySides.Tenant);
            alertSeals.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertSeal_Edit, L("EditingSocialSocialConflictAlertSeal"), multiTenancySides: MultiTenancySides.Tenant);
            alertSeals.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertSeal_Delete, L("DeletingSocialConflictAlertSeal"), multiTenancySides: MultiTenancySides.Tenant);

            var alertDemands = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertDemand, L("SocialConflictAlertDemand"), multiTenancySides: MultiTenancySides.Tenant);
            alertDemands.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertDemand_Create, L("CreatingNewSocialConflictAlertDemand"), multiTenancySides: MultiTenancySides.Tenant);
            alertDemands.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertDemand_Edit, L("EditingSocialSocialConflictAlertDemand"), multiTenancySides: MultiTenancySides.Tenant);
            alertDemands.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertDemand_Delete, L("DeletingSocialConflictAlertDemand"), multiTenancySides: MultiTenancySides.Tenant);

            var alertResponsibles = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertResponsible, L("SocialConflictAlertResponsible"), multiTenancySides: MultiTenancySides.Tenant);
            alertResponsibles.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertResponsible_Create, L("CreatingNewSocialConflictAlertResponsible"), multiTenancySides: MultiTenancySides.Tenant);
            alertResponsibles.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertResponsible_Edit, L("EditingSocialSocialConflictAlertResponsible"), multiTenancySides: MultiTenancySides.Tenant);
            alertResponsibles.CreateChildPermission(AppPermissions.Pages_Maintenance_AlertResponsible_Delete, L("DeletingSocialConflictAlertResponsible"), multiTenancySides: MultiTenancySides.Tenant);

            var directoryGovernmentSectors = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryGovernmentSector, L("DirectoryGovernmentSector"), multiTenancySides: MultiTenancySides.Tenant);
            directoryGovernmentSectors.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryGovernmentSector_Create, L("CreatingNewDirectoryGovernmentSector"), multiTenancySides: MultiTenancySides.Tenant);
            directoryGovernmentSectors.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryGovernmentSector_Edit, L("EditingDirectoryGovernmentSector"), multiTenancySides: MultiTenancySides.Tenant);
            directoryGovernmentSectors.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryGovernmentSector_Delete, L("DeletingDirectoryGovernmentSector"), multiTenancySides: MultiTenancySides.Tenant);

            var directorySectors = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectorySector, L("DirectorySectors"), multiTenancySides: MultiTenancySides.Tenant);
            directorySectors.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectorySector_Create, L("CreatingNewDirectorySector"), multiTenancySides: MultiTenancySides.Tenant);
            directorySectors.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectorySector_Edit, L("EditingDirectorySector"), multiTenancySides: MultiTenancySides.Tenant);
            directorySectors.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectorySector_Delete, L("DeletingDirectorySector"), multiTenancySides: MultiTenancySides.Tenant);

            var directoryConflictTypes = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryConflictType, L("DirectoryConflictTypes"), multiTenancySides: MultiTenancySides.Tenant);
            directoryConflictTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryConflictType_Create, L("CreatingNewDirectoryConflictType"), multiTenancySides: MultiTenancySides.Tenant);
            directoryConflictTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryConflictType_Edit, L("EditingDirectoryConflictType"), multiTenancySides: MultiTenancySides.Tenant);
            directoryConflictTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryConflictType_Delete, L("DeletingDirectoryConflictType"), multiTenancySides: MultiTenancySides.Tenant);

            var directoryGovernmentLevels = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryGovernmentLevel, L("DirectoryGovernmentLevels"), multiTenancySides: MultiTenancySides.Tenant);
            directoryGovernmentLevels.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryGovernmentLevel_Create, L("CreatingNewDirectoryGovernmentLevel"), multiTenancySides: MultiTenancySides.Tenant);
            directoryGovernmentLevels.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryGovernmentLevel_Edit, L("EditingDirectoryGovernmentLevel"), multiTenancySides: MultiTenancySides.Tenant);
            directoryGovernmentLevels.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryGovernmentLevel_Delete, L("DeletingDirectoryGovernmentLevel"), multiTenancySides: MultiTenancySides.Tenant);
            
            var directoryGovernmentTypes = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryGovernmentType, L("DirectoryGovernmentTypes"), multiTenancySides: MultiTenancySides.Tenant);
            directoryGovernmentTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryGovernmentType_Create, L("CreatingNewDirectoryGovernmentType"), multiTenancySides: MultiTenancySides.Tenant);
            directoryGovernmentTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryGovernmentType_Edit, L("EditingDirectoryGovernmentType"), multiTenancySides: MultiTenancySides.Tenant);
            directoryGovernmentTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryGovernmentType_Delete, L("DeletingDirectoryGovernmentType"), multiTenancySides: MultiTenancySides.Tenant);

            var compromisePhases = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_Phase, L("CompromisePhases"), multiTenancySides: MultiTenancySides.Tenant);
            compromisePhases.CreateChildPermission(AppPermissions.Pages_Maintenance_Phase_Create, L("CreatingNewCompromisePhase"), multiTenancySides: MultiTenancySides.Tenant);
            compromisePhases.CreateChildPermission(AppPermissions.Pages_Maintenance_Phase_Edit, L("EditingCompromisePhase"), multiTenancySides: MultiTenancySides.Tenant);
            compromisePhases.CreateChildPermission(AppPermissions.Pages_Maintenance_Phase_Delete, L("DeletingCompromisePhase"), multiTenancySides: MultiTenancySides.Tenant);

            var interventionPlanOptions = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanOption, L("InterventionPlanOptions"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlanOptions.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanOption_Create, L("CreatingNewInterventionPlanOptions"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlanOptions.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanOption_Edit, L("EditingInterventionPlanOptions"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlanOptions.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanOption_Delete, L("DeletingInterventionPlanOptions"), multiTenancySides: MultiTenancySides.Tenant);

            var interventionPlanActivities = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanActivity, L("InterventionPlanActivities"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlanActivities.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanActivity_Create, L("CreatingNewInterventionPlanActivities"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlanActivities.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanActivity_Edit, L("EditingInterventionPlanActivities"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlanActivities.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanActivity_Delete, L("DeletingInterventionPlanActivities"), multiTenancySides: MultiTenancySides.Tenant);

            var interventionPlanEntities = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanEntity, L("InterventionPlanEntities"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlanEntities.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanEntity_Create, L("CreatingNewInterventionPlanEntities"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlanEntities.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanEntity_Edit, L("EditingInterventionPlanEntities"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlanEntities.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanEntity_Delete, L("DeletingInterventionPlanEntities"), multiTenancySides: MultiTenancySides.Tenant);

            var interventionPlanRoles = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanRole, L("InterventionPlanRoles"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlanRoles.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanRole_Create, L("CreatingNewInterventionPlanRoles"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlanRoles.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanRole_Edit, L("EditingInterventionPlanRoles"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlanRoles.CreateChildPermission(AppPermissions.Pages_Maintenance_InterventionPlanRole_Delete, L("DeletingInterventionPlanRoles"), multiTenancySides: MultiTenancySides.Tenant);

            var crisisCommitteeJos = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_CrisisCommitteeJob, L("CrisisCommitteeJobs"), multiTenancySides: MultiTenancySides.Tenant);
            crisisCommitteeJos.CreateChildPermission(AppPermissions.Pages_Maintenance_CrisisCommitteeJob_Create, L("CreatingNewCrisisCommitteeJobs"), multiTenancySides: MultiTenancySides.Tenant);
            crisisCommitteeJos.CreateChildPermission(AppPermissions.Pages_Maintenance_CrisisCommitteeJob_Edit, L("EditingCrisisCommitteeJobs"), multiTenancySides: MultiTenancySides.Tenant);
            crisisCommitteeJos.CreateChildPermission(AppPermissions.Pages_Maintenance_CrisisCommitteeJob_Delete, L("DeletingCrisisCommitteeJobs"), multiTenancySides: MultiTenancySides.Tenant);

            var responsibleTypes = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_ResponsibleType, L("ResponsibleTypes"), multiTenancySides: MultiTenancySides.Tenant);
            responsibleTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_ResponsibleType_Create, L("CreatingNewResponsibleTypes"), multiTenancySides: MultiTenancySides.Tenant);
            responsibleTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_ResponsibleType_Edit, L("EditingResponsibleTypes"), multiTenancySides: MultiTenancySides.Tenant);
            responsibleTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_ResponsibleType_Delete, L("DeletingResponsibleTypes"), multiTenancySides: MultiTenancySides.Tenant);

            var compromiseLabels = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_CompromiseLabel, L("CompromiseLabels"), multiTenancySides: MultiTenancySides.Tenant);
            compromiseLabels.CreateChildPermission(AppPermissions.Pages_Maintenance_CompromiseLabel_Create, L("CreatingNewCompromiseLabels"), multiTenancySides: MultiTenancySides.Tenant);
            compromiseLabels.CreateChildPermission(AppPermissions.Pages_Maintenance_CompromiseLabel_Edit, L("EditingCompromiseLabels"), multiTenancySides: MultiTenancySides.Tenant);
            compromiseLabels.CreateChildPermission(AppPermissions.Pages_Maintenance_CompromiseLabel_Delete, L("DeletingCompromiseLabels"), multiTenancySides: MultiTenancySides.Tenant);

            var recorResourceTypes = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_RecordResourceType, L("RecordResourceTypes"), multiTenancySides: MultiTenancySides.Tenant);
            recorResourceTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_RecordResourceType_Create, L("CreatingNewRecordResourceTypes"), multiTenancySides: MultiTenancySides.Tenant);
            recorResourceTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_RecordResourceType_Edit, L("EditingRecordResourceTypes"), multiTenancySides: MultiTenancySides.Tenant);
            recorResourceTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_RecordResourceType_Delete, L("DeletingRecordResourceTypes"), multiTenancySides: MultiTenancySides.Tenant);

            var compromiseStates = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_CompromiseState, L("CompromiseStates"), multiTenancySides: MultiTenancySides.Tenant);
            compromiseStates.CreateChildPermission(AppPermissions.Pages_Maintenance_CompromiseState_Create, L("CreatingNewCompromiseStates"), multiTenancySides: MultiTenancySides.Tenant);
            compromiseStates.CreateChildPermission(AppPermissions.Pages_Maintenance_CompromiseState_Edit, L("EditingCompromiseStates"), multiTenancySides: MultiTenancySides.Tenant);
            compromiseStates.CreateChildPermission(AppPermissions.Pages_Maintenance_CompromiseState_Delete, L("DeletingCompromiseStates"), multiTenancySides: MultiTenancySides.Tenant);

            var dialogSpaceDocumentTypes = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceDocumentType, L("DialogSpaceDocumentTypes"), multiTenancySides: MultiTenancySides.Tenant);
            dialogSpaceDocumentTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceDocumentType_Create, L("CreatingNewDialogSpaceDocumentTypes"), multiTenancySides: MultiTenancySides.Tenant);
            dialogSpaceDocumentTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceDocumentType_Edit, L("EditingDialogSpaceDocumentTypes"), multiTenancySides: MultiTenancySides.Tenant);
            dialogSpaceDocumentTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceDocumentType_Delete, L("DeletingDialogSpaceDocumentTypes"), multiTenancySides: MultiTenancySides.Tenant);

            //var dialogSpaceHolidays = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceHoliday, L("DialogSpaceHolidays"), multiTenancySides: MultiTenancySides.Tenant);
            //dialogSpaceHolidays.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceHoliday_Create, L("CreatingNewDialogSpaceHolidays"), multiTenancySides: MultiTenancySides.Tenant);
            //dialogSpaceHolidays.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceHoliday_Edit, L("EditingDialogSpaceHolidays"), multiTenancySides: MultiTenancySides.Tenant);
            //dialogSpaceHolidays.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceHoliday_Delete, L("DeletingDialogSpaceHolidays"), multiTenancySides: MultiTenancySides.Tenant);

            var dialogSpaceTypes = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceType, L("DialogSpaceTypes"), multiTenancySides: MultiTenancySides.Tenant);
            dialogSpaceTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceType_Create, L("CreatingNewDialogSpaceTypes"), multiTenancySides: MultiTenancySides.Tenant);
            dialogSpaceTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceType_Edit, L("EditingDialogSpaceTypes"), multiTenancySides: MultiTenancySides.Tenant);
            dialogSpaceTypes.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceType_Delete, L("DeletingDialogSpaceTypes"), multiTenancySides: MultiTenancySides.Tenant);

            var dialogSpaceDocumentSitutations = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceDocumentSituation, L("DialogSpaceDocumentSituations"), multiTenancySides: MultiTenancySides.Tenant);
            dialogSpaceDocumentSitutations.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceDocumentSituation_Create, L("CreatingNewDialogSpaceDocumentSituations"), multiTenancySides: MultiTenancySides.Tenant);
            dialogSpaceDocumentSitutations.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceDocumentSituation_Edit, L("EditingDialogSpaceDocumentSituations"), multiTenancySides: MultiTenancySides.Tenant);
            dialogSpaceDocumentSitutations.CreateChildPermission(AppPermissions.Pages_Maintenance_DialogSpaceDocumentSituation_Delete, L("DeletingDialogSpaceDocumentSituations"), multiTenancySides: MultiTenancySides.Tenant);

            var directoryResponsibles = maintenance.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryResponsible, L("DirectoryResponsibles"), multiTenancySides: MultiTenancySides.Tenant);
            directoryResponsibles.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryResponsible_Create, L("CreatingNewDirectoryResponsible"), multiTenancySides: MultiTenancySides.Tenant);
            directoryResponsibles.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryResponsible_Edit, L("EditingDirectoryResponsible"), multiTenancySides: MultiTenancySides.Tenant);
            directoryResponsibles.CreateChildPermission(AppPermissions.Pages_Maintenance_DirectoryResponsible_Delete, L("DeletingDirectoryResponsible"), multiTenancySides: MultiTenancySides.Tenant);

            //Catalogs
            var catalogs = pages.CreateChildPermission(AppPermissions.Pages_Catalog, L("Catalogs"), multiTenancySides: MultiTenancySides.Tenant);

            var directoryDialogs = catalogs.CreateChildPermission(AppPermissions.Pages_Catalog_DirectoryDialog, L("DirectoryDialogs"), multiTenancySides: MultiTenancySides.Tenant);
            directoryDialogs.CreateChildPermission(AppPermissions.Pages_Catalog_DirectoryDialog_Create, L("CreatingNewDirectoryDialog"), multiTenancySides: MultiTenancySides.Tenant);
            directoryDialogs.CreateChildPermission(AppPermissions.Pages_Catalog_DirectoryDialog_Edit, L("EditingDirectoryDialog"), multiTenancySides: MultiTenancySides.Tenant);
            directoryDialogs.CreateChildPermission(AppPermissions.Pages_Catalog_DirectoryDialog_Delete, L("DeletingDirectoryDialog"), multiTenancySides: MultiTenancySides.Tenant);

            var directoryIndustries = catalogs.CreateChildPermission(AppPermissions.Pages_Catalog_DirectoryIndustry, L("DirectoryIndustries"), multiTenancySides: MultiTenancySides.Tenant);
            directoryIndustries.CreateChildPermission(AppPermissions.Pages_Catalog_DirectoryIndustry_Create, L("CreatingNewDirectoryIndustry"), multiTenancySides: MultiTenancySides.Tenant);
            directoryIndustries.CreateChildPermission(AppPermissions.Pages_Catalog_DirectoryIndustry_Edit, L("EditingDirectoryIndustry"), multiTenancySides: MultiTenancySides.Tenant);
            directoryIndustries.CreateChildPermission(AppPermissions.Pages_Catalog_DirectoryIndustry_Delete, L("DeletingDirectoryIndustry"), multiTenancySides: MultiTenancySides.Tenant);

            var directoryGovernments = catalogs.CreateChildPermission(AppPermissions.Pages_Catalog_DirectoryGovernment, L("DirectoryGovernments"), multiTenancySides: MultiTenancySides.Tenant);
            directoryGovernments.CreateChildPermission(AppPermissions.Pages_Catalog_DirectoryGovernment_Create, L("CreatingNewDirectoryGovernment"), multiTenancySides: MultiTenancySides.Tenant);
            directoryGovernments.CreateChildPermission(AppPermissions.Pages_Catalog_DirectoryGovernment_Edit, L("EditingDirectoryGovernment"), multiTenancySides: MultiTenancySides.Tenant);
            directoryGovernments.CreateChildPermission(AppPermissions.Pages_Catalog_DirectoryGovernment_Delete, L("DeletingDirectoryGovernment"), multiTenancySides: MultiTenancySides.Tenant);

            //Application
            var application = pages.CreateChildPermission(AppPermissions.Pages_Application, L("Application"), multiTenancySides: MultiTenancySides.Tenant);

            //Social Conflict Dashboard
            application.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictDashboard, L("SocialConflictDashboard"), multiTenancySides: MultiTenancySides.Tenant);

            var socialConflicts = application.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict, L("SocialConflicts"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflicts.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict_Create, L("CreatingNewSocialConflict"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflicts.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict_Edit, L("EditingSocialConflict"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflicts.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict_Delete, L("DeletingSocialConflict"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflicts.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict_Verification, L("VerificationsSocialConflict"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflicts.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict_Actor, L("ShowingActorsSocialConflict"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflicts.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict_Geo, L("GeoSocialConflict"), multiTenancySides: MultiTenancySides.Tenant);

            var socialConflictRecomendations = socialConflicts.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict_Sugerence, L("SocialConflictSugerences"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictRecomendations.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict_Sugerence_Show, L("ShowingSocialConflictSugerences"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictRecomendations.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict_Sugerence_Create, L("CreatingSocialConflictSugerences"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictRecomendations.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict_Sugerence_Edit, L("EditingSocialConflictSugerences"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictRecomendations.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict_Sugerence_Delete, L("DeletingSocialConflictSugerences"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictRecomendations.CreateChildPermission(AppPermissions.Pages_Application_SocialConflict_Sugerence_Accept, L("AcceptingSocialConflictSugerences"), multiTenancySides: MultiTenancySides.Tenant);

            //Social Conflict Sensible Dashboard
            application.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictSensibleDashboard, L("SocialConflictSensibleDashboard"), multiTenancySides: MultiTenancySides.Tenant);

            var socialConflictSensibles = application.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictSensible, L("SocialConflictSensibles"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictSensibles.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictSensible_Create, L("CreatingNewSocialConflictSensible"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictSensibles.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictSensible_Edit, L("EditingSocialConflictSensible"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictSensibles.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictSensible_Delete, L("DeletingSocialConflictSensible"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictSensibles.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictSensible_Verification, L("VerificationsSocialConflictSensible"), multiTenancySides: MultiTenancySides.Tenant);

            var socialConflictSensibleRecomendations = socialConflictSensibles.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictSensible_Sugerence, L("SocialConflictSensibleSugerences"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictSensibleRecomendations.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictSensible_Sugerence_Show, L("ShowingSocialConflictSensibleSugerences"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictSensibleRecomendations.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictSensible_Sugerence_Create, L("CreatingSocialConflictSensibleSugerences"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictSensibleRecomendations.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictSensible_Sugerence_Edit, L("EditingSocialConflictSensibleSugerences"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictSensibleRecomendations.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictSensible_Sugerence_Delete, L("DeletingSocialConflictSensibleSugerences"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictSensibleRecomendations.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictSensible_Sugerence_Accept, L("AcceptingSocialConflictSensibleSugerences"), multiTenancySides: MultiTenancySides.Tenant);

            //Social Conflict Alert Dashboard
            application.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictAlertDashboard, L("SocialConflictAlertDashboard"), multiTenancySides: MultiTenancySides.Tenant);

            var socialConflictAlerts = application.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictAlert, L("SocialConflictAlerts"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictAlerts.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictAlert_Create, L("CreatingNewSocialConflictAlert"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictAlerts.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictAlert_Edit, L("EditingSocialConflictAlert"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictAlerts.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictAlert_Delete, L("DeletingSocialConflictAlert"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictAlerts.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictAlert_Send, L("SendSocialConflictAlert"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictAlerts.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictAlert_History, L("HistorySocialConflictAlert"), multiTenancySides: MultiTenancySides.Tenant);

            var records = application.CreateChildPermission(AppPermissions.Pages_Application_Record, L("Records"), multiTenancySides: MultiTenancySides.Tenant);
            records.CreateChildPermission(AppPermissions.Pages_Application_Record_Create, L("CreatingNewRecord"), multiTenancySides: MultiTenancySides.Tenant);
            records.CreateChildPermission(AppPermissions.Pages_Application_Record_Edit, L("EditingRecord"), multiTenancySides: MultiTenancySides.Tenant);
            records.CreateChildPermission(AppPermissions.Pages_Application_Record_Delete, L("DeletingRecord"), multiTenancySides: MultiTenancySides.Tenant);

            var compromises = application.CreateChildPermission(AppPermissions.Pages_Application_Compromise, L("Compromises"), multiTenancySides: MultiTenancySides.Tenant);
            compromises.CreateChildPermission(AppPermissions.Pages_Application_Compromise_Create, L("CreatingNewCompromise"), multiTenancySides: MultiTenancySides.Tenant);
            compromises.CreateChildPermission(AppPermissions.Pages_Application_Compromise_Edit, L("EditingCompromise"), multiTenancySides: MultiTenancySides.Tenant);
            compromises.CreateChildPermission(AppPermissions.Pages_Application_Compromise_Delete, L("DeletingCompromise"), multiTenancySides: MultiTenancySides.Tenant);

            var taskManagemet = application.CreateChildPermission(AppPermissions.Pages_Application_TaskManagement, L("Tasks"), multiTenancySides: MultiTenancySides.Tenant);
            taskManagemet.CreateChildPermission(AppPermissions.Pages_Application_TaskManagement_History, L("TaskHistories"), multiTenancySides: MultiTenancySides.Tenant);

            var orders = application.CreateChildPermission(AppPermissions.Pages_Application_Order, L("Orders"), multiTenancySides: MultiTenancySides.Tenant);
            orders.CreateChildPermission(AppPermissions.Pages_Application_Order_Create, L("CreatingNewOrder"), multiTenancySides: MultiTenancySides.Tenant);
            orders.CreateChildPermission(AppPermissions.Pages_Application_Order_Edit, L("EditingOrder"), multiTenancySides: MultiTenancySides.Tenant);
            orders.CreateChildPermission(AppPermissions.Pages_Application_Order_Delete, L("DeletingOrder"), multiTenancySides: MultiTenancySides.Tenant);

            var compliance = application.CreateChildPermission(AppPermissions.Pages_Application_Compliance, L("Compliances"), multiTenancySides: MultiTenancySides.Tenant);

            var socialConflictTaskManagemet = application.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictTaskManagement, L("SocialConflictTaskManagements"), multiTenancySides: MultiTenancySides.Tenant);
            socialConflictTaskManagemet.CreateChildPermission(AppPermissions.Pages_Application_SocialConflictTaskManagement_History, L("SocialConflictTaskManagementHistories"), multiTenancySides: MultiTenancySides.Tenant);

            var helpMemories = application.CreateChildPermission(AppPermissions.Pages_Application_HelpMemory, L("HelpMemories"), multiTenancySides: MultiTenancySides.Tenant);
            helpMemories.CreateChildPermission(AppPermissions.Pages_Application_HelpMemory_Create, L("CreatingNewHelpMemory"), multiTenancySides: MultiTenancySides.Tenant);
            helpMemories.CreateChildPermission(AppPermissions.Pages_Application_HelpMemory_Edit, L("EditingHelpMemory"), multiTenancySides: MultiTenancySides.Tenant);
            helpMemories.CreateChildPermission(AppPermissions.Pages_Application_HelpMemory_Delete, L("DeletingHelpMemory"), multiTenancySides: MultiTenancySides.Tenant);

            //Management || Herramientas de Gestión
            var reports = pages.CreateChildPermission(AppPermissions.Pages_Report, L("Reports"), multiTenancySides: MultiTenancySides.Tenant);

            reports.CreateChildPermission(AppPermissions.Pages_Report_SocialConflict, L("ReportSocialConflicts"), multiTenancySides: MultiTenancySides.Tenant);
            reports.CreateChildPermission(AppPermissions.Pages_Report_SocialConflictAlert, L("ReportSocialConflictAlerts"), multiTenancySides: MultiTenancySides.Tenant);
            reports.CreateChildPermission(AppPermissions.Pages_Report_SocialConflictSensible, L("ReportSocialConflictSensibles"), multiTenancySides: MultiTenancySides.Tenant);
            reports.CreateChildPermission(AppPermissions.Pages_Report_HelpMemory_SocialConflict, L("ReportHelpMemorySocialConflicts"), multiTenancySides: MultiTenancySides.Tenant);
            reports.CreateChildPermission(AppPermissions.Pages_Report_HelpMemory_SocialConflictSensible, L("ReportHelpMemorySocialConflictSensibles"), multiTenancySides: MultiTenancySides.Tenant);
            reports.CreateChildPermission(AppPermissions.Pages_Report_ConflictTools_CrisisCommittee, L("ReportConflictToolCrisisCommittees"), multiTenancySides: MultiTenancySides.Tenant);
            reports.CreateChildPermission(AppPermissions.Pages_Report_ConflictTools_InteventionPlan, L("ReportConflictToolInteventionPlans"), multiTenancySides: MultiTenancySides.Tenant);
            reports.CreateChildPermission(AppPermissions.Pages_Report_ConflictTools_SectorMeetSession, L("ReportConflictToolsSectorMeetSession"), multiTenancySides: MultiTenancySides.Tenant);
            reports.CreateChildPermission(AppPermissions.Pages_Report_SocialConflictAlertResume, L("ReportSocialConflictAlertsResume"), multiTenancySides: MultiTenancySides.Tenant);

            //Management || Herramientas de Gestión
            var management = pages.CreateChildPermission(AppPermissions.Pages_Management, L("Management"), multiTenancySides: MultiTenancySides.Tenant);

            var prospectiveRisk = management.CreateChildPermission(AppPermissions.Pages_Management_ProspectiveRisk, L("ProspectiveRisk"), multiTenancySides: MultiTenancySides.Tenant);
            prospectiveRisk.CreateChildPermission(AppPermissions.Pages_Management_ProspectiveRisk_Process, L("ProcessProspectiveRisk"), multiTenancySides: MultiTenancySides.Tenant);
            prospectiveRisk.CreateChildPermission(AppPermissions.Pages_Management_ProspectiveRisk_Edit, L("EditingProspectiveRisk"), multiTenancySides: MultiTenancySides.Tenant);
            
            var prospectiveRiskHistory = prospectiveRisk.CreateChildPermission(AppPermissions.Pages_Management_ProspectiveRisk_History, L("ProspectiveRiskHistory"), multiTenancySides: MultiTenancySides.Tenant);
            prospectiveRiskHistory.CreateChildPermission(AppPermissions.Pages_Management_ProspectiveRisk_History_Delete, L("DeletingProspectiveRiskHistory"), multiTenancySides: MultiTenancySides.Tenant);

            var projectRisk = management.CreateChildPermission(AppPermissions.Pages_Management_ProjectRisk, L("ProjectRisk"), multiTenancySides: MultiTenancySides.Tenant);
            prospectiveRisk.CreateChildPermission(AppPermissions.Pages_Management_ProjectRisk_Process, L("ProcessProjectRisk"), multiTenancySides: MultiTenancySides.Tenant);
            projectRisk.CreateChildPermission(AppPermissions.Pages_Management_ProjectRisk_Create, L("CreatingProjectRisk"), multiTenancySides: MultiTenancySides.Tenant);
            projectRisk.CreateChildPermission(AppPermissions.Pages_Management_ProjectRisk_Edit, L("EditingProjectRisk"), multiTenancySides: MultiTenancySides.Tenant);
            projectRisk.CreateChildPermission(AppPermissions.Pages_Management_ProjectRisk_Delete, L("DeletingProjectRisk"), multiTenancySides: MultiTenancySides.Tenant);

            var projectRiskHistory = projectRisk.CreateChildPermission(AppPermissions.Pages_Management_ProjectRisk_History, L("ProjectRiskHistory"), multiTenancySides: MultiTenancySides.Tenant);
            projectRiskHistory.CreateChildPermission(AppPermissions.Pages_Management_ProjectRisk_History_Delete, L("DeletingProjectRiskHistory"), multiTenancySides: MultiTenancySides.Tenant);

            var projectStages = management.CreateChildPermission(AppPermissions.Pages_Management_ProjectStage, L("ProjectStages"), multiTenancySides: MultiTenancySides.Tenant);
            projectStages.CreateChildPermission(AppPermissions.Pages_Management_ProjectStage_Create, L("CreatingProjectStages"), multiTenancySides: MultiTenancySides.Tenant);
            projectStages.CreateChildPermission(AppPermissions.Pages_Management_ProjectStage_Edit, L("EditingProjectStages"), multiTenancySides: MultiTenancySides.Tenant);
            projectStages.CreateChildPermission(AppPermissions.Pages_Management_ProjectStage_Delete, L("DeletingProjectStages"), multiTenancySides: MultiTenancySides.Tenant);
            projectStages.CreateChildPermission(AppPermissions.Pages_Management_ProjectStage_Enable, L("EnablingProjectStages"), multiTenancySides: MultiTenancySides.Tenant);
            projectStages.CreateChildPermission(AppPermissions.Pages_Management_ProjectStage_Disable, L("DisablingProjectStages"), multiTenancySides: MultiTenancySides.Tenant);

            var dinamicVariable = management.CreateChildPermission(AppPermissions.Pages_Management_DinamicVariable, L("DinamicVariable"), multiTenancySides: MultiTenancySides.Tenant);
            dinamicVariable.CreateChildPermission(AppPermissions.Pages_Management_DinamicVariable_Create, L("CreatingNewDinamicVariable"), multiTenancySides: MultiTenancySides.Tenant);
            dinamicVariable.CreateChildPermission(AppPermissions.Pages_Management_DinamicVariable_Edit, L("EditingDinamicVariable"), multiTenancySides: MultiTenancySides.Tenant);
            dinamicVariable.CreateChildPermission(AppPermissions.Pages_Management_DinamicVariable_Delete, L("DeletingDinamicVariable"), multiTenancySides: MultiTenancySides.Tenant);
            dinamicVariable.CreateChildPermission(AppPermissions.Pages_Management_DinamicVariable_Enable, L("EnablingDinamicVariable"), multiTenancySides: MultiTenancySides.Tenant);
            dinamicVariable.CreateChildPermission(AppPermissions.Pages_Management_DinamicVariable_Disable, L("DisablingDinamicVariable"), multiTenancySides: MultiTenancySides.Tenant);

            var staticVariable = management.CreateChildPermission(AppPermissions.Pages_Management_StaticVariable, L("StaticVariable"), multiTenancySides: MultiTenancySides.Tenant);
            staticVariable.CreateChildPermission(AppPermissions.Pages_Management_StaticVariable_Create, L("CreatingNewStaticVariable"), multiTenancySides: MultiTenancySides.Tenant);
            staticVariable.CreateChildPermission(AppPermissions.Pages_Management_StaticVariable_Edit, L("EditingStaticVariable"), multiTenancySides: MultiTenancySides.Tenant);
            staticVariable.CreateChildPermission(AppPermissions.Pages_Management_StaticVariable_Delete, L("DeletingStaticVariable"), multiTenancySides: MultiTenancySides.Tenant);
            staticVariable.CreateChildPermission(AppPermissions.Pages_Management_StaticVariable_Enable, L("EnablingStaticVariable"), multiTenancySides: MultiTenancySides.Tenant);
            staticVariable.CreateChildPermission(AppPermissions.Pages_Management_StaticVariable_Disable, L("DisablingStaticVariable"), multiTenancySides: MultiTenancySides.Tenant);

            //Conflict tools
            var conflictTools = pages.CreateChildPermission(AppPermissions.Pages_ConflictTools, L("ConflictTools"), multiTenancySides: MultiTenancySides.Tenant);

            var interventionPlan = conflictTools.CreateChildPermission(AppPermissions.Pages_ConflictTools_InterventionPlan, L("InterventionPlans"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlan.CreateChildPermission(AppPermissions.Pages_ConflictTools_InterventionPlan_Create, L("CreatingNewInterventionPlan"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlan.CreateChildPermission(AppPermissions.Pages_ConflictTools_InterventionPlan_Edit, L("EditingInterventionPlan"), multiTenancySides: MultiTenancySides.Tenant);
            interventionPlan.CreateChildPermission(AppPermissions.Pages_ConflictTools_InterventionPlan_Delete, L("DeletingInterventionPlan"), multiTenancySides: MultiTenancySides.Tenant);

            var crisisCommittees = conflictTools.CreateChildPermission(AppPermissions.Pages_ConflictTools_CrisisCommittee, L("CrisisCommittees"), multiTenancySides: MultiTenancySides.Tenant);
            crisisCommittees.CreateChildPermission(AppPermissions.Pages_ConflictTools_CrisisCommittee_Create, L("CreatingNewCrisisCommittees"), multiTenancySides: MultiTenancySides.Tenant);
            crisisCommittees.CreateChildPermission(AppPermissions.Pages_ConflictTools_CrisisCommittee_Edit, L("EditingCrisisCommittees"), multiTenancySides: MultiTenancySides.Tenant);
            crisisCommittees.CreateChildPermission(AppPermissions.Pages_ConflictTools_CrisisCommittee_Delete, L("DeletingCrisisCommittees"), multiTenancySides: MultiTenancySides.Tenant);

            var sectorMeets = conflictTools.CreateChildPermission(AppPermissions.Pages_ConflictTools_SectorMeet, L("SectorMeets"), multiTenancySides: MultiTenancySides.Tenant);
            sectorMeets.CreateChildPermission(AppPermissions.Pages_ConflictTools_SectorMeet_Create, L("CreatingSectorMeets"), multiTenancySides: MultiTenancySides.Tenant);
            sectorMeets.CreateChildPermission(AppPermissions.Pages_ConflictTools_SectorMeet_Edit, L("EditingSectorMeets"), multiTenancySides: MultiTenancySides.Tenant);
            sectorMeets.CreateChildPermission(AppPermissions.Pages_ConflictTools_SectorMeet_Delete, L("DeletingSectorMeets"), multiTenancySides: MultiTenancySides.Tenant);

            var dialogSpaces = conflictTools.CreateChildPermission(AppPermissions.Pages_ConflictTools_DialogSpace, L("DialogSpaces"), multiTenancySides: MultiTenancySides.Tenant);
            dialogSpaces.CreateChildPermission(AppPermissions.Pages_ConflictTools_DialogSpace_Create, L("CreatingDialogSpaces"), multiTenancySides: MultiTenancySides.Tenant);
            dialogSpaces.CreateChildPermission(AppPermissions.Pages_ConflictTools_DialogSpace_Edit, L("EditingDialogSpaces"), multiTenancySides: MultiTenancySides.Tenant);
            dialogSpaces.CreateChildPermission(AppPermissions.Pages_ConflictTools_DialogSpace_Delete, L("DeletingDialogSpaces"), multiTenancySides: MultiTenancySides.Tenant);

            //Quiz
            var quiz = pages.CreateChildPermission(AppPermissions.Pages_Quiz, L("Quiz"), multiTenancySides: MultiTenancySides.Tenant);

            var customerQuiz = quiz.CreateChildPermission(AppPermissions.Pages_Quiz_Customer, L("CustomerQuiz"), multiTenancySides: MultiTenancySides.Tenant);

            var platformQuiz = quiz.CreateChildPermission(AppPermissions.Pages_Quiz_Platform, L("PlatformQuiz"), multiTenancySides: MultiTenancySides.Tenant);
            platformQuiz.CreateChildPermission(AppPermissions.Pages_Quiz_Platform_Edit, L("EditingPlatformQuiz"), multiTenancySides: MultiTenancySides.Tenant);

            var statesQuiz = quiz.CreateChildPermission(AppPermissions.Pages_Quiz_States, L("StateQuiz"), multiTenancySides: MultiTenancySides.Tenant);
            statesQuiz.CreateChildPermission(AppPermissions.Pages_Quiz_States_Create, L("CreatingStateQuiz"), multiTenancySides: MultiTenancySides.Tenant);
            statesQuiz.CreateChildPermission(AppPermissions.Pages_Quiz_States_Edit, L("EditingStateQuiz"), multiTenancySides: MultiTenancySides.Tenant);
            statesQuiz.CreateChildPermission(AppPermissions.Pages_Quiz_States_Delete, L("DeletingStateQuiz"), multiTenancySides: MultiTenancySides.Tenant);

            var customerResponses = quiz.CreateChildPermission(AppPermissions.Pages_Quiz_Responses, L("CustomerQuizReponses"), multiTenancySides: MultiTenancySides.Tenant);
            customerResponses.CreateChildPermission(AppPermissions.Pages_Quiz_Responses_Edit, L("EditingCustomerQuizReponses"), multiTenancySides: MultiTenancySides.Tenant);

            ///App

            var app = context.GetPermissionOrNull(AppPermissions.App) ?? context.CreatePermission(AppPermissions.App, L("App"));

            var appSocialConflict = app.CreateChildPermission(AppPermissions.App_SocialConflict, L("AppSocialConflict"), multiTenancySides: MultiTenancySides.Tenant);
            appSocialConflict.CreateChildPermission(AppPermissions.App_SocialConflict_Create, L("AppSocialConflictCreate"), multiTenancySides: MultiTenancySides.Tenant);
            appSocialConflict.CreateChildPermission(AppPermissions.App_SocialConflict_Edit, L("AppSocialConflictEdit"), multiTenancySides: MultiTenancySides.Tenant);
            appSocialConflict.CreateChildPermission(AppPermissions.App_SocialConflict_Resource, L("AppSocialConflictResource"), multiTenancySides: MultiTenancySides.Tenant);


            var appSocialConflictSensible = app.CreateChildPermission(AppPermissions.App_SocialConflictSensible, L("AppSocialConflictSensible"), multiTenancySides: MultiTenancySides.Tenant);
            appSocialConflictSensible.CreateChildPermission(AppPermissions.App_SocialConflictSensible_Resource, L("AppSocialConflictSensibleResource"), multiTenancySides: MultiTenancySides.Tenant);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ContableConsts.LocalizationSourceName);
        }

        #region RecicledCode 

        /*
        administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

        var webhooks = administration.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription, L("Webhooks"));
        webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Create, L("CreatingWebhooks"));
        webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Edit, L("EditingWebhooks"));
        webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_ChangeActivity, L("ChangingWebhookActivity"));
        webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Detail, L("DetailingSubscription"));
        webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ListSendAttempts, L("ListingSendAttempts"));
        webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ResendWebhook, L("ResendingWebhook"));

        var dynamicParameters = administration.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameters, L("DynamicParameters"));
        dynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameters_Create, L("CreatingDynamicParameters"));
        dynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameters_Edit, L("EditingDynamicParameters"));
        dynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameters_Delete, L("DeletingDynamicParameters"));

        var dynamicParameterValues = dynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameterValue, L("DynamicParameterValue"));
        dynamicParameterValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameterValue_Create, L("CreatingDynamicParameterValue"));
        dynamicParameterValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameterValue_Edit, L("EditingDynamicParameterValue"));
        dynamicParameterValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicParameterValue_Delete, L("DeletingDynamicParameterValue"));

        var entityDynamicParameters = dynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameters, L("EntityDynamicParameters"));
        entityDynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameters_Create, L("CreatingEntityDynamicParameters"));
        entityDynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameters_Edit, L("EditingEntityDynamicParameters"));
        entityDynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameters_Delete, L("DeletingEntityDynamicParameters"));

        var entityDynamicParameterValues = dynamicParameters.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameterValue, L("EntityDynamicParameterValue"));
        entityDynamicParameterValues.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameterValue_Create, L("CreatingEntityDynamicParameterValue"));
        entityDynamicParameterValues.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameterValue_Edit, L("EditingEntityDynamicParameterValue"));
        entityDynamicParameterValues.CreateChildPermission(AppPermissions.Pages_Administration_EntityDynamicParameterValue_Delete, L("DeletingEntityDynamicParameterValue"));
        */

        //var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
        //organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
        //organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
        //organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

        //pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

        //pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

        //var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
        //languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"), multiTenancySides: _isMultiTenancyEnabled? MultiTenancySides.Host : MultiTenancySides.Tenant);
        //languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"), multiTenancySides: _isMultiTenancyEnabled? MultiTenancySides.Host : MultiTenancySides.Tenant);
        //languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"), multiTenancySides: _isMultiTenancyEnabled? MultiTenancySides.Host : MultiTenancySides.Tenant);
        //languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

        //var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
        //editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
        //editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
        //editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
        //editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host);

        //var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        //tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
        //tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
        //tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
        //tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
        //tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

        //administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

        //administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
        #endregion
    }
}
