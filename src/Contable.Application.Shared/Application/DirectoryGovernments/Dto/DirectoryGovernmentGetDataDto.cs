using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DirectoryGovernments.Dto
{
    public class DirectoryGovernmentGetDataDto : EntityDto
    {
        public DirectoryGovernmentGetDto DirectoryGovernment { get; set; }
        public List<DirectoryGovernmentSectorDto> Sectors { get; set; }
        public List<DirectoryGovernmentDepartmentDto> Departments { get; set; }
        public List<DirectoryGovernmentTypeDto> Types { get; set; }
    }
}
