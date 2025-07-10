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

using PG.DBClass.SystemDC;
using PG.BLLibrary.SystemsBL;

namespace PG.Web.Admin
{
    public partial class AppMenuList : BagePage
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
            if (!IsPostBack)
            {
                FillCombo();
                LoadData();
            }
            SetHyperLink();
        }

        private void FillCombo()
        {

            //ddlLoanType.Items.Clear();
            //ddlLoanType.Items.Add(new ListItem("(all type)", "0"));
            //ddlLoanType.AppendDataBoundItems = true;
            //ddlLoanType.DataSource = BLLibrary.LoanBL.EmpLoanTypeBL.GetEmpLoanTypeList();
            //ddlLoanType.DataTextField = "EmpLoanTypeName";
            //ddlLoanType.DataValueField = "EmpLoanTypeID";
            //ddlLoanType.DataBind();

            //ddlLoanStatus.Items.Clear();
            //ddlLoanStatus.Items.Add(new ListItem("(all status)", "0"));
            //ddlLoanStatus.AppendDataBoundItems = true;
            //ddlLoanStatus.DataSource = BLLibrary.LoanBL.EmpLoanStatusBL.GetEmpLoanStatusList();
            //ddlLoanStatus.DataTextField = "EmpLoanStatusName";
            //ddlLoanStatus.DataValueField = "EmpLoanStatusID";
            //ddlLoanStatus.DataBind();

        }

        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(0)";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopen(0)";
                this.btnAddNew.PostBackUrl = string.Empty;
                this.btnAddNew.OnClientClick = hLink;
            }
            else
            {
                hLink = "~/Admin/AppMenu.aspx";
                this.btnAddNew.PostBackUrl = hLink;
                this.btnAddNew.OnClientClick = string.Empty;
            }
        }
        private void LoadData()
        {
            //int typeID = Convert.ToInt32(ddlLoanType.SelectedValue);
            //int statusID = Convert.ToInt32(ddlLoanStatus.SelectedValue);

            GridView1.DataSource = AppMenuBL.GetAppMenuList(BLLibrary.GlobalsBL.AppID);
            GridView1.DataBind();

            lblTotal.Text = "Total:" + GridView1.Rows.Count.ToString();
        }


        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
           // LoadData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strD = DataBinder.Eval(e.Row.DataItem, "AppMenuID").ToString(); ;
                HyperLink lnk = (HyperLink)e.Row.Cells[0].Controls[0];

                string hLink = "javascript:tbopen(" + strD + ")";
                if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
                {
                    hLink = "javascript:tbopen(" + strD + ")";
                }
                else
                {
                    hLink = "~/Admin/AppMenu.aspx?id=" + strD;
                }
                //lnk.NavigateUrl = "~/Admin/UserAddEdit.aspx?UserID=" + strD;
                lnk.NavigateUrl = hLink;


              //  LinkButton lnkBtn = (LinkButton)e.Row.Cells[4].Controls[0];
               // lnkBtn.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
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
    }
}
