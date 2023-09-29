using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Orders.Dto
{
    public class OrderPIPMEFUpdateDto : EntityDto<long>
    {
        public string UnifiedCode { get; set; }
        public string SNIPCode { get; set; }
    }
}
