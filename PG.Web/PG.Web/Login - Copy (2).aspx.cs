using PG.BLLibrary.OrganizationBL;
using PG.BLLibrary.SecurityBL;

using PG.Common;
using PG.Core.DBBase;
using PG.Core.Utility;

using PG.DBClass.SecurityDC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PG.Web
{
    public partial class Login : System.Web.UI.Page
    {
        int CompanyID = 0;
        public string GetJSonDataServiceLink = PageLinks.SystemLinks.GetLink_GetJSonData;
        string UserName = string.Empty;
        string Password = string.Empty;
        string Key = string.Empty;
     

        protected void Page_Load(object sender, EventArgs e)
        {

            this.CompanyID = CompanyInfo.CompanyID_Default;
            this.hdnCompanyID.Value = this.CompanyID.ToString();
            this.hdnBrowserPrivateMode.Value = AppGlobals.BrowserPrivateMode ? "1" : "0";
            this.hdnBrowserSupported.Value = Globals.IsBrowserSuppoted(Request) ? "1" : "0";

            //this.UserName = Request.QueryString["UserName"];
            //this.Password = Request.QueryString["Password"];
            //this.Key = Request.QueryString["Key"];
       
            if (Request.Browser.Browser == "IE")
            {
                decimal d;
                decimal.TryParse(Request.Browser.Version, out d);
                if (d < 8)
                {
                    lblStatus.Text = "Please User Internet Explorer 8.0 or Higher.";
                }
            }


            if (!IsPostBack)
            {
#if DEBUG
                hdnIsDebug.Value = "1";
                hdnUserID.Value = "Admin";
                hdnPass.Value = "abcd29n";
                if (AppGlobals.LocationLogin == true)
                {
                    txtLocationCode.Text = "00";
                    hdnLocationID.Value = "1";
                }

#endif
            }
            //else
            //{
            //    hdnUserID.Value = "Admin";
            //    hdnPass.Value = "abcd29n";
            //    txtLocationCode.Text = "00";
            //    hdnLocationID.Value = "1";
            //}

           
         
        }


        private void FillCombo()
        {

    

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            if (AppGlobals.LocationLogin)
            {
                if (txtLocationCode.Text.Trim() == "")
                {
                    lblStatus.Text = @"Please Enter Location.";
                    return;
                }

                int locationID = Conversion.StringToInt(hdnLocationID.Value);

                if (locationID == 0)
                {
                    lblStatus.Text = @"Location not valid!";
                    return;
                }
            }


            if ((txtUser.Text.Trim() == "") || (txtPassword.Text == ""))
            {
                lblStatus.Text = @"User or password is empty!!";
                return;
            }

            DoLogin();

        }
    
        private void DoLogin()
        {
            int userKey = 0;
            string userID = txtUser.Text.Trim();

            int locationID = Conversion.StringToInt(hdnLocationID.Value);
            int companyID = Conversion.StringToInt(hdnCompanyID.Value);

            AppGlobals.LoginInfoAppID = AppGlobals.AppIDUserLogin ? AppInfo.AppID : AppInfo.AppIDDefault;

            if (BLLibrary.SecurityBL.UserBL.Login(AppGlobals.LoginInfoAppID, userID, txtPassword.Text, out userKey, AppGlobals.PasswordCaseInsensitive))
            {
                if (AppGlobals.LocationLogin)
                {
                    bool UserLocationValid = LocationUserBL.IsLocationUserLoginAllowed(locationID, userKey);
                    if (!UserLocationValid)
                    {
                        lblStatus.Text = @"Invalid User Location! Please Select valid Location";
                        return;
                    }
                }

                if (!BLLibrary.SystemsBL.AppInfoBL.CheckAppRegistration(AppInfo.AppID))
                {
                    lblStatus.Text = @"Application Registration Expired! Please contact with Program Administrator";
                    return;
                }

                Globals.AfterLoginTask();


                AppSecurity.SetUserInfoToSession(userKey, companyID, locationID);
                FormsAuthenticationTicket tkt = new FormsAuthenticationTicket
                    (
                     1, // version
                     userID, // name
                     DateTime.Now,  // issueDate
                     DateTime.Now.Add(FormsAuthentication.Timeout), // expiration
                     false, // isPersistent
                     userKey.ToString(), // userData
                     FormsAuthentication.FormsCookiePath
                    );

                string encrTkt = FormsAuthentication.Encrypt(tkt);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrTkt);
                Response.Cookies.Add(cookie);

                //FormsAuthentication.SetAuthCookie(userID, false);

                string nextPage = "";
                if (Request.QueryString["ReturnURL"] != null)
                {
                    // user attempted to access a page without logging in so redirect
                    // them to their originally requested page
                    nextPage = Request.QueryString["ReturnURL"];
                }
                else
                {
                    // user came straight to the login page so just send them to the
                    // home page
                    nextPage = @"~\Main.aspx";
                }

                Response.Redirect(nextPage, false);
                HttpContext.Current.Response.Clear();
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                lblStatus.Text = @"Invalid Login!!";
            }

           
        }


        private void DoLoginGL()
        {
            //int userKey = 0;
            //string userID = txtUser.Text.Trim();

            //int locationID = Conversion.StringToInt(hdnLocationID.Value);
            //int companyID = Conversion.StringToInt(hdnCompanyID.Value);
            //if (EmpInfoBL.Login(userID, txtPassword.Text))
            //{

            //    //Globals.AfterLoginTask();


            //    //AppSecurity.SetUserInfoToSession(userKey, companyID, locationID);
            //    FormsAuthenticationTicket tkt = new FormsAuthenticationTicket
            //        (
            //         1, // version
            //         userID, // name
            //         DateTime.Now,  // issueDate
            //         DateTime.Now.Add(FormsAuthentication.Timeout), // expiration
            //         false, // isPersistent
            //         userID.ToString(), // userData
            //         FormsAuthentication.FormsCookiePath
            //        );

            //    string encrTkt = FormsAuthentication.Encrypt(tkt);
            //    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrTkt);
            //    Response.Cookies.Add(cookie);

            //    //FormsAuthentication.SetAuthCookie(userID, false);

            //    string nextPage = "";
            //    if (Request.QueryString["ReturnURL"] != null)
            //    {
            //        // user attempted to access a page without logging in so redirect
            //        // them to their originally requested page
            //        nextPage = Request.QueryString["ReturnURL"];
            //    }
            //    else
            //    {
            //        // user came straight to the login page so just send them to the
            //        // home page
            //        nextPage = @"~\Main.aspx";
            //    }

            //    Response.Redirect(nextPage, false);
            //    HttpContext.Current.Response.Clear();
            //    Context.ApplicationInstance.CompleteRequest();
            //}
            //else
            //{
            //    lblStatus.Text = @"Invalid Login!!";
            //}
        }

        protected string GetIPAddressNew()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        private string GetUserIP()
        {
            string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipList))
            {
                return ipList.Split(',')[0];
            }

            return Request.ServerVariables["REMOTE_ADDR"];
        }

        protected static string GetIPAddress()
        {
            string Str = "";
            Str = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(Str);
            IPAddress[] addr = ipEntry.AddressList;
            return addr[addr.Length - 1].ToString();
        }

        protected static string GetPCName()
        {
            string Str = "";
            Str = System.Net.Dns.GetHostName();
            //IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(Str);
            //IPAddress[] addr = ipEntry.AddressList;
            return Str.ToString();
        }

    }
}