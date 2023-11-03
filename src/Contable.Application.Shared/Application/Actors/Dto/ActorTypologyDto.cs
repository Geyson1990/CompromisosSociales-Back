using Abp.Application.Services.Dto;
using Contable.Application.Orders.Dto;
using System.Collections.Generic;

namespace Contable.Application.Actors.Dto
{
    public class ActorTypologyDto : EntityDto
    {
        public string Name { get; set; }
        public List<ActorSubTypologyDto> subTypologies { get; set; }
    }
}