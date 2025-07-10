using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using PG.BLLibrary.OrganizationBL;
using PG.DBClass.OrganiztionDC;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using PG.Core.Utility;
using PG.Common;
using PG.BLLibrary.SecurityBL;


namespace PG.Web
{
    public partial class Login_Bak : System.Web.UI.Page
    {
        int CompanyID = 0;
        public string GetJSonDataServiceLink = PageLinks.SystemLinks.GetLink_GetJSonData;
        

  

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //this.CompanyID = CompanyInfo.GetCompanyID();
            //this.hdnCompanyID.Value = this.CompanyID.ToString();
            this.CompanyID = 1;
            this.hdnCompanyID.Value = this.CompanyID.ToString();
            this.hdnBrowserPrivateMode.Value = AppGlobals.BrowserPrivateMode ? "1" : "0";
            this.hdnBrowserSupported.Value = Globals.IsBrowserSuppoted(Request) ? "1" : "0";

            this.Title = AppInfo.AppNameFull + " Login";
            this.lblAppName.Text = AppInfo.AppNameFull;

            if (Request.Browser.Browser == "IE")
            {
                decimal d;
                decimal.TryParse(Request.Browser.Version,out d);
                if (d < 8)
                {
                    lblStatus.Text = "Please User Internet Explorer 8.0 or Higher.";
                }
            }

            lblVersion.Text = "Version: " + AppInfo.GetAppVersion();


            if (AppGlobals.LocationLogin == false)
            {
                lblLocation.Visible = false;
                txtLocationCode.Visible = false;
                txtLocationName.Visible = false;
            }

            //if (AppGlobals.AppSystem == AppSystemEnum.GL)
            //{
            //    lblLocation.Visible = false;
            //    txtLocationCode.Visible = false;
            //    txtLocationName.Visible = false;
            //}

            //System.Diagnostics.Debug.Assert(


            //hdnPass.Value = HttpContext.Current.IsDebuggingEnabled ? "abcd" : "";

            #if DEBUG
            hdnIsDebug.Value = "1";
            hdnUserID.Value = "Admin";
            hdnPass.Value = "abcd29n";
            if (AppGlobals.LocationLogin == true)
            {
                txtLocationCode.Text = "02";
                txtLocationName.Text = "Dhaka Depot";
                hdnLocationID.Value = "3";
            }
            #endif


            if (!IsPostBack)
            {
                FillCombo();
            }

            this.txtLocationName.Attributes.Add("readonly", "readonly");

            //string cpuid = PG.Core.Registration.AppRegistration.GetSystemCPUID();
            //lblStatus.Text = cpuid;
            
            //string str = Request.UserAgent;

            //System.Diagnostics.Debug.WriteLine(Request.Browser.Browser);
            //System.Diagnostics.Debug.WriteLine(Request.Browser.Version);
        }


        private void FillCombo()
        {

            //ddlCompany.DataTextField = "CompanyName";
            //ddlCompany.DataValueField = "CompanyID";
            //ddlCompany.AppendDataBoundItems = true;
            //ddlCompany.DataSource = CompanyBL.GetCompanyList();
            //ddlCompany.DataBind();

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

            //if (usr.Login(txtUser.Text, txtPassword.Text, out GroupID))
            //{




            //    //lblStatus.Text="authenticated";

            //    string nextPage = "";

            //    //FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(txtUser.Text,true,20);
            //    FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, txtUser.Text, DateTime.Now, DateTime.Now.AddMinutes(180), true, GroupID);

            //    string encrTkt = FormsAuthentication.Encrypt(tkt);

            //    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrTkt);

            //    Response.Cookies.Add(cookie);

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


            //    Response.Redirect(nextPage);

            //}
            //else
            //{
            //    lblStatus.Text = @"Invalid Login!!";
            //}
        }


        private void DoLoginGL()
        {
            int userKey = 0;
            string userID = txtUser.Text.Trim();

            int locationID = 1; //Conversion.StringToInt(hdnLocationID.Value);
            int companyID = Conversion.StringToInt(hdnCompanyID.Value);
            if (BLLibrary.SecurityBL.UserBL.Login(AppInfo.AppID, userID, txtPassword.Text, out userKey))
            {

                //bool UserLocationValid = LocationUserBL.IsLocationUserLoginAllowed(locationID, userKey);
                //if (!UserLocationValid)
                //{
                //    lblStatus.Text = @"Invalid User Location! Please Select valid Location";
                //    return;
                //}

                if (!BLLibrary.SystemsBL.AppInfoBL.CheckAppRegistration(AppInfo.AppID))
                {
                    lblStatus.Text = @"Application Registration Expired! Please contact with Program Administrator";
                    return;
                }

                Globals.AfterLoginTask();


              //  AppSecurity.SetUserInfoToSession(userKey);
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
            /*if (EmpInfoBL.Login(userID, txtPassword.Text))
            {

                //Globals.AfterLoginTask();


                //AppSecurity.SetUserInfoToSession(userKey, companyID, locationID);
                FormsAuthenticationTicket tkt = new FormsAuthenticationTicket
                    (
                     1, // version
                     userID, // name
                     DateTime.Now,  // issueDate
                     DateTime.Now.Add(FormsAuthentication.Timeout), // expiration
                     false, // isPersistent
                     userID.ToString(), // userData
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
            }*/
            else
            {
                lblStatus.Text = @"Invalid Login!!";
            }
        }

    }
}
