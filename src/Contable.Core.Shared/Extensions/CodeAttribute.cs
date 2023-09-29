using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application.Extensions
{
    public class CodeAttribute : Attribute
    {
        public string StringValue { get; protected set; }
        public string Description { get; set; }
        public CodeAttribute(string value)
        {
            this.StringValue = value;
        }
    }
}
