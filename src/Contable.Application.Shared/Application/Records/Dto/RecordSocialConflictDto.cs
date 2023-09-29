using Abp.Application.Services.Dto;

namespace Contable.Application.Records.Dto
{
    public class RecordSocialConflictDto : EntityDto
    {
        public string Code { get; set; }
        public string CaseName { get; set; }
        public string Dialog { get; set; }
    }
}
