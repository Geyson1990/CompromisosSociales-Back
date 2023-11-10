using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Actors.Dto
{
    public class ActorUserDto : EntityDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
