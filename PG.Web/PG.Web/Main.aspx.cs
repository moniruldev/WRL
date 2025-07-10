using PG.BLLibrary.OrganizationBL;
using PG.BLLibrary.SecurityBL;
using PG.Common;
using PG.Core.Utility;
using PG.DBClass.SecurityDC;
using PG.DBClass.SystemDC;
using PG.Web.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
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

namespace PG.Web
{
    public partial class Main : System.Web.UI.Page
    {
        public int AppID = AppInfo.AppID;
        public string RootPath = PG.Core.Web.WebUtility.GetAbsoluteUrl("~/");
        public string GetMenuPageLinks = PageLinks.SystemLinks.GetLink_MenuItem;
        public string GetMenuKeepLive = PageLinks.SystemLinks.GetLink_KeepLive;

        protected void Page_Load(object sender, EventArgs e)
        {
            PG.Core.Web.WebUtility.SetPageCacheOff(this.Response);
            

            Globals.InitConnectionString();
            SetAppCompanyInfo();

            hdnKeepLive.Value = AppGlobals.KeepLive ? "1" : "0";
            hdnKeepLiveInterval.Value = AppGlobals.KeepLiveInterval.ToString();

            if (!IsPostBack)
            {

                //this is previous menu code before multiple role menu implement.


                //List<DBClass.SystemDC.dcAppMenu> cList = Systems.AppMenu.GetAppMenuList(AppInfo.AppID);

                //dcUser user = AppSecurity.GetUserInfoFromSession();
                //List<dcRoleMenu> cmList = RoleMenuBL.GetRoleMenuListByRole(user.RoleID);

                //FillTreeMenuRecursive(this.TreeView1, null, 0, cList, cmList, user, 0);



                //this is new menu code after multiple role menu implement.
                List<dcAppMenu> cList = null;           
                cList = AppMenu.GetAppMenuList(AppInfo.AppID);
                dcUser user = AppSecurity.GetUserInfoFromSession();
                //List<dcRoleMenu> cmList = RoleMenuBL.GetRoleMenuListByRole(AppInfo.AppID,user.RoleID);
                List<dcRoleMenu> cmList = RoleMenuBL.GetRoleMenuListByRole(AppInfo.AppID, user.RoleID, user.UserID);

                FillTreeMenuRecursive(this.TreeView1, null, 0, cList, cmList, user, 0);



                lblUserID.Text = "User: " + user.UserName + " -  " + user.FullName;







                
            }

            //string TClientID = this.TreeView1.ClientID;
            //int idx = 0;
            //foreach (TreeNode tNode in TreeView1.Nodes)
            //{

            //    if (tNode.Value == "0")
            //    {
            //        //string jS = "TreeView_ToggleNode(" + TClientID + "_Data,0," + TClientID + "n0" + ",' '," + TClientID + "n0" + "Nodes);";
            //        string jS = string.Format("TreeView_ToggleNode({0}_Data,{1},{0}n{1}" + ",' ',{0}n{1}" + "Nodes);", TClientID, idx.ToString());
            //        tNode.SelectAction = TreeNodeSelectAction.None;
            //        string nodeText = tNode.Text;
            //        //tNode.Text = string.Format("<span onclick=\"{1}\">{0}</span>", menu.AppMenuText, jS);
            //        tNode.Text = string.Format("<span onclick=\"{1}\">{0}</span>", nodeText, jS);
            //        tNode.Value = "0";
            //    }

            //    idx++;
            //}


           
            //this.lblDate.Text = DateTime.Today.ToString("dd-MMM-yyy, dddd");
            //if (HttpContext.Current.User.Identity.IsAuthenticated)
            //{
            //    dvLogin.Visible = false;
            //    dvLogout.Visible = true;

            //}
            //else
            //{
            //    dvLogin.Visible = true;
            //    dvLogout.Visible = false;
            //}
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            
        }

        private void SetAppCompanyInfo()
        {
            lblCopyright.Text = "Copyright© " + AppInfo.AppCompanyName;
            hlinkWeb.Text = AppInfo.AppCompanyWeb;
            hlinkWeb.NavigateUrl = "http://" + AppInfo.AppCompanyWeb;

            this.Title = AppInfo.AppNameFull;

            //this.lblVersion.Text = "Version: " + AppInfo.GetAppVersion();
        

            //imgTopBanner.ImageUrl = "~/image/" + AppInfo.AppNameImage;


            //dcUser user = AppSecurity.GetUserInfoFromSession();
            //this.lblUserLoc.Text = "Location: " + user.LoginLocationName;

        }

        private void FillTreeMenuRecursive(TreeView tvMenu, TreeNode pParentNode, int pParentNodeID, List<dcAppMenu> cList, List<dcRoleMenu> cmList, dcUser user, int pExpandMenuNo)
        {
            List<dcAppMenu> pList = (from c in cList
                                     where c.ParentMenuID == pParentNodeID && c.ShowMenu == true
                                     orderby c.AppMenuSLNo
                                     select c).ToList();

            string TClientID = tvMenu.ClientID;
            foreach (dcAppMenu menu in pList)
            {
                if (!user.IsUserAdmin)
                {
                    if (menu.IsRoleMenu)
                    {
                        dcRoleMenu rm = cmList.Find(c => c.APPMENUID == menu.AppMenuID);
                        if (rm == null)
                        {
                            continue;
                        }

                        if (!rm.SHOWMENU)
                        {
                            continue;
                        }
                    }
                }

                //if (menu.IsRoleMenu)


                TreeNode tNode = new TreeNode();
                tNode.Text = menu.AppMenuText;
                tNode.Value = menu.AppMenuName;
                tNode.Expanded = menu.Expanded;
                tNode.SelectAction = (TreeNodeSelectAction)menu.SelectAction;
                tNode.ToolTip = menu.ToolTip;

                if (menu.ShowImage)
                {
                    if (menu.ImageURL != string.Empty)
                    {
                        tNode.ImageUrl = menu.ImageURL;
                    }
                }


                tNode.Value = "0";
                if (menu.IsAppURL)
                {
                    //tNode.NavigateUrl = menu.AppMenuURL;
                    // tNode.NavigateUrl = "javascript:TabMenu.OpenMenu(" + menu.AppMenuID.ToString() + ")";
                    //tNode.NavigateUrl = "TabMenu.CheckWindowUnload = false;TabMenu.OpenMenu(" + menu.AppMenuID.ToString() + ")";

                    //tNode.NavigateUrl = "javascript: TabMenu.CheckWindowUnload = false;TabMenu.OpenMenu(" + menu.AppMenuID.ToString() + ")";

                    tNode.Value = menu.AppMenuID.ToString();
                    tNode.SelectAction = TreeNodeSelectAction.None;
                    tNode.Text = string.Format("<span class=\"spanMenu\" onclick=\"{1}\">{0}</span>", menu.AppMenuText, "TabMenu.OpenMenu(" + menu.AppMenuID.ToString() + ");");
                   // tNode.Value = menu.AppMenuID.ToString();
                   // tNode.SelectAction = TreeNodeSelectAction.None;
                    //tNode.Text = string.Format("<span class=\"spanMenu\" onclick=\"{1}\">{0}</span>", menu.AppMenuText, "TabMenu.OpenMenu(" + menu.AppMenuID.ToString() + ");");
                }
                else
                {

                    //string jS = "TreeView_ToggleNode(" + TClientID + "_Data,0," + TClientID + "n0" + ",' '," + TClientID + "n0" + "Nodes);";
                    //string jS = string.Format("TreeView_ToggleNode({0}_Data,{1},{0}n{1}" + ",' ',{0}n{1}" + "Nodes);",TClientID,pExpandMenuNo.ToString());

                    //tNode.SelectAction = TreeNodeSelectAction.None;
                    //tNode.Text = string.Format("<span onclick=\"{1}\">{0}</span>", menu.AppMenuText, jS);
                    //tNode.Value = "0";

                }
                pExpandMenuNo++;

                if (pParentNode == null)
                {
                    tvMenu.Nodes.Add(tNode);
                }
                else
                {
                    pParentNode.ChildNodes.Add(tNode);
                }




                //Recurisive call
                FillTreeMenuRecursive(tvMenu, tNode, menu.AppMenuID, cList, cmList, user, pExpandMenuNo);
            }
        }
       /* private void FillTreeMenuRecursive(TreeView tvMenu, TreeNode pParentNode, int pParentNodeID, List<DBClass.SystemDC.dcAppMenu> cList, int pExpandMenuNo)
        {
            List<DBClass.SystemDC.dcAppMenu> pList = (from c in cList
                                                      where c.ParentMenuID == pParentNodeID && c.ShowMenu == true
                                                      orderby c.AppMenuSLNo
                                                      select c).ToList();


            string TClientID = tvMenu.ClientID;
            foreach (DBClass.SystemDC.dcAppMenu menu in pList)
            {
                TreeNode tNode = new TreeNode();
                tNode.Text = menu.AppMenuText;
                tNode.Value = menu.AppMenuName;
                tNode.Expanded = menu.Expanded;
                tNode.SelectAction = (TreeNodeSelectAction)menu.SelectAction;
                tNode.ToolTip = menu.ToolTip;

                if (menu.ShowImage)
                {
                    if (menu.ImageURL != string.Empty)
                    {
                        tNode.ImageUrl = menu.ImageURL;
                    }
                }


                tNode.Value = "0";
                if (menu.IsAppURL)
                {
                    //tNode.NavigateUrl = menu.AppMenuURL;
                    // tNode.NavigateUrl = "javascript:TabMenu.OpenMenu(" + menu.AppMenuID.ToString() + ")";
                    //tNode.NavigateUrl = "TabMenu.CheckWindowUnload = false;TabMenu.OpenMenu(" + menu.AppMenuID.ToString() + ")";

                    //tNode.NavigateUrl = "javascript: TabMenu.CheckWindowUnload = false;TabMenu.OpenMenu(" + menu.AppMenuID.ToString() + ")";
                    tNode.Value = menu.AppMenuID.ToString();
                    tNode.SelectAction = TreeNodeSelectAction.None;
                    tNode.Text = string.Format("<span class=\"spanMenu\" onclick=\"{1}\">{0}</span>", menu.AppMenuText, "TabMenu.OpenMenu(" + menu.AppMenuID.ToString() + ");");
                }
                else
                {
                   
                    //string jS = "TreeView_ToggleNode(" + TClientID + "_Data,0," + TClientID + "n0" + ",' '," + TClientID + "n0" + "Nodes);";
                    //string jS = string.Format("TreeView_ToggleNode({0}_Data,{1},{0}n{1}" + ",' ',{0}n{1}" + "Nodes);",TClientID,pExpandMenuNo.ToString());

                    //tNode.SelectAction = TreeNodeSelectAction.None;
                    //tNode.Text = string.Format("<span onclick=\"{1}\">{0}</span>", menu.AppMenuText, jS);
                    //tNode.Value = "0";
                    
                }
                pExpandMenuNo++;

                if (pParentNode == null)
                {
                    tvMenu.Nodes.Add(tNode);
                }
                else
                {
                    pParentNode.ChildNodes.Add(tNode);
                }

                


                //Recurisive call
                FillTreeMenuRecursive(tvMenu, tNode, menu.AppMenuID, cList,pExpandMenuNo);
            }
        } */

        private void LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("Expires", "0");
            //FormsAuthentication.RedirectToLoginPage();
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            LogOut();
            //FormsAuthentication.SignOut();
            //Session.Abandon();
            ////FormsAuthentication.RedirectToLoginPage();
            //Response.Redirect("~/Default.aspx");
        }

        protected void TreeView1_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
        {
            //int x = 0;
        }

    
     

    }
}
