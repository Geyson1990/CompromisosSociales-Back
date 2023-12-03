using Abp.Application.Services.Dto;

namespace Contable.Application.SocialConflicts.Dto
{
    public class ActaMatrizExportDto : EntityDto
    {
        public string Code { get; set; }
        public string TerritorialUnit { get; set; }
        public string SocialConflictName { get; set; }
        public int Actas { get; set; }
    }
}
