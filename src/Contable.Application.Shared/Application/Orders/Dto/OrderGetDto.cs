using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Orders.Dto
{
    public class OrderGetDto : EntityDto<long>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public OrderType Type { get; set; }

        public string Description { get; set; }

        public string Observation { get; set; }

        public string Document { get; set; }

        public string Responsible { get; set; }

        public DateTime? OrderDate { get; set; }

        public OrderSocialConflictDto SocialConflict { get; set; }

        public OrderPIPMEFDto PIPMEF { get; set; }

        public OrderTerritorialUnitDto TerritorialUnit { get; set; }
        public OrderDepartmentDto Department { get; set; }
        public OrderProvinceDto Province { get; set; }
        public OrderDistrictDto District { get; set; }
    }
}
