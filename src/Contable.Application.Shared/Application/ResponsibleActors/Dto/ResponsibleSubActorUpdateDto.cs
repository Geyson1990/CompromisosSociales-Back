using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ResponsibleActors.Dto
{
    public class ResponsibleSubActorUpdateDto : EntityDto
    {
        public int ResponsibleActorId { get; set; }
        public string Name { get; set; }
    }
}
