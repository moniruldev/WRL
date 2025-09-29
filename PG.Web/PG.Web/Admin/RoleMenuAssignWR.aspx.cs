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
using PG.DBClass.InventoryDC;
using PG.BLLibrary.InventoryBL;
using PG.Core.Utility;
using PG.DBClass.HMSDC;
using PG.BLLibrary.HMSBL;
using PG.DBClass.WRELDC;
using PG.BLLibrary.WRElBL;
using PG.BLLibrary.SecurityBL;
using PG.DBClass.SecurityDC;
using PG.DBClass.SystemDC;
using PG.BLLibrary.SystemsBL;

namespace PG.Web.Admin
{
    public partial class RoleMenuAssignWR : BagePage
    {
        int CompanyID = 0;
        public string ItemListServiceLink = PageLinks.InventoryLink.GetLink_ItemList;
        public string ItemGroupListServiceLink = PageLinks.InventoryLink.GetLink_ItemGroupList;
        public string MenuItemListServiceLink = PageLinks.SystemLinks.GetLink_MenuItemList;
        public dcUser Cur_User = null;
        List<dcAppMenu> menuList = new List<dcAppMenu>();
        List<dcRoleMenu> roleMenuList = new List<dcRoleMenu>();
        protected override void OnPreInit(EventArgs e)
        {
            if (Globals.AppMasterPage != string.Empty)
            {
                this.MasterPageFile = Globals.AppMasterPage;
            }
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CompanyID = CompanyInfo.GetCompanyID();
            Cur_User = AppSecurity.GetUserInfoFromSession();
            hdnAppId.Value = Cur_User.AppID.ToString();

            if (!IsPostBack)
            {
                FillCombo();
                SetDate();
                //LoadData();
            }
         
        }

        private void FillCombo()
        {
            ddlRole.Items.Clear();
            this.ddlRole.DataSource = RoleBL.GetRoleList(0).Where(x=> x.IsActive).ToList();
            this.ddlRole.DataTextField = "RoleName";
            this.ddlRole.DataValueField = "RoleID";
            this.ddlRole.DataBind();

            ddlRole.Items.Insert(0, new ListItem("(Select Role)", "0"));


        }

        private void SetDate()
        {
            var now = DateTime.Now;
            var firstDate = new DateTime(now.Year, now.Month, 1);

        }

        private void LoadData()
        {

            menuList = AppMenuBL.GetAppMenuList(Conversion.StringToInt(ddlApp.SelectedValue));
            roleMenuList = RoleMenuBL.GetRoleMenuListByRole(Conversion.StringToInt(ddlApp.SelectedValue), Convert.ToInt16(ddlRole.SelectedValue));

            int menuId = 0;

            if (hdnMenu.Value != "0")
            {
                menuId = Convert.ToInt16(hdnMenu.Value);
                menuList = menuList.Where(w => w.AppMenuID == menuId).ToList();
            }

            menuList = menuList.OrderBy(o => o.AppMenuID).ThenBy(o => o.ParentMenuID).ThenBy(o => o.AppMenuSLNo).ToList();

            if (menuList.Count > 0)
            {
                GridView1.DataSource = menuList;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }

        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string checkedStatus = string.Empty;
                // checkedStatus = ddlCheckedStatus.SelectedValue; 

                var menuId = Convert.ToInt16(e.Row.Cells[1].Text);
                CheckBox chkMenuItem = (CheckBox)e.Row.FindControl("chkMenuItem");
                Boolean showMenu = true;

                // Get the current menu item based on menuId
                //var currentMenuItem = menuList.FirstOrDefault(x => x.AppMenuID == menuId);

                if (roleMenuList.Any(w => w.APPMENUID == menuId)) //|| (currentMenuItem != null && currentMenuItem.IsRoleMenu == false)
                {
                    chkMenuItem.Checked = true;

                    var roleMenuItem = roleMenuList.FirstOrDefault(w => w.APPMENUID == menuId);
                    if (roleMenuItem != null)
                    {
                        showMenu = roleMenuItem.SHOWMENU;
                    }

                    if (string.Compare(checkedStatus, "U", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        e.Row.Attributes.Add("style", "display:none");
                    }
                }
                else
                {
                    if (string.Compare(checkedStatus, "C", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        e.Row.Attributes.Add("style", "display:none");
                    }
                }

                var parentMenuId = Convert.ToInt16(e.Row.Cells[2].Text);
                var parentMenuName = string.Empty;

                if (menuList.Any(w => w.AppMenuID == parentMenuId))
                {
                    parentMenuName = menuList.First(w => w.AppMenuID == parentMenuId).AppMenuText;
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


        protected void btnLoadData_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            LoadData();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void GridView1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            dcRoleMenu roleMenu = null;
            int inserted = 0;
            int deleted = 0;
            int appId = Conversion.StringToInt(ddlApp.SelectedValue);
            int roleId = Convert.ToInt16(ddlRole.SelectedValue);
            int menuId = 0;


            foreach (GridViewRow row in GridView1.Rows)
            {

                menuId = Convert.ToInt16(row.Cells[1].Text);
                RoleMenuBL.Delete(appId, roleId, menuId);
                deleted++;
            }

            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkMenuItem = row.FindControl("chkMenuItem") as CheckBox;
                if (chkMenuItem != null && chkMenuItem.Checked)
                {
                    roleMenu = new DBClass.SecurityDC.dcRoleMenu();
                    roleMenu.APPID = appId;
                    roleMenu.ROLEID = roleId;
                    roleMenu.APPMENUID = Convert.ToInt16(row.Cells[1].Text);

                    DropDownList ddlShowMenu = row.FindControl("ddlShowMenu") as DropDownList;

                    roleMenu.SHOWMENU = Convert.ToBoolean(ddlShowMenu.SelectedValue);

                    RoleMenuBL.Insert(roleMenu);

                    inserted++;
                }
            }

            if (inserted > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('success', 'Role menu assigned successfully!', 'Success');", true);
            }
            else if (deleted > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('success', 'Role menu deleted successfully!', 'Success');", true);
            }


            txtMenu.Text = "";
            hdnMenu.Value = "0";
            //ddlCheckedStatus.SelectedIndex = -1;
            //ddlRole.SelectedIndex = -1;

            //GridView1.DataSource = null;
            //GridView1.DataBind();
        }
    }
}
