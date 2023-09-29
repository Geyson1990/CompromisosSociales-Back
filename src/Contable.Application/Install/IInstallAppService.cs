using System.Threading.Tasks;
using Abp.Application.Services;
using Contable.Install.Dto;

namespace Contable.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}