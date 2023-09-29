using Abp.Domain.Repositories;
using Abp.UI;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contable.Application.Extensions;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq;
using Abp.Linq.Extensions;
using Abp.Collections.Extensions;
using Abp.Authorization;
using Contable.Authorization;
using System;
using System.Linq.Expressions;
using Contable.Application.ProspestiveRisks.Dto;
using Contable.Application.ProspestiveRisks;
using Contable.Authorization.Users;
using Abp.Runtime.Session;
using Abp.Domain.Uow;
using NUglify.Helpers;

namespace Contable.Application
{
    [AbpAuthorize(AppPermissions.Pages_Management_ProspectiveRisk)]
    public class ProspectiveRiskAppService : ContableAppServiceBase, IProspectiveRiskAppService
    {
        private readonly IRepository<TerritorialUnit> _territorialUnitRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<ProspectiveRisk> _prospectiveRiskRepository;
        private readonly IRepository<ProspectiveRiskDetail> _prospectiveRiskDetailRepository;
        private readonly IRepository<ProspectiveRiskHistory> _prospectiveRiskHistoryRepository;
        private readonly IRepository<ProspectiveRiskHistoryDetail> _prospectiveRiskHistoryDetailRepository;
        private readonly IRepository<StaticVariable> _staticVariableRepository;
        private readonly IRepository<StaticVariableOption> _staticVariableOptionRepository;
        private readonly IRepository<StaticVariableOptionDetail> _staticVariableOptionDetailRepository;
        private readonly IRepository<DinamicVariable> _dinamicVariableRepository;
        private readonly IRepository<DinamicVariableDetail> _dinamicDetailVariableRepository;
        private readonly IRepository<User, long> _userRepository;
        public ProspectiveRiskAppService(
            IRepository<TerritorialUnit> territorialUnitRepository,
            IRepository<Department> departmentRepository,
            IRepository<Province> provinceRepository,
            IRepository<ProspectiveRisk> prospectiveRiskRepository,
            IRepository<ProspectiveRiskDetail> prospectiveRiskDetailRepository,
            IRepository<ProspectiveRiskHistory> prospectiveRiskHistoryRepository,
            IRepository<ProspectiveRiskHistoryDetail> prospectiveRiskHistoryDetailRepository,
            IRepository<StaticVariable> staticVariableRepository,
            IRepository<StaticVariableOption> staticVariableOptionRepository,
            IRepository<StaticVariableOptionDetail> staticVariableOptionDetailRepository,
            IRepository<DinamicVariable> dinamicVariableRepository,
            IRepository<DinamicVariableDetail> dinamicDetailVariableRepository,
            IRepository<User, long> userRepository)
        {
            _territorialUnitRepository = territorialUnitRepository;
            _departmentRepository = departmentRepository;
            _provinceRepository = provinceRepository;
            _prospectiveRiskRepository = prospectiveRiskRepository;
            _prospectiveRiskDetailRepository = prospectiveRiskDetailRepository;
            _prospectiveRiskHistoryRepository = prospectiveRiskHistoryRepository;
            _prospectiveRiskHistoryDetailRepository = prospectiveRiskHistoryDetailRepository;
            _staticVariableRepository = staticVariableRepository;
            _staticVariableOptionRepository = staticVariableOptionRepository;
            _staticVariableOptionDetailRepository = staticVariableOptionDetailRepository;
            _dinamicVariableRepository = dinamicVariableRepository;
            _dinamicDetailVariableRepository = dinamicDetailVariableRepository;
            _userRepository = userRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProspectiveRisk_Process)]
        public async Task Process()
        {
            await FunctionManager.CallProspectiveRiskProcess(AbpSession.ToUserIdentifier());
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProspectiveRisk_History_Delete)]
        public async Task DeleteHistory(EntityDto input)
        {
            if (await _prospectiveRiskHistoryRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            await _prospectiveRiskHistoryRepository.DeleteAsync(input.Id);
            await _prospectiveRiskHistoryDetailRepository.DeleteAsync(p => p.ProspectiveRiskHistoryId == input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProspectiveRisk)]
        public async Task<ProspectiveRiskGetDto> Get(EntityDto input)
        {
            if (await _provinceRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var prospective = (from province in _provinceRepository.GetAll().Where(p => p.Id == input.Id)
                               join department in _departmentRepository.GetAll() on province.Department.Id equals department.Id
                               join prospectiveRisk in _prospectiveRiskRepository.GetAll() on province.Id equals prospectiveRisk.ProvinceId
                               into DistrictProspectiveRisk
                               from result in DistrictProspectiveRisk.DefaultIfEmpty()
                               select new ProspectiveRiskGetDto()
                               {
                                   TerritorialUnits = new List<ProspectiveRiskTerritorialUnitDto>(),
                                   Department = new ProspectiveRiskDepartmentDto()
                                   {
                                       Id = department.Id,
                                       Name = department.Name
                                   },
                                   Province = new ProspectiveRiskProvinceDto()
                                   {
                                       Id = province.Id,
                                       Name = province.Name
                                   },
                                   Id = (result == null ? (int?)null : result.Id),
                                   Value = (result == null ? 0 : result.Value),
                                   CreationTime = (result == null ? (DateTime?)null : result.CreationTime),
                                   EvaluatedTime = (result == null ? (DateTime?)null : result.EvaluatedTime),
                                   LastModificationTime = (result == null ? (DateTime?)null : result.LastModificationTime),
                                   CreationUser = (result != null && result.CreatorUserId.HasValue ? new ProspectiveRiskUserDto() { Id = result.CreatorUserId.Value } : null),
                                   EditionUser = (result != null && result.LastModifierUserId.HasValue ? new ProspectiveRiskUserDto() { Id = result.LastModifierUserId.Value } : null),
                                   FixRate = (result != null ? result.FixRate : 1),
                                   Details = new List<ProspectiveRiskDetailGetDto>(),
                                   Variables = new List<ProspectiveRiskStaticVariableGetDto>()
                               }).First();


            prospective.Variables = ObjectMapper.Map<List<ProspectiveRiskStaticVariableGetDto>>(await _staticVariableRepository
                .GetAll()
                .Include(p => p.Options)
                .ThenInclude(p => p.Details)
                .Include(p => p.Options)
                .ThenInclude(p => p.DinamicVariable)
                .Where(p => p.Enabled && p.Family == StaticVariableFamily.ProspectiveRisk)
                .ToListAsync());

            foreach(var variable in prospective.Variables)
            {
                variable.Options = variable
                    .Options
                    .Where(p => p.Enabled)
                    .ToList();
            }

            if (prospective.Variables.Count == 0 || prospective.Variables.Any(p => p.Options.Count > 0) == false)
                throw new UserFriendlyException("Aviso", "Lo sentimos no podemos procesar su transacción debido a que no hay variables disponibles");

            if (prospective.Id > 0)
            {
                prospective.Details = ObjectMapper.Map<List<ProspectiveRiskDetailGetDto>>(await _prospectiveRiskDetailRepository
                    .GetAll()
                    .Where(p => p.ProspectiveRiskId == prospective.Id && p.Enabled)
                    .ToListAsync());
            }

            if (prospective.CreationUser != null)
            {
                var user = _userRepository
                    .GetAll()
                    .Where(p => p.Id == prospective.CreationUser.Id)
                    .FirstOrDefault();

                prospective.CreationUser.Name = user == null ? "N/A" : user.Name;
                prospective.CreationUser.Surname = user == null ? "" : user.Surname;
                prospective.CreationUser.EmailAddress = user == null ? "" : user.EmailAddress;
            }

            if (prospective.EditionUser != null)
            {
                var user = _userRepository
                    .GetAll()
                    .Where(p => p.Id == prospective.EditionUser.Id)
                    .FirstOrDefault();

                prospective.EditionUser.Name = user == null ? "N/A" : user.Name;
                prospective.EditionUser.Surname = user == null ? "" : user.Surname;
                prospective.EditionUser.EmailAddress = user == null ? "" : user.EmailAddress;
            }

            foreach (var variable in prospective.Variables)
            {
                foreach(var option in variable.Options)
                {
                    option.Details = option
                        .Details
                        .OrderBy(p => p.Value)
                        .ToList();

                    //If register dont's exists and need relation variable value
                    if (option.Type == StaticVariableType.Cuantitative && prospective.Details.Any(p => p.StaticVariableOptionId == option.Id) == false)
                    {
                        var dinamicVariableDetail = _dinamicDetailVariableRepository
                            .GetAll()
                            .Where(p => p.ProvinceId == prospective.Province.Id && p.DinamicVariableId == option.DinamicVariableId)
                            .FirstOrDefault();

                        if(dinamicVariableDetail != null)
                        {
                            prospective.Details.Add(new ProspectiveRiskDetailGetDto()
                            {
                                Id = 0,
                                StaticVariableOptionId = option.Id,
                                Value = dinamicVariableDetail.Value
                            });
                        }
                    }
                }
            }

            prospective.TerritorialUnits = ObjectMapper.Map<List<ProspectiveRiskTerritorialUnitDto>>(_territorialUnitRepository
                    .GetAll()
                    .Where(p => p.TerritorialUnitDepartments.Any(d => d.DepartmentId == prospective.Department.Id))
                    .ToList());

            return prospective;
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProspectiveRisk_History)]
        public async Task<ProspectiveRiskHistoryGetDto> GetHistory(EntityDto input)
        {
            if (await _prospectiveRiskHistoryRepository.CountAsync(p => p.Id == input.Id) == 0)
                throw new UserFriendlyException("Aviso", "El registro solicitado no existe o ya fue eliminado");

            var history = await _prospectiveRiskHistoryRepository.GetAsync(input.Id);
            var output = ObjectMapper.Map<ProspectiveRiskHistoryGetDto>(history);

            if (history.CreatorUserId.HasValue)
            {
                var user = _userRepository
                    .GetAll()
                    .Where(p => p.Id == history.CreatorUserId.Value)
                    .FirstOrDefault();

                output.CreationUser = new ProspectiveRiskUserDto()
                {
                    Name = user == null ? "N/A" : user.Name,
                    Surname = user == null ? "" : user.Surname,
                    EmailAddress = user == null ? "" : user.EmailAddress
                };
            }

            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var details = _prospectiveRiskHistoryDetailRepository
                    .GetAll()
                    .Include(p => p.StaticVariable)
                    .Include(p => p.StaticVariableOption)
                    .ThenInclude(p => p.Details)
                    .Where(p => p.ProspectiveRiskHistoryId == history.Id)
                    .ToList();

                output.Variables = ObjectMapper.Map<List<ProspectiveRiskHistoryVariableGetDto>>(details
                    .Select(p => p.StaticVariable)
                    .DistinctBy(p => p.Id)
                    .OrderBy(p => p.Id)
                    .ToList());

                foreach(var variable in output.Variables)
                {
                    variable.Options = ObjectMapper.Map<List<ProspectiveRiskHistoryVariableOptionGetDto>>(details
                    .Where(p => p.StaticVariableId == variable.Id)
                    .OrderBy(p => p.StaticVariableOption.Index)
                    .Select(p => new ProspectiveRiskHistoryVariableOptionGetDto()
                    {
                        Id = p.StaticVariableOptionId,
                        Name = p.StaticVariableOption.Name,
                        Index = p.StaticVariableOption.Index,
                        Value = p.Value,
                        Details = ObjectMapper.Map<List<ProspectiveRiskHistoryVariableOptionDetailGetDto>>(p.StaticVariableOption
                            .Details
                            .OrderBy(p => p.Index)
                            .DistinctBy(p => p.Value)
                            .ToList())
                    }).DistinctBy(p => p.Id)
                    .ToList());
                }
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProspectiveRisk_Edit)]
        public async Task<EntityDto> CreateOrUpdate(ProspectiveRiskCreateOrUpdateDto input)
        {
            if (input.Province == null)
                throw new UserFriendlyException("Aviso", "Solicitud inválida");
            if (await _provinceRepository.CountAsync(p => p.Id == input.Province.Id) == 0)
                throw new UserFriendlyException("Aviso", $"La provincia solicitada no existe o ya fue eliminada");
            if (input.FixRate <= 0)
                throw new UserFriendlyException("Aviso", "El factor de correción no puede ser cero");
            if(input.EvaluatedTime.HasValue == false)
                throw new UserFriendlyException("Aviso", "La fecha de evaluación es obligatoria");

            var province = await _provinceRepository.GetAsync(input.Province.Id);
            var prospectiveRisk = _prospectiveRiskRepository
                .GetAll()
                .Where(p => p.ProvinceId == province.Id)
                .FirstOrDefault();

            if (prospectiveRisk == null)
                prospectiveRisk = new ProspectiveRisk() { ProvinceId = province.Id };

            var prospectiveRiskId = await _prospectiveRiskRepository.InsertOrUpdateAndGetIdAsync(prospectiveRisk);

            var variables = _staticVariableRepository
                .GetAll()
                .Include(p => p.Options)
                .ThenInclude(p => p.Details)
                .Include(p => p.Options)
                .ThenInclude(p => p.DinamicVariable)
                .Where(p => p.Family == StaticVariableFamily.ProspectiveRisk)
                .ToList();

            var givens = new List<ProspectiveRiskFormDto>();

            //Is any option dont select
            foreach(var variable in variables)
            {
                foreach(var option in variable.Options)
                {
                    if(input.Details.Any(p => p.StaticVariableOptionId == option.Id) == false)
                    {
                        var projectRiskDetail = _prospectiveRiskDetailRepository
                            .GetAll()
                            .Where(p => p.ProspectiveRiskId == prospectiveRiskId && p.StaticVariableOptionId == option.Id)
                            .FirstOrDefault();

                        if (projectRiskDetail == null)
                            projectRiskDetail = new ProspectiveRiskDetail() { ProspectiveRiskId = prospectiveRiskId, StaticVariableOptionId = option.Id };

                        projectRiskDetail.Enabled = option.Enabled && variable.Enabled;

                        if (option.Type == StaticVariableType.Cuantitative)
                        {
                            var dinamicVariableDetail = _dinamicDetailVariableRepository
                                .GetAll()
                                .Where(p => p.DinamicVariableId == option.DinamicVariableId.Value && p.ProvinceId == province.Id)
                                .FirstOrDefault();

                            if (dinamicVariableDetail != null)
                                projectRiskDetail.Value = dinamicVariableDetail.Value;
                            else
                                projectRiskDetail.Value = 0;
                        }

                        if (option.Type == StaticVariableType.Cualitative)
                        {
                            projectRiskDetail.Value = 0;
                        }

                        if(variable.Enabled && option.Enabled)
                        {
                            givens.Add(new ProspectiveRiskFormDto()
                            {
                                StaticVariableId = option.StaticVariable.Id,
                                StaticVariableOptionId = option.Id,
                                Weight = option.Value,
                                Value = projectRiskDetail.Value
                            });
                        }

                        await _prospectiveRiskDetailRepository.InsertOrUpdateAsync(projectRiskDetail);
                    }
                }
            } 

            //Options selected or changed
            foreach (var detail in input.Details)
            {
                if (detail.Id.HasValue && detail.Id.Value > 0 && await _prospectiveRiskDetailRepository.CountAsync(p => p.Id == detail.Id && p.ProspectiveRiskId == prospectiveRisk.Id) == 0)
                    throw new UserFriendlyException("Aviso", "Hubo un error al procesar la solicitud debido a que una de las variables ya no existe o fue eliminada por favor refresque la página");

                if (await _staticVariableOptionRepository.CountAsync(p => p.Id == detail.StaticVariableOptionId && p.StaticVariable.Family == StaticVariableFamily.ProspectiveRisk) == 1)
                {
                    var option = _staticVariableOptionRepository
                        .GetAll()
                        .Include(p => p.StaticVariable)
                        .Include(p => p.DinamicVariable)
                        .Where(p => p.Id == detail.StaticVariableOptionId)
                        .First();

                    var projectRiskDetail = _prospectiveRiskDetailRepository
                        .GetAll()
                        .Where(p => p.ProspectiveRiskId == prospectiveRiskId && p.StaticVariableOptionId == option.Id)
                        .FirstOrDefault();

                    if(projectRiskDetail == null)
                        projectRiskDetail = new ProspectiveRiskDetail() { ProspectiveRiskId = prospectiveRiskId, StaticVariableOptionId = option.Id };

                    projectRiskDetail.Enabled = option.Enabled && option.StaticVariable.Enabled;

                    if (option.Type == StaticVariableType.Cuantitative)
                    {
                        var dinamicVariableDetail = _dinamicDetailVariableRepository
                            .GetAll()
                            .Where(p => p.DinamicVariableId == option.DinamicVariableId.Value && p.ProvinceId == province.Id)
                            .FirstOrDefault();

                        if (dinamicVariableDetail != null)
                            projectRiskDetail.Value = dinamicVariableDetail.Value;
                        else
                            projectRiskDetail.Value = 0;
                    }

                    if(option.Type == StaticVariableType.Cualitative)
                    {
                        projectRiskDetail.Value = detail.Value;
                    }

                    if (option.StaticVariable.Enabled && option.Enabled)
                    {
                        givens.Add(new ProspectiveRiskFormDto()
                        {
                            StaticVariableId = option.StaticVariable.Id,
                            StaticVariableOptionId = option.Id,
                            Weight = option.Value,
                            Value = projectRiskDetail.Value
                        });
                    }

                    await _prospectiveRiskDetailRepository.InsertOrUpdateAsync(projectRiskDetail);
                }
            }

            var totalWeight = givens.Sum(p => p.Weight);
            var totalPercentage = givens.Sum(p => p.Weight * p.Value);
            var totalCalculated = totalWeight == 0 ? 0 : ((totalPercentage) / totalWeight);

            prospectiveRisk.EvaluatedTime = new DateTime(input.EvaluatedTime.Value.Year, input.EvaluatedTime.Value.Month, input.EvaluatedTime.Value.Day);
            prospectiveRisk.FixRate = input.FixRate;
            prospectiveRisk.Value = Decimal.Round(totalCalculated * prospectiveRisk.FixRate, 2);

            await _prospectiveRiskRepository.UpdateAsync(prospectiveRisk);

            var historyId = await _prospectiveRiskHistoryRepository.InsertAndGetIdAsync(new ProspectiveRiskHistory()
            {
                ProspectiveRiskId = prospectiveRisk.Id,
                EvaluatedTime = prospectiveRisk.EvaluatedTime.Value,
                Weight = totalWeight,
                FixValue = prospectiveRisk.FixRate,
                Value = prospectiveRisk.Value
            });

            foreach(var given in givens)
            {
                await _prospectiveRiskHistoryDetailRepository.InsertAsync(new ProspectiveRiskHistoryDetail()
                {
                    ProspectiveRiskHistoryId = historyId,
                    StaticVariableId = given.StaticVariableId,
                    StaticVariableOptionId = given.StaticVariableOptionId,
                    Weight = given.Weight,
                    Value = given.Value
                });
            }

            await CurrentUnitOfWork.SaveChangesAsync();

            return new EntityDto(prospectiveRiskId);
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProspectiveRisk)]
        public async Task<PagedResultDto<ProspectiveRiskGetAllDto>> GetAll(ProspectiveRiskGetAllInputDto input)
        {
            var query = (from province in _provinceRepository.GetAll()
                         join department in _departmentRepository.GetAll() on province.Department.Id equals department.Id
                         join prospectiveRisk in _prospectiveRiskRepository.GetAll() on province.Id equals prospectiveRisk.ProvinceId
                         into DistrictProspectiveRisk
                         from result in DistrictProspectiveRisk.DefaultIfEmpty()
                         select new ProspectiveRiskGetAllDto()
                         {
                             TerritorialUnits = new List<ProspectiveRiskTerritorialUnitDto>(),
                             Department = new ProspectiveRiskDepartmentDto()
                             {
                                 Id = department.Id,
                                 Name = department.Name
                             },
                             Province = new ProspectiveRiskProvinceDto()
                             {
                                 Id = province.Id,
                                 Name = province.Name
                             },
                             Id = (result == null ? (int?)null : result.Id),
                             Value = (result == null ? 0 : result.Value),
                             EvaluatedTime = (result == null ? (DateTime?)null : result.EvaluatedTime),
                             CreationTime = (result == null ? (DateTime?)null : result.CreationTime)
                         })
            .LikeAllBidirectional(input.Filter.SplitByLike().Select(word => (Expression<Func<ProspectiveRiskGetAllDto, bool>>)
                (expression => EF.Functions.Like(expression.Department.Name, $"%{word}%") ||
                               EF.Functions.Like(expression.Province.Name, $"%{word}%"))).ToArray());

            var count = await query.CountAsync();
            var output = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            if (count > 0)
            {
                var territorialUnits = _territorialUnitRepository
                    .GetAll()
                    .Include(p => p.TerritorialUnitDepartments)
                    .ToList();

                foreach (var item in output)
                {
                    var avaliableTerritorialUnits = territorialUnits
                        .Where(p => p.TerritorialUnitDepartments
                        .Any(d => d.DepartmentId == item.Department.Id))
                        .ToList();

                    item.TerritorialUnits = ObjectMapper.Map<List<ProspectiveRiskTerritorialUnitDto>>(avaliableTerritorialUnits);
                }
            }

            return new PagedResultDto<ProspectiveRiskGetAllDto>(count, output);
        }

        [AbpAuthorize(AppPermissions.Pages_Management_ProspectiveRisk_History)]
        public async Task<PagedResultDto<ProspectiveRiskHistoryDto>> GetAllHistories(ProspectiveRiskHistoryGetAllInputDto input)
        {
            if(input.ProspectiveRiskHistoryId.HasValue == false || input.ProspectiveRiskHistoryId.Value <= 0)
                return new PagedResultDto<ProspectiveRiskHistoryDto>(0, new List<ProspectiveRiskHistoryDto>());

            var query = _prospectiveRiskHistoryRepository
                .GetAll()
                .Where(p => p.ProspectiveRiskId == input.ProspectiveRiskHistoryId.Value);

            var count = await query.CountAsync();
            var output = query.OrderBy(input.Sorting).PageBy(input);

            return new PagedResultDto<ProspectiveRiskHistoryDto>(count, ObjectMapper.Map<List<ProspectiveRiskHistoryDto>>(output));
        }
    }
}
