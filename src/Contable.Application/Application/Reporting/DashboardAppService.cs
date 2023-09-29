using Abp.Domain.Repositories;
using Abp.EntityFrameworkCore;
using Contable.Application.Reporting.Dto;
using Contable.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contable.Application.Reporting
{
    public class DashboardAppService : ContableAppServiceBase, IDashboardAppService
    {
        private readonly IDbContextProvider<ContableDbContext> _dbContextProvider;
        private ContableDbContext _context => _dbContextProvider.GetDbContext();

        public DashboardAppService(
            IDbContextProvider<ContableDbContext> dbContextProvider
            )
        {
            _dbContextProvider = dbContextProvider;
        }

        private void ChangeDefaultValue(DashboardGetAllInputDto input)
        {
            input.TerritorialUnitId = input.TerritorialUnitId == -1 ? 0 : input.TerritorialUnitId;
            input.DepartmentId = input.DepartmentId == -1 ? 0 : input.DepartmentId;
            input.ProvinceId = input.ProvinceId == -1 ? 0 : input.ProvinceId;
            input.DistrictId = input.DistrictId == -1 ? 0 : input.DistrictId;            
        }

        public async Task<DashboardGetAllDto> GetAll(DashboardGetAllInputDto input)
        {
            ChangeDefaultValue(input);

            var ouput = new DashboardGetAllDto();

            ouput.StatusList = ObjectMapper.Map <List<ReportParameterDto>> (await FunctionManager.GetParameterValues(CompromiseConsts.ParameterCategoryStatus));

            ouput.Summary = await TransformSummary(
                            await _context.ReportSummaryStatus
                            .FromSqlRaw("EXECUTE report_summary_status @p0, @p1, @p2, @p3", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, input.DistrictId)
                            .ToListAsync());

            ouput.Status = await _context.ReportStatus
                            .FromSqlRaw("EXECUTE report_status @p0, @p1, @p2, @p3", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, 0, input.DistrictId)
                            .ToListAsync();

            ouput.OpenStatus = await _context.ReportStatus
                            .FromSqlRaw("EXECUTE report_status_open @p0, @p1, @p2, @p3", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, 0, input.DistrictId)
                            .ToListAsync();

            ouput.CloseStatus = await _context.ReportStatus
                            .FromSqlRaw("EXECUTE report_status_close @p0, @p1, @p2, @p3", input.TerritorialUnitId, input.DepartmentId, input.ProvinceId, 0, input.DistrictId)
                            .ToListAsync();

            return ouput;           
        }

        private async Task<List<ReportSummaryStatusSplitDto>> TransformSummary(List<ReportSummaryStatusDto> summaryList)
        {
            var statusList = await FunctionManager.GetParameterValues(CompromiseConsts.ParameterCategoryStatus);

            var queryGroupName = summaryList.GroupBy(p => p.Name);

            var result = new List<ReportSummaryStatusSplitDto>();

            foreach (var group in queryGroupName)
            {
                var itemResult = new ReportSummaryStatusSplitDto()
                {
                    Name = group.Key,
                    OpenStatus = new List<ReportSummaryCountStatusDto>(),
                    CloseStatus = new List<ReportSummaryCountStatusDto>()
                };

                foreach (var item in group)
                {
                    var storeStatus = statusList.Where(e => e.Value.Equals(item.Status)).FirstOrDefault();
                    if (item.Status.Contains("Abierto /"))
                    {
                        itemResult.OpenStatus.Add(new ReportSummaryCountStatusDto()
                        {
                            Id = storeStatus.Id,
                            Status = item.Status.Replace("Abierto / ", "").Replace(" ", ""),
                            Count = item.Count
                        }); ;
                    }
                    else if (item.Status.Contains("Cerrado /"))
                    {
                        itemResult.CloseStatus.Add(new ReportSummaryCountStatusDto()
                        {
                            Id = storeStatus.Id,
                            Status = item.Status.Replace("Cerrado / ", "").Replace(" ", ""),
                            Count = item.Count
                        });
                    }
                }
                result.Add(itemResult);
            }

            return result;            
        }
    }
}
