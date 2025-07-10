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

namespace PG.Web
{
    public class AppCache
    {
        public static string CacheKey_AppMenu = "ck_AppMenu";
        public static string CacheKey_Users = "ck_Users";
        public static string CacheKey_Roles = "ck_Roles";
        public static string CacheKey_RolesPermission = "ck_RolesPermission";


        public static string CacheKey_WeekDay = "ck_WeekDay";


        public static string CacheKey_SysOption = "ck_SysOption";


        public static string Insert(string pKey, object pData)
        {
            return Insert(pKey, pData, Globals.DataCacheDuration);
        }
        public static string Insert(string pKey, object pData, int pMinutes)
        {
            HttpRuntime.Cache.Insert(pKey, pData, null, DateTime.Now.AddMinutes(pMinutes), TimeSpan.Zero);
            return pKey;
        }

        public static void Remove(string pKey)
        {
            if (HttpRuntime.Cache[pKey] != null)
            {
                HttpRuntime.Cache.Remove(pKey);
            }
        }

        public static void Clear()
        {
            Remove(CacheKey_AppMenu);
            Remove(CacheKey_Users);
            Remove(CacheKey_Roles);
            Remove(CacheKey_RolesPermission);
            Remove(CacheKey_SysOption);
            Remove(CacheKey_WeekDay);

        }
        
        public static bool CheckAndGetData(string pKey, out object oData)
        {
            bool isData = false;
            object data = null;
            if (HttpContext.Current.Cache[pKey] != null)
            {
                data = GetData(pKey);
                isData = true;
                oData = data;
            }
            else
            {
                oData = null;
            }
            isData = oData != null;
            return isData;
        }


        public static object GetData(string pKey)
        {
            return HttpContext.Current.Cache[pKey];
        }



    }
}
