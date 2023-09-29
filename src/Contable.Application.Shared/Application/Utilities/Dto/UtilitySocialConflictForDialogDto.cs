using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Utilities.Dto
{
    public class UtilitySocialConflictForDialogDto : EntityDto
    {
        public DateTime? LastModificationTime { get; set; }
        public DateTime CreationTime { get; set; }
        public string Code { get; set; }
        public string CaseName { get; set; }
        public string TerritorialUnits { get; set; }
    }
}
