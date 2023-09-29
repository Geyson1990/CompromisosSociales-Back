using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.QuizAdministratives;
using Contable.Application.QuizAdministratives.Dto;
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

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Quiz_Customer)]
    public class QuizAdministrativeAppService : ContableAppServiceBase, IQuizAdministrativeAppService
    {
        private readonly IRepository<QuizForm> _quizFormRepository;
        private readonly IRepository<QuizFormOption> _quizFormOptionRepository;
        private readonly IRepository<QuizComplete> _quizCompleteRepository;
        private readonly IRepository<QuizCompleteForm> _quizCompleteFormRepository;
        private readonly IRepository<QuizCompleteOption> _quizCompleteOptionRepository;
        private readonly IRepository<QuizCompleteResource> _quizCompleteResourceRepository;
        private readonly IRepository<QuizState> _quizStateRepository;

        public QuizAdministrativeAppService(
            IRepository<QuizForm> quizFormRepository,
            IRepository<QuizFormOption> quizFormOptionRepository,
            IRepository<QuizComplete> quizCompleteRepository,
            IRepository<QuizCompleteForm> quizCompleteFormRepository,
            IRepository<QuizCompleteOption> quizCompleteOptionRepository,
            IRepository<QuizCompleteResource> quizCompleteResourceRepository,
            IRepository<QuizState> quizStateRepository)
        {
            _quizFormRepository = quizFormRepository;
            _quizFormOptionRepository = quizFormOptionRepository;
            _quizCompleteRepository = quizCompleteRepository;
            _quizCompleteFormRepository = quizCompleteFormRepository;
            _quizCompleteOptionRepository = quizCompleteOptionRepository;
            _quizCompleteResourceRepository = quizCompleteResourceRepository;
            _quizStateRepository = quizStateRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_Customer)]
        public async Task Create(QuizAdministrativeCreateDto input)
        {
            if (input.Forms == null || input.Forms.Count == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "No se puede registrar una encuesta vacía");

            if (input.UploadFiles == null)
                input.UploadFiles = new List<UploadResourceInputDto>();

            var defaultQuizState = _quizStateRepository
                .GetAll()
                .Where(p => p.Default)
                .FirstOrDefault();

            var quizComplete = new QuizComplete()
            {
                QuizState = defaultQuizState,
                QuizStateId = defaultQuizState?.Id,
                Type = QuizCompleteType.ADMINITRATIVE,
                Forms = new List<QuizCompleteForm>(),
                Resources = new List<QuizCompleteResource>()
            };

            foreach (var form in input.Forms)
            {
                var dbForm = await _quizFormRepository
                    .GetAsync(form.Id);

                var dbOptions = _quizFormOptionRepository
                    .GetAll()
                    .Where(p => p.QuizFormId == dbForm.Id)
                    .ToList();

                if (form.Options == null || form.Options.Count == 0 || dbOptions.Count(p => p.Id == form.SelectedOptionId) == 0)
                    throw new UserFriendlyException(DefaultTitleMessage, "Por favor complete todas las preguntas antes de enviar la encuesta");

                if(dbOptions.Count == 1 && dbOptions[0].Extra && dbForm.Required && string.IsNullOrWhiteSpace(form.Options[0].Response))
                    throw new UserFriendlyException(DefaultTitleMessage, "Por favor complete todas las preguntas antes de enviar la encuesta");

                quizComplete.Forms.Add(new QuizCompleteForm()
                {
                    Name = dbForm.Name,
                    Index = dbForm.Index,
                    Required = dbForm.Required,
                    SelectedOptionId = form.SelectedOptionId,
                    FormReferenceId = form.Id,
                    Options = dbOptions.Select(p =>
                    {
                        var option = form
                            .Options
                            .Where(d => d.Id == p.Id)
                            .FirstOrDefault();

                        return new QuizCompleteOption()
                        {
                            Name = p.Name,
                            Index = p.Index,
                            Extra = p.Extra,
                            QuizOptionReferenceId = option == null ? 0 : option.Id,
                            Description = option == null ? null : option.Response
                        };
                    }).ToList()
                });
            }

            foreach (var resource in input.UploadFiles)
            {
                var createdResource = ObjectMapper.Map<QuizCompleteResource>(ResourceManager.Create(resource, ResourceConsts.QuizCompleteAdministrative));
                createdResource.Name = createdResource.FileName;

                quizComplete.Resources.Add(createdResource);
            }

            await _quizCompleteRepository.InsertAsync(quizComplete);
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_Customer)]
        public async Task<PagedResultDto<QuizAdministrativeFormDto>> GetQuestions()
        {
            var forms = await _quizFormRepository
                .GetAll()
                .Include(P => P.Options)
                .Where(p => p.Type == QuizFormType.ADMINISTRATIVE && p.Enabled)
                .OrderBy(p => p.Index)
                .ToListAsync();

            var output = new List<QuizAdministrativeFormDto>();

            foreach(var form in forms)
            {
                if(form.Options.Count > 0)
                {
                    var item = ObjectMapper.Map<QuizAdministrativeFormDto>(form);
                    item.Options = ObjectMapper.Map<List<QuizAdministrativeFormOptionDto>>(form.Options.OrderBy(p => p.Index).ToList());

                    output.Add(item);
                }
            }

            return new PagedResultDto<QuizAdministrativeFormDto>(output.Count, output);
        }
    }
}
