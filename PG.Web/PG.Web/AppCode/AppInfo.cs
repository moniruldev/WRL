using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Reflection;

using PG.DBClass.SystemDC;
using PG.Report;

namespace PG.Web
{
    public class AppInfo
    {
        public static int AppIDDefault = 1;

        public static int AppID = 1;
        public static string AppCode = "ISACC";

        public static decimal AppVersion = 0.9M;
        public static string AppVersionString = "0.9";
        public static string AppReleaseDate = "08-Jun-2014";

        public static int AppBuildNumber = 1;
        public static int AppDBVersion = 1;


        public static int AppCompanyID = 1;
        public static string AppName = "WRX";
        public static string AppNameFull = "World Runner";
        public static string AppCompanyName = "IT";
        public static string AppCompanyWeb = "";
        public static string AppCompanyEmail = "";
        public static string AppCompanyCopyright = "";

        public static string AppNameImage = "topbannertext.png";
        public static string AppLogoImage = "applogo.png";

        //public static string AppCompanyNameImage = "";
        //public static string AppCompanyLogoImage = "";


        public static dcAppInfo AppInfoFromDB = null;



        //public static int AppCompanyID = 2;
        //public static string AppName = "SystechAccounting";
        //public static string AppNameFull = "Systech Accounting";
        //public static string AppCompanyName = "Systech Digital";
        //public static string AppCompanyWeb = "www.systechdigital.com";
        //public static string AppCompanyEmail = "admin@systechdigital.com";
        //public static string AppCompanyCopyright = "www.systechdigital.com";

        //public static string AppNameImage = "topbannertextsys.png";
        //public static string AppLogoImage = "applogo.png";


        //public static string AppCompanyNameImage = "";
        //public static string AppCompanyLogoImage = "";



        
        public static void SetAppInfoToReportOptions(ReportOptions rptOption)
        {
            rptOption.AppCompanyName = AppInfo.AppCompanyName;
            rptOption.AppPoweredBy = AppInfo.AppCompanyWeb;
        }

        public static void SetAppInfoToReportOptions(NameValueCollection rptParams)
        {
            rptParams.Add("AppCompanyName", AppInfo.AppCompanyName);
            rptParams.Add("AppPoweredBy", AppInfo.AppCompanyWeb);
        }


        public static string GetAppVersion()
        {
            string version = string.Empty;

            Assembly web = Assembly.GetExecutingAssembly();
            AssemblyName webName = web.GetName();

            version = webName.Version.ToString();
            return version;
        }
    }
}