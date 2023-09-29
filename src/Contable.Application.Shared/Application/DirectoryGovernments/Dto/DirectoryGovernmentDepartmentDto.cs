using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DirectoryGovernments.Dto
{
    public class DirectoryGovernmentDepartmentDto : EntityDto
    {
        public string Name { get; set; }
        public List<DirectoryGovernmentProvinceDto> Provinces { get; set; }
    }
}
