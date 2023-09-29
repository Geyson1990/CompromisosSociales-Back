using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizAdministratives.Dto
{
    public class QuizAdministrativeFormOptionDto : EntityDto
    {
        public string Name { get; set; }
        public bool Extra { get; set; }
        public string Response { get; set; }    
    }
}
