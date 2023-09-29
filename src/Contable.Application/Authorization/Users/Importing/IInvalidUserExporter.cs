using System.Collections.Generic;
using Contable.Authorization.Users.Importing.Dto;
using Contable.Dto;

namespace Contable.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
