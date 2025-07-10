using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using PG.Core.Web;

using PG.Web.Systems;


namespace PG.Web.PageLinks
{
    public class SystemLinks
    {

        public static string GetLink_LoginSilent
        {
            get { return WebUtility.GetAbsoluteUrl("~/loginsilent.ashx"); }
        }

        public static string GetLink_KeepLive
        {
            get { return WebUtility.GetAbsoluteUrl("~/keeplive.ashx"); }
        }

        public static string GetLink_LongTask
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/GetLongTask.ashx"); }
        }

        public static string GetLink_MenuItem
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Systems/GetMenuItem.ashx"); }
        }

        public static string GetLink_MenuItemList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Systems/GetMenuItemList.ashx"); }
        }

        public static string GetLink_GetJSonData
        {
            get { return WebUtility.GetAbsoluteUrl("~/getjsondata.ashx"); }
        }

        public static string GetLink_GetServiceCommon
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Inventory/ServiceCommon.ashx"); }
        }

        public static string GetLink_Event
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/GetEvent.ashx"); }
        }

        public static string GetLink_GetDataInfo
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/GetDataInfo.ashx"); }
        }
      

        public static string GetLink_SearchTest
        {
            get { return WebUtility.GetAbsoluteUrl("~/SearchForms/SearchTest.aspx"); }
        }

        public static string GetPageLinkByMenuID(int pMenuID)
        {
            string mLink = string.Empty;
            DBClass.SystemDC.dcAppMenu menu = AppMenu.GetAppMenu(pMenuID);
            if (menu != null)
            {
                if (menu.AppMenuURL != string.Empty)
                {
                    mLink = WebUtility.GetAbsoluteUrl(menu.AppMenuURL);
                }
            }
            return mLink;
        }
        public static string GetLink_RoleList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Security/GetLink_RoleList.ashx"); }
        }
        
    }
}
