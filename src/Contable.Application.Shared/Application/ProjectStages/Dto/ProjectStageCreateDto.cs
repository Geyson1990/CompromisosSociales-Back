using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.ProjectStages.Dto
{
    public class ProjectStageCreateDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public List<ProjectStageDetailCreateDto> Details { get; set; }
    }
}
