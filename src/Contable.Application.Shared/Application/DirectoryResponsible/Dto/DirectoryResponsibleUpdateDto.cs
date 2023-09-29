using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.DirectoryResponsibles.Dto
{
    public class DirectoryResponsibleUpdateDto : EntityDto
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}
