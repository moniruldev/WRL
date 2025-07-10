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

using PG.Core.Web;

using PG.DBClass.SecurityDC;
using PG.BLLibrary.SecurityBL;
using PG.DBClass.OrganiztionDC;
using PG.BLLibrary.OrganizationBL;
using PG.DBClass;
//using PG.BLLibrary.InventoryBL;

namespace PG.Web.Admin
{
    public partial class UserDepartmentPermission : BagePage
    {
        int CompanyID = 0;
        private dcUser loggedinUser = null;
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
            loggedinUser = AppSecurity.GetUserInfoFromSession();
            //this.AppObjectID = AppObjectEnum.Frm_RolePermission;
            this.CheckPermissionRead();

            if (!IsPostBack)
            {
                this.pUserID =  loggedinUser.UserID;
                FillCombo();
                LoadData();
                ddlRoleCopy.Style["visibility"] = "hidden";
            }
            else
            {
                if (chkCopy.Checked)
                {
                    ddlRoleCopy.Style["visibility"] = "visible";
                }
                else
                {
                    ddlRoleCopy.Style["visibility"] = "hidden";
                }
            }
            SetHyperLink();
        }

        private void FillCombo()
        {

            this.ddlUser.DataSource = UserBL.GetUserList(AppInfo.AppID); //RoleBL.GetRoleList(AppInfo.AppID);
            this.ddlUser.DataTextField = "USERNAME";
            this.ddlUser.DataValueField = "USERID";
            this.ddlUser.DataBind();


            //this.ddlRoleCopy.DataSource = this.ddlUser.DataSource;
            //this.ddlRoleCopy.DataTextField = "RoleName";
            //this.ddlRoleCopy.DataValueField = "RoleID";
            //this.ddlRoleCopy.DataBind();
        }

        private void SetHyperLink()
        {
            //new button
            //string hLink = "javascript:tbopen(0)";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopen(0)";
            //    this.btnAddNew.PostBackUrl = string.Empty;
            //    this.btnAddNew.OnClientClick = hLink;
            //}
            //else
            //{
            //    hLink = "~/Admin/Role.aspx";
            //    this.btnAddNew.PostBackUrl = hLink;
            //    this.btnAddNew.OnClientClick = string.Empty;
            //}
        }
        private void LoadData()
        {


            int UserID = Convert.ToInt32(this.ddlUser.SelectedValue);
            List<dcDEPARTMENT_INFO> DepartmentList = new List<dcDEPARTMENT_INFO>();
            DepartmentList = TBLUSERDEPARTMENTBL.Department_Info_List(null, null, null);
                
                //DepartmentBL.GetDepartmentList(this.CompanyID);
           // List<dcDepartmentUser> UserdepartmentList = DepartmentUserBL.GetDepartmentList(CompanyID); //RolePermissionBL.GetRolePermissionList(AppInfo.AppID);


            //var result = from obj in objList
            //             from role in roleList
            dcTBLUSERDEPARTMENT defUser = new dcTBLUSERDEPARTMENT { USERID = UserID, DEPTNAME = string.Empty, AllowLogin = true };


            //List<dcDepartmentUser> list = (from c in DEPARTMENT_INFOBL.Department_Info_List()
            //                               select new dcDepartmentUser
            //                             {
            //                                 Department_ID = c.DEPARTMENT_ID,
            //                                 DepartmentCode = c.DEPARTMENT_CODE,
            //                                 DepartmentName = c.DEPARTMENT_NAME,
            //                                 UserID = pUserID,
            //                                 AllowLogin = false
            //                             }).ToList();





            var result = from obj in TBLUSERDEPARTMENTBL.Department_Info_List(null, null, null)
                         from User in TBLUSERDEPARTMENTBL.GetTBLUSERDEPARTMENTList(UserID).Where(rp => rp.USERID == UserID && rp.DEPTID == obj.DEPARTMENT_ID)
                         //.DefaultIfEmpty(defUser)
                         select new dcTBLUSERDEPARTMENT
                         {
                             DEPTID = obj.DEPARTMENT_ID,
                             DEPTNAME = obj.DEPARTMENT_NAME,

                             AllowLogin = true,

                         };


            foreach(var itmD in DepartmentList)
            {
                foreach(var itmU in result.ToList())
                {
                    if(itmD.DEPARTMENT_ID==itmU.DEPTID)
                    {
                        itmD.AllowLogin = true;
                    }
                }
            }

            //List<dcTBLUSERDEPARTMENT> aa = TBLUSERDEPARTMENTBL.GetTBLUSERDEPARTMENTList(UserID).ToList();

            //         RoleKey = role.RoleKey,
            //ObjectKey = role.ObjectKey,
            //Permission = role.Permission,

            //RoleID = role.RoleID,
            //ObjectID = obj.ObjectID,
            //ObjectName = obj.ObjectName

           // //GridView1.DataSource = BLLibrary.SystemBL.RolePermissionBL.GetRolePermissionList(Globals.AppID,roleKey);
            BindGridData(DepartmentList);
            lblTotal.Text = "Total:" + GridView1.Rows.Count.ToString();





            //GridView1.DataSource = BLLibrary.SystemBL.RoleBL.GetRoleList();
            //GridView1.DataBind();

            //lblTotal.Text = "Total:" + GridView1.Rows.Count.ToString();
        }

        
       
        private void BindGridData(List<dcDEPARTMENT_INFO> listData)
        {
            int rowCount = listData.Count;
            if (rowCount == 0)
            {
                listData.Add(new dcDEPARTMENT_INFO());
            }

            GridView1.DataSource = listData;
            GridView1.DataBind();

            if (rowCount == 0)
            {
                GridView1.Rows[0].Visible = false;
            }
            hdnRowCount.Value = rowCount.ToString();

            //using (System.IO.StreamWriter sw = new System.IO.StreamWriter("c:\\test.xls"))
            //{
            //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            //    {
            //        GridView1.RenderControl(hw);
            //    }
            //}
            GridView1.CssClass = "grid";
        }


protected void ddlUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                //int objID = Convert.ToInt32((int)DataBinder.Eval(e.Row.DataItem, "ObjectId"));
                //PermissionEnum permission = (PermissionEnum)Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Permission"));

                Boolean objType = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "AllowLogin"));

                //////set check box
                CheckBox chkRead = (CheckBox)e.Row.FindControl("chkAllowLogin");
                chkRead.Checked = objType;


               

            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            GridView1.PageIndex = 0;
            LoadData();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //GridView1.PageIndex = e.NewPageIndex;
            ////GridView1.DataBind();
            //LoadData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlUser.Items.Count == 0)
            {
                //lblMsg.Text = "Not Saved! No Role Specified";
                this.SetPageMessage("Not Saved! No User Specified", MessageTypeEnum.Error);
                this.ShowPageMessage(lblMessage, true);

                return;
            }

            
            //if (chkCopy.Checked && ddlRoleCopy.Items.Count == 0)
            //{
            ////    //Helper.SetStatusMessage(lblMessage, "Not Saved! Copy Role Not Specified", MessageTypeEnum.InvalidData);
            //    this.SetPageMessage("Not Saved! Copy Role Not Specified", MessageTypeEnum.Error);
            //    this.ShowPageMessage(lblMessage, true);
            //    return;
            //}


            //this.CheckPermissionEdit();


            bool bStatus = false;
            //if (chkCopy.Checked)
            //{
            //    int userID = Convert.ToInt32(ddlUser.SelectedValue);
            //    int roleIDFrom = Convert.ToInt32(ddlRoleCopy.SelectedValue);

            //    if (userID == roleIDFrom)
            //    {
            //        //Helper.SetStatusMessage(lblMessage, "Please Select different role.", MessageTypeEnum.InvalidData);
            //        this.SetPageMessage("Please Select different role.", MessageTypeEnum.Error);
            //        this.ShowPageMessage(lblMessage, true);
            //        return;
            //    }


            //    bStatus = RolePermissionBL.CopyRolePermission(roleIDFrom, userID);
            //}
            //else
            //{
                bStatus = SaveData();
            //}


            //AppCache.Remove(AppCache.CacheKey_RolesPermission);
            ////lblMsg.Text = "Permission updated successfully.";
                if (bStatus)
                {
                    //Helper.SetStatusMessage(lblMessage, "Permission updated successfully.", MessageTypeEnum.Successful);
                    this.SetPageMessage("Data updated successfully.", MessageTypeEnum.Successful);

                    LoadData();
                    //chkCopy.Checked = false;
                    //ddlRoleCopy.Style["visibility"] = "hidden";
                }
                else
                {
                    // Helper.SetStatusMessage(lblMessage, "Error! Not Saved", MessageTypeEnum.Error);
                    this.SetPageMessage("Error! Not Saved", MessageTypeEnum.Error);
                }
                this.ShowPageMessage(lblMessage, true);

        }

        private bool SaveData()
        {
            List<dcTBLUSERDEPARTMENT> cList = new List<dcTBLUSERDEPARTMENT>();

            int userID = Convert.ToInt32(ddlUser.SelectedValue);
            int departmentID = 0;
            //PermissionEnum permission = PermissionEnum.None;

            foreach (GridViewRow gvR in GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    departmentID = Convert.ToInt32(((DataKey)GridView1.DataKeys[gvR.RowIndex])["DEPARTMENT_ID"].ToString());
                    //permission = PermissionEnum.None;

                    CheckBox chkAllowlogin = (CheckBox)gvR.FindControl("chkAllowLogin");

                    dcTBLUSERDEPARTMENT cObj = new dcTBLUSERDEPARTMENT();
                    cObj.USERID = userID;
                    cObj.DEPTID = departmentID;
                    cObj.AllowLogin = chkAllowlogin.Checked;

                    cList.Add(cObj);
                }
            }

            TBLUSERDEPARTMENTBL.UpdateTBLUSERDEPARTMENT(cList);

          //   DepartmentUserBL.UpdateDepartmentUser(cList);


            return true;
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            switch (e.Row.RowType)
            {
                case DataControlRowType.DataRow:
                    e.Row.CssClass += " gridRow";
                    break;
                case DataControlRowType.Header:
                    e.Row.CssClass += " headerRow";
                    break;
                case DataControlRowType.Footer:
                    e.Row.CssClass += " footerRow";
                    break;
                case DataControlRowType.Pager:
                    e.Row.CssClass += " pagerRow";
                    break;
                case DataControlRowType.EmptyDataRow:
                    e.Row.CssClass += " gridRow";
                    break;
            }
        }

        public Core.DBBase.DBContext pCompanyID { get; set; }

        public int pUserID { get; set; }

        public PermissionEnum permission { get; set; }

        public bool bStatus { get; set; }
    }
}
