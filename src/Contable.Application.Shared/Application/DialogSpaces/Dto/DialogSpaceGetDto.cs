using Abp.Application.Services.Dto;
using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contable.Application.DialogSpaces.Dto
{
    public class DialogSpaceGetDto : EntityDto
    {
        public int Year { get; set; }
        public int Count { get; set; }
        public string Code { get; set; }
        public string CaseName { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public DialogSpaceDialogSpaceTypeRelatioDto DialogSpaceType { get; set; }
        public DialogSpaceSocialConflictRelationDto SocialConflict { get; set; }
        public DialogSpacePersonRelationDto Person { get; set; }

        public DialogSpaceUserDto CreatorUser { get; set; }
        public DialogSpaceUserDto EditUser { get; set; }

        public List<DialogSpaceLocationRelationDto> Locations { get; set; }
        public List<DialogSpaceLeaderRelationDto> Leaders { get; set; }
    }
}
