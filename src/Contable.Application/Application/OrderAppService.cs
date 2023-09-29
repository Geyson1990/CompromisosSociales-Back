using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using Contable.Application.Records;
using System.Threading.Tasks;
using Contable.Application.Extensions;
using Contable.Application.Orders.Dto;
using Contable.Application.Compromises;
using Contable.Application.External;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Abp.Authorization;
using Contable.Authorization;
using Contable.Dto;
using Contable.Application.Exporting;
using System;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Application_Order)]
    public class OrderAppService : ContableAppServiceBase, IOrderAppService
    {
        private readonly IRepository<Order, long> _orderRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly ICompromiseAppService _compromiseAppService;
        private readonly IPipMefAppService _pipMefAppService;
        private readonly IRepository<TerritorialUnitDepartment> _territorialUnitDepartmentRepository;
        private readonly IOrderExcelExporter _orderExcelExporter;

        public OrderAppService(IRepository<Order, long> orderRepository,
            IRepository<Department> departmentRepository,
            IRepository<Province> provinceRepository,
            IRepository<District> districtRepository,
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<SocialConflict> socialConflictRepository,
            ICompromiseAppService compromiseAppService,
            IPipMefAppService pipMefAppService,
            IRepository<TerritorialUnitDepartment> territorialUnitDepartmentRepository,
            IOrderExcelExporter orderExcelExporter)
        {
            _orderRepository = orderRepository;
            _socialConflictRepository = socialConflictRepository;
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _territorialUnitRepository = territorialUnitRepository;
            _compromiseAppService = compromiseAppService;
            _pipMefAppService = pipMefAppService;
            _territorialUnitDepartmentRepository = territorialUnitDepartmentRepository;
            _orderExcelExporter = orderExcelExporter;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Order_Create)]
        public async Task Create(OrderCreateDto input)
        {
            await _orderRepository.InsertAsync(await ValidateEntity(ObjectMapper.Map<Order>(input),
                socialConflict: input.SocialConflict ?? new EntityDto(),
                territorialUnit: input.TerritorialUnit ?? new EntityDto(),
                department: input.Department ?? new EntityDto(),
                province: input.Province ?? new EntityDto(),
                district: input.District ?? new EntityDto(),
                pipmef: input.PIPMEF ?? new OrderPIPMEFUpdateDto()));
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Order_Edit)]
        public async Task Update(OrderUpdateDto input)
        {
            await _orderRepository.UpdateAsync(await ValidateEntity(ObjectMapper.Map(input, await _orderRepository.GetAsync(input.Id)),
                socialConflict: input.SocialConflict ?? new EntityDto(),
                territorialUnit: input.TerritorialUnit ?? new EntityDto(),
                department: input.Department ?? new EntityDto(),
                province: input.Province ?? new EntityDto(),
                district: input.District ?? new EntityDto(),
                pipmef: input.PIPMEF ?? new OrderPIPMEFUpdateDto()));
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Order_Delete)]
        public async Task Delete(EntityDto<long> input)
        {
            VerifyCount(await _orderRepository.CountAsync(p => p.Id == input.Id));

            await _orderRepository.DeleteAsync(p => p.Id == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Order)]
        public async Task<OrderGetDataDto> Get(NullableIdDto input)
        {
            var output = new OrderGetDataDto
            {
                Order = new OrderGetDto()
            };

            if (input.Id.HasValue)
            {
                VerifyCount(await _orderRepository.CountAsync(p => p.Id == input.Id));

                var order = _orderRepository
                   .GetAll()
                   .Include(p => p.SocialConflict)
                   .Include(p => p.TerritorialUnit)
                   .Include(p => p.Department)
                   .Include(p => p.Province)
                   .Include(p => p.District)
                   .Include(p => p.PIPMEF)
                   .Where(p => p.Id == input.Id)
                   .First();

                output.Order = ObjectMapper.Map<OrderGetDto>(order);
            }

            var departments = await _territorialUnitDepartmentRepository
                .GetAll()
                .Include(p => p.TerritorialUnit)
                .Include(p => p.Department)
                .ThenInclude(p => p.Provinces)
                .ThenInclude(p => p.Districts)
                .ToListAsync();

            output.Departments = new List<OrderDepartmentDto>();

            foreach (var item in departments)
            {
                if (item.Department != null && item.TerritorialUnit != null)
                {
                    var department = new OrderDepartmentDto
                    {
                        Id = item.Department.Id,
                        Name = item.Department.Name,
                        TerritorialUnitId = item.TerritorialUnit.Id,
                        Provinces = ObjectMapper.Map<List<OrderProvinceDto>>(item.Department.Provinces)
                    };

                    output.Departments.Add(department);
                }
            }

            output.TerritorialUnits = ObjectMapper.Map<List<OrderTerritorialUnitDto>>(await _territorialUnitRepository.GetAll().ToListAsync());

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Order)]
        public async Task<PagedResultDto<OrderGetAllDto>> GetAll(OrderGetAllInputDto input)
        {
            var query = _orderRepository
               .GetAll()
               .Include(p => p.SocialConflict)            
               .Include(p => p.TerritorialUnit)
               .WhereIf(input.Type.HasValue && input.Type > 0, p => p.Type == (input.Type.Value == 1 ? OrderType.PIP : OrderType.Activity))
               .WhereIf(input.SocialConflictCode.IsValid(), p => p.SocialConflict.Code.Contains(input.SocialConflictCode))
               .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.TerritorialUnit.Id == input.TerritorialUnitId)
               .WhereIf(input.FilterByDate && input.StartTime.HasValue && input.EndTime.HasValue, p => p.CreationTime >= input.StartTime.Value && p.CreationTime <= input.EndTime.Value)
               .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                        p => p.Name.Contains(input.Filter) || p.Description.Contains(input.Filter) || p.Responsible.Contains(input.Filter)
                    );

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            var result = ObjectMapper.Map<List<OrderGetAllDto>>(output);
            return new PagedResultDto<OrderGetAllDto>(count, result);
        }

        [AbpAuthorize(AppPermissions.Pages_Application_Order)]
        public async Task<FileDto> GetMatrixToExcel(OrderGetMatrixExcelInputDto input)
        {
            
                var query = _orderRepository
                   .GetAll()
                   .Include(p => p.SocialConflict)
                   .Include(p => p.PIPMEF)
                   .Include(p => p.TerritorialUnit)
                   .Include(p => p.Department)
                   .Include(p => p.Province)
                   .Include(p => p.District)
                   .WhereIf(input.Type.HasValue && input.Type > 0, p => p.Type == (input.Type.Value == 1 ? OrderType.PIP : OrderType.Activity))
                   .WhereIf(input.SocialConflictCode.IsValid(), p => p.SocialConflict.Code.Contains(input.SocialConflictCode))
                   .WhereIf(input.TerritorialUnitId.HasValue && input.TerritorialUnitId.Value > 0, p => p.TerritorialUnit.Id == input.TerritorialUnitId)
                   .WhereIf(!input.Filter.IsNullOrWhiteSpace(),
                            p => p.Name.Contains(input.Filter) || p.Description.Contains(input.Filter) || p.Responsible.Contains(input.Filter)
                        );

                var output = await query.OrderBy(input.Sorting).ToListAsync();

                return _orderExcelExporter.ExportMatrixToFile(ObjectMapper.Map<List<OrderGetMatrixExcelDto>>(output));
            
        }

        private async Task<Order> ValidateEntity(Order input, EntityDto territorialUnit, EntityDto department, EntityDto province, EntityDto district, EntityDto socialConflict, OrderPIPMEFUpdateDto pipmef)
        {
            var isNew = (input.Id == 0);

            if (socialConflict.Id > 0 && await _socialConflictRepository.CountAsync(p => p.Id == socialConflict.Id) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "El conflicto social ya no se encuentra disponible");

            if (isNew && !input.OrderDate.HasValue)
                throw new UserFriendlyException(DefaultTitleMessage, "La fecha del pedido es obligatorio");

            input.Document.IsValidOrException(DefaultTitleMessage, "El número de documento es obligatorio");
            input.Document.VerifyTableColumn(OrderConsts.DocumentMinLength, OrderConsts.DocumentMaxLength, DefaultTitleMessage, $"El número de documento no debe exceder los {OrderConsts.NameMaxLength} caracteres");

            input.Name.IsValidOrException(DefaultTitleMessage, "La denominación del proyecto/actividad es obligatorio");
            input.Name.VerifyTableColumn(OrderConsts.NameMinLength, OrderConsts.NameMaxLength, DefaultTitleMessage, $"La denominación del proyecto/actividad no debe exceder los {OrderConsts.NameMaxLength} caracteres");

            if(territorialUnit.Id == 0 || await _territorialUnitRepository.CountAsync(p => p.Id == territorialUnit.Id) == 0)
                throw new UserFriendlyException(DefaultTitleMessage, "La unidad territorial es un dato obligatorio");

            input.Responsible.VerifyTableColumn(OrderConsts.ResponsibleMinLength, OrderConsts.ResponsibleMaxLength, DefaultTitleMessage, $"El responsable del pedido no debe exceder los {OrderConsts.ResponsibleMaxLength} caracteres");
            input.Description.VerifyTableColumn(OrderConsts.DescriptionMinLength, OrderConsts.DescriptionMaxLength, DefaultTitleMessage, $"La descripción del pedido no debe exceder los {OrderConsts.DescriptionMaxLength} caracteres");
            input.Observation.VerifyTableColumn(OrderConsts.ObservationMinLength, OrderConsts.ObservationMaxLength, DefaultTitleMessage, $"La observaciñon del pedido no debe exceder los {OrderConsts.ObservationMaxLength} caracteres");

            if (isNew)
            {
                var count = await _orderRepository.CountAsync() + 1;
                input.Code = input.OrderDate.Value.Year + "-" + ("" + count).PadLeft(4, '0');
            }

            if(socialConflict.Id > 0)
                input.SocialConflict = await _socialConflictRepository.GetAsync(socialConflict.Id);

            input.TerritorialUnit = await _territorialUnitRepository.GetAsync(territorialUnit.Id);

            if (department.Id > 0)
                input.Department = await _departmentRepository.GetAsync(department.Id);
            if (province.Id > 0)
                input.Province = await _provinceRepository.GetAsync(province.Id);
            if (district.Id > 0)
                input.District = await _districtRepository.GetAsync(district.Id);

            //validate PIP  
            if (pipmef.UnifiedCode.IsValid() || pipmef.SNIPCode.IsValid())
            {                
                input.PIPMEF = await _pipMefAppService.ValidatePIPMEFOrder(pipmef);
            }

            return input;
        }
    }
}
