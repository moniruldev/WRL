using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
//using System.Linq.Dynamic;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using PG.DBClass.SystemDC;
using PG.BLLibrary.SystemsBL;


namespace PG.Web.Systems
{
    public class AppMenu
    {


        public static List<dcAppMenu> GetAppMenuList()
        {
            return GetAppMenuList(AppInfo.AppID);
        }
        public static List<dcAppMenu> GetAppMenuList(int pAppID)
        {
            List<dcAppMenu> cList = null;
            object data;
            if (AppCache.CheckAndGetData(AppCache.CacheKey_AppMenu, out data))
            {
                cList = (List<dcAppMenu>)data;
            }
            else
            {
                cList = AppMenuBL.GetAppMenuList(pAppID);
                AppCache.Insert(AppCache.CacheKey_AppMenu, cList);
            }
            return cList;
        }

        public static dcAppMenu GetAppMenu(int pMenuID)
        {
            dcAppMenu menu = null;
            var result = from c in GetAppMenuList()
                         where c.AppMenuID == pMenuID
                         select c;
            if (result.Count() > 0)
            {
                menu = result.First();
            }
            return menu;
        }
        public static string ConvertMenuDataToJson(List<dcAppMenu> cList)
        {
            StringBuilder sb = new StringBuilder();
            string comma = string.Empty;

            string RootPath = PG.Core.Web.WebUtility.GetAbsoluteUrl("~/");
            string menuFullUrl = string.Empty;

            sb.Append("[");
            foreach (dcAppMenu menu in cList)
            {
                menuFullUrl = string.Empty;
                if (menu.AppMenuURL != string.Empty)
                {
                    menuFullUrl = RootPath + menu.AppMenuURL;
                }


                sb.Append(comma);
                sb.Append("{");
                sb.Append("'id':" + menu.AppMenuID.ToString() + ",");
                sb.Append("'name':\"" + menu.AppMenuName.ToString() + "\",");
                sb.Append("'type':" + menu.AppMenuType.ToString() + ",");
                sb.Append("'url':\"" + menuFullUrl + "\",");
                sb.Append("'tabaction':" + menu.TabAction.ToString() + ",");
                sb.Append("'selectaction':" + menu.SelectAction.ToString() + ",");
                sb.Append("'reload':" + "0" + ",");
                sb.Append("\"l\":" + "0" + "");   //for last column comma
                sb.Append("}");
                comma = ",";
            }

            sb.Append("]");
            return sb.ToString();
        }

    }
}
