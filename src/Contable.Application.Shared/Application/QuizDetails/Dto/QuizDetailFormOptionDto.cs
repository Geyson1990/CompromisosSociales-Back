using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizDetails.Dto
{
    public class QuizDetailFormOptionDto : EntityDto
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public bool Extra { get; set; }
        public int QuizOptionReferenceId { get; set; }
        public string Description { get; set; }
    }
}
