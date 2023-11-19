using Contable.Application;
using Contable.Application.Reports.Dto;
using System.Threading.Tasks;

namespace Contable.Manager.Base
{
    public interface IReportManagerBase
    {
        Task<JasperDocument> Create(JasperReportRequest input);
        string CreateAlertReportName(SocialConflictAlert alert, ReportType type);
        string CreateAlertResourceName(SocialConflictAlert alert, string extension, int index);
        string CreateAlertResumeReportName(SocialConflictAlert alert, ReportType type);
        string CreateCrisisCommitteeReportName(CrisisCommittee crisisCommittee, ReportType type);
        string CreateHelpMemoryReportName(SocialConflict socialConflict, ReportType type);
        string CreateHelpMemoryReportName(SocialConflictSensible socialConflictSensible, ReportType type);
        string CreateInterventionPlanReportName(InterventionPlan interventionPlan, ReportType type);
        string CreateSectorMeetSessionReportName(SectorMeet sectorMeet, SectorMeetSession sectorMeetSession, ReportType type);
        string CreateSensibleReportName(SocialConflictSensible sensible, ReportType type);
        string CreateSocialConflictReportName(SocialConflict socialConflict, ReportType type);
        string CreateActasReportName(ReportType type);
        string GetType(ReportType type);
        bool ReportServerEnabled();
    }
}