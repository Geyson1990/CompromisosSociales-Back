using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DialogSpaces.Dto
{
    public class DialogSpaceLeaderRelationDto : EntityDto
    {
        public DialogSpaceDirectoryGovernmentRelationDto DirectoryGovernment { get; set; }
        public List<DialogSpaceTeamRelationDto> Teams { get; set; }
        public bool Remove { get; set; }
    }
}
