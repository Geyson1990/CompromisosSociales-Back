using Contable.Application.Uploaders.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Compromises.Dto
{
    public class CompromiseUploadResourceDto
    {
        public string Description { get; set; }
        public UploadResourceInputDto UploadFile { get; set; }
    }
}
