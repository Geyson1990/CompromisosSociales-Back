using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaces.Dto
{
    public class DialogSpacePersonRelationDto : EntityDto
    {
        public string Name { get; set; }
        public PersonType Type { get; set; }
    }
}
