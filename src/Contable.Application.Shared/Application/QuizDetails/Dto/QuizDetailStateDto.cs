using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizDetails.Dto
{
    public class QuizDetailStateDto : EntityDto
    {
        public string Name { get; set; }
        public string Background { get; set; }
        public string Foreground { get; set; }
    }
}
