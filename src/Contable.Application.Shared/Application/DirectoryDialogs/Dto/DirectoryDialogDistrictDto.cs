using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DirectoryDialogs.Dto
{
    public class DirectoryDialogDistrictDto : EntityDto
    {
        public string Name { get; set; }
        public DirectoryDialogProvinceDto Province { get; set; }
    }
}
