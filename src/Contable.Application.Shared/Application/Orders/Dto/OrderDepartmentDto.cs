using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Orders.Dto
{
    public class OrderDepartmentDto : EntityDto
    {
        public int? TerritorialUnitId { get; set; }
        public string Name { get; set; }
        public List<OrderProvinceDto> Provinces { get; set; }
    }
}
