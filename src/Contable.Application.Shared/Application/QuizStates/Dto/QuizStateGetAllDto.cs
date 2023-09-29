using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizStates.Dto
{
    public class QuizStateGetAllDto : EntityDto
    {
        public string Name { get; set; }
        public string Background { get; set; }
        public string Foreground { get; set; }
        public bool Enabled { get; set; }
        public bool Default { get; set; }
    }
}
