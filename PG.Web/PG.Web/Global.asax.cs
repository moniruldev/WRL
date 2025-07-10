using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Caching;
using PG.Core.DBBase;

namespace PG.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Globals.SetLicenseInfo();
            Globals.ReadAppSettings();
            DBContextManager.LoadConfigruation();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //check if session is expired/new session then signout auth
            if (Context.Handler is IRequiresSessionState || Context.Handler is IReadOnlySessionState)
            {
                if (Session.IsNewSession)
                {
                    FormsAuthentication.SignOut();
                }
            }

            //if (HttpContext.Current.Request.IsAuthenticated)
            //{

            //    //old authentication, kill it
            //    FormsAuthentication.SignOut();
            //    //or use Response.Redirect to go to a different page
            //    FormsAuthentication.RedirectToLoginPage("Session=Expired");
            //    HttpContext.Current.Response.End();
            //}

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            //if (Session.IsNewSession)
            //{
            //    FormsAuthentication.SignOut();
            //}


            //int x;

            //if (Globals.IsDebug)
            //{
            //    x = 0;
            //}

            // Simulate internet latency on local browsing
            //if (Request.IsLocal)
            //    System.Threading.Thread.Sleep(50);

            //var request = Request;
            //var url = request.Url;
            //var applicationPath = request.ApplicationPath;

            //string fullurl = url.ToString();
            //string baseUrl = url.Scheme + "://" + url.Authority + applicationPath.TrimEnd('/') + '/';

            //string currentRelativePath = request.AppRelativeCurrentExecutionFilePath;

            //if (request.HttpMethod == "GET")
            //{
            //    if (currentRelativePath.EndsWith(".aspx"))
            //    {
            //         get the folder path from relative path. Eg ~/page.aspx returns empty. ~/folder/page.aspx returns folder/                    
            //        var folderPath = currentRelativePath.Substring(2, currentRelativePath.LastIndexOf('/') - 1);
                    
            //        Response.Filter = new  PayRoll.StaticContentFilter(
            //                Response,
            //                relativePath =>
            //                {
            //                    if (Context.Cache[relativePath] == null)
            //                    {
            //                        var physicalPath = Server.MapPath(relativePath);
            //                        var physicalPath = Server.MapPath(currentRelativePath);
            //                        var version = "?v=" +
            //                            new System.IO.FileInfo(physicalPath).LastWriteTime
            //                            .ToString("yyyyMMddhhmmss");
            //                        Context.Cache.Add(relativePath, version, null,
            //                            DateTime.Now.AddMinutes(1), TimeSpan.Zero,
            //                            CacheItemPriority.Normal, null);
            //                        return version;
            //                    }
            //                    else
            //                    {
            //                        return Context.Cache[relativePath] as string;
            //                    }
            //                },
            //                "",
            //                "",
            //                "",
            //                baseUrl,
            //                applicationPath,
            //                folderPath);
            //    }
            //}
        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            ////Only access session state if it is available
            //if (Context.Handler is IRequiresSessionState || Context.Handler is IReadOnlySessionState)
            //{
            //    //If we are authenticated AND we dont have a session here.. redirect to login page.
            //    HttpCookie authenticationCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //    if (authenticationCookie != null)
            //    {
            //        FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(authenticationCookie.Value);
            //        if (!authenticationTicket.Expired)
            //        {
            //            //of course.. replace ANYKNOWNVALUEHERETOCHECK with "UserId" or something you set on the login that you can check here to see if its empty.
            //            //if (Session[ANYKNOWNVALUEHERETOCHECK] == null)
            //            //{
            //            //    //This means for some reason the session expired before the authentication ticket. Force a login.
            //            //    FormsAuthentication.SignOut();
            //            //    Response.Redirect(FormsAuthentication.LoginUrl, true);
            //            //    return;
            //            //}
            //        }
            //    }
            //}
        }



        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            
            //HttpContextWrapper context = new HttpContextWrapper(Context);
            //if (context.Response.StatusCode == 302 && context.Request.IsAjaxRequest())
            //{
            //    context.Response.RedirectLocation = string.Empty;
            //}
        
        }


        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}