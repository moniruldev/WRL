using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using PG.Core.Web;
using PG.Common;
using PG.Core.Utility;

namespace PG.Web
{
    public class Globals
    {
        public static string AppMasterPage = string.Empty;

        public static string SecurityDBContextName = AppInfo.AppName + "_SecurityContext";
        public static int DataCacheDuration = 120;  //in munite
        public static int FormAuthenticationTimeOut = 30;


        public static bool LinkIncludeRandom = true;
        public static bool IsDebug = false;


        public static string GetAccountingConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Accounting_ConnectionString"].ToString();
        }



        public static string GetSecurityConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Security_ConnectionString"].ToString();
        }

        public static void AfterLoginTask()
        {
            CompanyInfo.SetCompanyInfoToSession(CompanyInfo.CompanyID_Default);
        }


        public static void SetLicenseInfo()
        {
            PG.Core.License.AppLicense.FilePath = HttpContext.Current.Server.MapPath("~/License/");
            //PG.Core.License.AppLicense.isDemo = 
        }

        public static void InitConnectionString()
        {
            //string cStr = GetAccountingConnectionString();
            //PG.Core.DBBase.DBContextSettings defDBContextSettings = PG.Core.DBBase.DBContextManager.GetOrCreateDBContextSettings();
            //defDBContextSettings.ApplicationName = AppInfo.AppName;
            //defDBContextSettings.DatabaseType = PG.Core.DBBase.DatabaseTypeEnum.SQLServer;
            //defDBContextSettings.ConnectionString = cStr;

            //string cStr = GetAccountingConnectionString();
            //PG.Core.DBBase.DBContextSettings defDBContextSettings = PG.Core.DBBase.DBContextManager.GetOrCreateDBContextSettings();
            //defDBContextSettings.ApplicationName = AppInfo.AppName;
            //defDBContextSettings.DatabaseType = PG.Core.DBBase.DatabaseTypeEnum.Oracle;
            //defDBContextSettings.ConnectionString = cStr;

            ////
            //string cStrSecuiry = GetSecurityConnectionString();
            //PG.Core.DBBase.DBContextSettings secDBContextSettings = PG.Core.DBBase.DBContextManager.GetOrCreateDBContextSettings(SecurityDBContextName);
            //secDBContextSettings.ApplicationName = SecurityDBContextName;
            //secDBContextSettings.DatabaseType = PG.Core.DBBase.DatabaseTypeEnum.SQLServer;
            //secDBContextSettings.ConnectionString = cStrSecuiry;
            //BLLibrary.GlobalsBL.SecurityDBContextName = SecurityDBContextName;

           // BLLibrary.GlobalsBL.AppID = Globals.AppID;
        }

        public static void ReadAppSettings()
        {
            string configValue = ConfigurationManager.AppSettings["AppSystem"];
            AppSystemEnum sTye = (AppSystemEnum)Enum.Parse(typeof(AppSystemEnum), configValue, true); 
            // YourEnumType value = (YourEnumType)Enum.Parse(typeof(YourEnumType), configValue);

            AppGlobals.AppSystem = sTye;

            configValue = ConfigurationManager.AppSettings["AppIDUserLogin"];
            AppGlobals.AppIDUserLogin = Boolean.Parse(configValue);


            configValue = ConfigurationManager.AppSettings["LoginInfoAppID"];
            AppGlobals.LoginInfoAppID = Int32.Parse(configValue);


            configValue = ConfigurationManager.AppSettings["LocationLogin"];
            AppGlobals.LocationLogin = Boolean.Parse(configValue);

            configValue = ConfigurationManager.AppSettings["PasswordCaseInsensitive"];
            AppGlobals.PasswordCaseInsensitive = Boolean.Parse(configValue);


            configValue = ConfigurationManager.AppSettings["KeepLive"];
            AppGlobals.KeepLive = Boolean.Parse(configValue);

            configValue = ConfigurationManager.AppSettings["KeepLiveInterval"];
            AppGlobals.KeepLiveInterval = Int32.Parse(configValue);


            configValue = ConfigurationManager.AppSettings["BrowserPrivateMode"];
            AppGlobals.BrowserPrivateMode = Boolean.Parse(configValue);

            configValue = ConfigurationManager.AppSettings["BrowserSupport"];
            AppGlobals.BrowserSupport = configValue.Split(',').ToList();

        }

        public static bool IsBrowserSuppoted(HttpRequest request)
        {
            bool bStatus = false;

            if (AppGlobals.BrowserSupport[0].ToUpper() == "ALL")
            {
                bStatus = true;
            }
            else
            {
                if (AppGlobals.BrowserSupport.Contains(request.Browser.Browser, StringComparer.OrdinalIgnoreCase))
                {
                    bStatus = true;
                }


            }
            return bStatus;
        }


        public static string GetDateFormat()
        {
            string dtFormat = "dd-MMM-yyyy";
            return dtFormat;
        }
        public static string GetDateFormatGrid()
        {
            string dtFormat = "dd-MMM-yyyy";
            return dtFormat;
        }

        public static string GetDateFormatReport()
        {
            string dtFormat = "dd-MMM-yy";
            return dtFormat;
        }

        public static string GetIntFormat()
        {
            string intFormat = "#0";
            return intFormat;
        }
        public static string GetDecimalFormat()
        {
            string dcFormat = "#0.00";
            return dcFormat;
        }
        public static string GetDecimalFormat2()
        {
            string dcFormat = "#0.000";
            return dcFormat;
        }
        public static string GetCurrencyFormat()
        {
            string dcFormat = "#0.00";
            return dcFormat;
        }
        public static string GetCurrencyFormat2()
        {
            string dcFormat = "#0.000";
            return dcFormat;
        }

        public static void ShowMessagePage(AppMessage appMsg)
        {
            int msgID = AppMessage.SetAppMessageToSession(appMsg);
            ShowMessagePage(msgID);
        }

        public static void ShowMessagePage(int pMsgID)
        {
            string qsKey = AppMessage.CreateQueryString(pMsgID);
            string page = "~/Common/Message.aspx?" + qsKey;
            HttpContext.Current.Server.Transfer(page);
        }


        public static string IsDateWithinSameMonth(string issdate,string rcvDate)
        {
            string msg = string.Empty;
            int issYear = Conversion.StringToDate(issdate).Year;
            int rcvYear = Conversion.StringToDate(rcvDate).Year;
            if (issYear == rcvYear)
            {
                int issMonth = Conversion.StringToDate(issdate).Month;
                int rcvMonth = Conversion.StringToDate(rcvDate).Month;
                if (rcvMonth > issMonth)
                {
                    msg = "Receive date and issue date must be within the same month.";                
                }
            }
            else
            {
                msg = "Issue year and receive year must be same.";              
            }
            return msg;
        }

        
    }
}
