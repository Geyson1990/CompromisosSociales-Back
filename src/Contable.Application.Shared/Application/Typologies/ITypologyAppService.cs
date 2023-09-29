using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Typologies.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Typologies
{
    public interface ITypologyAppService : IApplicationService
    {
        Task Create(TypologyCreateDto input);
        Task Delete(EntityDto input);
        Task<TypologyGetDto> Get(EntityDto input);
        Task<PagedResultDto<TypologyGetAllDto>> GetAll(TypologyGetAllInputDto input);
        Task Update(TypologyUpdateDto input);
    }
}
