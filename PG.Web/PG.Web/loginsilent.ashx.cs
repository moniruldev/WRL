using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

using PG.Core.Web;

namespace PG.Web
{
    /// <summary>
    /// Summary description for loginsilent
    /// </summary>
    public class loginsilent : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            int companyID = WebUtility.GetQueryStringInteger("companyid", context);
            int appID = WebUtility.GetQueryStringInteger("appid", context);
            int locationid = WebUtility.GetQueryStringInteger("locationid", context);
            string userID = WebUtility.GetQueryString("userid", context);
            string passWord = WebUtility.GetQueryString("password", context);

            string strReturn = string.Empty;

            System.Diagnostics.Debug.Write("auth: " + (context.Request.IsAuthenticated ? "True" : "False"));
            if (context.Request.IsAuthenticated)
            {
                strReturn = "ok";
                
            }
            else
            {
                int userKey = 0;
                if (BLLibrary.SecurityBL.UserBL.Login(AppInfo.AppID, userID, passWord, out userKey))
                {
                    //Globals.AfterLoginTask();
                    //AppSecurity.SetUserInfoToSession(userKey);
                    FormsAuthenticationTicket tkt = new FormsAuthenticationTicket(1, userID, DateTime.Now, DateTime.Now.AddMinutes(Globals.FormAuthenticationTimeOut), true, userKey.ToString());
                    string encrTkt = FormsAuthentication.Encrypt(tkt);
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrTkt);
                    context.Response.Cookies.Add(cookie);
                    strReturn = "ok";
                }
                else
                {
                    strReturn = "login failed!";
                }
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(strReturn);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}