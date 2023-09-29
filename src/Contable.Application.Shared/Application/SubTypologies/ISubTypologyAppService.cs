using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.SubTypologies.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.SubTypologies
{
    public interface ISubTypologyAppService : IApplicationService
    {
        Task Create(SubTypologyCreateDto input);
        Task Delete(EntityDto input);
        Task<SubTypologyGetDto> Get(EntityDto input);
        Task Update(SubTypologyUpdateDto input);
    }
}
