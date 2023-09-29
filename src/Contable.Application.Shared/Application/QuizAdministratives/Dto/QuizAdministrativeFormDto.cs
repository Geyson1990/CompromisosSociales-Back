using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizAdministratives.Dto
{
    public class QuizAdministrativeFormDto : EntityDto
    {
        public string Name { get; set; }
        public bool Required { get; set; }
        public int SelectedOptionId { get; set; }
        public List<QuizAdministrativeFormOptionDto> Options { get; set; }
    }
}
