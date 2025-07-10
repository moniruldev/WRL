using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG.Core.DBBase;
using PG.DBClass.SecurityDC;
using PG.BLLibrary.SystemsBL;

using PG.DBClass.SecurityDC;
using PG.DBClass.SystemDC;
using PG.BLLibrary.SecurityBL;
using PG.BLLibrary.SystemsBL;

namespace PG.Web.Admin
{
    public partial class RoleMenuAssign : BagePage
    {
        Int64 MASTER_ID = 0;
        string EmpCode = string.Empty;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;
        int CompanyID = 0;
        public dcUser Cur_User = null;
        public string MenuItemListServiceLink = PageLinks.SystemLinks.GetLink_MenuItemList;
        List<dcAppMenu> menuList = new List<dcAppMenu>();
        List<dcRoleMenu> roleMenuList = new List<dcRoleMenu>();
                
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CompanyID = base.GetPageQueryInteger("compid");

            this.EmpCode = base.GetPageQueryString("empcode").Trim();


            Cur_User = AppSecurity.GetUserInfoFromSession();
            hdnAppId.Value = Cur_User.AppID.ToString();

            


            if (!IsPostBack) //first Time
            {
                FillCombo();
            }
            else
            {
                this.EditMode = base.GetEditModeFromViewState(base.EditModeViewStateKey);
                //this.MonthlySalID = int.Parse(ViewState[ViewStateKey].ToString());
            }
            //SetHyperLink();
            this.ShowPageMessage(this.lblMessage);

        }

        private void FillCombo()
        {
            List<dcRole> roleList = new List<dcRole>();
            roleList = RoleBL.GetRoleList();


            ddlRole.DataSource = roleList;
            ddlRole.DataTextField = "ROLENAME";
            ddlRole.DataValueField = "ROLEID";
            ddlRole.DataBind();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            menuList = AppMenuBL.GetAppMenuList(Cur_User.AppID);
            roleMenuList = RoleMenuBL.GetRoleMenuListByRole(Cur_User.AppID, Convert.ToInt16(ddlRole.SelectedValue));

            int menuId = 0;

            if(hdnMenu.Value!="0")
            {
                menuId = Convert.ToInt16(hdnMenu.Value);
                menuList = menuList.Where(w => w.AppMenuID == menuId).ToList();
            }

            menuList = menuList.OrderBy(o => o.AppMenuID).ThenBy(o => o.ParentMenuID).ThenBy(o => o.AppMenuSLNo).ToList();

            if (menuList.Count > 0)
            {
                gvRoleMenu.DataSource = menuList;
                gvRoleMenu.DataBind();
            }
            else
            {
                gvRoleMenu.DataSource = null;
                gvRoleMenu.DataBind();
            }
        }


        protected void gvRoleMenu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string checkedStatus = string.Empty;
                checkedStatus = ddlCheckedStatus.SelectedValue;               

                var menuId = Convert.ToInt16(e.Row.Cells[1].Text);
                CheckBox chkMenuItem = (CheckBox)e.Row.FindControl("chkMenuItem");
                Boolean showMenu = true;
                if (roleMenuList.Count(w => w.APPMENUID == menuId) > 0)
                {
                    chkMenuItem.Checked = true;
                    showMenu = roleMenuList.Where(w => w.APPMENUID == menuId).FirstOrDefault().SHOWMENU;
                    if (string.Compare(checkedStatus, "U") == 0)
                        e.Row.Attributes.Add("style", "display:none");
                }
                else
                {
                    if(string.Compare(checkedStatus,"C")==0)
                    e.Row.Attributes.Add("style", "display:none");
                }


                var parentMenuId =Convert.ToInt16(e.Row.Cells[2].Text);
                var parentMenuName = string.Empty;

                if (menuList.Count(w => w.AppMenuID == parentMenuId) > 0)
                {
                    parentMenuName = menuList.Where(w => w.AppMenuID == parentMenuId).FirstOrDefault().AppMenuText;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.SkyBlue;
                }

                e.Row.Cells[2].Text = parentMenuName;

                DropDownList ddlShowMenu = (DropDownList)e.Row.FindControl("ddlShowMenu");


                ddlShowMenu.SelectedIndex = ddlShowMenu.Items.IndexOf(ddlShowMenu.Items.FindByText(Convert.ToString(showMenu)));

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            dcRoleMenu roleMenu = null;
            int inserted = 0;
            int deleted = 0;
            int appId = Cur_User.AppID;
            int roleId = Convert.ToInt16(ddlRole.SelectedValue);
            int menuId = 0;
           

            foreach (GridViewRow row in gvRoleMenu.Rows)
            {
              
                menuId = Convert.ToInt16(row.Cells[1].Text);
                RoleMenuBL.Delete(appId, roleId,menuId);
                deleted++;               
            }

            foreach (GridViewRow row in gvRoleMenu.Rows)
            {
                CheckBox chkMenuItem = row.FindControl("chkMenuItem") as CheckBox;
                if (chkMenuItem != null && chkMenuItem.Checked)
                {
                    roleMenu = new DBClass.SecurityDC.dcRoleMenu();
                    roleMenu.APPID = appId;
                    roleMenu.ROLEID =roleId;
                    roleMenu.APPMENUID =Convert.ToInt16( row.Cells[1].Text);

                    DropDownList ddlShowMenu = row.FindControl("ddlShowMenu") as DropDownList;

                    roleMenu.SHOWMENU =Convert.ToBoolean(ddlShowMenu.SelectedValue);

                    RoleMenuBL.Insert(roleMenu);
                    
                    inserted++;
                }
            }

            if(inserted>0)
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Role menu assigned successfully.!!');", true);
            }
            else if (deleted > 0)
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Role menu deleted successfully.!!');", true);
            }


            txtMenu.Text = "";
            hdnMenu.Value = "0";
            ddlCheckedStatus.SelectedIndex = -1;
            ddlRole.SelectedIndex = -1;

            gvRoleMenu.DataSource = null;
            gvRoleMenu.DataBind();
        }
    }
}