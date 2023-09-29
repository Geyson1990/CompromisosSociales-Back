using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.DirectoryDialogs;
using Contable.Application.DirectoryDialogs.Dto;
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
using Abp.UI;


namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Catalog_DirectoryDialog)]
    public class DirectoryDialogAppService : ContableAppServiceBase, IDirectoryDialogAppService
    {
        private readonly IRepository<DirectoryDialog> _directoryDialogRepository;
        private readonly IRepository<DirectoryGovernment> _directoryGovernmentRepository;
        private readonly IRepository<DirectoryResponsible> _directoryResponsibleRepository;

        public DirectoryDialogAppService(
            IRepository<DirectoryDialog> directoryDialogRepository, 
            IRepository<DirectoryGovernment> directoryGovernmentRepository, 
            IRepository<DirectoryResponsible> directoryResponsibleRepository)
        {
            _directoryDialogRepository = directoryDialogRepository;
            _directoryGovernmentRepository = directoryGovernmentRepository;
            _directoryResponsibleRepository = directoryResponsibleRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Catalog_DirectoryDialog_Create)]
        public async Task Create(DirectoryDialogCreateDto input)
        {
            await _directoryDialogRepository.InsertAsync(await ValidateEntity(
                directoryDialog: ObjectMapper.Map<DirectoryDialog>(input),
                directoryResponsibleId: input.DirectoryResponsible == null ? -1 : input.DirectoryResponsible.Id,
                directoryGovernmentId: input.DirectoryGovernment == null ? -1 : input.DirectoryGovernment.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Catalog_DirectoryDialog_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _directoryDialogRepository.CountAsync(p => p.Id == input.Id));

            await _directoryDialogRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Catalog_DirectoryDialog)]
        public async Task<DirectoryDialogGetDataDto> Get(NullableIdDto input)
        {
            var output = new DirectoryDialogGetDataDto();

            if (input.Id.HasValue)
            {
                VerifyCount(await _directoryDialogRepository.CountAsync(p => p.Id == input.Id));

                output.DirectoryDialog = ObjectMapper.Map<DirectoryDialogGetDto>(_directoryDialogRepository
                    .GetAll()
                    .Include(p => p.DirectoryResponsible)
                    .Include(p => p.DirectoryGovernment)
                    .ThenInclude(p => p.DirectoryGovernmentSector)
                    .Include(p => p.DirectoryGovernment)
                    .ThenInclude(p => p.District)
                    .ThenInclude(p => p.Province)
                    .ThenInclude(p => p.Department)
                    .Where(p => p.Id == input.Id)
                    .First());
            }

            output.Responsibles = ObjectMapper.Map<List<DirectoryDialogResponsibleDto>>(_directoryResponsibleRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .ToList());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Catalog_DirectoryDialog)]
        public async Task<PagedResultDto<DirectoryDialogGetAllDto>> GetAll(DirectoryDialogGetAllInputDto input)
        {
            var query = _directoryDialogRepository
                .GetAll()
                .Include(p => p.DirectoryResponsible)
                .Include(p => p.DirectoryGovernment)
                .ThenInclude(p => p.DirectoryGovernmentSector)
                .Include(p => p.DirectoryGovernment)
                .ThenInclude(p => p.District)
                .ThenInclude(p => p.Province)
                .ThenInclude(p => p.Department)
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<DirectoryDialog, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DirectoryDialogGetAllDto>(count, ObjectMapper.Map<List<DirectoryDialogGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Catalog_DirectoryDialog_Edit)]
        public async Task Update(DirectoryDialogUpdateDto input)
        {
            VerifyCount(await _directoryDialogRepository.CountAsync(p => p.Id == input.Id));

            await _directoryDialogRepository.UpdateAsync(await ValidateEntity(
                directoryDialog: ObjectMapper.Map<DirectoryDialog>(input),
                directoryResponsibleId: input.DirectoryResponsible == null ? -1 : input.DirectoryResponsible.Id,
                directoryGovernmentId: input.DirectoryGovernment == null ? -1 : input.DirectoryGovernment.Id));
        }

        private async Task<DirectoryDialog> ValidateEntity(DirectoryDialog directoryDialog, int directoryResponsibleId, int directoryGovernmentId)
        {
            directoryDialog.Name.IsValidOrException(DefaultTitleMessage, $"El nombre de la entidad es obligatoria");
            directoryDialog.Name.VerifyTableColumn(DirectoryDialogConsts.NameMinLength,
                DirectoryDialogConsts.NameMaxLength,
                DefaultTitleMessage,
                $"El nombre de la entidad no debe exceder los {DirectoryDialogConsts.NameMaxLength} caracteres");

            directoryDialog.FirstSurname.IsValidOrException(DefaultTitleMessage, $"El apellido paterno es obligatorio");
            directoryDialog.FirstSurname.VerifyTableColumn(DirectoryDialogConsts.FirstSurnameMinLength,
                DirectoryDialogConsts.FirstSurnameMaxLength,
                DefaultTitleMessage,
                $"El apellido paterno no debe exceder los {DirectoryDialogConsts.FirstSurnameMaxLength} caracteres");

            directoryDialog.SecondSurname.VerifyTableColumn(DirectoryDialogConsts.SecondSurnameMinLength,
                DirectoryDialogConsts.SecondSurnameMaxLength,
                DefaultTitleMessage,
                $"La apellido materno no debe exceder los {DirectoryDialogConsts.SecondSurnameMaxLength} caracteres");
            directoryDialog.Job.VerifyTableColumn(DirectoryDialogConsts.JobMinLength,
                DirectoryDialogConsts.JobMaxLength,
                DefaultTitleMessage,
                $"El cargo no debe exceder los {DirectoryDialogConsts.JobMaxLength} caracteres");
            directoryDialog.EmailAddress.VerifyTableColumn(DirectoryDialogConsts.EmailAddressMinLength,
                DirectoryDialogConsts.EmailAddressMaxLength,
                DefaultTitleMessage,
                $"El correo electrónico no debe exceder los {DirectoryDialogConsts.EmailAddressMaxLength} caracteres");
            directoryDialog.PhoneNumber.VerifyTableColumn(DirectoryDialogConsts.PhoneNumberMinLength,
                DirectoryDialogConsts.PhoneNumberMaxLength,
                DefaultTitleMessage,
                $"El teléfono fijo no debe exceder los {DirectoryDialogConsts.PhoneNumberMaxLength} caracteres");
            directoryDialog.MobilePhoneNumber.VerifyTableColumn(DirectoryDialogConsts.MobilePhoneNumberMinLength,
                DirectoryDialogConsts.MobilePhoneNumberMaxLength,
                DefaultTitleMessage,
                $"El teléfono celular no debe exceder los {DirectoryDialogConsts.MobilePhoneNumberMaxLength} caracteres");
            directoryDialog.AdditionalInformation.VerifyTableColumn(DirectoryDialogConsts.AdditionalInformationMinLength,
                DirectoryDialogConsts.AdditionalInformationMaxLength,
                DefaultTitleMessage,
                $"La información adicional no debe exceder los {DirectoryDialogConsts.AdditionalInformationMaxLength} caracteres");

            if (await _directoryResponsibleRepository.CountAsync(p => p.Id == directoryResponsibleId) == 0)
                throw new UserFriendlyException("Aviso", "El responsable seleccionado ya no existe o fue eliminado. Verifique la información antes de continuar");

            var directoryResponsible = await _directoryResponsibleRepository.GetAsync(directoryResponsibleId);

            directoryDialog.DirectoryResponsible = directoryResponsible;
            directoryDialog.DirectoryResponsibleId = directoryResponsible.Id;

            if (await _directoryGovernmentRepository.CountAsync(p => p.Id == directoryGovernmentId) == 0)
                throw new UserFriendlyException("Aviso", "El sector seleccionado ya no existe o fue eliminado. Verifique la información antes de continuar");

            var directoryGovernment = await _directoryGovernmentRepository.GetAsync(directoryGovernmentId);

            directoryDialog.DirectoryGovernment = directoryGovernment;
            directoryDialog.DirectoryGovernmentId = directoryGovernment.Id;

            return directoryDialog;
        }
    }
}
