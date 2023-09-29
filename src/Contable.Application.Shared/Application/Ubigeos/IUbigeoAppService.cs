using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Contable.Application.Ubigeos.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contable.Application.Ubigeos
{
    public interface IUbigeoAppService : IApplicationService
    {
        #region Department
        Task CreateDepartment(DepartmentCreateDto input); 
        Task DeleteDepartment(EntityDto input);
        Task<PagedResultDto<DepartmentGetAllDto>> GetAllDepartments(DepartmentGetAllInputDto input);
        Task<DepartmentGetDto> GetDepartment(EntityDto input);
        Task UpdateDepartment(DepartmentUpdateDto input);
        #endregion

        #region Province
        Task CreateProvince(ProvinceCreateDto input);
        Task DeleteProvince(EntityDto input);
        Task<PagedResultDto<ProvinceGetAllDto>> GetAllProvinces(ProvinceGetAllInputDto input);
        Task<ProvinceGetDto> GetProvince(EntityDto input);
        Task UpdateProvince(ProvinceUpdateDto input);
        #endregion

        #region District 

        Task CreateDistrict(DistrictCreateDto input); 
        Task DeleteDistrict(EntityDto input);
        Task<PagedResultDto<DistrictGetAllDto>> GetAllDistricts(DistrictGetAllInputDto input); 
        Task<DistrictGetDto> GetDistrict(EntityDto input);
        Task UpdateDistrict(DistrictUpdateDto input);

        #endregion

        #region Region
        Task CreateRegion(RegionCreateDto input);
        Task DeleteRegion(EntityDto input);
        Task<RegionGetDataDto> GetRegion(NullableIdDto input);
        Task<PagedResultDto<RegionGetAllDto>> GetAllRegions(RegionGetAllInputDto input);
        Task UpdateRegion(RegionUpdateDto input);
        #endregion
    }
}
