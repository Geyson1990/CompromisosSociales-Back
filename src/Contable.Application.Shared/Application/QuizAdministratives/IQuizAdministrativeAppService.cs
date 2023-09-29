using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.QuizAdministratives.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.QuizAdministratives
{
    public interface IQuizAdministrativeAppService : IApplicationService
    {
        Task<PagedResultDto<QuizAdministrativeFormDto>> GetQuestions();
        Task Create(QuizAdministrativeCreateDto input);
    }
}
