using Abp.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Uploaders.Dto
{
    public class UploadResourceDto : ErrorInfo
    {
        public string FileToken { get; set; }

        public UploadResourceDto()
        {

        }

        public UploadResourceDto(ErrorInfo error)
        {
            Code = error.Code;
            Details = error.Details;
            Message = error.Message;
            ValidationErrors = error.ValidationErrors;
        }
    }
}
