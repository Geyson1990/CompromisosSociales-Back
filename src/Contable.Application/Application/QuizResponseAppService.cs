using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.QuizResponses;
using Contable.Application.QuizResponses.Dto;
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

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Quiz_Responses)]
    public class QuizResponseAppService : ContableAppServiceBase, IQuizResponseAppService
    {
        public QuizResponseAppService()
        {

        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_Responses)]
        public async Task<QuizResponseGetDto> Get()
        {
            return new QuizResponseGetDto()
            {
                CustomerSubject = await SettingManager.GetSettingValueForTenantAsync(AppSettings.Template.QuizCustomerSubject, AbpSession.GetTenantId()),
                CustomerBody = await SettingManager.GetSettingValueForTenantAsync(AppSettings.Template.QuizCustomerBody, AbpSession.GetTenantId()),

                AdminSubject = await SettingManager.GetSettingValueForTenantAsync(AppSettings.Template.QuizAdminBody, AbpSession.GetTenantId()),
                AdminBody = await SettingManager.GetSettingValueForTenantAsync(AppSettings.Template.QuizAdminSubject, AbpSession.GetTenantId()),
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Quiz_Responses_Edit)]
        public async Task Update(QuizResponseUpdateDto input)
        {
            input.AdminSubject.IsValidOrException(DefaultTitleMessage, "El asunto del mensaje de respuesta al usuario de la SGSD es obligatorio");
            input.AdminBody.IsValidOrException(DefaultTitleMessage, "El cuerpo del mensaje de respuesta al usuario de la SGSD es obligatorio");

            input.CustomerSubject.IsValidOrException(DefaultTitleMessage, "El asunto del mensaje de respuesta al ciudadano es obligatorio");
            input.CustomerBody.IsValidOrException(DefaultTitleMessage, "El cuerpo del mensaje de respuesta al ciudadano es obligatorio");

            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.Template.QuizAdminBody, input.AdminSubject);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.Template.QuizAdminSubject, input.AdminBody);

            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.Template.QuizCustomerSubject, input.CustomerSubject);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettings.Template.QuizCustomerBody, input.CustomerBody);
        }
    }
}
