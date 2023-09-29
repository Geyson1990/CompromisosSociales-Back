using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaces.Dto
{
    public class DialogSpaceTeamRelationDto : EntityDto
    {
        public string Name { get; set; }
        public bool Remove { get; set; }
    }
}
