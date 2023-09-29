using Abp.Domain.Repositories;
using Castle.Core.Logging;
using Contable.Application;
using Contable.Manager.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Manager
{
    public class ReportManager : ReportManagerBase
    {
        public ReportManager(
            IWebHostEnvironment webHostEnvironment, 
            IHttpContextAccessor httpContextAccessor,
            ILogger logger, 
            IRepository<Report> reportRepository) : base(webHostEnvironment, httpContextAccessor, logger, reportRepository)
        {
        }
    }
}
