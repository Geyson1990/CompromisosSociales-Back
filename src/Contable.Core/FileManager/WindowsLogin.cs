using System;
using System.Security.Principal;

namespace Contable.FileManager
{
    public class WindowsLogin : System.IDisposable
    {
        protected const int LOGON32_PROVIDER_DEFAULT = 0;
        protected const int LOGON32_LOGON_INTERACTIVE = 2;
        protected const int LOGON32_LOGON_NEW_CREDENTIALS = 9;

        public WindowsIdentity Identity = null;
        private System.IntPtr m_accessToken;


        [System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LogonUser(string lpszUsername, string lpszDomain,
        string lpszPassword, int dwLogonType, int dwLogonProvider, ref System.IntPtr phToken);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private extern static bool CloseHandle(System.IntPtr handle);

        public WindowsLogin()
        {
            this.Identity = WindowsIdentity.GetCurrent();
        }
        public WindowsLogin(CredentialsConfigBlock configBlock)
        {
            if (configBlock.UsePoolIdentity == false)
            {
                Login(configBlock.User, configBlock.Domain, configBlock.Password);
            }
        }
        public WindowsLogin(string username, string domain, string password)
        {
            Login(username, domain, password);
        }

        private void Login(string username, string domain, string password)
        {
            if (this.Identity != null)
            {
                this.Identity.Dispose();
                this.Identity = null;
            }


            try
            {
                this.m_accessToken = new System.IntPtr(0);
                Logout();

                this.m_accessToken = System.IntPtr.Zero;
                bool logonSuccessfull = LogonUser(
                   username,
                   domain,
                   password,
#if DEBUG
                   LOGON32_LOGON_NEW_CREDENTIALS,
#else
                   LOGON32_LOGON_INTERACTIVE,
#endif
                   LOGON32_PROVIDER_DEFAULT,
                   ref this.m_accessToken);

                if (!logonSuccessfull)
                {
                    int error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                    throw new System.ComponentModel.Win32Exception(error);
                }
                Identity = new WindowsIdentity(this.m_accessToken);
            }
            catch
            {
                throw;
            }

        } // End Sub Login 

        private void Logout()
        {
            if (this.m_accessToken != System.IntPtr.Zero)
                CloseHandle(m_accessToken);

            this.m_accessToken = System.IntPtr.Zero;

            if (this.Identity != null)
            {
                this.Identity.Dispose();
                this.Identity = null;
            }

        } // End Sub Logout 

        void System.IDisposable.Dispose()
        {
            //Logout();
        } // End Sub Dispose 

        public void RunImpersonated(Action action)
        {
            //Si no se ha establecido identity (no se ha logueado la instancia actual)
            if (this.Identity?.AccessToken == null)
            {
                action();
            }
            else
            {
                WindowsIdentity.RunImpersonated(this.Identity.AccessToken, action);
            }
        }
    }
}
