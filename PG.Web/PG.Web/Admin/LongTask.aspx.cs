using System;
using System.Collections;
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

using PG.DBClass.SystemDC;
using PG.BLLibrary.SystemsBL;

namespace PG.Web.Admin
{
    public partial class LongTask : BagePage
    {
        bool isReLoad = false;


        protected override void OnPreInit(EventArgs e)
        {
            if (AdminGlobals.AdminMasterPage != string.Empty)
            {
                this.MasterPageFile = AdminGlobals.AdminMasterPage;
            }
            base.OnPreInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            this.SetPageCacheOff();
            if (!IsPostBack)
            {
                FillCombo();
                LoadData();
            }
            SetHyperLink();
        }

        protected void Page_PreRender(Object o, EventArgs e)
        {
            if (isReLoad)
            {

                LoadData();
            }
          //  base.OnPreRender(e);
        }


        private void FillCombo()
        {
            //this.ddlRole.DataSource = BLLibrary.SystemBL.RoleBL.GetRoleList(Globals.AppID);
            //this.ddlRole.DataTextField = "RoleName";
            //this.ddlRole.DataValueField = "RoleID";
            //this.ddlRole.DataBind();

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
            //    hLink = "~/Admin/User.aspx";
            //    this.btnAddNew.PostBackUrl = hLink;
            //    this.btnAddNew.OnClientClick = string.Empty;
            //}
            
        }


        private void LoadData()
        {

            //int roleKey = Convert.ToInt32(this.ddlRole.SelectedValue);
            
            
            //GridView1.DataSource = BLLibrary.SystemBL.UserBL.GetUserList(Globals.AppID,roleKey);
            GridView1.DataSource = LongTaskBL.GetLongTaskList();
            GridView1.DataBind();

            lblTotal.Text = "Total:" + GridView1.Rows.Count.ToString();
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btn = (Button)e.Row.Cells[3].Controls[0];
                //string strD = DataBinder.Eval(e.Row.DataItem, "UserID").ToString(); ;

                dcLongTask  task = (dcLongTask)e.Row.DataItem;

                if (task.TaskState == LongTaskStateEnum.InProgress)
                {
                    
                    btn.Enabled = true;
                }
                else
                {
                    btn.Enabled = false;
                }

                //HyperLink lnk = (HyperLink)e.Row.Cells[3].Controls[0];

                //string hLink = "javascript:tbopen(" + strD + ")";
                //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
                //{
                //    hLink = "javascript:tbopen(" + strD + ")";
                //}
                //else
                //{
                //    hLink = "~/Admin/User.aspx?uid=" + strD;
                //}
                ////lnk.NavigateUrl = "~/Admin/UserAddEdit.aspx?UserID=" + strD;
                //lnk.NavigateUrl = hLink;


                //LinkButton lnkBtn = (LinkButton)e.Row.Cells[4].Controls[0];
                //lnkBtn.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "stoptask")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvRow = GridView1.Rows[index]; 

                //GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                string taskid = GridView1.DataKeys[gvRow.RowIndex]["TaskID"].ToString();
                LongTaskBL.CancelTask(taskid);
                isReLoad = true;
                LoadData();
            }
        }
    }
}
