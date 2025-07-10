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


namespace PG.Web.Admin
{
    public partial class RolePermission : BagePage
    {
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

            this.AppObjectID = AppObjectEnum.Frm_RolePermission;
            this.CheckPermissionRead();

            if (!IsPostBack)
            {
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

            this.ddlRole.DataSource = RoleBL.GetRoleList(AppInfo.AppID);
            this.ddlRole.DataTextField = "RoleName";
            this.ddlRole.DataValueField = "RoleID";
            this.ddlRole.DataBind();


            this.ddlRoleCopy.DataSource = this.ddlRole.DataSource;
            this.ddlRoleCopy.DataTextField = "RoleName";
            this.ddlRoleCopy.DataValueField = "RoleID";
            this.ddlRoleCopy.DataBind();
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


            int roleID = Convert.ToInt32(this.ddlRole.SelectedValue);
            List<dcAppObject> objList = AppObjectsBL.GetAppObjectList(AppInfo.AppID);
            List<dcRolePermission> roleList = RolePermissionBL.GetRolePermissionList(AppInfo.AppID);


            //var result = from obj in objList
            //             from role in roleList
            dcRolePermission defRole = new dcRolePermission { RolePermissionID = 0, RoleID = 0, RoleName = string.Empty, Permission = 0 };



            var result = from obj in objList
                         from role in roleList.Where(rp => rp.AppObjectID == obj.AppObjectID && rp.RoleID == roleID).DefaultIfEmpty(defRole)
                         select new dcRolePermission
                         {
                             RoleID = role.RoleID,
                             RoleName = role.RoleName,
                             AppObjectID = obj.AppObjectID,
                             AppObjectCode = obj.AppObjectCode,
                             AppObjectName = obj.AppObjectName,
                             AppObjectTypeID = obj.AppObjectTypeID,
                             AppObjectTypeName =  ((AppObjectTypeEnum)obj.AppObjectTypeID).ToString(),
                             Permission = role.Permission
                         };

            //         RoleKey = role.RoleKey,
            //ObjectKey = role.ObjectKey,
            //Permission = role.Permission,

            //RoleID = role.RoleID,
            //ObjectID = obj.ObjectID,
            //ObjectName = obj.ObjectName

            //GridView1.DataSource = BLLibrary.SystemBL.RolePermissionBL.GetRolePermissionList(Globals.AppID,roleKey);
            BindGridData(result.ToList());
            lblTotal.Text = "Total:" + GridView1.Rows.Count.ToString();





            //GridView1.DataSource = BLLibrary.SystemBL.RoleBL.GetRoleList();
            //GridView1.DataBind();

            //lblTotal.Text = "Total:" + GridView1.Rows.Count.ToString();
        }

        private void BindGridData(List<dcRolePermission> listData)
        {
            int rowCount = listData.Count;
            if (rowCount == 0)
            {
                listData.Add(new dcRolePermission());
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



        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                //int objID = Convert.ToInt32((int)DataBinder.Eval(e.Row.DataItem, "ObjectId"));
                PermissionEnum permission = (PermissionEnum)Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Permission"));

                AppObjectTypeEnum objType =  (AppObjectTypeEnum)Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AppObjectTypeID"));

                ////set check box
                CheckBox chkRead = (CheckBox)e.Row.FindControl("chkRead");
                chkRead.Enabled = (objType == AppObjectTypeEnum.From) | (objType == AppObjectTypeEnum.Report) | (objType == AppObjectTypeEnum.FormOption);
                chkRead.Checked = (chkRead.Enabled) & ((permission & PermissionEnum.Read) == PermissionEnum.Read);

                CheckBox chkAdd = (CheckBox)e.Row.FindControl("chkAdd");
                chkAdd.Enabled = (objType == AppObjectTypeEnum.From) | (objType == AppObjectTypeEnum.FormOption);
                chkAdd.Checked = (chkAdd.Enabled) & ((permission & PermissionEnum.Add) == PermissionEnum.Add);

                CheckBox chkEdit = (CheckBox)e.Row.FindControl("chkEdit");
                chkEdit.Enabled = (objType == AppObjectTypeEnum.From) | (objType == AppObjectTypeEnum.FormOption);
                chkEdit.Checked = (chkEdit.Enabled) & ((permission & PermissionEnum.Edit) == PermissionEnum.Edit);

                CheckBox chkDelete = (CheckBox)e.Row.FindControl("chkDelete");
                chkDelete.Enabled = (objType == AppObjectTypeEnum.From) | (objType == AppObjectTypeEnum.FormOption);
                chkDelete.Checked = (chkDelete.Enabled) & ((permission & PermissionEnum.Delete) == PermissionEnum.Delete);


                CheckBox chkList = (CheckBox)e.Row.FindControl("chkList");
                chkList.Enabled = (objType == AppObjectTypeEnum.From) | (objType == AppObjectTypeEnum.FormOption);
                chkList.Checked = (chkList.Enabled) & ((permission & PermissionEnum.List) == PermissionEnum.List);


                CheckBox chkExecute = (CheckBox)e.Row.FindControl("chkExecute");
                chkExecute.Enabled = (objType == AppObjectTypeEnum.Procedure) | (objType == AppObjectTypeEnum.Task) | (objType == AppObjectTypeEnum.FormOption);
                chkExecute.Checked = (chkExecute.Enabled) & ((permission & PermissionEnum.Execute) == PermissionEnum.Execute);

                CheckBox chkEnabled = (CheckBox)e.Row.FindControl("chkEnabled");
                chkEnabled.Enabled = (objType == AppObjectTypeEnum.Menu) | (objType == AppObjectTypeEnum.Option) | (objType == AppObjectTypeEnum.FormOption);
                chkEnabled.Checked = (chkEnabled.Enabled) & ((permission & PermissionEnum.Enabled) == PermissionEnum.Enabled);

                CheckBox chkVisible = (CheckBox)e.Row.FindControl("chkVisible");
                chkVisible.Enabled = (objType == AppObjectTypeEnum.Menu) | (objType == AppObjectTypeEnum.FormOption);
                chkVisible.Checked = (chkVisible.Enabled) & ((permission & PermissionEnum.Visible) == PermissionEnum.Visible);

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
            if (ddlRole.Items.Count == 0)
            {
                //lblMsg.Text = "Not Saved! No Role Specified";
                this.SetPageMessage("Not Saved! No Role Specified", MessageTypeEnum.Error);
                this.ShowPageMessage(lblMessage, true);

                return;
            }

            if (chkCopy.Checked && ddlRoleCopy.Items.Count == 0)
            {
                //Helper.SetStatusMessage(lblMessage, "Not Saved! Copy Role Not Specified", MessageTypeEnum.InvalidData);
                this.SetPageMessage("Not Saved! Copy Role Not Specified", MessageTypeEnum.Error);
                this.ShowPageMessage(lblMessage, true);
                return;
            }


            this.CheckPermissionEdit();


            bool bStatus = false;
            if (chkCopy.Checked)
            {
                int roleID = Convert.ToInt32(ddlRole.SelectedValue);
                int roleIDFrom = Convert.ToInt32(ddlRoleCopy.SelectedValue);

                if (roleID == roleIDFrom)
                {
                    //Helper.SetStatusMessage(lblMessage, "Please Select different role.", MessageTypeEnum.InvalidData);
                    this.SetPageMessage("Please Select different role.", MessageTypeEnum.Error);
                    this.ShowPageMessage(lblMessage, true);
                    return;
                }


                bStatus = RolePermissionBL.CopyRolePermission(roleIDFrom, roleID);
            }
            else
            {
                bStatus = SaveData();
            }


            AppCache.Remove(AppCache.CacheKey_RolesPermission);
            //lblMsg.Text = "Permission updated successfully.";
            if (bStatus)
            {
                //Helper.SetStatusMessage(lblMessage, "Permission updated successfully.", MessageTypeEnum.Successful);
                this.SetPageMessage("Permission updated successfully.", MessageTypeEnum.Successful);
               
                LoadData();
                chkCopy.Checked = false;
                ddlRoleCopy.Style["visibility"] = "hidden";
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
            List<dcRolePermission> cList = new List<dcRolePermission>();

            int roleID = Convert.ToInt32(ddlRole.SelectedValue);
            int appObjectID = 0;
            PermissionEnum permission = PermissionEnum.None;

            foreach (GridViewRow gvR in GridView1.Rows)
            {
                if (gvR.RowType == DataControlRowType.DataRow)
                {
                    appObjectID = Convert.ToInt32(((DataKey)GridView1.DataKeys[gvR.RowIndex])["AppObjectID"].ToString());
                    permission = PermissionEnum.None;

                    CheckBox chkRead = (CheckBox)gvR.FindControl("chkRead");
                    if (chkRead.Enabled && chkRead.Checked)
                    {
                        permission = permission | PermissionEnum.Read;
                    }
                    CheckBox chkAdd = (CheckBox)gvR.FindControl("chkAdd");
                    if (chkAdd.Enabled && chkAdd.Checked)
                    {
                        permission = permission | PermissionEnum.Add;
                    }
                    CheckBox chkEdit = (CheckBox)gvR.FindControl("chkEdit");
                    if (chkEdit.Enabled && chkEdit.Checked)
                    {
                        permission = permission | PermissionEnum.Edit;
                    }
                    CheckBox chkDelete = (CheckBox)gvR.FindControl("chkDelete");
                    if (chkDelete.Enabled && chkDelete.Checked)
                    {
                        permission = permission | PermissionEnum.Delete;
                    }

                    CheckBox chkList = (CheckBox)gvR.FindControl("chkList");
                    if (chkList.Enabled && chkList.Checked)
                    {
                        permission = permission | PermissionEnum.List;
                    }

                    CheckBox chkExecute = (CheckBox)gvR.FindControl("chkExecute");
                    if (chkExecute.Enabled && chkExecute.Checked)
                    {
                        permission = permission | PermissionEnum.Execute;
                    }
                    CheckBox chkEnabled = (CheckBox)gvR.FindControl("chkEnabled");
                    if (chkEnabled.Enabled && chkEnabled.Checked)
                    {
                        permission = permission | PermissionEnum.Enabled;
                    }
                    CheckBox chkVisible = (CheckBox)gvR.FindControl("chkVisible");
                    if (chkVisible.Enabled && chkVisible.Checked)
                    {
                        permission = permission | PermissionEnum.Visible;
                    }

                    dcRolePermission cObj = new dcRolePermission();
                    cObj.AppObjectID = appObjectID;
                    cObj.RoleID = roleID;
                    cObj.Permission = (int)permission;

                    cList.Add(cObj);
                }
            }


            RolePermissionBL.UpdateRolePermission(cList);
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
    }
}
