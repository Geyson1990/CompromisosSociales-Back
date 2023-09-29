using Abp.Application.Services.Dto;

namespace Contable.Application.Orders.Dto
{
    public class OrderSocialConflictDto : EntityDto
    {
        public string Code { get; set; }
        public string CaseName { get; set; }        
    }
}
