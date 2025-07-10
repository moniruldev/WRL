using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Runtime.InteropServices;

namespace PG.Core.Web
{
    public class Impersonate
    {
        //for details: http://support.microsoft.com/kb/306158
        //http://www.west-wind.com/weblog/posts/2153.aspx
        //http://west-wind.com/weblog/posts/1572.aspx
        //http://msdn.microsoft.com/en-us/library/ms998292.aspx
        //http://msdn.microsoft.com/en-us/library/ms998297.aspx

        ///by configfile : web.config
        ///<identity impersonate="true" />
        ///

        ///for specifig user
        //<identity impersonate="true" userName="accountname" password="password" />
        public static int LOGON32_LOGON_INTERACTIVE = 2;
        public static int LOGON32_PROVIDER_DEFAULT = 0;

        //
        //const int LOGON32_LOGON_INTERACTIVE = 2;
        const int LOGON32_LOGON_NETWORK = 3;
        const int LOGON32_LOGON_BATCH = 4;
        const int LOGON32_LOGON_SERVICE = 5;
        const int LOGON32_LOGON_UNLOCK = 7;
        const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;
        const int LOGON32_LOGON_NEW_CREDENTIALS = 9;
        //const int LOGON32_PROVIDER_DEFAULT = 0;



        WindowsImpersonationContext impersonationContext; 


        public void Test()
        {
            System.Security.Principal.WindowsImpersonationContext impersonationContext;
            impersonationContext =
                ((System.Security.Principal.WindowsIdentity) System.Web.HttpContext.Current.User.Identity).Impersonate();

            //Insert your code that runs under the security context of the authenticating user here.

            impersonationContext.Undo();
        }


        private bool impersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                    LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }
            if (token != IntPtr.Zero)
                CloseHandle(token);
            if (tokenDuplicate != IntPtr.Zero)
                CloseHandle(tokenDuplicate);
            return false;
        }

        private void undoImpersonation()
        {
            impersonationContext.Undo();
        }


        private void Example()
        {
            if (impersonateValidUser("username", "domain", "password"))
            {
                //Insert your code that runs under the security context of a specific user here.
                undoImpersonation();
            }
            else
            {
                //Your impersonation failed. Therefore, include a fail-safe mechanism here.
            }
        }


        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);




        ////////////////////////////////
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int LogonUser(
            string lpszUsername,
            string lpszDomain,
            string lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            out IntPtr phToken
            );
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int ImpersonateLoggedOnUser(
            IntPtr hToken
        );

        //[DllImport("advapi32.dll", SetLastError = true)]
        //static extern int RevertToSelf();

        //[DllImport("kernel32.dll", SetLastError = true)]
        //static extern int CloseHandle(IntPtr hObject);


        public static void Example2()
        {
            IntPtr lnToken;
            int TResult = LogonUser("ricks", ".", "supersecret",
                        LOGON32_LOGON_NETWORK, LOGON32_PROVIDER_DEFAULT,
                        out lnToken);
            if (TResult > 0)
            {
                ImpersonateLoggedOnUser(lnToken);
                StringBuilder sb = new StringBuilder(80, 80);

                //uint Size = 79;
                //Response.Write(Environment.UserName + " - " +
                //        this.User.Identity.Name + "<hr>");

                RevertToSelf();
               // Response.Write("<hr>" + Environment.UserName);

                CloseHandle(lnToken);
            }
            else
            {
                //Response.Write("Not logged on: " + Environment.UserName);
            }


            return;

        }

    }
}
