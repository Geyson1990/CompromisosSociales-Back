using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Managers.Dto
{
    public class ManagerGetDto : EntityDto
    {
        public string Document { get; set; }
        public string Names { get; set; }
        public string Surname { get; set; }
        public string Surname2 { get; set; }
        public string EmailAddress { get; set; }
        public bool Enabled { get; set; }
        public ManagerTerritorialUnitDto TerritorialUnit { get; set; }
    }
}
