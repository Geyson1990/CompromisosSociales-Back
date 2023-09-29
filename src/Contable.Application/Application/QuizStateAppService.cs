using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.QuizStates;
using Contable.Application.QuizStates.Dto;
using Contable.Application.Extensions;
using Contable.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Quiz_States)]
    public class QuizStateAppService: ContableAppServiceBase, IQuizStateAppService
    {
        private readonly IRepository<QuizState> _quizStateRepository;

        public QuizStateAppService(IRepository<QuizState> quizStateRepository)
        {
            _quizStateRepository = quizStateRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_States_Create)]
        public async Task Create(QuizStateCreateDto input)
        {
            if (input.Default)
            {
                var allQuizStates = _quizStateRepository
                    .GetAll()
                    .ToList();

                foreach (var quizState in allQuizStates)
                {
                    quizState.Default = false;

                    await _quizStateRepository.UpdateAsync(quizState);
                }
            }

            await _quizStateRepository.InsertAsync(ValidateEntity(ObjectMapper.Map<QuizState>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_States_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _quizStateRepository.CountAsync(p => p.Id == input.Id));

            await _quizStateRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_States)]
        public async Task<QuizStateGetDto> Get(EntityDto input)
        {
            VerifyCount(await _quizStateRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<QuizStateGetDto>(await _quizStateRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_States)]
        public async Task<PagedResultDto<QuizStateGetAllDto>> GetAll(QuizStateGetAllInputDto input)
        {
            var query = _quizStateRepository
                .GetAll()
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<QuizState, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<QuizStateGetAllDto>(count, ObjectMapper.Map<List<QuizStateGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_States_Edit)]
        public async Task Update(QuizStateUpdateDto input)
        {
            VerifyCount(await _quizStateRepository.CountAsync(p => p.Id == input.Id));

            await _quizStateRepository.UpdateAsync(ValidateEntity(ObjectMapper.Map(input, await _quizStateRepository.GetAsync(input.Id))));

            if(input.Default)
            {
                var allQuizStates = _quizStateRepository
                    .GetAll()
                    .Where(p => p.Id != input.Id)
                    .ToList();

                foreach(var quizState in allQuizStates)
                {
                    quizState.Default = false;

                    await _quizStateRepository.UpdateAsync(quizState);
                }
            }
        }

        private QuizState ValidateEntity(QuizState input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, $"El nombre del estado es obligatorio");
            input.Name.VerifyTableColumn(QuizStateConsts.NameMinLength, 
                QuizStateConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre del estado no debe exceder los {QuizStateConsts.NameMaxLength} caracteres");

            input.Background.IsValidOrException(DefaultTitleMessage, $"El color de fondo del estado es obligatorio");
            input.Background.VerifyTableColumn(QuizStateConsts.BackgroundMinLength,
                QuizStateConsts.BackgroundMaxLength,
                DefaultTitleMessage,
                $"El color de fondo del estado no debe exceder los {QuizStateConsts.BackgroundMaxLength} caracteres");

            input.Foreground.IsValidOrException(DefaultTitleMessage, $"El color de letra del estado es obligatorio");
            input.Foreground.VerifyTableColumn(QuizStateConsts.ForegroundMinLength,
                QuizStateConsts.ForegroundMaxLength,
                DefaultTitleMessage,
                $"El color de letra del estado no debe exceder los {QuizStateConsts.ForegroundMaxLength} caracteres");

            return input;
        }
    }
}
