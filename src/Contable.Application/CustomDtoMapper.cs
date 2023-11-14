using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.DynamicEntityParameters;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using Contable.Application;
using Contable.Application.SocialConflicts.Dto;
using Contable.Application.ResponsibleActors.Dto;
using Contable.Application.Ubigeos.Dto;
using Contable.Auditing.Dto;
using Contable.Authorization.Accounts.Dto;
using Contable.Authorization.Delegation;
using Contable.Authorization.Permissions.Dto;
using Contable.Authorization.Roles;
using Contable.Authorization.Roles.Dto;
using Contable.Authorization.Users;
using Contable.Authorization.Users.Delegation.Dto;
using Contable.Authorization.Users.Dto;
using Contable.Authorization.Users.Importing.Dto;
using Contable.Authorization.Users.Profile.Dto;
using Contable.Chat;
using Contable.Chat.Dto;
using Contable.DynamicEntityParameters.Dto;
using Contable.Editions;
using Contable.Editions.Dto;
using Contable.Friendships;
using Contable.Friendships.Cache;
using Contable.Friendships.Dto;
using Contable.Localization.Dto;
using Contable.MultiTenancy;
using Contable.MultiTenancy.Dto;
using Contable.MultiTenancy.HostDashboard.Dto;
using Contable.MultiTenancy.Payments;
using Contable.MultiTenancy.Payments.Dto;
using Contable.Notifications.Dto;
using Contable.Organizations.Dto;
using Contable.Sessions.Dto;
using Contable.WebHooks.Dto;
using Contable.Application.TerritorialUnits.Dto;
using Contable.Application.Utilities.Dto;
using Contable.Application.Records.Dto;
using Contable.Application.Uploaders.Dto;
using Contable.Application.Compromises.Dto;
using Contable.Application.TaskManagements.Dto;
using Contable.Application.Parameters.Dto;
using Contable.Application.External.Dto;
using Contable.Application.Orders.Dto;
using Abp.Application.Services.Dto;
using Contable.Application.Reporting.Dto;
using Contable.Application.PIPMef.Dto;
using Contable.Application.DinamicVariables.Dto;
using Contable.Application.StaticVariables.Dto;
using Contable.Application.ProspestiveRisks.Dto;
using Contable.Application.ProjectStages.Dto;
using Contable.Application.ProjectRisks.Dto;
using Contable.Application.Typologies.Dto;
using Contable.Application.SubTypologies.Dto;
using Contable.Application.Risks.Dto;
using Contable.Application.Facts.Dto;
using Contable.Application.Sectors.Dto;
using Contable.Application.Coordinators.Dto;
using Contable.Application.Managements.Dto;
using Contable.Application.ActorTypes.Dto;
using Contable.Application.ActorMovements.Dto;
using Contable.Application.Analysts.Dto;
using Contable.Application.Managers.Dto;
using Contable.Application.SocialConflictAlerts.Dto;
using Contable.Application.AlertRisks.Dto;
using Contable.Application.AlertSectors.Dto;
using Contable.Application.AlertSeals.Dto;
using Contable.Application.AlertDemands.Dto;
using Contable.Application.AlertResponsibles.Dto;
using Contable.Application.SocialConflictSensibles.Dto;
using Contable.Application.DirectoryGovernments.Dto;
using Contable.Application.DirectoryGovernmentSectors.Dto;
using Contable.Application.DirectorySectors.Dto;
using Contable.Application.DirectoryResponsibles.Dto;
using Contable.Application.DirectoryDialogs.Dto;
using Contable.Application.DirectoryIndustries.Dto;
using Contable.Application.SocialConflictTaskManagements.Dto;
using Contable.Application.DirectoryConflictTypes.Dto;
using Contable.Application.DirectoryGovernmentLevels.Dto;
using Contable.Application.HelpMemories.Dto;
using Contable.Application.InterventionPlanOptions.Dto;
using Contable.Application.InterventionPlans.Dto;
using Contable.Application.InterventionPlanEntities.Dto;
using Contable.Application.InterventionPlanActivities.Dto;
using Contable.Application.InterventionPlanRoles.Dto;
using Contable.Application.CrisisCommitteeJobs.Dto;
using Contable.Application.CrisisCommittees.Dto;
using Contable.Application.ResponsibleTypes.Dto;
using Contable.Application.ResponsibleSubTypes.Dto;
using Contable.Application.CompromiseLabels.Dto;
using Contable.Application.DirectoryGovernmentTypes.Dto;
using Contable.Application.QuizStates.Dto;
using Contable.Application.QuizAdministratives.Dto;
using Contable.Application.QuizDetails.Dto;
using Contable.Application.RecordResourceTypes.Dto;
using Contable.Application.CompromiseStates.Dto;
using Contable.Application.CompromiseSubStates.Dto;
using Contable.Application.SectorMeets.Dto;
using Contable.Application.SectorMeetSessions.Dto;
using Contable.Application.Portal.Dto;
using Contable.Application.DialogSpaces.Dto;
using Contable.Application.DialogSpaceDocumentTypes.Dto;
using Contable.Application.DialogSpaceHolidays.Dto;
using Contable.Application.DialogSpaceDocuments.Dto;
using Contable.Application.DialogSpaceTypes.Dto;
using Contable.Application.DialogSpaceDocumentSituations.Dto;
using Contable.Application.Meets.Dto;
using Contable.Application.MeetsResponsibles.Dto;

namespace Contable
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Level
            configuration.CreateMap<Level, SessionLevelDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();


            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore())
                .ForMember(user => user.Person, options => options.Ignore())
                .ForMember(user => user.AlertResponsible, options => options.Ignore());
            configuration.CreateMap<Person, UserPersonDto>();
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();
            configuration.CreateMap<AlertResponsible, UserAlertResponsibleDto>().ReverseMap();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            //Webhooks
            configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                    options => options.MapFrom(l => l.WebhookEvent.WebhookName))
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                    options => options.MapFrom(l => l.WebhookEvent.Data));

            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

            configuration.CreateMap<DynamicParameter, DynamicParameterDto>().ReverseMap();
            configuration.CreateMap<DynamicParameterValue, DynamicParameterValueDto>().ReverseMap();
            configuration.CreateMap<EntityDynamicParameter, EntityDynamicParameterDto>()
                .ForMember(dto => dto.DynamicParameterName,
                    options => options.MapFrom(entity => entity.DynamicParameter.ParameterName));
            configuration.CreateMap<EntityDynamicParameterDto, EntityDynamicParameter>();

            configuration.CreateMap<EntityDynamicParameterValue, EntityDynamicParameterValueDto>().ReverseMap();
            //User Delegations
            configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
            //Application

            //Ubigeos
            configuration.CreateMap<Department, DepartmentGetDto>();
            configuration.CreateMap<Department, DepartmentGetAllDto>();
            configuration.CreateMap<DepartmentCreateDto, Department>();
            configuration.CreateMap<DepartmentUpdateDto, Department>();

            configuration.CreateMap<Province, ProvinceGetDto>();
            configuration.CreateMap<Province, ProvinceGetAllDto>();
            configuration.CreateMap<ProvinceCreateDto, Province>();
            configuration.CreateMap<ProvinceUpdateDto, Province>();

            configuration.CreateMap<District, DistrictGetDto>();
            configuration.CreateMap<District, DistrictGetAllDto>();
            configuration.CreateMap<DistrictCreateDto, District>();
            configuration.CreateMap<DistrictUpdateDto, District>();

            configuration.CreateMap<Region, RegionGetDto>();
            configuration.CreateMap<Region, RegionGetAllDto>();
            configuration.CreateMap<District, RegionDistrictGetAllDto>();
            configuration.CreateMap<Province, RegionProvinceGetAllDto>();
            configuration.CreateMap<Department, RegionDepartmentGetAllDto>();
            configuration.CreateMap<Region, RegionGetAllDto>();
            configuration.CreateMap<RegionCreateDto, Region>();
            configuration.CreateMap<RegionUpdateDto, Region>();
            configuration.CreateMap<District, RegionDistrictGetDto>();
            configuration.CreateMap<Province, RegionProvinceGetDto>();
            configuration.CreateMap<Department, RegionDepartmentGetDto>();

            //Responsible Actors
            configuration.CreateMap<ResponsibleActorCreateDto, ResponsibleActor>()
                .ForMember(p => p.ResponsibleType, options => options.Ignore())
                .ForMember(p => p.ResponsibleSubType, options => options.Ignore());
            configuration.CreateMap<ResponsibleActorUpdateDto, ResponsibleActor>()
                .ForMember(p => p.ResponsibleType, options => options.Ignore())
                .ForMember(p => p.ResponsibleSubType, options => options.Ignore());
            configuration.CreateMap<ResponsibleActor, ResponsibleActorGetDto>();
            configuration.CreateMap<ResponsibleActor, ResponsibleActorGetAllDto>();
            configuration.CreateMap<ResponsibleActorGetDto, ResponsibleSubActor>();
            configuration.CreateMap<ResponsibleType, ResponsibleActorTypeDto>();
            configuration.CreateMap<ResponsibleSubType, ResponsibleActorSubTypeDto>();
            configuration.CreateMap<ResponsibleType, ResponsibleActorTypeRelationDto>();
            configuration.CreateMap<ResponsibleSubType, ResponsibleActorSubTypeRelationDto>();

            //Responsible Sub Actors
            configuration.CreateMap<ResponsibleSubActor, ResponsibleSubActorGetDto>().ReverseMap();
            configuration.CreateMap<ResponsibleSubActorCreateDto, ResponsibleSubActor>();
            configuration.CreateMap<ResponsibleSubActorUpdateDto, ResponsibleSubActor>();

            configuration.CreateMap<ResponsibleSubActor, ResponsibleActorGetDto>();

            //SocialConflict
            configuration.CreateMap<SocialConflict, SocialConflictGetDto>()
                .ForMember(p => p.CaseNameVerificationState, options => options.MapFrom(p => p.CaseNameVerification ? "true" : "false"))
                .ForMember(p => p.ProblemVerificationState, options => options.MapFrom(p => p.ProblemVerification ? "true" : "false"))
                .ForMember(p => p.DescriptionVerificationState, options => options.MapFrom(p => p.DescriptionVerification ? "true" : "false"));
            configuration.CreateMap<SocialConflict, SocialConflictGetAllDto>();
            configuration.CreateMap<SocialConflict, SocialConflictMatrizExportDto>();
            configuration.CreateMap<SocialConflict, SocialConflictGeoDto>();
            configuration.CreateMap<SocialConflictCreateDto, SocialConflict>()
                .ForMember(dto => dto.Analyst, options => options.Ignore())
                .ForMember(dto => dto.Coordinator, options => options.Ignore())
                .ForMember(dto => dto.Manager, options => options.Ignore())
                .ForMember(dto => dto.Typology, options => options.Ignore())
                .ForMember(dto => dto.SubTypology, options => options.Ignore())
                .ForMember(dto => dto.Sector, options => options.Ignore())
                .ForMember(dto => dto.Locations, options => options.Ignore())
                .ForMember(dto => dto.Actors, options => options.Ignore())
                .ForMember(dto => dto.GeneralFacts, options => options.Ignore())
                .ForMember(dto => dto.Sugerences, options => options.Ignore())
                .ForMember(dto => dto.Managements, options => options.Ignore())
                .ForMember(dto => dto.States, options => options.Ignore())
                .ForMember(dto => dto.ViolenceFacts, options => options.Ignore())
                .ForMember(dto => dto.Resources, options => options.Ignore())
                .ForMember(dto => dto.Notes, options => options.Ignore())
                .ForMember(dto => dto.Risks, options => options.Ignore());

            configuration.CreateMap<SocialConflictUpdateDto, SocialConflict>()
                .ForMember(dto => dto.Analyst, options => options.Ignore())
                .ForMember(dto => dto.Coordinator, options => options.Ignore())
                .ForMember(dto => dto.Manager, options => options.Ignore())
                .ForMember(dto => dto.Typology, options => options.Ignore())
                .ForMember(dto => dto.SubTypology, options => options.Ignore())
                .ForMember(dto => dto.Sector, options => options.Ignore())
                .ForMember(dto => dto.Locations, options => options.Ignore())
                .ForMember(dto => dto.Actors, options => options.Ignore())
                .ForMember(dto => dto.GeneralFacts, options => options.Ignore())
                .ForMember(dto => dto.Sugerences, options => options.Ignore())
                .ForMember(dto => dto.Managements, options => options.Ignore())
                .ForMember(dto => dto.States, options => options.Ignore())
                .ForMember(dto => dto.ViolenceFacts, options => options.Ignore())
                .ForMember(dto => dto.Risks, options => options.Ignore())
                .ForMember(dto => dto.Resources, options => options.Ignore())
                .ForMember(dto => dto.Notes, options => options.Ignore());

            configuration.CreateMap<SocialConflictGetDto, SocialConflict>();
            configuration.CreateMap<User, SocialConflictUserDto>();
            configuration.CreateMap<SocialConflictLocationDto, SocialConflictLocation>()
                .ReverseMap();
            configuration.CreateMap<TerritorialUnit, SocialConflictTerritorialUnitDto>();
            configuration.CreateMap<Department, SocialConflictDepartmentDto>();
            configuration.CreateMap<Province, SocialConflictProvinceDto>();
            configuration.CreateMap<District, SocialConflictDistrictDto>();
            configuration.CreateMap<Compromise, SocialConflictCompromiseGetAllDto>();
            configuration.CreateMap<Parameter, SocialConflictParameterDto>();
            configuration.CreateMap<UploadResourceOutputDto, SocialConflictManagementResource>();
            configuration.CreateMap<SocialConflictActor, SocialConflictActorGetAllDto>();
            configuration.CreateMap<UploadResourceOutputDto, SocialConflictResource>();
            configuration.CreateMap<SocialConflictResource, SocialConflictResourceDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictActor, SocialConflictActorLocationDto>()
                .ReverseMap();
            configuration.CreateMap<ActorType, SocialConflictActorTypeRelationDto>()
                .ReverseMap();
            configuration.CreateMap<ActorMovement, SocialConflictActorMovementRelationDto>()
                .ReverseMap();
            configuration.CreateMap<Person, SocialConflictPersonDto>()
                .ReverseMap();
            configuration.CreateMap<Typology, SocialConflictTypologyDto>()
                .ReverseMap();
            configuration.CreateMap<SubTypology, SocialConflictSubTypologyDto>()
                .ReverseMap();
            configuration.CreateMap<Fact, SocialConflictFactDto>()
                .ReverseMap();
            configuration.CreateMap<ActorType, SocialConflictActorTypeDto>()
                .ReverseMap();
            configuration.CreateMap<ActorMovement, SocialConflictActorMovementDto>()
                .ReverseMap();
            configuration.CreateMap<Sector, SocialConflictSectorDto>()
                .ReverseMap();
            configuration.CreateMap<Risk, SocialConflictRiskDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictGeneralFact, SocialConflictGeneralFactDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictSugerence, SocialConflictSugerenceDto>()
                .ReverseMap();
            configuration.CreateMap<Sector, SocialConflictSectorLocationDto>()
                .ReverseMap();
            configuration.CreateMap<Management, SocialConflictManagementDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictManagement, SocialConflictManagementLocationDto>()
                .ForMember(p => p.VerificationState, options => options.MapFrom(p => p.Verification ? "true" : "false"))
                .ForMember(p => p.VerificationLocation, options => options.MapFrom(p => p.Verification))
                .ReverseMap();
            configuration.CreateMap<SocialConflictManagementResource, SocialConflictManagementResourceDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictState, SocialConflictStateDto>()
                .ForMember(p => p.VerificationState, options => options.MapFrom(p => p.Verification ? "true" : "false"))
                .ForMember(p => p.VerificationLocation, options => options.MapFrom(p => p.Verification))
                .ReverseMap();
            configuration.CreateMap<Department, SocialConflictDepartmentRelationDto>()
                .ReverseMap();
            configuration.CreateMap<Province, SocialConflictProvinceRelationDto>()
                .ReverseMap();
            configuration.CreateMap<District, SocialConflictDistrictRelationDto>()
                .ReverseMap();
            configuration.CreateMap<Region, SocialConflictRegionDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictViolenceFact, SocialConflictViolenceFactDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictViolenceFactLocation, SocialConflictViolenceFactLocationDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictRisk, SocialConflictRiskLocationDto>()
                .ForMember(p => p.VerificationState, options => options.MapFrom(p => p.Verification ? "true" : "false"))
                .ForMember(p => p.VerificationLocation, options => options.MapFrom(p => p.Verification))
                .ReverseMap();
            configuration.CreateMap<SocialConflictCondition, SocialConflictConditionDto>()
                .ForMember(p => p.VerificationState, options => options.MapFrom(p => p.Verification ? "true" : "false"))
                .ForMember(p => p.VerificationLocation, options => options.MapFrom(p => p.Verification))
                .ReverseMap();
            configuration.CreateMap<SocialConflictNote, SocialConflictNoteLocationDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictNoteCreateDto, SocialConflictNote>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictNoteUpdateDto, SocialConflictNote>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictCreateDto, SocialConflictVerificationRequestDto>();
            configuration.CreateMap<SocialConflictUpdateDto, SocialConflictVerificationRequestDto>();

            configuration.CreateMap<SocialConflictLocation, SocialConflictLocationGeoDto>();
            configuration.CreateMap<TerritorialUnit, SocialConflictTerritorialUnitGeoDto>();
            configuration.CreateMap<Department, SocialConflictDepartmentGeoDto>();
            configuration.CreateMap<Province, SocialConflictProvinceGeoDto>();
            configuration.CreateMap<District, SocialConflictDistrictGeoDto>();
            configuration.CreateMap<Region, SocialConflictRegionGeoDto>();

            //TerritorialUnit
            configuration.CreateMap<TerritorialUnit, TerritorialUnitGetAllDto>();
            configuration.CreateMap<TerritorialUnit, TerritorialUnitGetDto>();
            configuration.CreateMap<TerritorialUnitCreateDto, TerritorialUnit>()
                .ForMember(p => p.Coordinators, options => options.Ignore());
            configuration.CreateMap<TerritorialUnitUpdateDto, TerritorialUnit>()
                .ForMember(p => p.Coordinators, options => options.Ignore());

            configuration.CreateMap<TerritorialUnitDepartment, TerritorialUnitDepartmentRelationDto>();
            configuration.CreateMap<Department, TerritorialUnitDepartmentDto>();
            configuration.CreateMap<Person, TerritorialUnitCoordinatorGetAllDto>();
            configuration.CreateMap<TerritorialUnitCoordinator, TerritorialUnitCoordinatorRelationDto>()
                .ReverseMap();
            configuration.CreateMap<Person, TerritorialUnitCoordinatorDto>()
                .ReverseMap();

            //Utilities / Utilidades
            configuration.CreateMap<TerritorialUnit, UtilityTerritorialUnitDto>();
            configuration.CreateMap<SocialConflict, UtilitySocialConflictDto>();
            configuration.CreateMap<SocialConflict, UtilitySocialConflictUserDto>();
            configuration.CreateMap<Record, UtilityRecordDto>();
            configuration.CreateMap<SocialConflictLocation, UtilitySocialConflictLocationDto>();
            configuration.CreateMap<Department, UtilityDepartmentDto>();
            configuration.CreateMap<Province, UtilityProvinceDto>();
            configuration.CreateMap<District, UtilityDistrictDto>();
            configuration.CreateMap<SocialConflict, UtilitySocialConflictForDialogDto>();
            configuration.CreateMap<Region, UtilityRegionGetAllDto>();
            configuration.CreateMap<Person, UtilityPersonGetAllDto>();
            configuration.CreateMap<User, UtilityPersonUserGetAllDto>();
            configuration.CreateMap<Typology, UtilityTypologyDto>();
            configuration.CreateMap<AlertResponsible, UtilitySocialConflictAlertResponsibleDto>();
            configuration.CreateMap<Person, UtilityPersonDto>();
            configuration.CreateMap<AlertRisk, UtilityAlertRiskDto>();
            configuration.CreateMap<AlertSeal, UtilitySealDto>();
            configuration.CreateMap<DirectoryGovernment, UtilityDirectoryGovernmentDto>();
            configuration.CreateMap<DirectoryGovernmentSector, UtilityDirectoryGovernmentSectorDto>();
            configuration.CreateMap<District, UtilityDirectoryDistrictDto>();
            configuration.CreateMap<Province, UtilityDirectoryProvinceDto>();
            configuration.CreateMap<Department, UtilityDirectoryDepartmentDto>();
            configuration.CreateMap<QuizState, UtilityQuizStateDto>();
            configuration.CreateMap<DirectoryIndustry, UtilityDirectoryIndustryGetAllDto>();
            configuration.CreateMap<DirectorySector, UtilityDirectoryIndustrySectorDto>();

            //Record // Actas
            configuration.CreateMap<RecordCreateDto, Record>()
                .ForMember(p => p.SocialConflict, options => options.Ignore());
            configuration.CreateMap<RecordUpdateDto, Record>()
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.Resources, options => options.Ignore());
            configuration.CreateMap<Record, RecordGetAllDto>();
            configuration.CreateMap<Record, RecordGetDto>();
            configuration.CreateMap<SocialConflict, RecordSocialConflictDto>().ReverseMap();
            configuration.CreateMap<UploadResourceOutputDto, RecordResource>();
            configuration.CreateMap<User, RecordUserDto>();
            configuration.CreateMap<RecordResource, RecordResourceDto>();
            configuration.CreateMap<RecordResourceType, RecordResourceTypeDto>();

            //Compromise 
            configuration.CreateMap<CompromiseCreateDto, Compromise>()
                .ForMember(p => p.Record, options => options.Ignore())
                .ForMember(p => p.CompromiseLocations, options => options.Ignore())
                .ForMember(p => p.Situations, options => options.Ignore())
                .ForMember(p => p.CompromiseState, options => options.Ignore())
                .ForMember(p => p.CompromiseSubState, options => options.Ignore())
                .ForMember(p => p.PIPMEF, options => options.Ignore())
                .ForMember(p => p.ResponsibleActor, options => options.Ignore())
                .ForMember(p => p.ResponsibleSubActor, options => options.Ignore())
                .ForMember(p => p.CompromiseInvolveds, options => options.Ignore())
                .ForMember(p => p.Status, options => options.Ignore())
                .ForMember(p => p.Timelines, options => options.Ignore())
                .ForMember(p => p.CompromiseLabel, options => options.Ignore())
                .ForMember(p => p.CompromiseResponsibles, options => options.Ignore());

            configuration.CreateMap<CompromiseUpdateDto, Compromise>()
                .ForMember(p => p.Record, options => options.Ignore())
                .ForMember(p => p.CompromiseLocations, options => options.Ignore())
                .ForMember(p => p.Situations, options => options.Ignore())
                .ForMember(p => p.CompromiseState, options => options.Ignore())
                .ForMember(p => p.CompromiseSubState, options => options.Ignore())
                .ForMember(p => p.PIPMEF, options => options.Ignore())
                .ForMember(p => p.ResponsibleActor, options => options.Ignore())
                .ForMember(p => p.ResponsibleSubActor, options => options.Ignore())
                .ForMember(p => p.CompromiseInvolveds, options => options.Ignore())
                .ForMember(p => p.Status, options => options.Ignore())
                .ForMember(p => p.Timelines, options => options.Ignore())
                .ForMember(p => p.CompromiseLabel, options => options.Ignore())
                .ForMember(p => p.CompromiseResponsibles, options => options.Ignore());

            configuration.CreateMap<ParameterDto, Parameter>().ReverseMap();
            configuration.CreateMap<Compromise, CompromiseGetDto>();
            configuration.CreateMap<Record, CompromiseRecordDto>();
            configuration.CreateMap<SocialConflictLocation, CompromiseLocationDto>();
            configuration.CreateMap<Compromise, CompromiseGetAllDto>();
            configuration.CreateMap<Compromise, CompromiseGetAllExcelDto>();
            configuration.CreateMap<Situation, CompromiseSituationDto>();
            configuration.CreateMap<SituationResource, CompromiseSituationResourceDto>();
            configuration.CreateMap<CompromiseSituationDto, Situation>();
            configuration.CreateMap<CompromiseSituationResourceDto, SituationResource>();
            configuration.CreateMap<ResponsibleActor, CompromiseResponsibleActorDto>();
            configuration.CreateMap<CompromiseInvolved, CompromiseInvolvedDto>().ReverseMap();
            configuration.CreateMap<CompromiseResponsible, CompromiseResponsibleDto>().ReverseMap();
            configuration.CreateMap<ResponsibleSubActor, CompromiseResponsibleSubActorDto>();
            configuration.CreateMap<UploadResourceOutputDto, SituationResource>();
            configuration.CreateMap<CompromiseLocation, CompromiseLocationDto>();
            configuration.CreateMap<SocialConflictLocation, CompromiseSocialConflictLocationDto>();
            configuration.CreateMap<TerritorialUnit, CompromiseTerritorialUnitDto>();
            configuration.CreateMap<Department, CompromiseDepartmentDto>();
            configuration.CreateMap<Province, CompromiseProvinceDto>();
            configuration.CreateMap<District, CompromiseDistrictDto>();
            configuration.CreateMap<CompromiseTimeLine, CompromiseTimeLineDto>();
            configuration.CreateMap<ResponsibleType, CompromiseResponsibleTypeDto>();
            configuration.CreateMap<ResponsibleSubType, CompromiseResponsibleSubTypeDto>();
            configuration.CreateMap<ResponsibleType, CompromiseResponsibleTypeRelationDto>().ReverseMap();
            configuration.CreateMap<ResponsibleSubType, CompromiseResponsibleSubTypeRelationDto>().ReverseMap();
            configuration.CreateMap<CompromiseLabel, CompromiseLabelLocationDto>().ReverseMap();
            configuration.CreateMap<User, CompromiseUserDto>();
            configuration.CreateMap<CompromiseState, CompromiseStateDto>();
            configuration.CreateMap<CompromiseState, CompromiseStateLocationDto>();
            configuration.CreateMap<CompromiseSubState, CompromiseSubStateDto>();
            configuration.CreateMap<CompromiseSubState, CompromiseSubStateLocationDto>();

            configuration.CreateMap<PIPMEF, PIPMEFDto>().ReverseMap();
            configuration.CreateMap<CompromiseUpdatePIPMEFDto, PIPMEF>();

            //Task, Activity, Comments, TaskExtend
            configuration.CreateMap<TaskManagementCreateDto, TaskManagement>()
                .ForMember(p => p.Compromise, options => options.Ignore())
                .ForMember(p => p.Comments, options => options.Ignore())
                .ForMember(p => p.Persons, options => options.Ignore());
            configuration.CreateMap<TaskManagementUpdateDto, TaskManagement>()
                .ForMember(p => p.Compromise, options => options.Ignore())
                .ForMember(p => p.Comments, options => options.Ignore())
                .ForMember(p => p.Persons, options => options.Ignore());
            configuration.CreateMap<TaskManagement, TaskManagementGetAllDto>();
            configuration.CreateMap<TaskManagement, TaskManagementGetDto>();
            configuration.CreateMap<Compromise, TaskManagementCompromiseGetAllDto>();
            configuration.CreateMap<Parameter, TaskManagementParameterDto>();
            configuration.CreateMap<Record, TaskManagementRecordDto>();
            configuration.CreateMap<SocialConflict, TaskManagementSocialConflictDto>();
            configuration.CreateMap<User, TaskManagementUserDto>();
            configuration.CreateMap<Person, TaskManagementPersonDto>();
            configuration.CreateMap<TaskManagementPerson, TaskManagementPersonRelationDto>();

            configuration.CreateMap<ResponsibleActor, TaskManagementResponsibleActorDto>();
            configuration.CreateMap<ResponsibleSubActor, TaskManagementResponsibleSubActorDto>();

            configuration.CreateMap<TaskManagementExtendCreateDto, TaskManagemetExtend>().ForMember(p => p.TaskManagement, options => options.Ignore());
            configuration.CreateMap<TaskManagementExtendUpdateDto, TaskManagemetExtend>().ForMember(p => p.TaskManagement, options => options.Ignore());
            configuration.CreateMap<TaskManagemetExtend, TaskManagementExtendGetDto>();
            configuration.CreateMap<TaskManagemetExtend, TaskManagementExtendGetAllDto>();

            configuration.CreateMap<TaskManagementCommentCreateDto, Comment>().ForMember(p => p.TaskManagement, options => options.Ignore());
            configuration.CreateMap<TaskManagementCommentUpdateDto, Comment>().ForMember(p => p.TaskManagement, options => options.Ignore());
            configuration.CreateMap<Comment, TaskManagementCommentGetAllDto>();
            configuration.CreateMap<Comment, TaskManagementCommentGetDto>();


            //Orders
            configuration.CreateMap<OrderCreateDto, Order>()
                .ForMember(p => p.PIPMEF, options => options.Ignore())
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.TerritorialUnit, options => options.Ignore())
                .ForMember(p => p.Department, options => options.Ignore())
                .ForMember(p => p.Province, options => options.Ignore())
                .ForMember(p => p.District, options => options.Ignore());
            configuration.CreateMap<OrderUpdateDto, Order>()
                .ForMember(p => p.PIPMEF, options => options.Ignore())
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.TerritorialUnit, options => options.Ignore())
                .ForMember(p => p.Department, options => options.Ignore())
                .ForMember(p => p.Province, options => options.Ignore())
                .ForMember(p => p.District, options => options.Ignore());

            configuration.CreateMap<Order, OrderGetDto>();
            configuration.CreateMap<PIPMEF, OrderPIPMEFDto>();
            configuration.CreateMap<TerritorialUnit, OrderTerritorialUnitDto>();
            configuration.CreateMap<Department, OrderDepartmentDto>();
            configuration.CreateMap<Province, OrderProvinceDto>();
            configuration.CreateMap<District, OrderDistrictDto>();
            configuration.CreateMap<SocialConflict, OrderSocialConflictDto>().ReverseMap();
            configuration.CreateMap<Order, OrderGetAllDto>();
            configuration.CreateMap<Order, OrderGetMatrixExcelDto>();

            configuration.CreateMap<CompromiseUpdatePIPMEFDto, PIPMEF>();
            configuration.CreateMap<OrderPIPMEFUpdateDto, PIPMEF>();

            configuration.CreateMap<Compromise, CompromiseGetMatrixExcelDto>();
            configuration.CreateMap<Compromise, CompromiseGetAllExcelDto>();
            configuration.CreateMap<Record, RecordGetMatrixExcelDto>();
            configuration.CreateMap<TaskManagement, TaskManagementGetMatrixExcelDto>();

            configuration.CreateMap<TerritorialUnit, UtilityTerritorialUnitDto>();
            configuration.CreateMap<Department, UtilityDepartmentDataDto>();
            configuration.CreateMap<Province, UtilityProvinceDataDto>();
            configuration.CreateMap<District, UtilityDistrictDataDto>();
            configuration.CreateMap<SocialConflict, UtilitySocialConflictDataDto>();
            configuration.CreateMap<SocialConflictLocation, UtilitySocialConflictLocationDataDto>();
            configuration.CreateMap<DialogSpaceType, UtilityDialogSpaceTypeDto>();

            //Portal
            configuration.CreateMap<TerritorialUnit, PortalTerritorialUnitDto>();
            configuration.CreateMap<Department, PortalDepartmentDto>();
            configuration.CreateMap<Province, PortalProvinceDto>();
            configuration.CreateMap<District, PortalDistrictDto>();
            configuration.CreateMap<SocialConflict, PortalSocialConflictDto>();
            configuration.CreateMap<SocialConflictLocation, PortalSocialConflictLocationDto>();
            configuration.CreateMap<TerritorialUnit, EntityDto>();
            configuration.CreateMap<Department, EntityDto>();
            configuration.CreateMap<Province, EntityDto>();
            configuration.CreateMap<District, EntityDto>();
            configuration.CreateMap<PIPMEF, PortalPipMEFGroupDto>();
            configuration.CreateMap<Risk, PortalRiskDto>();
            configuration.CreateMap<Typology, PortalTypologyDto>();
            configuration.CreateMap<QuizForm, PortalQuizFormDto>();
            configuration.CreateMap<QuizFormOption, PortalQuizFormOptionDto>();
            configuration.CreateMap<Parameter, ReportParameterDto>();

            //Dinamic Variable
            configuration.CreateMap<DinamicVariableCreateDto, DinamicVariable>();
            configuration.CreateMap<DinamicVariableUpdateDto, DinamicVariable>();
            configuration.CreateMap<DinamicVariable, DinamicVariableGetDto>();
            configuration.CreateMap<DinamicVariable, DinamicVariableGetAllDto>();
            configuration.CreateMap<TerritorialUnit, DinamicVariableTerritorialUnitDto>();
            configuration.CreateMap<Department, DinamicVariableDepartmentDto>();
            configuration.CreateMap<Province, DinamicVariableProvinceDto>();

            //Static Variable
            configuration.CreateMap<DinamicVariable, StaticVariableCuantitativeGetAllDto>();
            configuration.CreateMap<StaticVariableCreateDto, StaticVariable>()
                .ForMember(p => p.Options, p => p.Ignore())
                .ForMember(p => p.Details, p => p.Ignore());
            configuration.CreateMap<StaticVariableOptionCreateDto, StaticVariableOption>()
                .ForMember(p => p.DinamicVariable, p => p.Ignore())
                .ForMember(p => p.Details, p => p.Ignore());
            configuration.CreateMap<StaticVariableOptionDetailCreateDto, StaticVariableOptionDetail>();
            configuration.CreateMap<StaticVariableUpdateDto, StaticVariable>()
                .ForMember(p => p.Options, p => p.Ignore())
                .ForMember(p => p.Details, p => p.Ignore());
            configuration.CreateMap<StaticVariableOptionUpdateDto, StaticVariableOption>()
                .ForMember(p => p.Type, p => p.Ignore())
                .ForMember(p => p.DinamicVariable, p => p.Ignore())
                .ForMember(p => p.Details, p => p.Ignore());
            configuration.CreateMap<StaticVariableOptionDetailUpdateDto, StaticVariableOptionDetail>();
            configuration.CreateMap<StaticVariable, StaticVariableGetAllDto>();
            configuration.CreateMap<StaticVariable, StaticVariableGetDto>();
            configuration.CreateMap<StaticVariableOption, StaticVariableOptionGetDto>();
            configuration.CreateMap<StaticVariableOptionDetail, StaticVariableOptionDetailGetDto>();
            configuration.CreateMap<DinamicVariable, StaticVariableCuantitativeDto>();

            //Propestive Risk
            configuration.CreateMap<ProspectiveRisk, ProspectiveRiskGetAllDto>();
            configuration.CreateMap<TerritorialUnit, ProspectiveRiskTerritorialUnitDto>();
            configuration.CreateMap<Department, ProspectiveRiskDepartmentDto>();
            configuration.CreateMap<Province, ProspectiveRiskProvinceDto>();

            configuration.CreateMap<StaticVariable, ProspectiveRiskStaticVariableGetDto>();
            configuration.CreateMap<StaticVariableOption, ProspectiveRiskStaticVariableOptionGetDto>();
            configuration.CreateMap<StaticVariableOptionDetail, ProspectiveRiskStaticVariableOptionDetailGetDto>();
            configuration.CreateMap<DinamicVariable, ProspectiveRiskDinamicVariableGetDto>();
            configuration.CreateMap<ProspectiveRiskDetail, ProspectiveRiskDetailGetDto>();
            configuration.CreateMap<User, ProspectiveRiskUserDto>();
            configuration.CreateMap<ProspectiveRiskHistory, ProspectiveRiskHistoryDto>();
            configuration.CreateMap<ProspectiveRiskHistory, ProspectiveRiskHistoryGetDto>();
            configuration.CreateMap<StaticVariable, ProspectiveRiskHistoryVariableGetDto>();
            configuration.CreateMap<StaticVariableOption, ProspectiveRiskHistoryVariableOptionGetDto>();
            configuration.CreateMap<StaticVariableOptionDetail, ProspectiveRiskHistoryVariableOptionDetailGetDto>();

            //Project Stages
            configuration.CreateMap<StaticVariable, ProjectStageStaticVariableGetAllDto>();
            configuration.CreateMap<StaticVariableOption, ProjectStageStaticVariableOptionDto>();
            configuration.CreateMap<DinamicVariable, ProjectStageDinamicVariableDto>();
            configuration.CreateMap<ProjectStageCreateDto, ProjectStage>()
                .ForMember(p => p.Details, options => options.Ignore());
            configuration.CreateMap<ProjectStage, ProjectStageGetAllDto>();
            configuration.CreateMap<ProjectStage, ProjectStageGetDto>();
            configuration.CreateMap<ProjectStageDetail, ProjectStageDetailGetDto>();
            configuration.CreateMap<StaticVariable, ProjectStageStaticVariableGetDto>();
            configuration.CreateMap<ProjectStageUpdateDto, ProjectStage>()
                .ForMember(p => p.Details, options => options.Ignore());

            //Project Risk
            configuration.CreateMap<ProjectStage, ProjectRiskStageDto>();
            configuration.CreateMap<ProjectStageDetail, ProjectRiskStageDetailDto>();
            configuration.CreateMap<StaticVariable, ProjectRiskStaticVariableDto>();
            configuration.CreateMap<StaticVariableOption, ProjectRiskStaticVariableOptionDto>();
            configuration.CreateMap<StaticVariableOptionDetail, ProjectRiskStaticVariableOptionDetailDto>();
            configuration.CreateMap<DinamicVariable, ProjectRiskDinamicVariableDto>();
            configuration.CreateMap<Department, ProjectRiskDepartmentDto>();
            configuration.CreateMap<Province, ProjectRiskProvinceDto>();
            configuration.CreateMap<TerritorialUnitDepartment, ProjectRiskDepartmentTerrotorialUnitDto>();
            configuration.CreateMap<TerritorialUnit, ProjectRiskTerritorialUnitDto>();
            configuration.CreateMap<ProjectRisk, ProjectRiskGetDto>();
            configuration.CreateMap<ProjectRiskDetail, ProjectRiskDetailDto>();
            configuration.CreateMap<DinamicVariableDetail, ProjectRiskDinamicVariableDetailDto>();
            configuration.CreateMap<ProjectRiskCreateOrUpdateDto, ProjectRisk>()
                .ForMember(p => p.Details, options => options.Ignore());
            configuration.CreateMap<ProjectRiskDetailCreateOrUpdateDto, ProjectRiskDetail>();
            configuration.CreateMap<ProjectRisk, ProjectRiskGetAllDto>();
            configuration.CreateMap<Province, ProjectRiskProvinceGetAllDto>();
            configuration.CreateMap<Department, ProjectRiskDepartmentGetAllDto>();
            configuration.CreateMap<TerritorialUnitDepartment, ProjectRiskDepartmentTerritorialUnitGetAllDto>();
            configuration.CreateMap<TerritorialUnit, ProjectRiskTerritorialUnitGetAllDto>();
            configuration.CreateMap<ProjectStage, ProjectRiskStageGetAllDto>();
            configuration.CreateMap<User, ProjectRiskUserDto>();
            configuration.CreateMap<ProjectRiskHistory, ProjectRiskHistoryGetAllDto>();
            configuration.CreateMap<ProjectStage, ProjectRiskHistoryStageGetAllDto>();

            configuration.CreateMap<ProjectRiskHistory, ProjectRiskHistoryGetDto>();
            configuration.CreateMap<ProjectStage, ProjectRiskHistoryStageGetDto>()
                .ForMember(p => p.Details, options => options.Ignore());
            configuration.CreateMap<ProjectStageDetail, ProjectRiskHistoryStageDetailGetDto>();
            configuration.CreateMap<StaticVariable, ProjectRiskHistoryStaticVariableGetDto>()
                .ForMember(p => p.Options, options => options.Ignore());
            configuration.CreateMap<StaticVariableOption, ProjectRiskHistoryStaticVariableOptionGetDto>();
            configuration.CreateMap<StaticVariableOptionDetail, ProjectRiskHistoryStaticVariableOptionDetailGetDto>();

            //Social conflict actor types
            configuration.CreateMap<ActorType, ActorTypeGetAllDto>();
            configuration.CreateMap<ActorType, ActorTypeGetDto>();
            configuration.CreateMap<ActorTypeCreateDto, ActorType>();
            configuration.CreateMap<ActorTypeUpdateDto, ActorType>();

            //Social conflict actor movements
            configuration.CreateMap<ActorMovement, ActorMovementGetAllDto>();
            configuration.CreateMap<ActorMovement, ActorMovementGetDto>();
            configuration.CreateMap<ActorMovementCreateDto, ActorMovement>();
            configuration.CreateMap<ActorMovementUpdateDto, ActorMovement>();

            //Social conflict analysts
            configuration.CreateMap<Person, AnalystGetAllDto>();
            configuration.CreateMap<Person, AnalystGetDto>();
            configuration.CreateMap<AnalystCreateDto, Person>();
            configuration.CreateMap<AnalystUpdateDto, Person>();

            //Social conflict typologies
            configuration.CreateMap<Typology, TypologyGetAllDto>();
            configuration.CreateMap<SubTypology, TypologySubTypologyGetAllDto>();
            configuration.CreateMap<Typology, TypologyGetDto>();
            configuration.CreateMap<TypologyCreateDto, Typology>();
            configuration.CreateMap<TypologyUpdateDto, Typology>();

            //Social conflict sub typologies
            configuration.CreateMap<SubTypology, SubTypologyGetDto>();
            configuration.CreateMap<SubTypologyCreateDto, SubTypology>();
            configuration.CreateMap<SubTypologyUpdateDto, SubTypology>();

            //Social conflict risks
            configuration.CreateMap<Risk, RiskGetAllDto>();
            configuration.CreateMap<Risk, RiskGetDto>();
            configuration.CreateMap<RiskCreateDto, Risk>();
            configuration.CreateMap<RiskUpdateDto, Risk>();

            //Social conflict facts
            configuration.CreateMap<Fact, FactGetAllDto>();
            configuration.CreateMap<Fact, FactGetDto>();
            configuration.CreateMap<FactCreateDto, Fact>();
            configuration.CreateMap<FactUpdateDto, Fact>();

            //Social conflict sectors
            configuration.CreateMap<Sector, SectorGetAllDto>();
            configuration.CreateMap<Sector, SectorGetDto>();
            configuration.CreateMap<SectorCreateDto, Sector>();
            configuration.CreateMap<SectorUpdateDto, Sector>();

            //Social conflict coordinators
            configuration.CreateMap<Person, CoordinatorGetAllDto>();
            configuration.CreateMap<Person, CoordinatorGetDto>();
            configuration.CreateMap<CoordinatorCreateDto, Person>();
            configuration.CreateMap<CoordinatorUpdateDto, Person>();
            configuration.CreateMap<TerritorialUnitCoordinator, CoordinatorTerritorialUnitRelationDto>();
            configuration.CreateMap<TerritorialUnit, CoordinatorTerritorialUnitDto>();
            configuration.CreateMap<TerritorialUnit, CoordinatorTerritorialUnitGetAllDto>();

            //Social conflict managers
            configuration.CreateMap<Person, ManagerGetAllDto>();
            configuration.CreateMap<Person, ManagerGetDto>();
            configuration.CreateMap<ManagerCreateDto, Person>()
                .ForMember(p => p.TerritorialUnit, options => options.Ignore());
            configuration.CreateMap<ManagerUpdateDto, Person>()
                .ForMember(p => p.TerritorialUnit, options => options.Ignore());
            configuration.CreateMap<TerritorialUnit, ManagerTerritorialUnitDto>()
                .ReverseMap();

            //Social conflict management
            configuration.CreateMap<Management, ManagementGetAllDto>();
            configuration.CreateMap<Management, ManagementGetDto>();
            configuration.CreateMap<ManagementCreateDto, Management>();
            configuration.CreateMap<ManagementUpdateDto, Management>();

            //Social conflict alerts
            configuration.CreateMap<TerritorialUnitDepartment, SocialConflictAlertDepartmentDto>()
                .ForMember(p => p.TerritorialUnitId, options => options.MapFrom(p => p.TerritorialUnitId))
                .ForMember(p => p.Id, options => options.MapFrom(p => p.DepartmentId))
                .ForMember(p => p.Name, options => options.MapFrom(p => p.Department.Name))
                .ForMember(p => p.Provinces, options => options.MapFrom(p => p.Department.Provinces));
            configuration.CreateMap<TerritorialUnit, SocialConflictAlertTerritorialUnitDto>();
            configuration.CreateMap<Department, SocialConflictAlertDepartmentDto>();
            configuration.CreateMap<UploadResourceOutputDto, SocialConflictAlertResource>();
            configuration.CreateMap<Province, SocialConflictAlertProvinceDto>();
            configuration.CreateMap<District, SocialConflictAlertDistrictDto>();
            configuration.CreateMap<SocialConflictAlert, SocialConflictAlertGetDto>();
            configuration.CreateMap<SocialConflictAlert, SocialConflictAlertGetAllDto>();
            configuration.CreateMap<Person, SocialConflictAlertAnalystGetAllDto>();
            configuration.CreateMap<AlertResponsible, SocialConflictAlertResponsibleGetAllDto>();
            configuration.CreateMap<SocialConflictActor, SocialConflictAlertActorGetAllDto>();
            configuration.CreateMap<ActorType, SocialConflictAlertActorTypeGetAllDto>();
            configuration.CreateMap<ActorMovement, SocialConflictAlertActorMovementGetAllDto>();
            configuration.CreateMap<SocialConflictAlertRisk, SocialConflictAlertRiskLocationGetAllDto>();
            configuration.CreateMap<AlertRisk, SocialConflictAlertRiskGetAllDto>();
            configuration.CreateMap<User, SocialConflictAlertUserDto>();
            configuration.CreateMap<SocialConflictAlertCreateDto, SocialConflictAlert>()
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.Analyst, options => options.Ignore())
                .ForMember(p => p.Manager, options => options.Ignore())
                .ForMember(p => p.Coordinator, options => options.Ignore())
                .ForMember(p => p.AlertResponsible, options => options.Ignore())
                .ForMember(p => p.AlertDemand, options => options.Ignore())
                .ForMember(p => p.Typology, options => options.Ignore())
                .ForMember(p => p.SubTypology, options => options.Ignore())
                .ForMember(p => p.TerritorialUnit, options => options.Ignore())
                .ForMember(p => p.Locations, options => options.Ignore())
                .ForMember(p => p.Risks, options => options.Ignore())
                .ForMember(p => p.States, options => options.Ignore())
                .ForMember(p => p.Seals, options => options.Ignore())
                .ForMember(p => p.Resources, options => options.Ignore());
            configuration.CreateMap<SocialConflictAlertUpdateDto, SocialConflictAlert>()
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.Analyst, options => options.Ignore())
                .ForMember(p => p.Manager, options => options.Ignore())
                .ForMember(p => p.Coordinator, options => options.Ignore())
                .ForMember(p => p.AlertResponsible, options => options.Ignore())
                .ForMember(p => p.AlertDemand, options => options.Ignore())
                .ForMember(p => p.Typology, options => options.Ignore())
                .ForMember(p => p.SubTypology, options => options.Ignore())
                .ForMember(p => p.TerritorialUnit, options => options.Ignore())
                .ForMember(p => p.Locations, options => options.Ignore())
                .ForMember(p => p.Risks, options => options.Ignore())
                .ForMember(p => p.States, options => options.Ignore())
                .ForMember(p => p.Seals, options => options.Ignore())
                .ForMember(p => p.Resources, options => options.Ignore());
            configuration.CreateMap<SocialConflict, SocialConflictAlertConflictDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictAlertLocation, SocialConflictAlertLocationDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictAlertRisk, SocialConflictAlertRiskLocationDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictAlertSector, SocialConflictAlertSectorLocationDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictAlertState, SocialConflictAlertStateLocationDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictAlertSeal, SocialConflictAlertSealLocationDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictActor, SocialConflictAlertActorLocationDto>()
                .ReverseMap();
            configuration.CreateMap<TerritorialUnit, SocialConflictAlertTerritorialUnitLocationDto>()
                .ReverseMap();
            configuration.CreateMap<Department, SocialConflictAlertDepartmentLocationDto>()
                .ReverseMap();
            configuration.CreateMap<Province, SocialConflictAlertProvinceLocationDto>()
                .ReverseMap();
            configuration.CreateMap<District, SocialConflictAlertDistrictLocationDto>()
                .ReverseMap();
            configuration.CreateMap<AlertRisk, SocialConflictAlertRiskDto>()
                .ReverseMap();
            configuration.CreateMap<AlertSector, SocialConflictAlertSectorDto>()
                .ReverseMap();
            configuration.CreateMap<AlertSeal, SocialConflictAlertSealDto>()
                .ReverseMap();
            configuration.CreateMap<ActorType, SocialConflictAlertActorTypeDto>()
                .ReverseMap();
            configuration.CreateMap<ActorMovement, SocialConflictAlertActorMovementDto>()
                .ReverseMap();
            configuration.CreateMap<Person, SocialConflictAlertPersonDto>()
                .ReverseMap();
            configuration.CreateMap<Typology, SocialConflictAlertTypologyDto>()
                .ReverseMap();
            configuration.CreateMap<SubTypology, SocialConflictAlertSubTypologyDto>()
                .ReverseMap();
            configuration.CreateMap<AlertDemand, SocialConflictAlertDemandDto>()
                .ReverseMap();
            configuration.CreateMap<AlertResponsible, SocialConflictAlertResponsibleDto>()
                .ReverseMap();
            configuration.CreateMap<Region, SocialConflictAlertRegionLocationDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictAlertResource, SocialConflictAlertResourceDto>()
                .ReverseMap();

            //Social conflict alert risks
            configuration.CreateMap<AlertRisk, AlertRiskGetAllDto>();
            configuration.CreateMap<AlertRisk, AlertRiskGetDto>();
            configuration.CreateMap<AlertRiskCreateDto, AlertRisk>();
            configuration.CreateMap<AlertRiskUpdateDto, AlertRisk>();

            //Social conflict alert states
            configuration.CreateMap<AlertSector, AlertSectorGetAllDto>();
            configuration.CreateMap<AlertSector, AlertSectorGetDto>();
            configuration.CreateMap<AlertSectorCreateDto, AlertSector>();
            configuration.CreateMap<AlertSectorUpdateDto, AlertSector>();

            //Social conflict alert seals
            configuration.CreateMap<AlertSeal, AlertSealGetAllDto>();
            configuration.CreateMap<AlertSeal, AlertSealGetDto>();
            configuration.CreateMap<AlertSealCreateDto, AlertSeal>();
            configuration.CreateMap<AlertSealUpdateDto, AlertSeal>();

            //Social conflict alert demands
            configuration.CreateMap<AlertDemand, AlertDemandGetAllDto>();
            configuration.CreateMap<AlertDemand, AlertDemandGetDto>();
            configuration.CreateMap<AlertDemandCreateDto, AlertDemand>();
            configuration.CreateMap<AlertDemandUpdateDto, AlertDemand>();

            //Social conflict alert responsibles
            configuration.CreateMap<AlertResponsible, AlertResponsibleGetAllDto>();
            configuration.CreateMap<AlertResponsible, AlertResponsibleGetDto>();
            configuration.CreateMap<AlertResponsibleCreateDto, AlertResponsible>();
            configuration.CreateMap<AlertResponsibleUpdateDto, AlertResponsible>();

            //Region
            configuration.CreateMap<Region, RegionGetAllDto>();
            configuration.CreateMap<Region, RegionGetDto>();
            configuration.CreateMap<RegionCreateDto, Region>();
            configuration.CreateMap<RegionUpdateDto, Region>();

            //SocialConflictSensible
            configuration.CreateMap<SocialConflictSensible, SocialConflictSensibleGetDto>()
                .ForMember(p => p.CaseNameVerificationState, options => options.MapFrom(p => p.CaseNameVerification ? "true" : "false"))
                .ForMember(p => p.ProblemVerificationState, options => options.MapFrom(p => p.ProblemVerification ? "true" : "false"));
            configuration.CreateMap<SocialConflictSensible, SocialConflictSensibleGetAllDto>();
            configuration.CreateMap<SocialConflictSensibleCreateDto, SocialConflictSensible>()
                .ForMember(dto => dto.Analyst, options => options.Ignore())
                .ForMember(dto => dto.Coordinator, options => options.Ignore())
                .ForMember(dto => dto.Manager, options => options.Ignore())
                .ForMember(dto => dto.Typology, options => options.Ignore())
                .ForMember(dto => dto.Locations, options => options.Ignore())
                .ForMember(dto => dto.Actors, options => options.Ignore())
                .ForMember(dto => dto.GeneralFacts, options => options.Ignore())
                .ForMember(dto => dto.Sugerences, options => options.Ignore())
                .ForMember(dto => dto.Managements, options => options.Ignore())
                .ForMember(dto => dto.States, options => options.Ignore())
                .ForMember(dto => dto.Risks, options => options.Ignore());

            configuration.CreateMap<SocialConflictSensibleUpdateDto, SocialConflictSensible>()
                .ForMember(dto => dto.Analyst, options => options.Ignore())
                .ForMember(dto => dto.Coordinator, options => options.Ignore())
                .ForMember(dto => dto.Manager, options => options.Ignore())
                .ForMember(dto => dto.Typology, options => options.Ignore())
                .ForMember(dto => dto.Locations, options => options.Ignore())
                .ForMember(dto => dto.Actors, options => options.Ignore())
                .ForMember(dto => dto.GeneralFacts, options => options.Ignore())
                .ForMember(dto => dto.Sugerences, options => options.Ignore())
                .ForMember(dto => dto.Managements, options => options.Ignore())
                .ForMember(dto => dto.States, options => options.Ignore())
                .ForMember(dto => dto.Resources, options => options.Ignore())
                .ForMember(dto => dto.Notes, options => options.Ignore())
                .ForMember(dto => dto.Risks, options => options.Ignore());

            configuration.CreateMap<SocialConflictSensibleGetDto, SocialConflictSensible>();
            configuration.CreateMap<User, SocialConflictSensibleUserDto>();
            configuration.CreateMap<SocialConflictSensibleLocationDto, SocialConflictSensibleLocation>()
                .ReverseMap();
            configuration.CreateMap<TerritorialUnit, SocialConflictSensibleTerritorialUnitDto>();
            configuration.CreateMap<Department, SocialConflictSensibleDepartmentDto>();
            configuration.CreateMap<Province, SocialConflictSensibleProvinceDto>();
            configuration.CreateMap<District, SocialConflictSensibleDistrictDto>();
            configuration.CreateMap<UploadResourceOutputDto, SocialConflictSensibleManagementResource>();
            configuration.CreateMap<SocialConflictActor, SocialConflictSensibleActorLocationDto>()
                .ReverseMap();
            configuration.CreateMap<ActorType, SocialConflictSensibleActorTypeRelationDto>()
                .ReverseMap();
            configuration.CreateMap<ActorMovement, SocialConflictSensibleActorMovementRelationDto>()
                .ReverseMap();
            configuration.CreateMap<Person, SocialConflictSensiblePersonDto>()
                .ReverseMap();
            configuration.CreateMap<Typology, SocialConflictSensibleTypologyDto>()
                .ReverseMap();
            configuration.CreateMap<Fact, SocialConflictSensibleFactDto>()
                .ReverseMap();
            configuration.CreateMap<ActorType, SocialConflictSensibleActorTypeDto>()
                .ReverseMap();
            configuration.CreateMap<ActorMovement, SocialConflictSensibleActorMovementDto>()
                .ReverseMap();
            configuration.CreateMap<Risk, SocialConflictSensibleRiskDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictSensibleGeneralFact, SocialConflictSensibleGeneralFactDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictSensibleSugerence, SocialConflictSensibleSugerenceDto>()
                .ReverseMap();
            configuration.CreateMap<Management, SocialConflictSensibleManagementDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictSensibleManagement, SocialConflictSensibleManagementLocationDto>()
                .ForMember(p => p.VerificationState, options => options.MapFrom(p => p.Verification ? "true" : "false"))
                .ForMember(p => p.VerificationLocation, options => options.MapFrom(p => p.Verification))
                .ReverseMap();
            configuration.CreateMap<SocialConflictSensibleManagementResource, SocialConflictSensibleManagementResourceDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictSensibleState, SocialConflictSensibleStateDto>()
                .ForMember(p => p.VerificationState, options => options.MapFrom(p => p.Verification ? "true" : "false"))
                .ForMember(p => p.VerificationLocation, options => options.MapFrom(p => p.Verification))
                .ReverseMap();
            configuration.CreateMap<Department, SocialConflictSensibleDepartmentRelationDto>()
                .ReverseMap();
            configuration.CreateMap<Province, SocialConflictSensibleProvinceRelationDto>()
                .ReverseMap();
            configuration.CreateMap<District, SocialConflictSensibleDistrictRelationDto>()
                .ReverseMap();
            configuration.CreateMap<Region, SocialConflictSensibleRegionDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictSensibleRisk, SocialConflictSensibleRiskLocationDto>()
                .ForMember(p => p.VerificationState, options => options.MapFrom(p => p.Verification ? "true" : "false"))
                .ForMember(p => p.VerificationLocation, options => options.MapFrom(p => p.Verification))
                .ReverseMap();
            configuration.CreateMap<SocialConflictSensibleCondition, SocialConflictSensibleConditionDto>()
                .ForMember(p => p.VerificationState, options => options.MapFrom(p => p.Verification ? "true" : "false"))
                .ForMember(p => p.VerificationLocation, options => options.MapFrom(p => p.Verification))
                .ReverseMap();
            configuration.CreateMap<SocialConflictSensibleCreateDto, SocialConflictSensibleVerificationRequestDto>();
            configuration.CreateMap<SocialConflictSensibleUpdateDto, SocialConflictSensibleVerificationRequestDto>();
            configuration.CreateMap<UploadResourceOutputDto, SocialConflictSensibleResource>();
            configuration.CreateMap<SocialConflictSensibleResource, SocialConflictSensibleResourceDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictSensibleNote, SocialConflictSensibleNoteLocationDto>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictSensibleNoteCreateDto, SocialConflictSensibleNote>()
                .ReverseMap();
            configuration.CreateMap<SocialConflictSensibleNoteUpdateDto, SocialConflictSensibleNote>()
                .ReverseMap();

            //Directory Governments
            configuration.CreateMap<DirectoryGovernment, DirectoryGovernmentGetAllDto>();
            configuration.CreateMap<DirectoryGovernment, DirectoryGovernmentGetDto>();
            configuration.CreateMap<DirectoryGovernmentCreateDto, DirectoryGovernment>()
                .ForMember(p => p.District, options => options.Ignore())
                .ForMember(p => p.DirectoryGovernmentSector, options => options.Ignore())
                .ForMember(p => p.DirectoryGovernmentType, options => options.Ignore());
            configuration.CreateMap<DirectoryGovernmentUpdateDto, DirectoryGovernment>()
                .ForMember(p => p.District, options => options.Ignore())
                .ForMember(p => p.DirectoryGovernmentSector, options => options.Ignore())
                .ForMember(p => p.DirectoryGovernmentType, options => options.Ignore());
            configuration.CreateMap<Department, DirectoryGovernmentDepartmentDto>();
            configuration.CreateMap<Province, DirectoryGovernmentProvinceDto>();
            configuration.CreateMap<District, DirectoryGovernmentDistrictDto>();
            configuration.CreateMap<DirectoryGovernmentSector, DirectoryGovernmentSectorDto>();
            configuration.CreateMap<DirectoryGovernmentSector, DirectoryGovernmentSectorRelationDto>()
                .ReverseMap();
            configuration.CreateMap<District, DirectoryGovernmentDistrictRelationDto>()
                .ReverseMap();
            configuration.CreateMap<DirectoryGovernmentType, DirectoryGovernmentTypeDto>()
                .ReverseMap();


            //Directory Government Sectors
            configuration.CreateMap<DirectoryGovernmentSector, DirectoryGovernmentSectorGetAllDto>();
            configuration.CreateMap<DirectoryGovernmentSector, DirectoryGovernmentSectorGetDto>();
            configuration.CreateMap<DirectoryGovernmentSectorCreateDto, DirectoryGovernmentSector>();
            configuration.CreateMap<DirectoryGovernmentSectorUpdateDto, DirectoryGovernmentSector>();

            //Directory Sectors
            configuration.CreateMap<DirectorySector, DirectorySectorGetAllDto>();
            configuration.CreateMap<DirectorySector, DirectorySectorGetDto>();
            configuration.CreateMap<DirectorySectorCreateDto, DirectorySector>();
            configuration.CreateMap<DirectorySectorUpdateDto, DirectorySector>();

            //Directory Responsibles
            configuration.CreateMap<DirectoryResponsible, DirectoryResponsibleGetAllDto>();
            configuration.CreateMap<DirectoryResponsible, DirectoryResponsibleGetDto>();
            configuration.CreateMap<DirectoryResponsibleCreateDto, DirectoryResponsible>();
            configuration.CreateMap<DirectoryResponsibleUpdateDto, DirectoryResponsible>();

            //Directory Dialogs
            configuration.CreateMap<DirectoryDialog, DirectoryDialogGetAllDto>();
            configuration.CreateMap<DirectoryDialog, DirectoryDialogGetDto>();
            configuration.CreateMap<DirectoryDialogCreateDto, DirectoryDialog>()
                .ForMember(p => p.DirectoryResponsible, options => options.Ignore())
                .ForMember(p => p.DirectoryGovernment, options => options.Ignore());
            configuration.CreateMap<DirectoryDialogUpdateDto, DirectoryDialog>()
                .ForMember(p => p.DirectoryResponsible, options => options.Ignore())
                .ForMember(p => p.DirectoryGovernment, options => options.Ignore());
            configuration.CreateMap<DirectoryDialogGovernmentRelationDto, DirectoryGovernment>();
            configuration.CreateMap<DirectoryGovernment, DirectoryDialogGovernmentDto>();
            configuration.CreateMap<Department, DirectoryDialogDepartmentDto>();
            configuration.CreateMap<Province, DirectoryDialogProvinceDto>();
            configuration.CreateMap<District, DirectoryDialogDistrictDto>();
            configuration.CreateMap<DirectoryGovernmentSector, DirectoryDialogGovernmentSectorDto>();
            configuration.CreateMap<DirectoryResponsible, DirectoryDialogResponsibleDto>()
                .ReverseMap();

            //Directory Industries
            configuration.CreateMap<DirectoryIndustry, DirectoryIndustryGetAllDto>();
            configuration.CreateMap<DirectoryIndustry, DirectoryIndustryGetDto>();
            configuration.CreateMap<DirectoryIndustryCreateDto, DirectoryIndustry>()
                .ForMember(p => p.DirectorySector, options => options.Ignore())
                .ForMember(p => p.District, options => options.Ignore());
            configuration.CreateMap<DirectoryIndustryUpdateDto, DirectoryIndustry>()
                .ForMember(p => p.DirectorySector, options => options.Ignore())
                .ForMember(p => p.District, options => options.Ignore());
            configuration.CreateMap<Department, DirectoryIndustryDepartmentDto>();
            configuration.CreateMap<Province, DirectoryIndustryProvinceDto>();
            configuration.CreateMap<District, DirectoryIndustryDistrictDto>();
            configuration.CreateMap<Department, DirectoryIndustryDepartmentReverseDto>();
            configuration.CreateMap<Province, DirectoryIndustryProvinceReverseDto>();
            configuration.CreateMap<District, DirectoryIndustryDistrictReverseDto>();
            configuration.CreateMap<DirectorySector, DirectoryIndustrySectorDto>();
            configuration.CreateMap<DirectoryIndustrySectorLocationDto, DirectorySector>();
            configuration.CreateMap<DirectoryIndustryDistrictLocationDto, District>();

            //Directory Conflict Type
            configuration.CreateMap<DirectoryConflictType, DirectoryConflictTypeGetAllDto>();
            configuration.CreateMap<DirectoryConflictType, DirectoryConflictTypeGetDto>();
            configuration.CreateMap<DirectoryConflictTypeCreateDto, DirectoryConflictType>();
            configuration.CreateMap<DirectoryConflictTypeUpdateDto, DirectoryConflictType>();

            //Directory Government Level
            configuration.CreateMap<DirectoryGovernmentLevel, DirectoryGovernmentLevelGetAllDto>();
            configuration.CreateMap<DirectoryGovernmentLevel, DirectoryGovernmentLevelGetDto>();
            configuration.CreateMap<DirectoryGovernmentLevelCreateDto, DirectoryGovernmentLevel>();
            configuration.CreateMap<DirectoryGovernmentLevelUpdateDto, DirectoryGovernmentLevel>();

            //Social Conflict Task Managements
            configuration.CreateMap<SocialConflictTaskManagementCreateDto, SocialConflictTaskManagement>()
                .ForMember(p => p.Persons, options => options.Ignore());
            configuration.CreateMap<SocialConflictTaskManagementUpdateDto, SocialConflictTaskManagement>()
                .ForMember(p => p.Persons, options => options.Ignore())
                .ForMember(p => p.Resources, options => options.Ignore());
            configuration.CreateMap<SocialConflictTaskManagement, SocialConflictTaskManagementGetDto>();
            configuration.CreateMap<SocialConflictTaskManagementPerson, SocialConflictTaskManagementPersonRelationDto>();
            configuration.CreateMap<Person, SocialConflictTaskManagementPersonDto>();
            configuration.CreateMap<SocialConflictTaskManagementComment, SocialConflictTaskManagementCommentGetDto>();
            configuration.CreateMap<User, SocialConflictTaskManagementUserDto>();
            configuration.CreateMap<SocialConflictTaskManagementExtendCreateDto, SocialConflictTaskManagementExtend>()
                .ForMember(p => p.SocialConflictTaskManagement, options => options.Ignore());
            configuration.CreateMap<SocialConflictTaskManagementExtend, SocialConflictTaskManagementExtendGetDto>();
            configuration.CreateMap<SocialConflictTaskManagementExtend, SocialConflictTaskManagementExtendGetDto>();
            configuration.CreateMap<SocialConflictTaskManagementCommentCreateDto, SocialConflictTaskManagementComment>()
                .ForMember(p => p.SocialConflictTaskManagement, options => options.Ignore());
            configuration.CreateMap<SocialConflictTaskManagementComment, SocialConflictTaskManagementCommentGetDto>();
            configuration.CreateMap<SocialConflictTaskManagementResource, SocialConflictTaskManagementResourceGetDto>();
            configuration.CreateMap<UploadResourceOutputDto, SocialConflictTaskManagementResource>();

            //Help Memory
            configuration.CreateMap<HelpMemory, HelpMemoryGetAllDto>();
            configuration.CreateMap<HelpMemory, HelpMemoryGetDto>();
            configuration.CreateMap<HelpMemoryCreateDto, HelpMemory>()
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.SocialConflictSensible, options => options.Ignore())
                .ForMember(p => p.DirectoryGovernment, options => options.Ignore())
                .ForMember(p => p.Resources, options => options.Ignore());
            configuration.CreateMap<HelpMemoryUpdateDto, HelpMemory>()
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.SocialConflictSensible, options => options.Ignore())
                .ForMember(p => p.DirectoryGovernment, options => options.Ignore())
                .ForMember(p => p.Resources, options => options.Ignore());
            configuration.CreateMap<SocialConflict, HelpMemorySocialConflictDto>();
            configuration.CreateMap<SocialConflictSensible, HelpMemorySocialConflictSensibleDto>();
            configuration.CreateMap<DirectoryGovernment, HelpMemoryDirectoryGovernmentDto>();
            configuration.CreateMap<HelpMemoryResource, HelpMemoryResourceDto>();
            configuration.CreateMap<UploadResourceOutputDto, HelpMemoryResource>();
            configuration.CreateMap<User, HelpMemoryUserDto>();

            //Intervention Plan Option
            configuration.CreateMap<InterventionPlanOption, InterventionPlanOptionGetAllDto>();
            configuration.CreateMap<InterventionPlanOption, InterventionPlanOptionGetDto>();
            configuration.CreateMap<InterventionPlanOptionCreateDto, InterventionPlanOption>();
            configuration.CreateMap<InterventionPlanOptionUpdateDto, InterventionPlanOption>();

            //Intervention Plan
            configuration.CreateMap<InterventionPlanCreateDto, InterventionPlan>()
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.SocialConflictSensible, options => options.Ignore())
                .ForMember(p => p.Person, options => options.Ignore())
                .ForMember(p => p.Locations, options => options.Ignore())
                .ForMember(p => p.Risks, options => options.Ignore())
                .ForMember(p => p.Schedules, options => options.Ignore())
                .ForMember(p => p.Teams, options => options.Ignore())
                .ForMember(p => p.Solutions, options => options.Ignore());
            configuration.CreateMap<InterventionPlanUpdateDto, InterventionPlan>()
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.SocialConflictSensible, options => options.Ignore())
                .ForMember(p => p.Person, options => options.Ignore())
                .ForMember(p => p.Locations, options => options.Ignore())
                .ForMember(p => p.Risks, options => options.Ignore())
                .ForMember(p => p.Schedules, options => options.Ignore())
                .ForMember(p => p.Teams, options => options.Ignore())
                .ForMember(p => p.Solutions, options => options.Ignore());
            configuration.CreateMap<Department, InterventionPlanDepartmentDto>();
            configuration.CreateMap<Province, InterventionPlanProvinceDto>();
            configuration.CreateMap<District, InterventionPlanDistrictDto>();
            configuration.CreateMap<Region, InterventionPlanRegionRelationDto>();
            configuration.CreateMap<SocialConflict, InterventionPlanSocialConflictRelationDto>();
            configuration.CreateMap<SocialConflictSensible, InterventionPlanSocialConflictSensibleRelationDto>();
            configuration.CreateMap<InterventionPlanLocation, InterventionPlanLocationRelationDto>();
            configuration.CreateMap<InterventionPlan, InterventionPlanGetDto>();
            configuration.CreateMap<SocialConflictActor, InterventionPlanActorRelationDto>();
            configuration.CreateMap<User, InterventionPlanUserDto>();
            configuration.CreateMap<SocialConflictLocation, InterventionPlanLocationReferenceDto>();
            configuration.CreateMap<SocialConflictSensibleLocation, InterventionPlanLocationReferenceDto>();
            configuration.CreateMap<InterventionPlanTerritorialUnitRelationDto, TerritorialUnit>().ReverseMap();
            configuration.CreateMap<InterventionPlanOptionRelationDto, InterventionPlanOption>().ReverseMap();
            configuration.CreateMap<InterventionPlanActorMovementRelationDto, ActorMovement>().ReverseMap();
            configuration.CreateMap<InterventionPlanActorTypeRelationDto, ActorType>().ReverseMap();
            configuration.CreateMap<InterventionPlanPersonRelationDto, Person>().ReverseMap();
            configuration.CreateMap<InterventionPlanActorRelationDto, InterventionPlanActor>().ReverseMap();
            configuration.CreateMap<InterventionPlanStateRelationDto, InterventionPlanState>().ReverseMap();
            configuration.CreateMap<InterventionPlanMethodologyRelationDto, InterventionPlanMethodology>().ReverseMap();
            configuration.CreateMap<InterventionPlanRiskRelationDto, InterventionPlanRisk>().ReverseMap();
            configuration.CreateMap<InterventionPlanRiskLevelRelationDto, Risk>().ReverseMap();
            configuration.CreateMap<InterventionPlanActivityRelationDto, InterventionPlanActivity>().ReverseMap();
            configuration.CreateMap<InterventionPlanEntityRelationDto, InterventionPlanEntity>().ReverseMap();
            configuration.CreateMap<InterventionPlanScheduleRelationDto, InterventionPlanSchedule>().ReverseMap();
            configuration.CreateMap<InterventionPlanAlertResponsibleRelationDto, AlertResponsible>().ReverseMap();
            configuration.CreateMap<InterventionPlanDirectoryGovernmentRelationDto, DirectoryGovernment>().ReverseMap();
            configuration.CreateMap<InterventionPlanRoleRelationDto, InterventionPlanRole>().ReverseMap();
            configuration.CreateMap<InterventionPlanTeamRelationDto, InterventionPlanTeam>().ReverseMap();
            configuration.CreateMap<InterventionPlanSolutionRelationDto, InterventionPlanSolution>().ReverseMap();

            //Intervention Plan Activity
            configuration.CreateMap<InterventionPlanActivityCreateDto, InterventionPlanActivity>();
            configuration.CreateMap<InterventionPlanActivityUpdateDto, InterventionPlanActivity>();
            configuration.CreateMap<InterventionPlanActivity, InterventionPlanActivityGetDto>();
            configuration.CreateMap<InterventionPlanActivity, InterventionPlanActivityGetAllDto>();

            //Intervention Plan Entity
            configuration.CreateMap<InterventionPlanEntityCreateDto, InterventionPlanEntity>();
            configuration.CreateMap<InterventionPlanEntityUpdateDto, InterventionPlanEntity>();
            configuration.CreateMap<InterventionPlanEntity, InterventionPlanEntityGetDto>();
            configuration.CreateMap<InterventionPlanEntity, InterventionPlanEntityGetAllDto>();

            //Intervention Plan Roles
            configuration.CreateMap<InterventionPlanRoleCreateDto, InterventionPlanRole>();
            configuration.CreateMap<InterventionPlanRoleUpdateDto, InterventionPlanRole>();
            configuration.CreateMap<InterventionPlanRole, InterventionPlanRoleGetDto>();
            configuration.CreateMap<InterventionPlanRole, InterventionPlanRoleGetAllDto>();

            //Crisis Committee
            configuration.CreateMap<CrisisCommitteeCreateDto, CrisisCommittee>()
                .ForMember(p => p.InterventionPlan, options => options.Ignore())
                .ForMember(p => p.Person, options => options.Ignore())
                .ForMember(p => p.Teams, options => options.Ignore())
                .ForMember(p => p.Plans, options => options.Ignore())
                .ForMember(p => p.Actions, options => options.Ignore())
                .ForMember(p => p.Messages, options => options.Ignore())
                .ForMember(p => p.Channels, options => options.Ignore())
                .ForMember(p => p.Sectors, options => options.Ignore())
                .ForMember(p => p.Tasks, options => options.Ignore())
                .ForMember(p => p.Agreements, options => options.Ignore());
            configuration.CreateMap<CrisisCommitteeUpdateDto, CrisisCommittee>()
                .ForMember(p => p.InterventionPlan, options => options.Ignore())
                .ForMember(p => p.Teams, options => options.Ignore())
                .ForMember(p => p.Plans, options => options.Ignore())
                .ForMember(p => p.Actions, options => options.Ignore())
                .ForMember(p => p.Messages, options => options.Ignore())
                .ForMember(p => p.Channels, options => options.Ignore())
                .ForMember(p => p.Sectors, options => options.Ignore())
                .ForMember(p => p.Tasks, options => options.Ignore())
                .ForMember(p => p.Person, options => options.Ignore())
                .ForMember(p => p.Agreements, options => options.Ignore());
            configuration.CreateMap<CrisisCommittee, CrisisCommitteeGetAllDto>();
            configuration.CreateMap<CrisisCommittee, CrisisCommitteeGetDto>();
            configuration.CreateMap<User, CrisisCommitteeUserDto>();
            configuration.CreateMap<CrisisCommitteeInterventionPlanRelationDto, InterventionPlan>().ReverseMap();
            configuration.CreateMap<CrisisCommitteeTeamRelationDto, CrisisCommitteeTeam>().ReverseMap();
            configuration.CreateMap<CrisisCommitteeAlertResponsibleRelationDto, AlertResponsible>().ReverseMap();
            configuration.CreateMap<CrisisCommitteeJobRelationDto, CrisisCommitteeJob>().ReverseMap();
            configuration.CreateMap<CrisisCommitteePersonRelationDto, Person>().ReverseMap();
            configuration.CreateMap<CrisisCommitteePlanRelationDto, CrisisCommitteePlan>().ReverseMap();
            configuration.CreateMap<CrisisCommitteeActionRelationDto, CrisisCommitteeAction>().ReverseMap();
            configuration.CreateMap<CrisisCommitteeMessageRelationDto, CrisisCommitteeMessage>().ReverseMap();
            configuration.CreateMap<CrisisCommitteeChannelRelationDto, CrisisCommitteeChannel>().ReverseMap();
            configuration.CreateMap<CrisisCommitteeSectorRelationDto, CrisisCommitteeSector>().ReverseMap();
            configuration.CreateMap<CrisisCommitteeDirectoryGovernmentRelationDto, DirectoryGovernment>().ReverseMap();
            configuration.CreateMap<CrisisCommitteeDirectoryGovernmentSectorRelationDto, DirectoryGovernmentSector>().ReverseMap();
            configuration.CreateMap<CrisisCommitteeTaskRelationDto, CrisisCommitteeTask>().ReverseMap();
            configuration.CreateMap<CrisisCommitteeAgreementRelationDto, CrisisCommitteeAgreement>().ReverseMap();

            //Crisis Committee Job
            configuration.CreateMap<CrisisCommitteeJobCreateDto, CrisisCommitteeJob>();
            configuration.CreateMap<CrisisCommitteeJobUpdateDto, CrisisCommitteeJob>();
            configuration.CreateMap<CrisisCommitteeJob, CrisisCommitteeJobGetDto>();
            configuration.CreateMap<CrisisCommitteeJob, CrisisCommitteeJobGetAllDto>();

            //Responsible Type
            configuration.CreateMap<ResponsibleTypeCreateDto, ResponsibleType>();
            configuration.CreateMap<ResponsibleTypeUpdateDto, ResponsibleType>();
            configuration.CreateMap<ResponsibleType, ResponsibleTypeGetDto>();
            configuration.CreateMap<ResponsibleType, ResponsibleTypeGetAllDto>();
            configuration.CreateMap<ResponsibleSubType, ResponsibleTypeResponsibleSubTypeDto>();

            //Responsible Sub Type
            configuration.CreateMap<ResponsibleSubTypeCreateDto, ResponsibleSubType>()
                .ForMember(p => p.ResponsibleType, options => options.Ignore());
            configuration.CreateMap<ResponsibleSubTypeUpdateDto, ResponsibleSubType>()
                .ForMember(p => p.ResponsibleType, options => options.Ignore());
            configuration.CreateMap<ResponsibleSubType, ResponsibleSubTypeGetDto>();
            configuration.CreateMap<ResponsibleSubType, ResponsibleSubTypeGetAllDto>();
            configuration.CreateMap<ResponsibleType, ResponsibleSubTypeResponsibleTypeDto>();

            //Compromise Label
            configuration.CreateMap<CompromiseLabelCreateDto, CompromiseLabel>();
            configuration.CreateMap<CompromiseLabelUpdateDto, CompromiseLabel>();
            configuration.CreateMap<CompromiseLabel, CompromiseLabelGetDto>();
            configuration.CreateMap<CompromiseLabel, CompromiseLabelGetAllDto>();

            //Directory Government Type
            configuration.CreateMap<DirectoryGovernmentTypeCreateDto, DirectoryGovernmentType>();
            configuration.CreateMap<DirectoryGovernmentTypeUpdateDto, DirectoryGovernmentType>();
            configuration.CreateMap<DirectoryGovernmentType, DirectoryGovernmentTypeGetDto>();
            configuration.CreateMap<DirectoryGovernmentType, DirectoryGovernmentTypeGetAllDto>();

            //Quiz State
            configuration.CreateMap<QuizStateCreateDto, QuizState>();
            configuration.CreateMap<QuizStateUpdateDto, QuizState>();
            configuration.CreateMap<QuizState, QuizStateGetDto>();
            configuration.CreateMap<QuizState, QuizStateGetAllDto>();

            //Quiz Forms
            configuration.CreateMap<QuizForm, QuizAdministrativeFormDto>()
                .ForMember(p => p.Options, options => options.Ignore());
            configuration.CreateMap<QuizFormOption, QuizAdministrativeFormOptionDto>();
            configuration.CreateMap<UploadResourceOutputDto, QuizCompleteResource>();

            //Quiz Details
            configuration.CreateMap<QuizComplete, QuizDetailGetAllDto>();
            configuration.CreateMap<QuizComplete, QuizDetailGetDto>();
            configuration.CreateMap<QuizState, QuizDetailStateDto>();
            configuration.CreateMap<User, QuizDetailUserDto>();
            configuration.CreateMap<QuizCompleteForm, QuizDetailFormDto>();
            configuration.CreateMap<QuizCompleteOption, QuizDetailFormOptionDto>();
            configuration.CreateMap<QuizCompleteResource, QuizDetailResourceDto>();

            //Record Resource Types
            configuration.CreateMap<RecordResourceTypeCreateDto, RecordResourceType>();
            configuration.CreateMap<RecordResourceTypeUpdateDto, RecordResourceType>();
            configuration.CreateMap<RecordResourceType, RecordResourceTypeGetDto>();
            configuration.CreateMap<RecordResourceType, RecordResourceTypeGetAllDto>();

            //Compromise States
            configuration.CreateMap<CompromiseStateCreateDto, CompromiseState>();
            configuration.CreateMap<CompromiseStateUpdateDto, CompromiseState>();
            configuration.CreateMap<CompromiseState, CompromiseStateGetDto>();
            configuration.CreateMap<CompromiseState, CompromiseStateGetAllDto>();
            configuration.CreateMap<CompromiseSubState, CompromiseSubStateRelationDto>();

            //Compromise Sub States
            configuration.CreateMap<CompromiseSubStateCreateDto, CompromiseSubState>()
                .ForMember(p => p.CompromiseState, options => options.Ignore());
            configuration.CreateMap<CompromiseSubStateUpdateDto, CompromiseSubState>()
                .ForMember(p => p.CompromiseState, options => options.Ignore());
            configuration.CreateMap<CompromiseSubState, CompromiseSubStateGetDto>();
            configuration.CreateMap<CompromiseSubState, CompromiseSubStateGetAllDto>();
            configuration.CreateMap<CompromiseState, CompromiseSubStateStateRelationDto>();

            //Sector Meets
            configuration.CreateMap<SectorMeetCreateDto, SectorMeet>()
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.TerritorialUnit, options => options.Ignore());
            configuration.CreateMap<SectorMeetUpdateDto, SectorMeet>()
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.TerritorialUnit, options => options.Ignore());
            configuration.CreateMap<SectorMeet, SectorMeetGetDto>();
            configuration.CreateMap<SectorMeet, SectorMeetGetAllDto>();
            configuration.CreateMap<TerritorialUnit, SectorMeetTerritorialUnitRelationDto>();
            configuration.CreateMap<SocialConflict, SectorMeetSocialConflictRelationDto>();

            //Meets

            configuration.CreateMap<MeetCreateDto, Meet>()
                .ForMember(dto => dto.SocialConflict, opt => opt.Ignore())
                .ForMember(prop => prop.SocialConflictId, opt => opt.MapFrom(p => p.SocialConflict.Id));


            configuration.CreateMap<Meet, MeetGetDto>()
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.Participants, options => options.Ignore())
                ;

            configuration.CreateMap<MeetParticipants, MeetParticipantsGetDto>()
                .ForMember(p => p.Meets, options => options.Ignore())
                ;
            configuration.CreateMap<MeetParticipantsCreateDto, MeetParticipants>()
                .ForMember(p => p.Meets, options => options.Ignore());



            //Sector Meet Sessions
            configuration.CreateMap<SectorMeetSessionCreateDto, SectorMeetSession>()
                .ForMember(p => p.SectorMeet, options => options.Ignore())
                .ForMember(p => p.Person, options => options.Ignore())
                .ForMember(p => p.Department, options => options.Ignore())
                .ForMember(p => p.Province, options => options.Ignore())
                .ForMember(p => p.District, options => options.Ignore())
                .ForMember(p => p.Actions, options => options.Ignore())
                .ForMember(p => p.Agreements, options => options.Ignore())
                .ForMember(p => p.CriticalAspects, options => options.Ignore())
                .ForMember(p => p.Schedules, options => options.Ignore())
                .ForMember(p => p.Summaries, options => options.Ignore())
                .ForMember(p => p.Leaders, options => options.Ignore())
                .ForMember(p => p.Resources, options => options.Ignore());
            configuration.CreateMap<SectorMeetSessionUpdateDto, SectorMeetSession>()
                .ForMember(p => p.SectorMeet, options => options.Ignore())
                .ForMember(p => p.Person, options => options.Ignore())
                .ForMember(p => p.Department, options => options.Ignore())
                .ForMember(p => p.Province, options => options.Ignore())
                .ForMember(p => p.District, options => options.Ignore())
                .ForMember(p => p.Actions, options => options.Ignore())
                .ForMember(p => p.Agreements, options => options.Ignore())
                .ForMember(p => p.CriticalAspects, options => options.Ignore())
                .ForMember(p => p.Schedules, options => options.Ignore())
                .ForMember(p => p.Summaries, options => options.Ignore())
                .ForMember(p => p.Leaders, options => options.Ignore())
                .ForMember(p => p.Resources, options => options.Ignore());
            configuration.CreateMap<SectorMeetSession, SectorMeetSessionGetDto>();
            configuration.CreateMap<SectorMeetSession, SectorMeetSessionGetAllDto>();
            configuration.CreateMap<SectorMeet, SectorMeetSessionSectorMeetRelationDto>();
            configuration.CreateMap<Department, SectorMeetSessionDepartmentRelationDto>();
            configuration.CreateMap<Department, SectorMeetSessionDepartmentReverseDto>();
            configuration.CreateMap<Province, SectorMeetSessionProvinceRelationDto>();
            configuration.CreateMap<Province, SectorMeetSessionProvinceReverseDto>();
            configuration.CreateMap<District, SectorMeetSessionDistrictRelationDto>();
            configuration.CreateMap<District, SectorMeetSessionDistrictReverseDto>();
            configuration.CreateMap<SectorMeetSessionAction, SectorMeetSessionActionRelationDto>();
            configuration.CreateMap<SectorMeetSessionAgreement, SectorMeetSessionAgreementRelationDto>();
            configuration.CreateMap<SectorMeetSessionCriticalAspect, SectorMeetSessionCriticalAspectRelationDto>();
            configuration.CreateMap<SectorMeetSessionSchedule, SectorMeetSessionScheduleRelationDto>();
            configuration.CreateMap<SectorMeetSessionSummary, SectorMeetSessionSummaryRelationDto>();
            configuration.CreateMap<SectorMeetSessionResource, SectorMeetSessionResourceRelationDto>();
            configuration.CreateMap<Person, SectorMeetSessionPersonRelationDto>();
            configuration.CreateMap<UploadResourceOutputDto, SectorMeetSessionResource>();
            configuration.CreateMap<SectorMeetSessionAttachmentDto, UploadResourceInputDto>();
            configuration.CreateMap<SectorMeetSessionLeader, SectorMeetSessionLeaderRelationDto>();
            configuration.CreateMap<SectorMeetSessionLeader, SectorMeetSessionLeaderGetAllDto>();
            configuration.CreateMap<SectorMeetSessionTeam, SectorMeetSessionTeamRelationDto>();
            configuration.CreateMap<DirectoryGovernment, SectorMeetSessionDirectoryGovernmentRelationDto>();
            configuration.CreateMap<DirectoryIndustry, SectorMeetSessionDirectoryIndustryRelationDto>();
            configuration.CreateMap<DirectorySector, SectorMeetSessionDirectorySectorRelationDto>();
            configuration.CreateMap<DirectoryGovernmentSector, SectorMeetSessionDirectorySectorRelationDto>();
            configuration.CreateMap<District, SectorMeetSessionDirectoryDistrictRelationDto>();
            configuration.CreateMap<Province, SectorMeetSessionDirectoryProvinceRelationDto>();
            configuration.CreateMap<Department, SectorMeetSessionDirectoryDepartmentRelationDto>();

            //Dialog Spaces
            configuration.CreateMap<DialogSpaceCreateDto, DialogSpace>()
                .ForMember(p => p.DialogSpaceType, options => options.Ignore())
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.Person, options => options.Ignore())
                .ForMember(p => p.Leaders, options => options.Ignore())
                .ForMember(p => p.Locations, options => options.Ignore());
            configuration.CreateMap<DialogSpaceUpdateDto, DialogSpace>()
                .ForMember(p => p.DialogSpaceType, options => options.Ignore())
                .ForMember(p => p.SocialConflict, options => options.Ignore())
                .ForMember(p => p.Person, options => options.Ignore())
                .ForMember(p => p.Leaders, options => options.Ignore())
                .ForMember(p => p.Locations, options => options.Ignore());
            configuration.CreateMap<DialogSpace, DialogSpaceGetDto>();
            configuration.CreateMap<DialogSpace, DialogSpaceGetAllDto>();
            configuration.CreateMap<DialogSpaceLocation, DialogSpaceLocationRelationDto>();
            configuration.CreateMap<DialogSpaceLeader, DialogSpaceLeaderRelationDto>();
            configuration.CreateMap<DialogSpaceTeam, DialogSpaceTeamRelationDto>();
            configuration.CreateMap<SocialConflictLocation, DialogSpaceLocationReferenceDto>();
            configuration.CreateMap<TerritorialUnit, DialogSpaceTerritorialUnitDto>();
            configuration.CreateMap<TerritorialUnit, DialogSpaceTerritorialUnitRelationDto>();
            configuration.CreateMap<Department, DialogSpaceDepartmentDto>();
            configuration.CreateMap<Department, DialogSpaceDepartmentRelationDto>();
            configuration.CreateMap<Department, DialogSpaceDirectoryDepartmentReverseDto>();
            configuration.CreateMap<District, DialogSpaceDistrictDto>();
            configuration.CreateMap<District, DialogSpaceDistrictRelationDto>();
            configuration.CreateMap<District, DialogSpaceDirectoryDistrictRelationDto>();
            configuration.CreateMap<Province, DialogSpaceProvinceDto>();
            configuration.CreateMap<Province, DialogSpaceProvinceRelationDto>();
            configuration.CreateMap<Province, DialogSpaceDirectoryProvinceRelationDto>();
            configuration.CreateMap<Region, DialogSpaceRegionRelationDto>();
            configuration.CreateMap<SocialConflict, DialogSpaceSocialConflictRelationDto>();
            configuration.CreateMap<User, DialogSpaceUserDto>();
            configuration.CreateMap<DirectoryGovernment, DialogSpaceDirectoryGovernmentRelationDto>();
            configuration.CreateMap<DirectoryGovernmentSector, DialogSpaceDirectorySectorRelationDto>();
            configuration.CreateMap<DialogSpaceType, DialogSpaceDialogSpaceTypeRelatioDto>();
            configuration.CreateMap<Person, DialogSpacePersonRelationDto>();

            //Dialog Space Document Type
            configuration.CreateMap<DialogSpaceDocumentType, DialogSpaceDocumentTypeGetAllDto>();
            configuration.CreateMap<DialogSpaceDocumentType, DialogSpaceDocumentTypeGetDto>();
            configuration.CreateMap<DialogSpaceDocumentTypeCreateDto, DialogSpaceDocumentType>();
            configuration.CreateMap<DialogSpaceDocumentTypeUpdateDto, DialogSpaceDocumentType>();

            //Dialog Space Holiday
            configuration.CreateMap<DialogSpaceHoliday, DialogSpaceHolidayGetAllDto>();
            configuration.CreateMap<DialogSpaceHoliday, DialogSpaceHolidayGetDto>();
            configuration.CreateMap<DialogSpaceHolidayCreateDto, DialogSpaceHoliday>();
            configuration.CreateMap<DialogSpaceHolidayUpdateDto, DialogSpaceHoliday>();

            //Dialog Space Type
            configuration.CreateMap<DialogSpaceType, DialogSpaceTypeGetAllDto>();
            configuration.CreateMap<DialogSpaceType, DialogSpaceTypeGetDto>();
            configuration.CreateMap<DialogSpaceTypeCreateDto, DialogSpaceType>();
            configuration.CreateMap<DialogSpaceTypeUpdateDto, DialogSpaceType>();

            //Dialog Space Document Situation
            configuration.CreateMap<DialogSpaceDocumentSituation, DialogSpaceDocumentSituationGetAllDto>();
            configuration.CreateMap<DialogSpaceDocumentSituation, DialogSpaceDocumentSituationGetDto>();
            configuration.CreateMap<DialogSpaceDocumentSituationCreateDto, DialogSpaceDocumentSituation>();
            configuration.CreateMap<DialogSpaceDocumentSituationUpdateDto, DialogSpaceDocumentSituation>();

            //Dialog Space Document
            configuration.CreateMap<DialogSpaceDocumentCreateDto, DialogSpaceDocument>()
                .ForMember(p => p.DialogSpace, options => options.Ignore())
                .ForMember(p => p.DialogSpaceDocumentSituation, options => options.Ignore())
                .ForMember(p => p.DialogSpaceDocumentType, options => options.Ignore());
            configuration.CreateMap<DialogSpaceDocumentUpdateDto, DialogSpaceDocument>()
                .ForMember(p => p.DialogSpace, options => options.Ignore())
                .ForMember(p => p.DialogSpaceDocumentSituation, options => options.Ignore())
                .ForMember(p => p.DialogSpaceDocumentType, options => options.Ignore());
            configuration.CreateMap<DialogSpaceDocument, DialogSpaceDocumentGetDto>();
            configuration.CreateMap<DialogSpaceDocument, DialogSpaceDocumentGetAllDto>();
            configuration.CreateMap<DialogSpace, DialogSpaceDocumentDialogSpaceRelationDto>();
            configuration.CreateMap<SocialConflict, DialogSpaceDocumentSocialConflictRelationDto>();
            configuration.CreateMap<DialogSpaceDocumentType, DialogSpaceDocumentTypeRelationDto>();
            configuration.CreateMap<DialogSpaceDocumentResource, DialogSpaceDocumentResourceDto>();
            configuration.CreateMap<UploadResourceOutputDto, DialogSpaceDocumentResource>();
            configuration.CreateMap<DialogSpaceDocumentSituation, DialogSpaceDocumentSituationRelationDto>();
            configuration.CreateMap<User, DialogSpaceDocumentUserDto>();
        }
    }
}
