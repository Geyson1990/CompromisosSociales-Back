using Abp.Authorization;
using Abp.EntityFrameworkCore;
using Contable.Application.Reporting.Dto;
using Contable.Authorization;
using Contable.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Reporting
{
    [AbpAuthorize(AppPermissions.Pages_Application_Compliance)]
    public class ComplianceAppService : ContableAppServiceBase, IComplianceAppService
    {
        private readonly IDbContextProvider<ContableDbContext> _dbContextProvider;
        private ContableDbContext _context => _dbContextProvider.GetDbContext();

        public ComplianceAppService(
            IDbContextProvider<ContableDbContext> dbContextProvider
            )
        {
            _dbContextProvider = dbContextProvider;
        }

        private void ChangeDefaultValue(ComplianceGetAllInputDto input)
        {
            input.TerritorialUnitId = input.TerritorialUnitId == -1 ? 0 : input.TerritorialUnitId;
            input.DepartmentId = input.DepartmentId == -1 ? 0 : input.DepartmentId;
            input.ProvinceId = input.ProvinceId == -1 ? 0 : input.ProvinceId;
            input.DistrictId = input.DistrictId == -1 ? 0 : input.DistrictId;
            input.SocialConflictId = input.SocialConflictId == -1 ? 0 : input.SocialConflictId;
        }

        public async Task<ComplianceGetAllDto> GetAll(ComplianceGetAllInputDto input)
        {
            ChangeDefaultValue(input);

            var ouput = new ComplianceGetAllDto();

            ouput.Summary = TransformSummary(
                            await _context.ReportSummary
                            .FromSqlRaw("EXECUTE report_summary @p0, @p1, @p2", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId)
                            .ToListAsync());

            ouput.Status = await _context.ReportStatus
                            .FromSqlRaw("EXECUTE report_status @p0, @p1, @p2, @p3", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId)
                            .ToListAsync();

            ouput.OpenStatus = await _context.ReportStatus
                            .FromSqlRaw("EXECUTE report_status_open @p0, @p1, @p2, @p3", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.SocialConflictId)
                            .ToListAsync();

            ouput.PipOpenStatus = await _context.ReportStatus
                           .FromSqlRaw("EXECUTE report_pip_open @p0, @p1, @p2, @p3, @p4", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.DistrictId, input.SocialConflictId)
                           .ToListAsync();

            ouput.ResponsibleStatus = TransformResponsible(
                            await _context.ReportResponsibleStatus
                            .FromSqlRaw("EXECUTE report_responsible_status @p0, @p1, @p2, @p3, @p4", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.DistrictId, input.SocialConflictId)
                            .ToListAsync());  
            return ouput;
        }

        private List<ReportSummaryJoinDto> TransformSummary(List<ReportSummaryDto> summaryList)
        {
            var result = new Dictionary<string, ReportSummaryJoinDto>();
            foreach (var item in summaryList)
            {
                if (!result.ContainsKey(item.Name))
                    result[item.Name] = new ReportSummaryJoinDto() { Name = item.Name };

                var elementHash = result[item.Name];
                if (item.Type == CompromiseType.PIP)
                    elementHash.PipTotal = item.Count;
                else if (item.Type == CompromiseType.Activity)
                    elementHash.ActivityTotal = item.Count;
            }
            return result.Values.ToList();
        }

        private List<ReportResponsibleStatusJoinDto>  TransformResponsible(List<ReportResponsibleStatusDto> responsibleStatusList)
        {
            var result = new Dictionary<string, ReportResponsibleStatusJoinDto>();
            foreach (var item in responsibleStatusList)
            {
                if (!result.ContainsKey(item.Name))
                    result[item.Name] = new ReportResponsibleStatusJoinDto() { Name = item.Name };

                var elementHash = result[item.Name];

                if (item.Type == CompromiseType.PIP)
                {
                    elementHash.PipTotal = item.Total;
                    elementHash.PipCompliments = item.Compliments;
                }
                else if (item.Type == CompromiseType.Activity)
                {
                    elementHash.ActivityTotal = item.Total;
                    elementHash.ActivityCompliments = item.Compliments;
                }
            }
            return result.Values.ToList();
        }
    }
}
