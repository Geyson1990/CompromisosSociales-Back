using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Contable.Application.DirectoryGovernments;
using Contable.Application.DirectoryGovernments.Dto;
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
    [AbpAuthorize]
    public class DirectoryGovernmentAppService : ContableAppServiceBase, IDirectoryGovernmentAppService
    {
        private readonly IRepository<DirectoryGovernment> _directoryGovernmentRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<DirectoryGovernmentSector> _directoryGovernmentSectorRepository;
        private readonly IRepository<DirectoryGovernmentType> _directoryGovernmentTypeRepository;

        public DirectoryGovernmentAppService(
            IRepository<DirectoryGovernment> directoryGovernmentRepository,
            IRepository<Department> departmentRepository,
            IRepository<District> districtRepository,
            IRepository<DirectoryGovernmentSector> directoryGovernmentSectorRepository,
            IRepository<DirectoryGovernmentType> directoryGovernmentTypeRepository)
        {
            _directoryGovernmentRepository = directoryGovernmentRepository;
            _departmentRepository = departmentRepository;
            _districtRepository = districtRepository;
            _directoryGovernmentSectorRepository = directoryGovernmentSectorRepository;
            _directoryGovernmentTypeRepository = directoryGovernmentTypeRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Catalog_DirectoryGovernment_Create)]
        public async Task Create(DirectoryGovernmentCreateDto input)
        {
            await _directoryGovernmentRepository.InsertAsync(await ValidateEntity(
                directoryGovernment: ObjectMapper.Map<DirectoryGovernment>(input), 
                districtId: input.District == null ? -1 : input.District.Id,
                sectorId: input.DirectoryGovernmentSector == null ? -1 : input.DirectoryGovernmentSector.Id,
                typeId: input.DirectoryGovernmentType == null ? -1 : input.DirectoryGovernmentType.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Catalog_DirectoryGovernment_Delete)]
        public async Task Delete(EntityDto input)
        {
            VerifyCount(await _directoryGovernmentRepository.CountAsync(p => p.Id == input.Id));

            await _directoryGovernmentRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Catalog_DirectoryGovernment)]
        public async Task<DirectoryGovernmentGetDataDto> Get(NullableIdDto input)
        {
            var output = new DirectoryGovernmentGetDataDto();

            if (input.Id.HasValue)
            {
                VerifyCount(await _directoryGovernmentRepository.CountAsync(p => p.Id == input.Id));

                output.DirectoryGovernment = ObjectMapper.Map<DirectoryGovernmentGetDto>(_directoryGovernmentRepository
                    .GetAll()
                    .Include(p => p.District)
                    .Include(p => p.DirectoryGovernmentSector)
                    .Include(p => p.DirectoryGovernmentType)
                    .Where(p => p.Id == input.Id)
                    .First());
            }

            output.Departments = ObjectMapper.Map<List<DirectoryGovernmentDepartmentDto>>(_departmentRepository
                .GetAll()
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .ToList())
                .Select(p =>
                {
                    p.Provinces = p.Provinces.OrderBy(p => p.Name).ToList();

                    foreach (var province in p.Provinces)
                    {
                        province.Districts = province
                            .Districts
                            .OrderBy(p => p.Name)
                            .ToList();
                    }

                    return p;
                }).ToList();

            output.Sectors = ObjectMapper.Map<List<DirectoryGovernmentSectorDto>>(_directoryGovernmentSectorRepository
                .GetAll()
                .Where(p => p.Enabled)
                .ToList());

            output.Types = ObjectMapper.Map<List<DirectoryGovernmentTypeDto>>(_directoryGovernmentTypeRepository
                .GetAll()
                .Where(p => p.Enabled)
                .ToList());

            return output;
        }

        [AbpAuthorize]
        public async Task<PagedResultDto<DirectoryGovernmentGetAllDto>> GetAll(DirectoryGovernmentGetAllInputDto input)
        {
            var query = _directoryGovernmentRepository
                .GetAll()
                .Include(p => p.District)
                .Include(p => p.DirectoryGovernmentSector)
                .Include(p => p.DirectoryGovernmentType)
                .LikeAllBidirectional(input
                    .Filter
                    .SplitByLike()
                    .Select(word => (Expression<Func<DirectoryGovernment, bool>>)(expression => EF.Functions.Like(expression.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DirectoryGovernmentGetAllDto>(count, ObjectMapper.Map<List<DirectoryGovernmentGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Catalog_DirectoryGovernment_Edit)]
        public async Task Update(DirectoryGovernmentUpdateDto input)
        {
            VerifyCount(await _directoryGovernmentRepository.CountAsync(p => p.Id == input.Id));

            await _directoryGovernmentRepository.UpdateAsync(await ValidateEntity(
                directoryGovernment: ObjectMapper.Map<DirectoryGovernment>(input),
                districtId: input.District == null ? -1 : input.District.Id,
                sectorId: input.DirectoryGovernmentSector == null ? -1 : input.DirectoryGovernmentSector.Id,
                typeId: input.DirectoryGovernmentType == null ? -1 : input.DirectoryGovernmentType.Id));
        }

        private async Task<DirectoryGovernment> ValidateEntity(DirectoryGovernment directoryGovernment, int districtId, int sectorId, int typeId)
        {
            directoryGovernment.Name.IsValidOrException(DefaultTitleMessage, $"El nombre de la entidad es obligatoria");
            directoryGovernment.Name.VerifyTableColumn(DirectoryGovernmentConsts.NameMinLength, 
                DirectoryGovernmentConsts.NameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre de la entidad no debe exceder los {DirectoryGovernmentConsts.NameMaxLength} caracteres");

            directoryGovernment.ShortName.IsValidOrException(DefaultTitleMessage, $"El nombre corto de la entidad es obligatorio");
            directoryGovernment.ShortName.VerifyTableColumn(DirectoryGovernmentConsts.ShortNameMinLength,
                DirectoryGovernmentConsts.ShortNameMaxLength, 
                DefaultTitleMessage, 
                $"El nombre corto de la entidad no debe exceder los {DirectoryGovernmentConsts.ShortNameMaxLength} caracteres");

            directoryGovernment.Address.VerifyTableColumn(DirectoryGovernmentConsts.AddressMinLength, 
                DirectoryGovernmentConsts.AddressMaxLength, 
                DefaultTitleMessage,
                $"La dirección de la entidad no debe exceder los {DirectoryGovernmentConsts.AddressMaxLength} caracteres");
            directoryGovernment.PhoneNumber.VerifyTableColumn(DirectoryGovernmentConsts.PhoneNumberMinLength,
                DirectoryGovernmentConsts.PhoneNumberMaxLength,
                DefaultTitleMessage,
                $"El teléfono de la entidad no debe exceder los {DirectoryGovernmentConsts.PhoneNumberMaxLength} caracteres");
            directoryGovernment.Url.VerifyTableColumn(DirectoryGovernmentConsts.UrlMinLength,
                DirectoryGovernmentConsts.UrlMaxLength,
                DefaultTitleMessage,
                $"El URL de la página de la entidad no debe exceder los {DirectoryGovernmentConsts.UrlMaxLength} caracteres");
            directoryGovernment.AdditionalInformation.VerifyTableColumn(DirectoryGovernmentConsts.AdditionalInformationMinLength,
                DirectoryGovernmentConsts.AdditionalInformationMaxLength,
                DefaultTitleMessage,
                $"La información adicional de la entidad no debe exceder los {DirectoryGovernmentConsts.AdditionalInformationMaxLength} caracteres");

            if (await _districtRepository.CountAsync(p => p.Id == districtId) == 0)
                throw new UserFriendlyException("Aviso", "El distrito seleccionado ya no existe o fue eliminado. Verifique la información antes de continuar");

            var district = await _districtRepository.GetAsync(districtId);

            directoryGovernment.District = district;
            directoryGovernment.DistrictId = district.Id;

            if (await _directoryGovernmentSectorRepository.CountAsync(p => p.Id == sectorId) == 0)
                throw new UserFriendlyException("Aviso", "El sector seleccionado ya no existe o fue eliminado. Verifique la información antes de continuar");

            var sector = await _directoryGovernmentSectorRepository.GetAsync(sectorId);

            directoryGovernment.DirectoryGovernmentSector = sector;
            directoryGovernment.DirectoryGovernmentSectorId = sector.Id;

            if (await _directoryGovernmentTypeRepository.CountAsync(p => p.Id == typeId) == 0)
                throw new UserFriendlyException("Aviso", "El tipo de entidad pública seleccionado ya no existe o fue eliminado. Verifique la información antes de continuar");

            var type = await _directoryGovernmentTypeRepository.GetAsync(typeId);

            directoryGovernment.DirectoryGovernmentType = type;
            directoryGovernment.DirectoryGovernmentTypeId = type.Id;

            directoryGovernment.Alias = directoryGovernment.Name.Replace(" ", "_").Replace("__", "_");

            return directoryGovernment;
        }
    }
}
