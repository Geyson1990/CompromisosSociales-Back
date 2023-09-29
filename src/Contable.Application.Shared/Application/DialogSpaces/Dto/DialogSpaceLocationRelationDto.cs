using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaces.Dto
{
    public class DialogSpaceLocationRelationDto : EntityDto
    {
        public DialogSpaceTerritorialUnitRelationDto TerritorialUnit { get; set; }
        public DialogSpaceDepartmentRelationDto Department { get; set; }
        public DialogSpaceProvinceRelationDto Province { get; set; }
        public DialogSpaceDistrictRelationDto District { get; set; }
        public DialogSpaceRegionRelationDto Region { get; set; }
        public string Ubication { get; set; }
        public bool Remove { get; set; }
    }
}
