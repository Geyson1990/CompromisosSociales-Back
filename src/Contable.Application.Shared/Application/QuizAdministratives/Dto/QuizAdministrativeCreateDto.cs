using Contable.Application.Uploaders.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.QuizAdministratives.Dto
{
    public class QuizAdministrativeCreateDto
    {
        public List<QuizAdministrativeFormDto> Forms { get; set; }
        public List<UploadResourceInputDto> UploadFiles { get; set; }
    }
}
