using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Contable.Application.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using Abp.Authorization;
using Contable.Authorization;
using Contable.Application.Ubigeos;
using Contable.Application.Ubigeos.Dto;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Linq.Expressions;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo)]
    public class UbigeoAppService : ContableAppServiceBase, IUbigeoAppService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<Region> _regionRepository;

        public UbigeoAppService(IRepository<Department> departmentRepository, IRepository<Province> provinceRepository, IRepository<District> districtRepository, IRepository<Region> regionRepository)
        {
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _regionRepository = regionRepository;
        }

        #region Department

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo_Create)]
        public async Task CreateDepartment(DepartmentCreateDto input)
        {
            await _departmentRepository.InsertAsync(await ValidateEntity(ObjectMapper.Map<Department>(input)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo_Delete)]
        public async Task DeleteDepartment(EntityDto input)
        {
            VerifyCount(await _departmentRepository.CountAsync(p => p.Id == input.Id));

            var department = _departmentRepository
                .GetAll()
                .Include(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .Where(p => p.Id == input.Id)
                .First();

            foreach (var province in department.Provinces)
            {
                foreach (var district in province.Districts)
                {
                    await _districtRepository.DeleteAsync(district.Id);
                }

                await _provinceRepository.DeleteAsync(province.Id);
            }

            await _departmentRepository.DeleteAsync(department.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo)]
        public async Task<PagedResultDto<DepartmentGetAllDto>> GetAllDepartments(DepartmentGetAllInputDto input)
        {
            var query = _departmentRepository
                .GetAll()
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(Department.Filter));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DepartmentGetAllDto>(count, ObjectMapper.Map<List<DepartmentGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo)]
        public async Task<DepartmentGetDto> GetDepartment(EntityDto input)
        {
            VerifyCount(await _departmentRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<DepartmentGetDto>(await _departmentRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo_Edit)]
        public async Task UpdateDepartment(DepartmentUpdateDto input)
        {
            VerifyCount(await _departmentRepository.CountAsync(p => p.Id == input.Id));

            await _departmentRepository.UpdateAsync(await ValidateEntity(ObjectMapper.Map<Department>(input)));
        }

        private async Task<Department> ValidateEntity(Department input)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, "El nombre el obligatorio");
            input.Name.VerifyTableColumn(DepartmentConsts.NameMinLength, DepartmentConsts.NameMaxLength,
                DefaultTitleMessage, @$"El nombre no debe exceder los {DepartmentConsts.NameMaxLength} caracteres");

            input.Code.IsValidOrException(DefaultTitleMessage, "El código es obligatorio");
            input.Code.VerifyTableColumn(DepartmentConsts.CodeMinLength, DepartmentConsts.CodeMaxLength,
                DefaultTitleMessage, @$"El código debe tener {DepartmentConsts.CodeMaxLength} caracteres");

            input.Filter = input.Name + " " + input.Code;

            if ((input.Id == 0 && await _departmentRepository.CountAsync(p => p.Code.Equals(input.Code)) > 0) ||
                (input.Id != 0 && await _departmentRepository.CountAsync(p => p.Code.Equals(input.Code) && p.Id != input.Id) > 0))
                throw new UserFriendlyException(DefaultTitleMessage, @$"Ya existe un registro con el código {input.Code}");

            if ((input.Id == 0 && await _departmentRepository.CountAsync(p => p.Name.Equals(input.Name)) > 0) ||
                (input.Id != 0 && await _departmentRepository.CountAsync(p => p.Name.Equals(input.Name) && p.Id != input.Id) > 0))
                throw new UserFriendlyException(DefaultTitleMessage, @$"Ya existe un registro con el nombre {input.Name}");

            return input;
        }
        #endregion

        #region Province

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo_Create)]
        public async Task CreateProvince(ProvinceCreateDto input)
        {
            if (await _departmentRepository.CountAsync(p => p.Id == input.DepartmentId) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "El departamento solicitado no existe o ya no se encuentra disponible");

            await _provinceRepository.InsertAsync(await ValidateEntity(ObjectMapper.Map<Province>(input), await _departmentRepository.GetAsync(input.DepartmentId)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo_Delete)]
        public async Task DeleteProvince(EntityDto input)
        {
            VerifyCount(await _provinceRepository.CountAsync(p => p.Id == input.Id));

            var province = _provinceRepository
                .GetAll()
                .Include(p => p.Districts)
                .Where(p => p.Id == input.Id)
                .First();

            foreach (var district in province.Districts)
            {
                await _districtRepository.DeleteAsync(district.Id);
            }

            await _provinceRepository.DeleteAsync(province.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo)]
        public async Task<PagedResultDto<ProvinceGetAllDto>> GetAllProvinces(ProvinceGetAllInputDto input)
        {
            var query = _provinceRepository
                .GetAll()
                .Include(p => p.Districts)
                .Where(p => p.Department.Id == input.DeparmentId)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(Province.Filter));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<ProvinceGetAllDto>(count, ObjectMapper.Map<List<ProvinceGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo)]
        public async Task<ProvinceGetDto> GetProvince(EntityDto input)
        {
            VerifyCount(await _provinceRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<ProvinceGetDto>(await _provinceRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo_Edit)]
        public async Task UpdateProvince(ProvinceUpdateDto input)
        {
            VerifyCount(await _provinceRepository.CountAsync(p => p.Id == input.Id));

            await _provinceRepository.UpdateAsync(await ValidateEntity(ObjectMapper.Map<Province>(input)));
        }

        private async Task<Province> ValidateEntity(Province input, Department department = null)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, "El nombre es obligatorio");
            input.Name.VerifyTableColumn(ProvinceConsts.NameMinLength, ProvinceConsts.NameMaxLength,
                DefaultTitleMessage, @$"El nombre no debe exceder los {ProvinceConsts.NameMaxLength} caracteres");

            input.Code.IsValidOrException(DefaultTitleMessage, "El código es obligatorio");
            input.Code.VerifyTableColumn(ProvinceConsts.CodeMinLength, ProvinceConsts.CodeMaxLength,
                DefaultTitleMessage, @$"El código debe tener {ProvinceConsts.CodeMaxLength} caracteres");

            input.Filter = input.Name + " " + input.Code;

            if ((input.Id == 0 && await _provinceRepository.CountAsync(p => p.Code.Equals(input.Code)) > 0) ||
                (input.Id != 0 && await _provinceRepository.CountAsync(p => p.Code.Equals(input.Code) && p.Id != input.Id) > 0))
                throw new UserFriendlyException(DefaultTitleMessage, @$"Ya existe un registro con el código {input.Code}");

            if (department != null)
                input.Department = department;

            return input;
        }
        #endregion

        #region District 

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo_Create)]
        public async Task CreateDistrict(DistrictCreateDto input)
        {
            if (await _provinceRepository.CountAsync(p => p.Id == input.ProvinceId) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "La provincia solicitada no existe o ya no se encuentra disponible");

            await _districtRepository.InsertAsync(await ValidateEntity(ObjectMapper.Map<District>(input), await _provinceRepository.GetAsync(input.ProvinceId)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo_Delete)]
        public async Task DeleteDistrict(EntityDto input)
        {
            VerifyCount(await _districtRepository.CountAsync(p => p.Id == input.Id));
            await _districtRepository.DeleteAsync(input.Id);
            await _regionRepository.DeleteAsync(p => p.DistrictId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo)]
        public async Task<PagedResultDto<DistrictGetAllDto>> GetAllDistricts(DistrictGetAllInputDto input)
        {
            var query = _districtRepository
                .GetAll()
                .Where(p => p.Province.Id == input.ProvinceId)
                .LikeAllBidirectional(input.Filter.SplitByLike(), nameof(District.Filter));

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<DistrictGetAllDto>(count, ObjectMapper.Map<List<DistrictGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo)]
        public async Task<DistrictGetDto> GetDistrict(EntityDto input)
        {
            VerifyCount(await _districtRepository.CountAsync(p => p.Id == input.Id));

            return ObjectMapper.Map<DistrictGetDto>(await _districtRepository.GetAsync(input.Id));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo_Edit)]
        public async Task UpdateDistrict(DistrictUpdateDto input)
        {
            VerifyCount(await _districtRepository.CountAsync(p => p.Id == input.Id));

            await _districtRepository.UpdateAsync(await ValidateEntity(ObjectMapper.Map<District>(input)));
        }

        private async Task<District> ValidateEntity(District input, Province province = null)
        {
            input.Name.IsValidOrException(DefaultTitleMessage, "El nombre el obligatorio");
            input.Name.VerifyTableColumn(DistrictConsts.NameMinLength, DistrictConsts.NameMaxLength,
                DefaultTitleMessage, @$"El nombre no debe exceder los {DistrictConsts.NameMaxLength} caracteres");

            input.Code.IsValidOrException(DefaultTitleMessage, "El código es obligatorio");
            input.Code.VerifyTableColumn(DistrictConsts.CodeMinLength, DistrictConsts.CodeMaxLength,
                DefaultTitleMessage, @$"El código debe tener {DistrictConsts.CodeMaxLength} caracteres");

            input.Ubigeo.IsValidOrException(DefaultTitleMessage, "El código es obligatorio");
            input.Ubigeo.VerifyTableColumn(DistrictConsts.UbigeoMinLength, DistrictConsts.UbigeoMaxLength,
                DefaultTitleMessage, @$"El ubigeo debe tener {DistrictConsts.UbigeoMaxLength} caracteres");

            input.Filter = input.Name + " " + input.Code + " " + input.Ubigeo;

            if ((input.Id == 0 && await _districtRepository.CountAsync(p => p.Code.Equals(input.Code)) > 0) ||
                (input.Id != 0 && await _districtRepository.CountAsync(p => p.Code.Equals(input.Code) && p.Id != input.Id) > 0))
                throw new UserFriendlyException(DefaultTitleMessage, @$"Ya existe un registro con el código {input.Code}");

            if (province != null)
                input.Province = province;

            return input;
        }
        #endregion

        #region Region 

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo_Create)]
        public async Task CreateRegion(RegionCreateDto input)
        {
            if (await _districtRepository.CountAsync(p => p.Id == input.DistrictId) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "El distrito del centro poblado solicitado no existe o ya no se encuentra disponible");

            await _regionRepository.InsertAsync(await ValidateEntity(ObjectMapper.Map<Region>(input), await _districtRepository.GetAsync(input.DistrictId)));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo_Delete)]
        public async Task DeleteRegion(EntityDto input)
        {
            VerifyCount(await _regionRepository.CountAsync(p => p.Id == input.Id));
            await _regionRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo)]
        public async Task<PagedResultDto<RegionGetAllDto>> GetAllRegions(RegionGetAllInputDto input)
        {
            var query = _regionRepository
                .GetAll()
                .Include(p => p.District)
                .ThenInclude(p => p.Province)
                .ThenInclude(p => p.Department)
                .LikeAllBidirectional(input.Filter.SplitByLike()
                    .Select(word => (Expression<Func<Region, bool>>)
                        (expression => EF.Functions.Like(expression.Filter, $"%{word}%") || EF.Functions.Like(expression.District.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<RegionGetAllDto>(count, ObjectMapper.Map<List<RegionGetAllDto>>(output));
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo)]
        public async Task<RegionGetDataDto> GetRegion(NullableIdDto input)
        {
            if(input.Id.HasValue)
            {
                VerifyCount(await _regionRepository.CountAsync(p => p.Id == input.Id));
            }

            return new RegionGetDataDto()
            {
                Region = input.Id.HasValue ? ObjectMapper.Map<RegionGetDto>(_regionRepository
                    .GetAll()
                    .Include(p => p.District)
                    .ThenInclude(p => p.Province)
                    .ThenInclude(p => p.Department)
                    .Where(p => p.Id == input.Id)
                    .First()) : new RegionGetDto(),
                Departments = ObjectMapper.Map<List<RegionDepartmentGetDto>>(_departmentRepository
                    .GetAll()
                    .Include(p => p.Provinces)
                    .ThenInclude(p => p.Districts))
            };
        }

        [AbpAuthorize(AppPermissions.Pages_Maintenance_Ubigeo_Edit)]
        public async Task UpdateRegion(RegionUpdateDto input)
        {
            VerifyCount(await _regionRepository.CountAsync(p => p.Id == input.Id));

            await _regionRepository.UpdateAsync(await ValidateEntity(ObjectMapper.Map(input, await _regionRepository.GetAsync(input.Id)), await _districtRepository.GetAsync(input.DistrictId)));
        }

        private async Task<Region> ValidateEntity(Region input, District district)
        {
            input.Ubigeo = input.Code;

            input.Name.IsValidOrException(DefaultTitleMessage, "El nombre el obligatorio");
            input.Name.VerifyTableColumn(RegionConsts.NameMinLength, RegionConsts.NameMaxLength,
                DefaultTitleMessage, @$"El nombre no debe exceder los {RegionConsts.NameMaxLength} caracteres");

            input.Code.IsValidOrException(DefaultTitleMessage, "El código es obligatorio");
            input.Code.VerifyTableColumn(RegionConsts.CodeMinLength, RegionConsts.CodeMaxLength,
                DefaultTitleMessage, @$"El código debe tener {RegionConsts.CodeMaxLength} caracteres");

            input.Ubigeo.IsValidOrException(DefaultTitleMessage, "El código es obligatorio");
            input.Ubigeo.VerifyTableColumn(RegionConsts.UbigeoMinLength, RegionConsts.UbigeoMaxLength,
                DefaultTitleMessage, @$"El ubigeo debe tener {RegionConsts.UbigeoMaxLength} caracteres");

            input.Filter = $"{input.Name ?? ""} {input.Code ?? ""} {input.Ubigeo ?? ""}";

            if ((input.Id == 0 && await _regionRepository.CountAsync(p => p.Code.Equals(input.Code)) > 0) ||
                (input.Id != 0 && await _regionRepository.CountAsync(p => p.Code.Equals(input.Code) && p.Id != input.Id) > 0))
                throw new UserFriendlyException(DefaultTitleMessage, @$"Ya existe un registro con el ubigeo {input.Code}");

            if (district != null)
            {
                input.District = district;
                input.DistrictId = district.Id;
            }

            return input;
        }

        #endregion
    }
}
