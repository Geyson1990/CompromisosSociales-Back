using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.QuizDetails;
using Contable.Application.QuizDetails.Dto;
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
using Contable.Configuration;
using Abp.Runtime.Session;
using Abp.UI;
using Contable.Application.Uploaders.Dto;
using Contable.Authorization.Users;
using Contable.Application.Exporting;
using Contable.Application.SocialConflictAlerts.Dto;
using Contable.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Quiz_Platform)]
    public class QuizDetailAppService : ContableAppServiceBase, IQuizDetailAppService
    {
        private readonly IRepository<QuizState> _quizStateRepository;
        private readonly IRepository<QuizComplete> _quizCompleteRepository;
        private readonly IRepository<QuizForm> _quizFromRepository;
        private readonly IRepository<QuizFormOption> _quizFromOptionRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IQuizExcelExporter _quizExcelExporter;

        public QuizDetailAppService(
            IRepository<QuizState> quizStateRepository,
            IRepository<QuizComplete> quizCompleteRepository,
            IRepository<QuizForm> quizFromRepository,
            IRepository<QuizFormOption> quizFromOptionRepository,
            IRepository<User, long> userRepository,
            IQuizExcelExporter quizExcelExporter)
        {
            _quizStateRepository = quizStateRepository;
            _quizCompleteRepository = quizCompleteRepository;
            _quizFromRepository = quizFromRepository;
            _quizFromOptionRepository = quizFromOptionRepository;
            _userRepository = userRepository;
            _quizExcelExporter = quizExcelExporter;
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_Platform)]
        public async Task<QuizDetailGetDataDto> Get(EntityDto input)
        {
            var output = new QuizDetailGetDataDto();

            var dbQuiz = _quizCompleteRepository
                .GetAll()
                .Include(p => p.QuizState)
                .Include(p => p.Forms)
                .ThenInclude(p => p.Options)
                .Include(p => p.Resources)
                .Where(p => p.Id == input.Id)
                .First();

            output.Quiz = ObjectMapper.Map<QuizDetailGetDto>(dbQuiz);

            if (dbQuiz.CreatorUserId.HasValue)
            {
                var user = _userRepository
                    .GetAll()
                    .Where(p => p.Id == dbQuiz.CreatorUserId.Value)
                    .FirstOrDefault();

                output.Quiz.Customer = user == null ? null : ObjectMapper.Map<QuizDetailUserDto>(user);
            }

            if (dbQuiz.AdminitrativeId.HasValue)
            {
                var user = _userRepository
                    .GetAll()
                    .Where(p => p.Id == dbQuiz.AdminitrativeId.Value)
                    .FirstOrDefault();

                output.Quiz.Administrative = user == null ? null : ObjectMapper.Map<QuizDetailUserDto>(user);
            }

            output.Quiz.Forms = output
                .Quiz
                .Forms
                .OrderBy(p => p.Name)
                .ToList();

            foreach (var form in output.Quiz.Forms)
            {
                form.Options = form
                    .Options
                    .OrderBy(p => p.Index)
                    .ToList();
            }

            output.States = ObjectMapper.Map<List<QuizDetailStateDto>>(await _quizStateRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .ToListAsync());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_Platform)]
        public async Task<PagedResultDto<QuizDetailGetAllDto>> GetAll(QuizDetailGetAllInputDto input)
        {
            var query = _quizCompleteRepository
                .GetAll()
                .Include(p => p.Administrative)
                .Include(p => p.QuizState)
                .WhereIf(input.StateId.HasValue, p => p.QuizStateId == input.StateId.Value)
                .WhereIf(input.CompleteType.HasValue, p => p.Type == input.CompleteType.Value)
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.CreationTime >= input.StartTime.Value && p.CreationTime <= input.EndTime.Value);

            var count = await query.CountAsync();
            var result = query.OrderBy(input.Sorting).PageBy(input).ToList();

            var output = new List<QuizDetailGetAllDto>();

            foreach (var item in result)
            {
                var mappedItem = ObjectMapper.Map<QuizDetailGetAllDto>(item);

                if (item.CreatorUserId.HasValue)
                {
                    var user = _userRepository
                        .GetAll()
                        .Where(p => p.Id == item.CreatorUserId.Value)
                        .FirstOrDefault();

                    mappedItem.Customer = user == null ? null : ObjectMapper.Map<QuizDetailUserDto>(user);
                }

                if (item.AdminitrativeId.HasValue)
                {
                    var user = _userRepository
                        .GetAll()
                        .Where(p => p.Id == item.AdminitrativeId.Value)
                        .FirstOrDefault();

                    mappedItem.Administrative = user == null ? null : ObjectMapper.Map<QuizDetailUserDto>(user);
                }

                output.Add(mappedItem);
            }

            return new PagedResultDto<QuizDetailGetAllDto>(count, output);
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_Platform_Edit)]
        public async Task Update(QuizDetailUpdateDto input)
        {
            VerifyCount(await _quizCompleteRepository.CountAsync(p => p.Id == input.Id));

            if (input.QuizState == null || await _quizStateRepository.CountAsync(p => p.Id == input.QuizState.Id) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "El estado seleccionado es inválido o fue eliminado. Por favor verifique la información antes de continuar");

            var quizComplete = await _quizCompleteRepository.GetAsync(input.Id);
            var quizState = await _quizStateRepository.GetAsync(input.QuizState.Id);
            var administrativeUser = await GetCurrentUserAsync();

            quizComplete.QuizState = quizState;
            quizComplete.QuizStateId = quizState.Id;
            quizComplete.Administrative = administrativeUser;
            quizComplete.AdminitrativeId = administrativeUser.Id;

            await _quizCompleteRepository.UpdateAsync(quizComplete);
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_Platform)]
        [HttpGet]
        public async Task<FileDto> ExportAdministrativeMatriz(QuizDetailGetAllInputDto input)
        {
            return await ExportData(input, QuizCompleteType.ADMINITRATIVE);
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_Platform)]
        [HttpGet]
        public async Task<FileDto> ExportPublicMatriz(QuizDetailGetAllInputDto input)
        {
            return await ExportData(input, QuizCompleteType.PUBLIC);
        }

        private async Task<FileDto> ExportData(QuizDetailGetAllInputDto input, QuizCompleteType type)
        {
            var query = _quizCompleteRepository
                .GetAll()
                .Include(p => p.Administrative)
                .Include(p => p.QuizState)
                .Include(p => p.Forms)
                .ThenInclude(p => p.Options)
                .Include(p => p.Resources)
                .Where(p => p.Type == type)
                .WhereIf(input.StateId.HasValue, p => p.QuizStateId == input.StateId.Value)
                .WhereIf(input.CompleteType.HasValue, p => p.Type == input.CompleteType.Value)
                .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.CreationTime >= input.StartTime.Value && p.CreationTime <= input.EndTime.Value);

            var questions = await _quizFromRepository
                .GetAll()
                .Where(p => p.Type == (type == QuizCompleteType.PUBLIC ? QuizFormType.PUBLIC : QuizFormType.ADMINISTRATIVE))
                .Include(p => p.Options)
                .OrderBy(p => p.Index)
                .ToListAsync();

            var headers = questions.Select(p => p.Name).ToList();

            var data = new List<QuizDetailExcelExportDto>();
            var result = await query.OrderBy(input.Sorting).ToListAsync();

            foreach (var dbQuiz in result)
            {
                var item = new QuizDetailExcelExportDto();

                item.CreationTime = dbQuiz.CreationTime;
                item.LastModificationTime = dbQuiz.LastModificationTime;
                item.Type = dbQuiz.Type;

                if (dbQuiz.CreatorUserId.HasValue)
                {
                    var user = _userRepository
                        .GetAll()
                        .Where(p => p.Id == dbQuiz.CreatorUserId.Value)
                        .FirstOrDefault();

                    item.Name = user?.Name ?? "";
                    item.Surname = user?.Surname ?? "";
                    item.SecondSurname = user?.Surname2 ?? "";
                    item.EmailAddress = user?.EmailAddress ?? "";
                }
                else
                {
                    item.Name = dbQuiz.Name ?? "";
                    item.Surname = dbQuiz.Surname ?? "";
                    item.SecondSurname = dbQuiz.SecondSurname ?? "";
                    item.EmailAddress = dbQuiz.EmailAddress ?? "";
                }

                if (dbQuiz.AdminitrativeId.HasValue)
                {
                    var user = _userRepository
                        .GetAll()
                        .Where(p => p.Id == dbQuiz.AdminitrativeId.Value)
                        .FirstOrDefault();

                    var name = user?.Name ?? "";
                    var surname = user?.Surname ?? "";
                    var secondSurname = user?.Surname2 ?? "";

                    item.AdministrativeUser = $"{name} {surname} {secondSurname}";
                }

                item.State = dbQuiz.QuizState == null ? "" : (dbQuiz.QuizState.Name ?? "");
                item.Resources = String.Join(", ", dbQuiz.Resources.Select(p => p.Name).ToArray());
                item.Quetions = new List<QuizDetailQuestionExcelExportDto>();

                foreach (var form in dbQuiz.Forms)
                {
                    var formIndex = questions.FindIndex(p => p.Id == form.FormReferenceId);

                    if (formIndex != -1)
                    {
                        var optionIndex = form.Options.FindIndex(p => p.QuizOptionReferenceId == form.SelectedOptionId);

                        if (optionIndex != -1)
                        {
                            var option = form.Options[optionIndex];

                            item.Quetions.Add(new QuizDetailQuestionExcelExportDto()
                            {
                                Description = option.Extra ? (option.Name == null ? "" : option.Name + " ") + option.Description : (option.Name ?? ""),
                            });
                        }
                        else
                        {
                            item.Quetions.Add(new QuizDetailQuestionExcelExportDto()
                            {
                                Description = ""
                            });
                        }
                    }
                    else
                    {
                        item.Quetions.Add(new QuizDetailQuestionExcelExportDto()
                        {
                            Description = ""
                        });
                    }
                }

                data.Add(item);
            }

            return _quizExcelExporter.ExportMatrizToFile(data, headers, type);
        }
    }
}
