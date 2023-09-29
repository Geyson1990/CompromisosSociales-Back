using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Orders.Dto
{
    public class OrderGetDataDto : EntityDto<long>
    {
        public OrderGetDto Order { get; set; }
        public List<OrderDepartmentDto> Departments { get; set; }
        public List<OrderTerritorialUnitDto> TerritorialUnits { get; set; }
    }
}
