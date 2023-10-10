using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Compromises.Dto;
using Contable.Dto;
using System.Threading.Tasks;

namespace Contable.Application.Compromises
{
    public interface ICompromiseAppService : IApplicationService
    {
        Task<EntityDto<long>> Create(CompromiseCreateDto input);
        Task Update(CompromiseUpdateDto input);
        Task<CompromiseGetDataDto> Get(NullableIdDto input);
        Task<PagedResultDto<CompromiseGetAllDto>> GetAll(CompromiseGetAllInputDto input);        
        Task Delete(EntityDto<long> input);
        Task<FileDto> GetMatrixToExcel(CompromiseGetToExcelInput input);     

        
    }
}
