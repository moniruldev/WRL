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

using PG.DBClass.AccountingDC.AccEnums;
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;


namespace PG.Web.Accounting
{
    public partial class CostCenterList : BagePage
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
                btnGridPageGoTo.Style.Add("display", "none");
            }
            SetHyperLink();
            this.ShowPageMessage(this.lblMessage);
        }

        private void FillCombo()
        {
            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(new ListItem("(all category)", "0"));

            ddlCategory.DataTextField = "AccRefCategoryName";
            ddlCategory.DataValueField = "AccRefCategoryID";
            ddlCategory.AppendDataBoundItems = true;
            ddlCategory.DataSource = AccRefCategoryBL.GetAccRefCategoryList(this.CompanyID, (int)AccRefTypeEnum.CostCenter);
            ddlCategory.DataBind();
            
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
            int accRefCategoryID = Convert.ToInt32(ddlCategory.SelectedValue);

            List<dcAccRef> listData = AccRefBL.GetAccRefList(this.CompanyID, (int)AccRefTypeEnum.CostCenter, accRefCategoryID); ;
            //listData.Clear();
            
            BindGridData(listData);
            SetGridInfo(listData.Count);
            
        }


        private void BindGridData(List<dcAccRef> listData)
        {
            int pageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            if (pageSize == 0)
            {
                GridView1.AllowPaging = false;
                GridView1.PageIndex = 0;
            }
            else
            {
                GridView1.AllowPaging = true;
                GridView1.PageSize = pageSize;
            }
            int rowCount = listData.Count;
            GridView1.DataSource = listData;
            GridView1.DataBind();
            GridView1.CssClass = "grid";
        }


        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
           // LoadData();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string strD = DataBinder.Eval(e.Row.DataItem, "AccRefID").ToString(); ;
                HyperLink lnk = (HyperLink)e.Row.Cells[0].Controls[0];

                string hLink = "javascript:tbopen(" + strD + ")";
                if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
                {
                    hLink = "javascript:tbopen(" + strD + ")";
                }
                else
                {
                    hLink = "~/Accounting/CostCenter.aspx?id=" + strD;
                }
                //lnk.NavigateUrl = "~/Admin/UserAddEdit.aspx?UserID=" + strD;
                lnk.NavigateUrl = hLink;


              //  LinkButton lnkBtn = (LinkButton)e.Row.Cells[4].Controls[0];
               // lnkBtn.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                e.Row.Visible = false;
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
            GridView1.PageIndex = e.NewPageIndex;
            LoadData();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void SetGridInfo(int rowCount)
        {
            txtGridPageNo.Text = "0";
            lblGridPageInfo.Text = " of 0";
            if (GridView1.PageCount > 0)
            {
                txtGridPageNo.Text =  (GridView1.PageIndex + 1).ToString();
                lblGridPageInfo.Text = "of " + GridView1.PageCount.ToString(); 
            }

            hdnRowCount.Value = rowCount.ToString();

            int startRow = 0;
            int endRow = 0;

            int pageSize = GridView1.AllowPaging ? GridView1.PageSize : rowCount;
            
            if (rowCount > 0)
            {
                startRow = (GridView1.PageIndex * pageSize);
                endRow = startRow + pageSize;
                endRow = endRow > rowCount ? rowCount : endRow;

                startRow = startRow + 1;
            }


            if (rowCount > 1)
            {
                lblTotal.Text = string.Format("Rows: {0}-{1} of {2}", startRow, endRow, rowCount);
            }
            else
            {
                lblTotal.Text = string.Format("Rows: {0} of {0}", rowCount);
            }


        }

        public void GoToPageNext()
        {
            if (GridView1.PageCount > 0)
            {
                int totPage = GridView1.PageCount;
                int curPage = GridView1.PageIndex + 1;

                int gotoPage = curPage + 1;
                gotoPage = gotoPage > totPage ? totPage : gotoPage;
                GridView1.PageIndex = gotoPage - 1;
                LoadData();
            }
            else
            {
                BindGridData(new List<dcAccRef>());
                SetGridInfo(0);
            }
        }

        public void GoTotPagePrevious()
        {
            if (GridView1.PageCount > 0)
            {
                int totPage = GridView1.PageCount;
                int curPage = GridView1.PageIndex + 1;

                int gotoPage = curPage - 1;
                gotoPage = gotoPage < 1 ? 1 : gotoPage;
                GridView1.PageIndex = gotoPage - 1;
                LoadData();
            }
            else
            {
                BindGridData(new List<dcAccRef>());
                SetGridInfo(0);
            }

        }

        public void GoToPageFirst()
        {
            GoTotPageNo(1);
        }

        public void GoTotPageLast()
        {
            GoTotPageNo(GridView1.PageCount);
        }

        public void GoTotPageNo(int pageNo)
        {
            if (GridView1.PageCount > 0)
            {
                pageNo = pageNo > GridView1.PageCount ? GridView1.PageCount : pageNo;
                pageNo = pageNo < 1 ? 1 : pageNo;
                GridView1.PageIndex = pageNo - 1;
                LoadData();
            }
            else
            {
                BindGridData(new List<dcAccRef>());
                SetGridInfo(0);
            }
        }


        protected void btnGridPagePrev_Click(object sender, EventArgs e)
        {
            GoTotPagePrevious();
        }

        protected void btnGridPageNext_Click(object sender, EventArgs e)
        {
            GoToPageNext();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void btnGridPageLast_Click(object sender, EventArgs e)
        {
            GoTotPageLast();
        }

        protected void btnGridPageFirst_Click(object sender, EventArgs e)
        {
            GoToPageFirst();
        }

        protected void btnGridPageGoTo_Click(object sender, EventArgs e)
        {
            GoTotPageNo(PG.Core.Utility.Conversion.StringToInt(txtGridPageNo.Text));
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }


    }
}
