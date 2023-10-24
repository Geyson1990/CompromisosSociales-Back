using System;
using System.Collections.Generic;
using System.Text;

namespace Contable.FileManager
{
    public static class FileManager
    {
        public static void RunImpersonated(CredentialsConfigBlock credentials, Action action)
        {
            if (credentials != null && !credentials.UsePoolIdentity)
            {
                using (WindowsLogin wi = new WindowsLogin(credentials))
                {
                    wi.RunImpersonated(action);
                }
            }
            else
            {
                action();
            }
        }
    }
}
