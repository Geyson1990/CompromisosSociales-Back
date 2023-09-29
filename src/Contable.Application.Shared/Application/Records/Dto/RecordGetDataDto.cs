using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;

namespace Contable.Application.Records.Dto
{
    public class RecordGetDataDto
    {
        public RecordGetDto Record { get; set; }
        public List<RecordResourceTypeDto> ResourceTypes { get; set; }
    }
}
