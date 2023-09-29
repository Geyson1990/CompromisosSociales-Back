using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizDetails.Dto
{
    public class QuizDetailUpdateDto : EntityDto
    {
        public QuizDetailStateDto QuizState { get; set; }
    }
}
