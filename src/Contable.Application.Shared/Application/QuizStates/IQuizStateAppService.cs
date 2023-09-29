using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.QuizStates.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.QuizStates
{
    public interface IQuizStateAppService : IApplicationService
    {
        Task Create(QuizStateCreateDto input);
        Task Delete(EntityDto input);
        Task<QuizStateGetDto> Get(EntityDto input);
        Task<PagedResultDto<QuizStateGetAllDto>> GetAll(QuizStateGetAllInputDto input);
        Task Update(QuizStateUpdateDto input);
    }
}
