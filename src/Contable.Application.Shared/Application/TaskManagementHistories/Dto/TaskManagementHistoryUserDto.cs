using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.TaskManagementHistories.Dto
{
    public class TaskManagementHistoryUserDto : EntityDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
    }
}
