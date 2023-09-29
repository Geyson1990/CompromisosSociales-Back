using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaces.Dto
{
    public class DialogSpaceDirectoryDistrictRelationDto : EntityDto
    {
        public string Name { get; set; }
        public DialogSpaceDirectoryProvinceRelationDto Province { get; set; }
    }
}
