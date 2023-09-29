using System.Collections.Generic;
using Contable.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace Contable.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
