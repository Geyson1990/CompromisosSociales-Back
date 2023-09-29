using Contable.Application.Records.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Uploaders.Dto
{
    public class UploadResourceInputDto
    {
        public string FileName { get; set; }
        public string Size { get; set; }
        public string Extension { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public RecordResourceTypeDto RecordResourceType { get; set; }
    }
}
