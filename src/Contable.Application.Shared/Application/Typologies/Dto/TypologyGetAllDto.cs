using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Typologies.Dto
{
    public class TypologyGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public List<TypologySubTypologyGetAllDto> SubTypologies { get; set; }
    }
}
