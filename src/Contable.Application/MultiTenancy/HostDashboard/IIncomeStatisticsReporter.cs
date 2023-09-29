using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contable.MultiTenancy.HostDashboard.Dto;

namespace Contable.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}