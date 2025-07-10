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
using PG.BLLibrary.AccountingBL;


namespace PG.Web.Accounting
{
    public partial class AccYearList : BagePage
    {
        int CompanyID = 0;

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

            if (!IsPostBack)
            {
                FillCombo();
                LoadData();
            }
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
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
                //hLink = "~/Accounting/AccYear.aspx";
                //hLink = ResolveUrl("~/Accounting/AccYear.aspx");
                
                hLink = "javascript:tbopen(0)";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
        }
        private void LoadData()
        {
            GridView1.DataSource = AccYearBL.GetAccYearList(this.CompanyID);
            GridView1.DataBind();

            lblTotal.Text = "Total: " + GridView1.Rows.Count.ToString();
        }


        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
           // LoadData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strD = DataBinder.Eval(e.Row.DataItem, "AccYearID").ToString(); ;
                HyperLink lnk = (HyperLink)e.Row.Cells[0].Controls[0];

                string hLink = "javascript:tbopen(" + strD + ")";
                if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
                {
                    hLink = "javascript:tbopen(" + strD + ")";
                }
                else
                {
                    hLink = "~/Accounting/AccYear.aspx?id=" + strD;
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
