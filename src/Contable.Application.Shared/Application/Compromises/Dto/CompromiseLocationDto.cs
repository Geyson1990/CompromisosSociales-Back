using Abp.Application.Services.Dto;
using Contable.Application;
using Contable.Application.SocialConflicts.Dto;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseLocationDto : EntityDto
    {
        public CompromiseSocialConflictLocationDto SocialConflictLocation { get; set; }
        public bool Check { get; set; }
    }
}
