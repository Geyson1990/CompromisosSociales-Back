using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementCompromiseGetAllDto : EntityDto
    {
        public DateTime CreationTime { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TerritorialUnits { get; set; }
        public CompromiseType Type { get; set; }
        public TaskManagementParameterDto Status { get; set; }
        public TaskManagementRecordDto Record { get; set; }
    }
}
