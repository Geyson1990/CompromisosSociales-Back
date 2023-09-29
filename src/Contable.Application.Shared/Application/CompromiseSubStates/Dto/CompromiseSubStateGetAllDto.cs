using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.CompromiseSubStates.Dto
{
    public class CompromiseSubStateGetAllDto : EntityDto
    {
        public CompromiseSubStateStateRelationDto CompromiseState { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
