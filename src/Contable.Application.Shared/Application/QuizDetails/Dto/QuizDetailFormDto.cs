using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizDetails.Dto
{
    public class QuizDetailFormDto : EntityDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public bool Required { get; set; }
        public int SelectedOptionId { get; set; }
        public int FormReferenceId { get; set; }

        public List<QuizDetailFormOptionDto> Options { get; set; }
    }
}
