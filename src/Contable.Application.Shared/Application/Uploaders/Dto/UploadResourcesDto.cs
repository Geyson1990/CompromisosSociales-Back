using Abp.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Uploaders.Dto
{
    public class UploadResourcesDto : ErrorInfo
    {
        public List<string> FileTokens { get; set; }

        public UploadResourcesDto() => FileTokens = new List<string>();

        public UploadResourcesDto(ErrorInfo error)
        {
            this.Code = error.Code;
            this.Details = error.Details;
            this.Message = error.Message;
            this.ValidationErrors = error.ValidationErrors;
        }
    }
}
