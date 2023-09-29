using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.QuizDetails.Dto;
using Contable.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.QuizDetails
{
    public interface IQuizDetailAppService : IApplicationService
    {
        Task<QuizDetailGetDataDto> Get(EntityDto input);
        Task<PagedResultDto<QuizDetailGetAllDto>> GetAll(QuizDetailGetAllInputDto input);
        Task Update(QuizDetailUpdateDto input);
        Task<FileDto> ExportAdministrativeMatriz(QuizDetailGetAllInputDto input);
        Task<FileDto> ExportPublicMatriz(QuizDetailGetAllInputDto input);
    }
}
