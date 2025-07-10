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
using PG.DBClass.SecurityDC;
using PG.Core.DBBase;


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
            
            //this.CompanyID = CompanyInfo.GetCompanyID();
            //this.hdnCompanyID.Value = this.CompanyID.ToString();
            this.CompanyID = 1;
            this.hdnCompanyID.Value = this.CompanyID.ToString();
            this.hdnBrowserPrivateMode.Value = AppGlobals.BrowserPrivateMode ? "1" : "0";
            this.hdnBrowserSupported.Value = Globals.IsBrowserSuppoted(Request) ? "1" : "0";
            this.UserName = Request.QueryString["UserName"];
            this.Password = Request.QueryString["Password"];
            this.Key = Request.QueryString["Key"];

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

            if (UserName != null)
            {

                if (CheckValidity(this.UserName, this.Key, null))
                {
                    //this.DoLogin();
                    txtUser.Text = Request.QueryString["UserName"];
                    txtPassword.Text = ConvertToDecrypt(Request.QueryString["Password"]);
                    //dcUser User = new dcUser();
                    //if (AppGlobals.LocationLogin)
                    //{

                    //    User = UserBL.GetUserPassByUserName(this.UserName, null);
                    //    if (User != null)
                    //    {
                    //        txtLocationCode.Text = User.LoginLocationCode;
                    //        hdnLocationID.Value = User.LoginLocationID.ToString();
                    //    }
                    //}

                }
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
          
            #endif


            if (!IsPostBack)
            {
                FillCombo();
                btnLogin_Click(sender, e);
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

        private bool CheckValidity(string UserName, string Key, DBContext dc)
        {
            List<dcUser> userList = new List<dcUser>();
            bool IsValid = false;
            bool isDCInit = false;
            int NewID = 0;
            try
            {

                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                DBCommandInfo cmdInfo = new DBCommandInfo();
                DBQuery dbq = new DBQuery();
                dbq.DBQueryMode = DBQueryModeEnum.DBCommandInfo;

                string abbr = " SELECT * FROM USER_LOGIN_CODE @SPB104 WHERE 1=1 AND UPPER(USER_NAME) = UPPER(@UserName) AND LOGIN_CODE=@LoginCode ";

                //cmdInfo.DBParametersInfo.Add("@UserID", user.UserID);
                cmdInfo.DBParametersInfo.Add("@UserName", UserName);
                cmdInfo.DBParametersInfo.Add("@LoginCode", Key);



                cmdInfo.CommandText = abbr;
                cmdInfo.CommandType = CommandType.Text;
                dbq.DBCommandInfo = cmdInfo;
                //NewID = DBQuery.ExecuteDBNonQuery(dbq, dc);
                userList = DBQuery.ExecuteDBQuery<dcUser>(dbq, dc);
                if (userList.Count > 0)
                {
                    IsValid = true;
                }

                // _TRANS_ID = pTRANS_ID.ToString();
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }

            return IsValid;
        }

        public static string ConvertToDecrypt(string info)
        {
            string Key1 = "bdr@3#$f";
            string Key2 = "bd12*mpk!";
            if (string.IsNullOrEmpty(info)) return "";
            var infoBytes = Convert.FromBase64String(info);
            var result = Encoding.UTF8.GetString(infoBytes);
            result = result.Substring(Math.Max(0, Key1.Length));
            result = result.Substring(0, result.Length - Key2.Length);
            return result;

        }

    }
}
