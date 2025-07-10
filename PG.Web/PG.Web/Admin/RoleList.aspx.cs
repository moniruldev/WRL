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

using PG.DBClass.SecurityDC;
using PG.BLLibrary.SecurityBL;

namespace PG.Web.Admin
{
    public partial class RoleList : BagePage
    {

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
            if (!IsPostBack)
            {
                FillCombo();
                LoadData();
            }
            SetHyperLink();
        }

        private void FillCombo()
        {

        }

        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(0)";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopen(0)";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = string.Format("javascript:window.open({0})", "/Admin/User.aspx");
                this.btnAddNew.Attributes.Add("onclick", hLink);

                //hLink = "~/Admin/User.aspx";
                //this.btnAddNew.PostBackUrl = hLink;
                //this.btnAddNew.OnClientClick = string.Empty;
            }
            
        }


        private void LoadData()
        {

            //int roleKey = Convert.ToInt32(this.ddlRole.SelectedValue);
            List<dcRole> listData = RoleBL.GetRoleList(AppInfo.AppID);
            BindGridData(listData);

            lblTotal.Text = "Total:" + GridView1.Rows.Count.ToString();
        }

        private void BindGridData(List<dcRole> listData)
        {
            int rowCount = listData.Count;
            if (rowCount == 0)
            {
                listData.Add(new dcRole());
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strD = DataBinder.Eval(e.Row.DataItem, "RoleID").ToString(); ;
                HyperLink lnk = (HyperLink)e.Row.Cells[0].Controls[0];

                string hLink = "javascript:tbopen(" + strD + ")";
                if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
                {
                    hLink = "javascript:tbopen(" + strD + ")";
                }
                else
                {
                    hLink = "~/Admin/Role.aspx?uid=" + strD;
                }
                //lnk.NavigateUrl = "~/Admin/UserAddEdit.aspx?UserID=" + strD;
                lnk.NavigateUrl = hLink;


                LinkButton lnkBtn = (LinkButton)e.Row.Cells[1].Controls[0];
                lnkBtn.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
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
