using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectStages.Dto
{
    public class ProjectStageUpdateDto : EntityDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public List<ProjectStageDetailCreateDto> Details { get; set; }
        public List<EntityDto> DeletedDetails { get; set; }
    }
}
