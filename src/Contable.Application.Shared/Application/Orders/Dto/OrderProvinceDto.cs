using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Orders.Dto
{
    public class OrderProvinceDto : EntityDto
    {
        public string Name { get; set; }
        public List<OrderDistrictDto> Districts { get; set; }
    }
}
