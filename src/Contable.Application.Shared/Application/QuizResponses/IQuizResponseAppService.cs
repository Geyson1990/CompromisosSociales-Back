using Abp.Application.Services;
using Contable.Application.QuizResponses.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.QuizResponses
{
    public interface IQuizResponseAppService : IApplicationService
    {
        Task<QuizResponseGetDto> Get();
        Task Update(QuizResponseUpdateDto input);
    }
}
