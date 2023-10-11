using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.FileManager
{
    public class CredentialsConfigBlock
    {
        public bool UsePoolIdentity { get; set; }
        public string Domain { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
