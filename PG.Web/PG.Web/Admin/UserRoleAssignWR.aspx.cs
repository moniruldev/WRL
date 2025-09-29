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
    public partial class UserRoleAssignWR : BagePage
    {
        int CompanyID = 0;
        public string ItemListServiceLink = PageLinks.InventoryLink.GetLink_ItemList;
        public string ItemGroupListServiceLink = PageLinks.InventoryLink.GetLink_ItemGroupList;
        public string UserListServiceLink = PageLinks.SystemLinks.GetLink_UserInfo;
        public dcUser Cur_User = null;
        List<dcRole> roleList = new List<dcRole>();
        List<dcUserRole> UserRoleList = new List<dcUserRole>();
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
            txtDefaultRole.Attributes.Add("readonly", "readonly");

         
        }

        private void FillCombo()
        {
        }

        private void SetDate()
        {
            var now = DateTime.Now;
            var firstDate = new DateTime(now.Year, now.Month, 1);

        }

        private void LoadData()
        {
            clsPrmInventory PObj = new clsPrmInventory();
            PObj.user_id = Conversion.StringToInt(hdnUserId.Value);
            roleList = RoleBL.GetRoleList(Conversion.StringToInt(ddlApp.SelectedValue));
            UserRoleList = UserRoleBL.GetUserPermittedRoleListByUserID(PObj);


            roleList = roleList.OrderBy(o => o.RoleID).ToList();

            if (roleList.Count > 0)
            {
                GridView1.DataSource = roleList;
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

                var roleId = Convert.ToInt16(e.Row.Cells[1].Text);
                CheckBox chkMenuItem = (CheckBox)e.Row.FindControl("chkMenuItem");
                Boolean showMenu = true;

                // Get the current menu item based on menuId
                //var currentMenuItem = menuList.FirstOrDefault(x => x.AppMenuID == menuId);

                if (UserRoleList.Any(w => w.RoleID == roleId)) //|| (currentMenuItem != null && currentMenuItem.IsRoleMenu == false)
                {
                    chkMenuItem.Checked = true;

                }
                else
                {
                 
                }

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
            dcUserRole userRole = null;
            int inserted = 0;
            int deleted = 0;
            int appId = Conversion.StringToInt(ddlApp.SelectedValue);
            int roleId = 0;


            foreach (GridViewRow row in GridView1.Rows)
            {
                roleId = Convert.ToInt16(row.Cells[1].Text);
                UserRoleBL.Delete(appId,roleId,Conversion.StringToInt(hdnUserId.Value));
                deleted++;
            }

            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkMenuItem = row.FindControl("chkMenuItem") as CheckBox;
                if (chkMenuItem != null && chkMenuItem.Checked)
                {
                    userRole = new DBClass.SecurityDC.dcUserRole();
                    userRole.AppID = appId;
                    userRole.RoleID = Convert.ToInt16(row.Cells[1].Text);
                    userRole.UserID = Conversion.StringToInt(hdnUserId.Value);
                    UserRoleBL.Insert(userRole);

                    inserted++;
                }
            }

            if (inserted > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('success', 'User role assigned successfully!', 'Success');", true);
            }
            else if (deleted > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "toastrMessage", "showToastr('success', 'User role deleted successfully!', 'Success');", true);
            }


            //txtUserName.Text = "";
            //hdnUserId.Value = "0";
            //ddlCheckedStatus.SelectedIndex = -1;
            //ddlRole.SelectedIndex = -1;

            //GridView1.DataSource = null;
            //GridView1.DataBind();
        }
    }
}
