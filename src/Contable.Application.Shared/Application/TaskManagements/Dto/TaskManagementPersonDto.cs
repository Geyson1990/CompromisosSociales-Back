using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagements.Dto
{
    public class TaskManagementPersonDto : EntityDto
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public PersonType Type { get; set; }
    }
}