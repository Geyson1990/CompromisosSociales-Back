using Abp.Application.Services;
using Contable.Dto;
using Contable.Logging.Dto;

namespace Contable.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
