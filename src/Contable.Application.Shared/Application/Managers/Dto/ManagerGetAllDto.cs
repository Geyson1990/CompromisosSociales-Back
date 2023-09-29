using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Managers.Dto
{
    public class ManagerGetAllDto : EntityDto
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public bool Enabled { get; set; }
        public ManagerTerritorialUnitDto TerritorialUnit { get; set; }
    }
}
