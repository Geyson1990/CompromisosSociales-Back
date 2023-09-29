using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.CompromiseLabels.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.CompromiseLabels
{
    public interface ICompromiseLabelAppService : IApplicationService
    {
        Task Create(CompromiseLabelCreateDto input);
        Task Delete(EntityDto input);
        Task<CompromiseLabelGetDto> Get(EntityDto input);
        Task<PagedResultDto<CompromiseLabelGetAllDto>> GetAll(CompromiseLabelGetAllInputDto input);
        Task Update(CompromiseLabelUpdateDto input);
    }
}
