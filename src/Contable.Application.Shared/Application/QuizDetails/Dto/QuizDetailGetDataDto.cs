using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizDetails.Dto
{
    public class QuizDetailGetDataDto
    {
        public QuizDetailGetDto Quiz { get; set; }
        public List<QuizDetailStateDto> States { get; set; }
    }
}
