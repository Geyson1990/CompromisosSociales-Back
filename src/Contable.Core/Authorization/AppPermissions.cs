namespace Contable.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_Unlock = "Pages.Administration.Users.Unlock";
        public const string Pages_Administration_Users_Lock = "Pages.Administration.Users.Lock";
        public const string Pages_Administration_Users_Conflict = "Pages.Administration.Users.Conflict";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_HangfireDashboard = "Pages.Administration.HangfireDashboard";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        //HOST-SPECIFIC PERMISSIONS


        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Dashboard = "Pages.Administration.Host.Dashboard";


        //Aplication
        public const string Pages_Maintenance = "Pages.Maintenance";

        //Ubigeos
        public const string Pages_Maintenance_Ubigeo = "Pages.Maintenance.Ubigeo";
        public const string Pages_Maintenance_Ubigeo_Create = "Pages.Maintenance.Ubigeo.Create";
        public const string Pages_Maintenance_Ubigeo_Edit = "Pages.Maintenance.Ubigeo.Edit";
        public const string Pages_Maintenance_Ubigeo_Delete = "Pages.Maintenance.Ubigeo.Delete";

        //ResponsibleActors || Actores responsables && Responsable específicos
        public const string Pages_Maintenance_Responsible_Actor = "Pages.Maintenance.ResponsibleActor";
        public const string Pages_Maintenance_Responsible_Actor_Create = "Pages.Maintenance.ResponsibleActor.Create";
        public const string Pages_Maintenance_Responsible_Actor_Edit = "Pages.Maintenance.ResponsibleActor.Edit";
        public const string Pages_Maintenance_Responsible_Actor_Delete = "Pages.Maintenance.ResponsibleActor.Delete";

        //ResponsibleActors || Actores responsables && Responsable específicos
        public const string Pages_Maintenance_Responsible_Actor_SubActor = "Pages.Maintenance.ResponsibleActor.SubActor";
        public const string Pages_Maintenance_Responsible_Actor_SubActor_Create = "Pages.Maintenance.ResponsibleActor.SubActor.Create";
        public const string Pages_Maintenance_Responsible_Actor_SubActor_Edit = "Pages.Maintenance.ResponsibleActor.SubActor.Edit";
        public const string Pages_Maintenance_Responsible_Actor_SubActor_Delete = "Pages.Maintenance.ResponsibleActor.SubActor.Delete";

        //TerritorialUnit || Unidades territoriales
        public const string Pages_Maintenance_TerritorialUnit = "Pages.Maintenance.TerritorialUnit";
        public const string Pages_Maintenance_TerritorialUnit_Create = "Pages.Maintenance.TerritorialUnit.Create";
        public const string Pages_Maintenance_TerritorialUnit_Edit = "Pages.Maintenance.TerritorialUnit.Edit";
        public const string Pages_Maintenance_TerritorialUnit_Delete = "Pages.Maintenance.TerritorialUnit.Delete";

        //Social conflict actor || Tipo de actores de conflictividad social
        public const string Pages_Maintenance_ActorType = "Pages.Maintenance.ActorType";
        public const string Pages_Maintenance_ActorType_Create = "Pages.Maintenance.ActorType.Create";
        public const string Pages_Maintenance_ActorType_Edit = "Pages.Maintenance.ActorType.Edit";
        public const string Pages_Maintenance_ActorType_Delete = "Pages.Maintenance.ActorType.Delete";

        //Social conflict sub actor || Actores detallados de conflictividad social
        public const string Pages_Maintenance_ActorMovement = "Pages.Maintenance.ActorMovement";
        public const string Pages_Maintenance_ActorMovement_Create = "Pages.Maintenance.ActorMovement.Create";
        public const string Pages_Maintenance_ActorMovement_Edit = "Pages.Maintenance.ActorMovement.Edit";
        public const string Pages_Maintenance_ActorMovement_Delete = "Pages.Maintenance.ActorMovement.Delete";

        //Social conflict risk || Nivel de riesgo de casos de conflictividad social
        public const string Pages_Maintenance_Risk = "Pages.Maintenance.Risk";
        public const string Pages_Maintenance_Risk_Create = "Pages.Maintenance.Risk.Create";
        public const string Pages_Maintenance_Risk_Edit = "Pages.Maintenance.Risk.Edit";
        public const string Pages_Maintenance_Risk_Delete = "Pages.Maintenance.Risk.Delete";

        //Social conflict typology || Tipologia general de conflictividad social
        public const string Pages_Maintenance_Typology = "Pages.Maintenance.Typology";
        public const string Pages_Maintenance_Typology_Create = "Pages.Maintenance.Typology.Create";
        public const string Pages_Maintenance_Typology_Edit = "Pages.Maintenance.Typology.Edit";
        public const string Pages_Maintenance_Typology_Delete = "Pages.Maintenance.Typology.Delete";

        //Social conflict sub typology || Tipologia detallada de conflictividad social
        public const string Pages_Maintenance_Typology_SubTypology = "Pages.Maintenance.Typology.SubTypology";
        public const string Pages_Maintenance_Typology_SubTypology_Create = "Pages.Maintenance.Typology.SubTypology.Create";
        public const string Pages_Maintenance_Typology_SubTypology_Edit = "Pages.Maintenance.Typology.SubTypology.Edit";
        public const string Pages_Maintenance_Typology_SubTypology_Delete = "Pages.Maintenance.Typology.SubTypology.Delete";

        //Social conflict typology || Sectores de conflictividad social
        public const string Pages_Maintenance_Sector = "Pages.Maintenance.Sector";
        public const string Pages_Maintenance_Sector_Create = "Pages.Maintenance.Sector.Create";
        public const string Pages_Maintenance_Sector_Edit = "Pages.Maintenance.Sector.Edit";
        public const string Pages_Maintenance_Sector_Delete = "Pages.Maintenance.Sector.Delete";

        //Social conflict facts || Hechos de conflictividad social
        public const string Pages_Maintenance_Fact = "Pages.Maintenance.Fact";
        public const string Pages_Maintenance_Fact_Create = "Pages.Maintenance.Fact.Create";
        public const string Pages_Maintenance_Fact_Edit = "Pages.Maintenance.Fact.Edit";
        public const string Pages_Maintenance_Fact_Delete = "Pages.Maintenance.Fact.Delete";

        //Social conflict coordinators || Coordinadores de conflictividad social
        public const string Pages_Maintenance_Coordinator = "Pages.Maintenance.Coordinator";
        public const string Pages_Maintenance_Coordinator_Create = "Pages.Maintenance.Coordinator.Create";
        public const string Pages_Maintenance_Coordinator_Edit = "Pages.Maintenance.Coordinator.Edit";
        public const string Pages_Maintenance_Coordinator_Delete = "Pages.Maintenance.Coordinator.Delete";

        //Social conflict managers || Gestores de conflictividad social
        public const string Pages_Maintenance_Manager = "Pages.Maintenance.Manager";
        public const string Pages_Maintenance_Manager_Create = "Pages.Maintenance.Manager.Create";
        public const string Pages_Maintenance_Manager_Edit = "Pages.Maintenance.Manager.Edit";
        public const string Pages_Maintenance_Manager_Delete = "Pages.Maintenance.Manager.Delete";

        //Social conflict management type || Tipos de gestion de conflictividad social
        public const string Pages_Maintenance_Management = "Pages.Maintenance.Management";
        public const string Pages_Maintenance_Management_Create = "Pages.Maintenance.Management.Create";
        public const string Pages_Maintenance_Management_Edit = "Pages.Maintenance.Management.Edit";
        public const string Pages_Maintenance_Management_Delete = "Pages.Maintenance.Management.Delete";

        //Social conflict management type || Analistas de conflictividad social
        public const string Pages_Maintenance_Analyst = "Pages.Maintenance.Analyst";
        public const string Pages_Maintenance_Analyst_Create = "Pages.Maintenance.Analyst.Create";
        public const string Pages_Maintenance_Analyst_Edit = "Pages.Maintenance.Analyst.Edit";
        public const string Pages_Maintenance_Analyst_Delete = "Pages.Maintenance.Analyst.Delete";

        //Social conflict alert risk || Nivel de riesgo de casos de alertas conflictividad social
        public const string Pages_Maintenance_AlertRisk = "Pages.Maintenance.AlertRisk";
        public const string Pages_Maintenance_AlertRisk_Create = "Pages.Maintenance.AlertRisk.Create";
        public const string Pages_Maintenance_AlertRisk_Edit = "Pages.Maintenance.AlertRisk.Edit";
        public const string Pages_Maintenance_AlertRisk_Delete = "Pages.Maintenance.AlertRisk.Delete";

        public const string Pages_Maintenance_AlertSector = "Pages.Maintenance.AlertSector";
        public const string Pages_Maintenance_AlertSector_Create = "Pages.Maintenance.AlertSector.Create";
        public const string Pages_Maintenance_AlertSector_Edit = "Pages.Maintenance.AlertSector.Edit";
        public const string Pages_Maintenance_AlertSector_Delete = "Pages.Maintenance.AlertSector.Delete";

        public const string Pages_Maintenance_AlertSeal = "Pages.Maintenance.AlertSeal";
        public const string Pages_Maintenance_AlertSeal_Create = "Pages.Maintenance.AlertSeal.Create";
        public const string Pages_Maintenance_AlertSeal_Edit = "Pages.Maintenance.AlertSeal.Edit";
        public const string Pages_Maintenance_AlertSeal_Delete = "Pages.Maintenance.AlertSeal.Delete";

        public const string Pages_Maintenance_AlertDemand = "Pages.Maintenance.AlertDemand";
        public const string Pages_Maintenance_AlertDemand_Create = "Pages.Maintenance.AlertDemand.Create";
        public const string Pages_Maintenance_AlertDemand_Edit = "Pages.Maintenance.AlertDemand.Edit";
        public const string Pages_Maintenance_AlertDemand_Delete = "Pages.Maintenance.AlertDemand.Delete";

        public const string Pages_Maintenance_AlertResponsible = "Pages.Maintenance.AlertResponsible";
        public const string Pages_Maintenance_AlertResponsible_Create = "Pages.Maintenance.AlertResponsible.Create";
        public const string Pages_Maintenance_AlertResponsible_Edit = "Pages.Maintenance.AlertResponsible.Edit";
        public const string Pages_Maintenance_AlertResponsible_Delete = "Pages.Maintenance.AlertResponsible.Delete";

        public const string Pages_Maintenance_DirectoryGovernmentSector = "Pages.Maintenance.DirectoryGovernmentSector";
        public const string Pages_Maintenance_DirectoryGovernmentSector_Create = "Pages.Maintenance.DirectoryGovernmentSector.Create";
        public const string Pages_Maintenance_DirectoryGovernmentSector_Edit = "Pages.Maintenance.DirectoryGovernmentSector.Edit";
        public const string Pages_Maintenance_DirectoryGovernmentSector_Delete = "Pages.Maintenance.DirectoryGovernmentSector.Delete";

        public const string Pages_Maintenance_DirectoryResponsible = "Pages.Maintenance.DirectoryResponsible";
        public const string Pages_Maintenance_DirectoryResponsible_Create = "Pages.Maintenance.DirectoryResponsible.Create";
        public const string Pages_Maintenance_DirectoryResponsible_Edit = "Pages.Maintenance.DirectoryResponsible.Edit";
        public const string Pages_Maintenance_DirectoryResponsible_Delete = "Pages.Maintenance.DirectoryResponsible.Delete";

        public const string Pages_Maintenance_DirectorySector = "Pages.Maintenance.DirectorySector";
        public const string Pages_Maintenance_DirectorySector_Create = "Pages.Maintenance.DirectorySector.Create";
        public const string Pages_Maintenance_DirectorySector_Edit = "Pages.Maintenance.DirectorySector.Edit";
        public const string Pages_Maintenance_DirectorySector_Delete = "Pages.Maintenance.DirectorySector.Delete";

        public const string Pages_Maintenance_DirectoryConflictType = "Pages.Maintenance.DirectoryConflictType";
        public const string Pages_Maintenance_DirectoryConflictType_Create = "Pages.Maintenance.DirectoryConflictType.Create";
        public const string Pages_Maintenance_DirectoryConflictType_Edit = "Pages.Maintenance.DirectoryConflictType.Edit";
        public const string Pages_Maintenance_DirectoryConflictType_Delete = "Pages.Maintenance.DirectoryConflictType.Delete";

        public const string Pages_Maintenance_DirectoryGovernmentLevel = "Pages.Maintenance.DirectoryGovernmentLevel";
        public const string Pages_Maintenance_DirectoryGovernmentLevel_Create = "Pages.Maintenance.DirectoryGovernmentLevel.Create";
        public const string Pages_Maintenance_DirectoryGovernmentLevel_Edit = "Pages.Maintenance.DirectoryGovernmentLevel.Edit";
        public const string Pages_Maintenance_DirectoryGovernmentLevel_Delete = "Pages.Maintenance.DirectoryGovernmentLevel.Delete";

        public const string Pages_Maintenance_DirectoryGovernmentType = "Pages.Maintenance.DirectoryGovernmentType";
        public const string Pages_Maintenance_DirectoryGovernmentType_Create = "Pages.Maintenance.DirectoryGovernmentType.Create";
        public const string Pages_Maintenance_DirectoryGovernmentType_Edit = "Pages.Maintenance.DirectoryGovernmentType.Edit";
        public const string Pages_Maintenance_DirectoryGovernmentType_Delete = "Pages.Maintenance.DirectoryGovernmentType.Delete";

        public const string Pages_Maintenance_InterventionPlanOption = "Pages.Maintenance.InterventionPlanOption";
        public const string Pages_Maintenance_InterventionPlanOption_Create = "Pages.Maintenance.InterventionPlanOption.Create";
        public const string Pages_Maintenance_InterventionPlanOption_Edit = "Pages.Maintenance.InterventionPlanOption.Edit";
        public const string Pages_Maintenance_InterventionPlanOption_Delete = "Pages.Maintenance.InterventionPlanOption.Delete";

        public const string Pages_Maintenance_InterventionPlanEntity = "Pages.Maintenance.InterventionPlanEntity";
        public const string Pages_Maintenance_InterventionPlanEntity_Create = "Pages.Maintenance.InterventionPlanEntity.Create";
        public const string Pages_Maintenance_InterventionPlanEntity_Edit = "Pages.Maintenance.InterventionPlanEntity.Edit";
        public const string Pages_Maintenance_InterventionPlanEntity_Delete = "Pages.Maintenance.InterventionPlanEntity.Delete";

        public const string Pages_Maintenance_InterventionPlanActivity = "Pages.Maintenance.InterventionPlanActivity";
        public const string Pages_Maintenance_InterventionPlanActivity_Create = "Pages.Maintenance.InterventionPlanActivity.Create";
        public const string Pages_Maintenance_InterventionPlanActivity_Edit = "Pages.Maintenance.InterventionPlanActivity.Edit";
        public const string Pages_Maintenance_InterventionPlanActivity_Delete = "Pages.Maintenance.InterventionPlanActivity.Delete";

        public const string Pages_Maintenance_InterventionPlanRole = "Pages.Maintenance.InterventionPlanRole";
        public const string Pages_Maintenance_InterventionPlanRole_Create = "Pages.Maintenance.InterventionPlanRole.Create";
        public const string Pages_Maintenance_InterventionPlanRole_Edit = "Pages.Maintenance.InterventionPlanRole.Edit";
        public const string Pages_Maintenance_InterventionPlanRole_Delete = "Pages.Maintenance.InterventionPlanRole.Delete";

        public const string Pages_Maintenance_CrisisCommitteeJob = "Pages.Maintenance.CrisisCommitteeJob";
        public const string Pages_Maintenance_CrisisCommitteeJob_Create = "Pages.Maintenance.CrisisCommitteeJob.Create";
        public const string Pages_Maintenance_CrisisCommitteeJob_Edit = "Pages.Maintenance.CrisisCommitteeJob.Edit";
        public const string Pages_Maintenance_CrisisCommitteeJob_Delete = "Pages.Maintenance.CrisisCommitteeJob.Delete";

        public const string Pages_Maintenance_ResponsibleType = "Pages.Maintenance.ResponsibleType";
        public const string Pages_Maintenance_ResponsibleType_Create = "Pages.Maintenance.ResponsibleType.Create";
        public const string Pages_Maintenance_ResponsibleType_Edit = "Pages.Maintenance.ResponsibleType.Edit";
        public const string Pages_Maintenance_ResponsibleType_Delete = "Pages.Maintenance.ResponsibleType.Delete";

        public const string Pages_Maintenance_CompromiseLabel = "Pages.Maintenance.CompromiseLabel";
        public const string Pages_Maintenance_CompromiseLabel_Create = "Pages.Maintenance.CompromiseLabel.Create";
        public const string Pages_Maintenance_CompromiseLabel_Edit = "Pages.Maintenance.CompromiseLabel.Edit";
        public const string Pages_Maintenance_CompromiseLabel_Delete = "Pages.Maintenance.CompromiseLabel.Delete";

        public const string Pages_Maintenance_RecordResourceType = "Pages.Maintenance.RecordResourceType";
        public const string Pages_Maintenance_RecordResourceType_Create = "Pages.Maintenance.RecordResourceType.Create";
        public const string Pages_Maintenance_RecordResourceType_Edit = "Pages.Maintenance.RecordResourceType.Edit";
        public const string Pages_Maintenance_RecordResourceType_Delete = "Pages.Maintenance.RecordResourceType.Delete";

        public const string Pages_Maintenance_CompromiseState = "Pages.Maintenance.CompromiseState";
        public const string Pages_Maintenance_CompromiseState_Create = "Pages.Maintenance.CompromiseState.Create";
        public const string Pages_Maintenance_CompromiseState_Edit = "Pages.Maintenance.CompromiseState.Edit";
        public const string Pages_Maintenance_CompromiseState_Delete = "Pages.Maintenance.CompromiseState.Delete";

        //Social conflict actor || Tipo de documento de espacios de dialogo
        public const string Pages_Maintenance_DialogSpaceDocumentType = "Pages.Maintenance.DialogSpaceDocumentType";
        public const string Pages_Maintenance_DialogSpaceDocumentType_Create = "Pages.Maintenance.DialogSpaceDocumentType.Create";
        public const string Pages_Maintenance_DialogSpaceDocumentType_Edit = "Pages.Maintenance.DialogSpaceDocumentType.Edit";
        public const string Pages_Maintenance_DialogSpaceDocumentType_Delete = "Pages.Maintenance.DialogSpaceDocumentType.Delete";

        //Dialog Space Holiday || Dias feriados de espacios de dialogo
        public const string Pages_Maintenance_DialogSpaceHoliday = "Pages.Maintenance.DialogSpaceHoliday";
        public const string Pages_Maintenance_DialogSpaceHoliday_Create = "Pages.Maintenance.DialogSpaceHoliday.Create";
        public const string Pages_Maintenance_DialogSpaceHoliday_Edit = "Pages.Maintenance.DialogSpaceHoliday.Edit";
        public const string Pages_Maintenance_DialogSpaceHoliday_Delete = "Pages.Maintenance.DialogSpaceHoliday.Delete";

        //Dialog Space Type || Tipos de Espacios de Dialogo
        public const string Pages_Maintenance_DialogSpaceType = "Pages.Maintenance.DialogSpaceType";
        public const string Pages_Maintenance_DialogSpaceType_Create = "Pages.Maintenance.DialogSpaceType.Create";
        public const string Pages_Maintenance_DialogSpaceType_Edit = "Pages.Maintenance.DialogSpaceType.Edit";
        public const string Pages_Maintenance_DialogSpaceType_Delete = "Pages.Maintenance.DialogSpaceType.Delete";

        //Dialog Space Document Situtation || Situacion de Documentos de Espacios de Dialogo
        public const string Pages_Maintenance_DialogSpaceDocumentSituation = "Pages.Maintenance.DialogSpaceDocumentSituation";
        public const string Pages_Maintenance_DialogSpaceDocumentSituation_Create = "Pages.Maintenance.DialogSpaceDocumentSituation.Create";
        public const string Pages_Maintenance_DialogSpaceDocumentSituation_Edit = "Pages.Maintenance.DialogSpaceDocumentSituation.Edit";
        public const string Pages_Maintenance_DialogSpaceDocumentSituation_Delete = "Pages.Maintenance.DialogSpaceDocumentSituation.Delete";

        //Phases compromises PIP || Fases de compromisos PIP
        public const string Pages_Maintenance_Phase = "Pages.Maintenance.Phase";
        public const string Pages_Maintenance_Phase_Create = "Pages.Maintenance.Phase.Create";
        public const string Pages_Maintenance_Phase_Edit = "Pages.Maintenance.Phase.Edit";
        public const string Pages_Maintenance_Phase_Delete = "Pages.Maintenance.Phase.Delete";

        public const string Pages_Catalog = "Pages.Catalog";

        public const string Pages_Catalog_DirectoryDialog = "Pages.Catalog.DirectoryDialog";
        public const string Pages_Catalog_DirectoryDialog_Create = "Pages.Catalog.DirectoryDialog.Create";
        public const string Pages_Catalog_DirectoryDialog_Edit = "Pages.Catalog.DirectoryDialog.Edit";
        public const string Pages_Catalog_DirectoryDialog_Delete = "Pages.Catalog.DirectoryDialog.Delete";

        public const string Pages_Catalog_DirectoryIndustry = "Pages.Catalog.DirectoryIndustry";
        public const string Pages_Catalog_DirectoryIndustry_Create = "Pages.Catalog.DirectoryIndustry.Create";
        public const string Pages_Catalog_DirectoryIndustry_Edit = "Pages.Catalog.DirectoryIndustry.Edit";
        public const string Pages_Catalog_DirectoryIndustry_Delete = "Pages.Catalog.DirectoryIndustry.Delete";

        public const string Pages_Catalog_DirectoryGovernment = "Pages.Catalog.DirectoryGovernment";
        public const string Pages_Catalog_DirectoryGovernment_Create = "Pages.Catalog.DirectoryGovernment.Create";
        public const string Pages_Catalog_DirectoryGovernment_Edit = "Pages.Catalog.DirectoryGovernment.Edit";
        public const string Pages_Catalog_DirectoryGovernment_Delete = "Pages.Catalog.DirectoryGovernment.Delete";

        //Application || Aplicacion
        public const string Pages_Application = "Pages.Application";

        //SocialConflict || Conflicto Social
        public const string Pages_Application_SocialConflict = "Pages.Application.SocialConflict";
        public const string Pages_Application_SocialConflictDashboard = "Pages.Application.SocialConflictDashboard";
        public const string Pages_Application_SocialConflict_Create = "Pages.Application.SocialConflict.Create";
        public const string Pages_Application_SocialConflict_Edit = "Pages.Application.SocialConflict.Edit";
        public const string Pages_Application_SocialConflict_Delete = "Pages.Application.SocialConflict.Delete";
        public const string Pages_Application_SocialConflict_Verification = "Pages.Application.SocialConflict.Verification";
        public const string Pages_Application_SocialConflict_Actor = "Pages.Application.SocialConflict.Actor";
        public const string Pages_Application_SocialConflict_Sugerence = "Pages.Application.SocialConflict.Sugerence";
        public const string Pages_Application_SocialConflict_Sugerence_Create = "Pages.Application.SocialConflict.Sugerence.Create";
        public const string Pages_Application_SocialConflict_Sugerence_Edit = "Pages.Application.SocialConflict.Sugerence.Edit";
        public const string Pages_Application_SocialConflict_Sugerence_Delete = "Pages.Application.SocialConflict.Sugerence.Delete";
        public const string Pages_Application_SocialConflict_Sugerence_Accept = "Pages.Application.SocialConflict.Sugerence.Accept";
        public const string Pages_Application_SocialConflict_Sugerence_Show = "Pages.Application.SocialConflict.Sugerence.Show";
        public const string Pages_Application_SocialConflict_Geo = "Pages.Application.SocialConflict.Geo";

        //SocialConflictAlert || Conflicto Social Alertas
        public const string Pages_Application_SocialConflictAlertDashboard = "Pages.Application.SocialConflictAlertDashboard";
        public const string Pages_Application_SocialConflictAlert = "Pages.Application.SocialConflictAlert";
        public const string Pages_Application_SocialConflictAlert_Create = "Pages.Application.SocialConflictAlert.Create";
        public const string Pages_Application_SocialConflictAlert_Edit = "Pages.Application.SocialConflictAlert.Edit";
        public const string Pages_Application_SocialConflictAlert_Delete = "Pages.Application.SocialConflictAlert.Delete";
        public const string Pages_Application_SocialConflictAlert_Send = "Pages.Application.SocialConflictAlert.Send";
        public const string Pages_Application_SocialConflictAlert_History = "Pages.Application.SocialConflictAlert.History";
        
        //SocialConflictSensible || Conflicto Social Sensibles
        public const string Pages_Application_SocialConflictSensible = "Pages.Application.SocialConflictSensible";
        public const string Pages_Application_SocialConflictSensibleDashboard = "Pages.Application.SocialConflictSensibleDashboard";
        public const string Pages_Application_SocialConflictSensible_Create = "Pages.Application.SocialConflictSensible.Create";
        public const string Pages_Application_SocialConflictSensible_Edit = "Pages.Application.SocialConflictSensible.Edit";
        public const string Pages_Application_SocialConflictSensible_Delete = "Pages.Application.SocialConflictSensible.Delete";
        public const string Pages_Application_SocialConflictSensible_Verification = "Pages.Application.SocialConflictSensible.Verification";
        public const string Pages_Application_SocialConflictSensible_Sugerence = "Pages.Application.SocialConflictSensible.Sugerence";
        public const string Pages_Application_SocialConflictSensible_Sugerence_Create = "Pages.Application.SocialConflictSensible.Sugerence.Create";
        public const string Pages_Application_SocialConflictSensible_Sugerence_Edit = "Pages.Application.SocialConflictSensible.Sugerence.Edit";
        public const string Pages_Application_SocialConflictSensible_Sugerence_Delete = "Pages.Application.SocialConflictSensible.Sugerence.Delete";
        public const string Pages_Application_SocialConflictSensible_Sugerence_Accept = "Pages.Application.SocialConflictSensible.Sugerence.Accept";
        public const string Pages_Application_SocialConflictSensible_Sugerence_Show = "Pages.Application.SocialConflictSensible.Sugerence.Show";

        //Record || Actas
        public const string Pages_Application_Record = "Pages.Application.Record";
        public const string Pages_Application_Record_Create = "Pages.Application.Record.Create";
        public const string Pages_Application_Record_Edit = "Pages.Application.Record.Edit";
        public const string Pages_Application_Record_Delete = "Pages.Application.Record.Delete";

        //Compromise || Compromisos
        public const string Pages_Application_Compromise = "Pages.Application.Compromise";
        public const string Pages_Application_Compromise_Create = "Pages.Application.Compromise.Create";
        public const string Pages_Application_Compromise_Edit = "Pages.Application.Compromise.Edit";
        public const string Pages_Application_Compromise_Delete = "Pages.Application.Compromise.Delete";

        //TaskManagement || Gestor de tareas
        public const string Pages_Application_TaskManagement = "Pages.Application.TaskManagement";
        public const string Pages_Application_TaskManagement_History = "Pages.Application.TaskManagement.History";

        //TaskManagement || Gestor de tareas
        public const string Pages_Application_SocialConflictTaskManagement = "Pages.Application.SocialConflictTaskManagement";
        public const string Pages_Application_SocialConflictTaskManagement_History = "Pages.Application.SocialConflictTaskManagement.History";

        //Order || Pedidos
        public const string Pages_Application_Order = "Pages.Application.Order";
        public const string Pages_Application_Order_Create = "Pages.Application.Order.Create";
        public const string Pages_Application_Order_Edit = "Pages.Application.Order.Edit";
        public const string Pages_Application_Order_Delete = "Pages.Application.Order.Delete";

        //Help memories || Ayuda Memorias
        public const string Pages_Application_HelpMemory = "Pages.Application.HelpMemory";
        public const string Pages_Application_HelpMemory_Create = "Pages.Application.HelpMemory.Create";
        public const string Pages_Application_HelpMemory_Edit = "Pages.Application.HelpMemory.Edit";
        public const string Pages_Application_HelpMemory_Delete = "Pages.Application.HelpMemory.Delete";

        //Compliance || Cumplimiento de compromisos
        public const string Pages_Application_Compliance = "Pages.Application.Compliance";

        //Report || Reportes
        public const string Pages_Report = "Pages.Report";

        //Report List || Lista de reportes
        public const string Pages_Report_SocialConflict = "Pages.Report.SocialConflict";
        public const string Pages_Report_SocialConflictAlert = "Pages.Report.SocialConflictAlert";
        public const string Pages_Report_SocialConflictSensible = "Pages.Report.SocialConflictSensible";
        public const string Pages_Report_HelpMemory_SocialConflict = "Pages.Report.HelpMemory.SocialConflict";
        public const string Pages_Report_HelpMemory_SocialConflictSensible = "Pages.Report.HelpMemory.SocialConflictSensible";
        public const string Pages_Report_ConflictTools_CrisisCommittee = "Pages.Report.ConflictTools.CrisisCommittee";
        public const string Pages_Report_ConflictTools_InteventionPlan = "Pages.Report.ConflictTools.InteventionPlan";
        public const string Pages_Report_ConflictTools_SectorMeetSession = "Pages.Report.ConflictTools.SectorMeetSession";
        public const string Pages_Report_SocialConflictAlertResume = "Pages.Report.SocialConflictAlertResume";

        //Management || Herramientas de Gestión
        public const string Pages_Management = "Pages.Management";

        //Prospective Risk || Riesgo prospectivo provincial
        public const string Pages_Management_ProspectiveRisk = "Pages.Management.ProspectiveRisk";
        public const string Pages_Management_ProspectiveRisk_Process = "Pages.ProspectiveRisk.Process";
        public const string Pages_Management_ProspectiveRisk_Edit = "Pages.Management.ProspectiveRisk.Edit";

        public const string Pages_Management_ProspectiveRisk_History = "Pages.Management.ProspectiveRisk.History";
        public const string Pages_Management_ProspectiveRisk_History_Delete = "Pages.Management.ProspectiveRisk.History.Delete";

        //Project Risk || Riesgo de proyecto estratégico
        public const string Pages_Management_ProjectRisk = "Pages.Management.ProjectRisk";
        public const string Pages_Management_ProjectRisk_Process = "Pages.Management.ProjectRisk.Process";
        public const string Pages_Management_ProjectRisk_Create = "Pages.Management.ProjectRisk.Create";
        public const string Pages_Management_ProjectRisk_Edit = "Pages.Management.ProjectRisk.Edit";
        public const string Pages_Management_ProjectRisk_Delete = "Pages.Management.ProjectRisk.Delete";

        public const string Pages_Management_ProjectRisk_History = "Pages.Management.ProjectRisk.History";
        public const string Pages_Management_ProjectRisk_History_Delete = "Pages.Management.ProjectRisk.History.Delete";

        //Dinamic Varible || Variables dinamicas
        public const string Pages_Management_DinamicVariable = "Pages.Management.DinamicVariable";
        public const string Pages_Management_DinamicVariable_Create = "Pages.Management.DinamicVariable.Create";
        public const string Pages_Management_DinamicVariable_Edit = "Pages.Management.DinamicVariable.Edit";
        public const string Pages_Management_DinamicVariable_Delete = "Pages.Management.DinamicVariable.Delete";
        public const string Pages_Management_DinamicVariable_Enable = "Pages.Management.DinamicVariable.Enable";
        public const string Pages_Management_DinamicVariable_Disable = "Pages.Management.DinamicVariable.Disable";

        //Dinamic Varible || Variables dinamicas
        public const string Pages_Management_StaticVariable = "Pages.Management.StaticVariable";
        public const string Pages_Management_StaticVariable_Create = "Pages.Management.StaticVariable.Create";
        public const string Pages_Management_StaticVariable_Edit = "Pages.Management.StaticVariable.Edit";
        public const string Pages_Management_StaticVariable_Delete = "Pages.Management.StaticVariable.Delete";
        public const string Pages_Management_StaticVariable_Enable = "Pages.Management.StaticVariable.Enable";
        public const string Pages_Management_StaticVariable_Disable = "Pages.Management.StaticVariable.Disable";

        //Project Stages
        public const string Pages_Management_ProjectStage = "Pages.Management.ProjectStage";
        public const string Pages_Management_ProjectStage_Create = "Pages.Management.ProjectStage.Create";
        public const string Pages_Management_ProjectStage_Edit = "Pages.Management.ProjectStage.Edit";
        public const string Pages_Management_ProjectStage_Delete = "Pages.Management.ProjectStage.Delete";
        public const string Pages_Management_ProjectStage_Enable = "Pages.Management.ProjectStage.Enable";
        public const string Pages_Management_ProjectStage_Disable = "Pages.Management.ProjectStage.Disable";

        //Conflict tools || Herramientas de conflictos
        public const string Pages_ConflictTools = "Pages.ConflictTools";

        public const string Pages_ConflictTools_InterventionPlan = "Pages.ConflictTools.InterventionPlan";
        public const string Pages_ConflictTools_InterventionPlan_Create = "Pages.ConflictTools.InterventionPlan.Create";
        public const string Pages_ConflictTools_InterventionPlan_Edit = "Pages.ConflictTools.InterventionPlan.Edit";
        public const string Pages_ConflictTools_InterventionPlan_Delete = "Pages.ConflictTools.InterventionPlan.Delete";

        public const string Pages_ConflictTools_CrisisCommittee = "Pages.ConflictTools.CrisisCommittee";
        public const string Pages_ConflictTools_CrisisCommittee_Create = "Pages.ConflictTools.CrisisCommittee.Create";
        public const string Pages_ConflictTools_CrisisCommittee_Edit = "Pages.ConflictTools.CrisisCommittee.Edit";
        public const string Pages_ConflictTools_CrisisCommittee_Delete = "Pages.ConflictTools.CrisisCommittee.Delete";

        //Sector Meets || Reuniones Multisectoriales para la Gestión de Conflictos
        public const string Pages_ConflictTools_SectorMeet = "Pages.ConflictTools.SectorMeet";
        public const string Pages_ConflictTools_SectorMeet_Create = "Pages.ConflictTools.SectorMeet.Create";
        public const string Pages_ConflictTools_SectorMeet_Edit = "Pages.ConflictTools.SectorMeet.Edit";
        public const string Pages_ConflictTools_SectorMeet_Delete = "Pages.ConflictTools.SectorMeet.Delete";

        //Dialog Spaces Document Types || Tipo de Documento Espacios de Diálogo
        public const string Pages_ConflictTools_DialogSpace = "Pages.ConflictTools.DialogSpace";
        public const string Pages_ConflictTools_DialogSpace_Create = "Pages.ConflictTools.DialogSpace.Create";
        public const string Pages_ConflictTools_DialogSpace_Edit = "Pages.ConflictTools.DialogSpace.Edit";
        public const string Pages_ConflictTools_DialogSpace_Delete = "Pages.ConflictTools.DialogSpace.Delete";

        //Quiz || Encuentas
        public const string Pages_Quiz = "Pages.Quiz";

        public const string Pages_Quiz_Customer = "Pages.Quiz.Customer";

        public const string Pages_Quiz_Platform = "Pages.Quiz.Platform";
        public const string Pages_Quiz_Platform_Edit = "Pages.Quiz.Platform.Edit";

        public const string Pages_Quiz_Responses = "Pages.Quiz.Responses";
        public const string Pages_Quiz_Responses_Edit = "Pages.Quiz.Responses.Edit";

        public const string Pages_Quiz_States = "Pages.Quiz.States";
        public const string Pages_Quiz_States_Create = "Pages.Quiz.States.Create";
        public const string Pages_Quiz_States_Edit = "Pages.Quiz.States.Edit";
        public const string Pages_Quiz_States_Delete = "Pages.Quiz.States.Delete";

        //App Permissions
        public const string App = "App";

        public const string App_SocialConflict = "App.SocialConflict";
        public const string App_SocialConflict_Create = "App.SocialConflict.Create";
        public const string App_SocialConflict_Edit = "App.SocialConflict.Edit";
        public const string App_SocialConflict_Resource = "App.SocialConflict.Resource";

        public const string App_SocialConflictSensible = "App.SocialConflictSensible";
        public const string App_SocialConflictSensible_Resource = "App.SocialConflictSensible.Resource";

        #region Recicled Permissions

        public const string Pages_Administration_Tenant_SubscriptionManagement = "Pages.Administration.Tenant.SubscriptionManagement";

        public const string Pages_DemoUiComponents = "Pages.DemoUiComponents";

        public const string Pages_Administration_UiCustomization = "Pages.Administration.UiCustomization";

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";
        public const string Pages_Editions_MoveTenantsToAnotherEdition = "Pages.Editions.MoveTenantsToAnotherEdition";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";
        public const string Pages_Administration_OrganizationUnits_ManageRoles = "Pages.Administration.OrganizationUnits.ManageRoles";

        public const string Pages_Administration_WebhookSubscription = "Pages.Administration.WebhookSubscription";
        public const string Pages_Administration_WebhookSubscription_Create = "Pages.Administration.WebhookSubscription.Create";
        public const string Pages_Administration_WebhookSubscription_Edit = "Pages.Administration.WebhookSubscription.Edit";
        public const string Pages_Administration_WebhookSubscription_ChangeActivity = "Pages.Administration.WebhookSubscription.ChangeActivity";
        public const string Pages_Administration_WebhookSubscription_Detail = "Pages.Administration.WebhookSubscription.Detail";
        public const string Pages_Administration_Webhook_ListSendAttempts = "Pages.Administration.Webhook.ListSendAttempts";
        public const string Pages_Administration_Webhook_ResendWebhook = "Pages.Administration.Webhook.ResendWebhook";

        public const string Pages_Administration_DynamicParameters = "Pages.Administration.DynamicParameters";
        public const string Pages_Administration_DynamicParameters_Create = "Pages.Administration.DynamicParameters.Create";
        public const string Pages_Administration_DynamicParameters_Edit = "Pages.Administration.DynamicParameters.Edit";
        public const string Pages_Administration_DynamicParameters_Delete = "Pages.Administration.DynamicParameters.Delete";

        public const string Pages_Administration_DynamicParameterValue = "Pages.Administration.DynamicParameterValue";
        public const string Pages_Administration_DynamicParameterValue_Create = "Pages.Administration.DynamicParameterValue.Create";
        public const string Pages_Administration_DynamicParameterValue_Edit = "Pages.Administration.DynamicParameterValue.Edit";
        public const string Pages_Administration_DynamicParameterValue_Delete = "Pages.Administration.DynamicParameterValue.Delete";

        public const string Pages_Administration_EntityDynamicParameters = "Pages.Administration.EntityDynamicParameters";
        public const string Pages_Administration_EntityDynamicParameters_Create = "Pages.Administration.EntityDynamicParameters.Create";
        public const string Pages_Administration_EntityDynamicParameters_Edit = "Pages.Administration.EntityDynamicParameters.Edit";
        public const string Pages_Administration_EntityDynamicParameters_Delete = "Pages.Administration.EntityDynamicParameters.Delete";

        public const string Pages_Administration_EntityDynamicParameterValue = "Pages.Administration.EntityDynamicParameterValue";
        public const string Pages_Administration_EntityDynamicParameterValue_Create = "Pages.Administration.EntityDynamicParameterValue.Create";
        public const string Pages_Administration_EntityDynamicParameterValue_Edit = "Pages.Administration.EntityDynamicParameterValue.Edit";
        public const string Pages_Administration_EntityDynamicParameterValue_Delete = "Pages.Administration.EntityDynamicParameterValue.Delete";
       
        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";

        //Actor || Actor
        public const string Pages_Maintenance_Actor = "Pages.Maintenance.Actor";
        public const string Pages_Maintenance_Actor_Create = "Pages.Maintenance.Actor.Create";
        public const string Pages_Maintenance_Actor_Edit = "Pages.Maintenance.Actor.Edit";
        public const string Pages_Maintenance_Actor_Delete = "Pages.Maintenance.Actor.Delete";
        #endregion

    }
}