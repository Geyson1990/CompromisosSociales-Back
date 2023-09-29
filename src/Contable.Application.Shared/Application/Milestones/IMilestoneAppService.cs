using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Milestones.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Milestones
{
    public interface IMilestoneAppService : IApplicationService
    {
        Task Create(MilestoneCreateDto input);
        Task Delete(EntityDto input);
        Task<MilestoneGetDto> Get(EntityDto input);
        Task Update(MilestoneUpdateDto input);
    }
}
