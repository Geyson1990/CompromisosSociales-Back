using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DirectoryDialogs.Dto
{
    public class DirectoryDialogProvinceDto : EntityDto
    {
        public string Name { get; set; }
        public DirectoryDialogDepartmentDto Department { get; set; }
    }
}
