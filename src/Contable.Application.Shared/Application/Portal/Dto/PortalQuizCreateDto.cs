using Contable.Application.Uploaders.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Portal.Dto
{
    public class PortalQuizCreateDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondSurname { get; set; }
        public string EmailAddress { get; set; }
        public List<PortalQuizFormDto> Forms { get; set; }
        public List<UploadResourceInputDto> UploadFiles { get; set; }
    }
}
