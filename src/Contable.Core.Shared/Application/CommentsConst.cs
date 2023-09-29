using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.Application
{
    public class CommentsConst
    {
        public const int DescriptionMinLength = 0;
        public const int DescriptionMaxLength = 500;
        public const string DescriptionType = "VARCHAR(500)";

        public const int MaxMessageLength = 4 * 1024; //4KB

    }
}
