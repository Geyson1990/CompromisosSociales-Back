using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Configuration;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Notifications;
using Abp.Organizations;
using Abp.Runtime.Session;
using Abp.UI;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Contable.Authorization.Permissions;
using Contable.Authorization.Permissions.Dto;
using Contable.Authorization.Roles;
using Contable.Authorization.Users.Dto;
using Contable.Dto;
using Contable.Notifications;
using Contable.Url;
using Contable.Organizations.Dto;
using Microsoft.AspNetCore.Hosting;
using Contable.Configuration;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Contable.Application.Extensions;
using Contable.Application;
using System.Text.RegularExpressions;

namespace Contable.Authorization.Users
{
    [AbpAuthorize]
    public class UserAppService : ContableAppServiceBase, IUserAppService
    {
        public IAppUrlService AppUrlService { get; set; }

        private readonly RoleManager _roleManager;
        private readonly IUserEmailer _userEmailer;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IRepository<RolePermissionSetting, long> _rolePermissionRepository;
        private readonly IRepository<UserPermissionSetting, long> _userPermissionRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IUserPolicy _userPolicy;
        private readonly IEnumerable<IPasswordValidator<User>> _passwordValidators;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRoleManagementConfig _roleManagementConfig;
        private readonly UserManager _userManager;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IRepository<OrganizationUnitRole, long> _organizationUnitRoleRepository;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IRepository<SocialConflict> _socialConflictRepository;
        private readonly IRepository<SocialConflictUser> _socialConflictUserRepository;
        private readonly IRepository<AlertResponsible> _alertResponsibleRepository;

        public UserAppService(
            RoleManager roleManager,
            IUserEmailer userEmailer,
            INotificationSubscriptionManager notificationSubscriptionManager,
            IRepository<RolePermissionSetting, long> rolePermissionRepository,
            IRepository<UserPermissionSetting, long> userPermissionRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> roleRepository,
            IUserPolicy userPolicy,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            IPasswordHasher<User> passwordHasher,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRoleManagementConfig roleManagementConfig,
            UserManager userManager,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IRepository<OrganizationUnitRole, long> organizationUnitRoleRepository,
            IWebHostEnvironment webHostEnvironment,
            IRepository<User, long> userRepository,
            IRepository<Person> personRepository,
            IRepository<SocialConflict> socialConflictRepository,
            IRepository<SocialConflictUser> socialConflictUserRepository,
            IRepository<AlertResponsible> alertResponsibleRepository)
        {
            _roleManager = roleManager;
            _userEmailer = userEmailer;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _rolePermissionRepository = rolePermissionRepository;
            _userPermissionRepository = userPermissionRepository;
            _userRoleRepository = userRoleRepository;
            _userPolicy = userPolicy;
            _passwordValidators = passwordValidators;
            _passwordHasher = passwordHasher;
            _organizationUnitRepository = organizationUnitRepository;
            _roleManagementConfig = roleManagementConfig;
            _userManager = userManager;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _organizationUnitRoleRepository = organizationUnitRoleRepository;
            _roleRepository = roleRepository;
            _webHostEnvironment = webHostEnvironment;
            _configurationRoot = webHostEnvironment.GetAppConfiguration();
            _userRepository = userRepository;
            _personRepository = personRepository;
            _socialConflictRepository = socialConflictRepository;
            _socialConflictUserRepository = socialConflictUserRepository;
            _alertResponsibleRepository = alertResponsibleRepository;
            AppUrlService = NullAppUrlService.Instance;
        }

        [AbpAuthorize]
        public async Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input)
        {
            var query = _userRepository
                .GetAll()
                .Include(p => p.Person)
                .Include(p => p.AlertResponsible)
                .WhereIf(string.IsNullOrWhiteSpace(input.EmailAddress) == false, p => p.EmailAddress == input.EmailAddress)
                .WhereIf(input.OnlyLockedUsers, u => u.LockoutEndDateUtc.HasValue && u.LockoutEndDateUtc.Value > DateTime.UtcNow)
                .WhereIf(input.Filter.IsNullOrWhiteSpace() == false, u =>
                        u.Name.Contains(input.Filter) ||
                        u.Surname.Contains(input.Filter) ||
                        u.UserName.Contains(input.Filter) ||
                        u.EmailAddress.Contains(input.Filter));

            var count = await query.CountAsync();
            var result = query.OrderBy(input.Sorting).PageBy(input);

            var output = ObjectMapper.Map<List<UserListDto>>(result);
            await FillRoleNames(output);

            return new PagedResultDto<UserListDto>(count, output);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Create, AppPermissions.Pages_Administration_Users_Edit)]
        public async Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input)
        {
            //Getting all available roles
            var userRoleDtos = await _roleManager.Roles
                .OrderBy(r => r.DisplayName)
                .Select(r => new UserRoleDto
                {
                    RoleId = r.Id,
                    RoleName = r.Name,
                    RoleDisplayName = r.DisplayName
                }).ToArrayAsync();

            var userReponsibles = ObjectMapper.Map<List<UserAlertResponsibleDto>>(_alertResponsibleRepository
                .GetAll()
                .OrderBy(p => p.Name)
                .Where(p => p.Enabled)
                .ToList())
                .ToArray();

            var output = new GetUserForEditOutput
            {
                Roles = userRoleDtos,
                Responsibles = userReponsibles
            };

            if (input.Id.HasValue)
            {
                var user = _userRepository
                    .GetAll()
                    .Include(p => p.Person)
                    .Include(p => p.AlertResponsible)
                    .Where(p => p.Id == input.Id.Value)
                    .First();

                output.User = ObjectMapper.Map<UserEditDto>(user);
                
                var currentRoles = await _userManager.GetRolesAsync(user); 

                foreach (var role in output.Roles)
                {
                    role.IsAssigned = currentRoles.Any(p => p == role.RoleName);
                }
            }
            else
            {
                //Creating a new user
                output.User = new UserEditDto
                {
                    Type = PersonType.None,
                    GeneratePerson = true
                };

                foreach (var defaultRole in await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync())
                {
                    var defaultUserRole = userRoleDtos.FirstOrDefault(ur => ur.RoleName == defaultRole.Name);
                    if (defaultUserRole != null)
                    {
                        defaultUserRole.IsAssigned = true;
                    }
                }
            }

            return output;
        }

        public async Task CreateOrUpdateUser(CreateOrUpdateUserInput input)
        {
            if (input.User.Id.HasValue)
            {
                await UpdateUserAsync(input);
            }
            else
            {
                await CreateUserAsync(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Conflict)]
        public async Task AssignSocialConflicts(UserSocialConflictAssingmentListDto input)
        {
            var user = _userRepository
                .GetAll()
                .Where(p => p.Id == input.UserId)
                .FirstOrDefault();

            if (user == null)
                throw new UserFriendlyException("Aviso", "El usuario solicitado no existe o ya fue eliminado. Por favor verifique la información antes de continuar");

            foreach(var change in input.Assignments)
            {
                if(await _socialConflictRepository.CountAsync(p => p.Id == change.Id) > 0)
                {
                    if(change.Checked)
                    {
                        if(await _socialConflictUserRepository.CountAsync(p => p.SocialConflictId == change.Id && p.UserId == user.Id) == 0)
                        {
                            await _socialConflictUserRepository.InsertAndGetIdAsync(new SocialConflictUser()
                            {
                                SocialConflictId = change.Id,
                                UserId = user.Id
                            });
                        }
                    }
                    else
                    {
                        await _socialConflictUserRepository.DeleteAsync(p => p.SocialConflictId == change.Id && p.UserId == user.Id);
                    }
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Delete)]
        public async Task DeleteUser(EntityDto<long> input)
        {
            if (input.Id == AbpSession.GetUserId())
                throw new UserFriendlyException("Aviso", "No puedes eliminar su misma cuenta");

            var user = await UserManager.GetUserByIdAsync(input.Id);

            if (user.UserName == AbpUserBase.AdminUserName)
                throw new UserFriendlyException("Aviso", "No se puede eliminar la cuenta de administrador");

            CheckErrors(await UserManager.DeleteAsync(user));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Unlock)]
        public async Task UnlockUser(EntityDto<long> input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);

            if (user.UserName == AbpUserBase.AdminUserName)
                throw new UserFriendlyException("Aviso", "No se pueden realizar cambios en la cuenta de administrador");

            user.IsActive = true;
            await UserManager.UpdateAsync(user);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Lock)]
        public async Task LockUser(EntityDto<long> input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);

            if (user.UserName == AbpUserBase.AdminUserName)
                throw new UserFriendlyException("Aviso", "No se pueden realizar cambios en la cuenta de administrador");

            user.IsActive = false;
            await UserManager.UpdateAsync(user);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Edit)]
        protected virtual async Task UpdateUserAsync(CreateOrUpdateUserInput input)
        {
            Debug.Assert(input.User.Id != null, "input.User.Id should be set.");

            var user = await UserManager.FindByIdAsync(input.User.Id.Value.ToString());

            if (user.UserName != AbpUserBase.AdminUserName)
                user.UserName = user.EmailAddress;
            
            //Update user properties
            ObjectMapper.Map(input.User, user); //Passwords is not mapped (see mapping configuration)

            user.Name = (user.Name ?? "").Trim().ToUpper();
            user.Surname = (user.Surname ?? "").Trim().ToUpper();
            user.Surname2 = (user.Surname2 ?? "").Trim().ToUpper();

            if(string.IsNullOrWhiteSpace(user.Document) == false)
                user.Document.VerifyTableColumn(UserConsts.DocumentMinLength,
                    UserConsts.DocumentMaxLength, 
                    DefaultTitleMessage, $"El DNI debe tener {UserConsts.DocumentMaxLength} caracteres");

            if (user.UserName == AbpUserBase.AdminUserName)
            {
                user.Type = PersonType.None;
            }

            if (input.User.AlertResponsible != null)
            {
                if (input.User.AlertResponsible.Id == -1)
                {
                    user.AlertResponsible = null;
                    user.AlertResponsibleId = null;
                }
                else
                {
                    if (await _alertResponsibleRepository.CountAsync(p => p.Id == input.User.AlertResponsible.Id) == 0)
                        throw new UserFriendlyException("Aviso", "La subsecretaría seleccionado ya no existe o fue eliminado. Verifique la información antes de continuar");

                    var responsible = await _alertResponsibleRepository.GetAsync(input.User.AlertResponsible.Id);

                    user.AlertResponsible = responsible;
                    user.AlertResponsibleId = responsible.Id;
                }
            }

            var personNameType = input.User.Type == PersonType.Coordinator ? "Coordinador" :
            input.User.Type == PersonType.Manager ? "Gestor" :
            input.User.Type == PersonType.Analyst ? "Analista" : "";

            if (user.Type != PersonType.None || user.PersonId.HasValue)
            {
                if (input.User.GeneratePerson && user.PersonId.HasValue == false)
                {
                    user.PersonId = await _personRepository.InsertAndGetIdAsync(new Person()
                    {
                        Document = user.Document,
                        Names = user.Name,
                        Surname = user.Surname,
                        Surname2 = user.Surname2,
                        Name = Regex.Replace(string.Concat((user.Surname ?? "").Trim().ToUpper(), " ", (user.Surname2 ?? "").Trim().ToUpper(), ", ", (user.Name ?? "").Trim().ToUpper()).Replace(" ,", ","), @"\s+", " "),
                        EmailAddress = user.EmailAddress,
                        Enabled = true,
                        Type = user.Type
                    });
                }
                else
                {
                    if (input.User.Person == null)
                        throw new UserFriendlyException("Aviso", $"Debe seleccionar el {personNameType} antes de registrar el usuario o presionar en \"Generar automáticamente\"");

                    if(!user.PersonId.HasValue || user.PersonId.Value != input.User.Person.Id)
                    {
                        if (await _personRepository.CountAsync(p => p.Id == input.User.Person.Id) == 0)
                            throw new UserFriendlyException("Aviso", $"El {personNameType} seleccionado no existe o ya fue eliminado. Por favor verifique la información antes de continuar");

                        user.PersonId = input.User.Person.Id;
                    }
                    else
                    {
                        var person = _personRepository
                            .GetAll()
                            .Where(p => p.Id == user.PersonId.Value)
                            .FirstOrDefault();

                        if(person != null)
                        {
                            person.Document = user.Document;
                            person.Names = user.Name;
                            person.Surname = user.Surname;
                            person.Surname2 = user.Surname2;
                            person.Name = Regex.Replace(string.Concat((user.Surname ?? "").Trim().ToUpper(), " ", (user.Surname2 ?? "").Trim().ToUpper(), ", ", (user.Name ?? "").Trim().ToUpper()).Replace(" ,", ","), @"\s+", " ");
                            person.Type = user.Type;
                            person.EmailAddress = user.EmailAddress;

                            await _personRepository.UpdateAsync(person);
                        }
                    }
                }
            }

            CheckErrors(await UserManager.UpdateAsync(user));

            if (input.User.Password.IsNullOrEmpty() == false)
            {
                var key = _configurationRoot["Authentication:AES:Key"];
                var iv = _configurationRoot["Authentication:AES:IV"];

                await UserManager.InitializeOptionsAsync(AbpSession.TenantId);

                CheckErrors(await UserManager.ChangePasswordAsync(user, HelperExtensions.DecryptStringAES(cipherText: input.User.Password, keyText: key, ivText: iv)));
            }

            //Update roles
            CheckErrors(await UserManager.SetRolesAsync(user, input.AssignedRoleNames));

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Users_Create)]
        protected virtual async Task CreateUserAsync(CreateOrUpdateUserInput input)
        {
            if (AbpSession.TenantId.HasValue)
            {
                await _userPolicy.CheckMaxUserCountAsync(AbpSession.GetTenantId());
            }

            var user = ObjectMapper.Map<User>(input.User); //Passwords is not mapped (see mapping configuration)
            user.Name = (user.Name ?? "").Trim().ToUpper();
            user.Surname = (user.Surname ?? "").Trim().ToUpper();
            user.Surname2 = (user.Surname2 ?? "").Trim().ToUpper();
            user.TenantId = AbpSession.TenantId;
            user.IsActive = true;

            if (string.IsNullOrWhiteSpace(user.Document) == false)
                user.Document.VerifyTableColumn(UserConsts.DocumentMinLength,
                    UserConsts.DocumentMaxLength,
                    DefaultTitleMessage, $"El DNI debe tener {UserConsts.DocumentMaxLength} caracteres");

            if (user.UserName != AbpUserBase.AdminUserName)
                user.UserName = user.EmailAddress;

            var personNameType = input.User.Type == PersonType.Coordinator ? "Coordinador" :
                input.User.Type == PersonType.Manager ? "Gestor" :
                input.User.Type == PersonType.Analyst ? "Analista" : "";

            if (user.Type != PersonType.None)
            {
                if (input.User.GeneratePerson)
                {
                    user.PersonId = await _personRepository.InsertAndGetIdAsync(new Person() 
                    {
                        Document = user.Document,
                        Names = user.Name,
                        Surname = user.Surname,
                        Surname2 = user.Surname2,
                        Name = Regex.Replace(string.Concat((user.Surname ?? "").Trim().ToUpper(), " ", (user.Surname2 ?? "").Trim().ToUpper(), ", ", (user.Name ?? "").Trim().ToUpper()).Replace(" ,", ","), @"\s+", " "),
                        Enabled = true,
                        Type = user.Type,
                        EmailAddress = user.EmailAddress
                    });
                }
                else 
                { 
                    if(input.User.Person == null)
                        throw new UserFriendlyException("Aviso", $"Debe seleccionar el {personNameType} antes de registrar el usuario o presionar en \"Generar automáticamente\"");

                    if(await _personRepository.CountAsync(p => p.Id == input.User.Person.Id) == 0)
                        throw new UserFriendlyException("Aviso", $"El {personNameType} seleccionado no existe o ya fue eliminado. Por favor verifique la información antes de continuar");

                    user.PersonId = input.User.Person.Id;
                }
            }

            if (input.User.AlertResponsible != null)
            {
                if(input.User.AlertResponsible.Id == -1)
                {
                    user.AlertResponsible = null;
                    user.AlertResponsibleId = null;
                }
                else
                {
                    if (await _alertResponsibleRepository.CountAsync(p => p.Id == input.User.AlertResponsible.Id) == 0)
                        throw new UserFriendlyException("Aviso", "La subsecretaría seleccionado ya no existe o fue eliminado. Verifique la información antes de continuar");

                    var responsible = await _alertResponsibleRepository.GetAsync(input.User.AlertResponsible.Id);

                    user.AlertResponsible = responsible;
                    user.AlertResponsibleId = responsible.Id;
                }
            }

            //Set password
            if (input.User.Password.IsNullOrEmpty() == false)
            {
                var key = _configurationRoot["Authentication:AES:Key"];
                var iv = _configurationRoot["Authentication:AES:IV"];

                await UserManager.InitializeOptionsAsync(AbpSession.TenantId);

                foreach (var validator in _passwordValidators)
                {
                    CheckErrors(await validator.ValidateAsync(UserManager, user, HelperExtensions.DecryptStringAES(cipherText: input.User.Password, keyText: key, ivText: iv)));
                }

                user.Password = _passwordHasher.HashPassword(user, HelperExtensions.DecryptStringAES(cipherText: input.User.Password, keyText: key, ivText: iv));
            }

            //Assign roles
            user.Roles = new Collection<UserRole>();

            foreach (var roleName in input.AssignedRoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }

            CheckErrors(await UserManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync(); //To get new user's Id.
        }

        private async Task FillRoleNames(IReadOnlyCollection<UserListDto> userListDtos)
        {
            /* This method is optimized to fill role names to given list. */
            var userIds = userListDtos.Select(u => u.Id);

            var userRoles = await _userRoleRepository.GetAll()
                .Where(userRole => userIds.Contains(userRole.UserId))
                .Select(userRole => userRole).ToListAsync();

            var distinctRoleIds = userRoles.Select(userRole => userRole.RoleId).Distinct();

            foreach (var user in userListDtos)
            {
                var rolesOfUser = userRoles.Where(userRole => userRole.UserId == user.Id).ToList();
                user.Roles = ObjectMapper.Map<List<UserListRoleDto>>(rolesOfUser);
            }

            var roleNames = new Dictionary<int, string>();
            foreach (var roleId in distinctRoleIds)
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());
                if (role != null)
                {
                    roleNames[roleId] = role.DisplayName;
                }
            }

            foreach (var userListDto in userListDtos)
            {
                foreach (var userListRoleDto in userListDto.Roles)
                {
                    if (roleNames.ContainsKey(userListRoleDto.RoleId))
                    {
                        userListRoleDto.RoleName = roleNames[userListRoleDto.RoleId];
                    }
                }

                userListDto.Roles = userListDto.Roles.Where(r => r.RoleName != null).OrderBy(r => r.RoleName).ToList();
            }
        }
    }
}
