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
using PG.Report;
using PG.Report.ReportEnums;
using PG.Report.ReportGen.WRELRGN;
using PG.DBClass.SecurityDC;

namespace PG.Web.WREL
{
    public partial class ParcelList : BagePage
    {
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;

        int CompanyID = 0;
        public string ItemListServiceLink = PageLinks.InventoryLink.GetLink_ItemList;
        public string ItemGroupListServiceLink = PageLinks.InventoryLink.GetLink_ItemGroupList;
        public string ClientListServiceLink = PageLinks.InventoryLink.GetLink_ClientList;
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
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
                SetDate();
                LoadData();
                btnGridPageGoTo.Style.Add("display", "none");
            }
            SetHyperLink();
        }

        private void FillCombo()
        {
           
        }

        private void SetDate()
        {
            var now = DateTime.Now;

            // First day of current month
            var firstDate = new DateTime(now.Year, now.Month, 1);

            // Last day of current month
            var lastDate = firstDate.AddMonths(1).AddDays(-1);

            txtFromDate.Text = firstDate.ToString("dd-MMM-yyyy");
            txtToDate.Text = lastDate.ToString("dd-MMM-yyyy");

        }

        private void SetHyperLink()
        {
            //new button
            string hLink = "javascript:tbopen(0)";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopen(0)";
                this.btnNewAdd.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "javascript:tbopen(0)";
                this.btnNewAdd.Attributes.Add("onclick", hLink);
            }
        }
        private void LoadData()
        {
            dcUser loggedinUser = AppSecurity.GetUserInfoFromSession();
            clsPrmWREL prmCn = new clsPrmWREL();
            DateTime? fromDate = null;
            DateTime? toDate = null;

            DateTime dt;
            if (DateTime.TryParse(txtFromDate.Text, out dt))
            {
                fromDate = dt;
            }
            if (DateTime.TryParse(txtToDate.Text, out dt))
            {
                toDate = dt;
            }
            prmCn.FromDate = fromDate;
            prmCn.ToDate = toDate;
            prmCn.USER_TYPE = loggedinUser.UserType;
            prmCn.CLIENT_ID =Conversion.StringToInt(hdnClientId.Value);
            prmCn.Status_ID = Conversion.StringToInt(ddlStatus.SelectedValue);

            List<dcCN_CREATION_MST> listData = CN_CREATION_MSTBL.GetCNInfoBookingList(prmCn, null);
            listData = listData
                .OrderBy(x => x.CN_ID) // ascending
                .ToList();
            BindGridData(listData);
            SetGridInfo(listData.Count);

        }


        private void BindGridData(List<dcCN_CREATION_MST> listData)
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
            //GridView1.CssClass = "grid";
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dcCN_CREATION_MST rowData = (dcCN_CREATION_MST)e.Row.DataItem;

                // Example: If EntryDate is older than SLA days → make row red
                if (rowData.CREATE_DATE != null && rowData.SLA_DAYS > 0)
                {
                    DateTime expiryDate = rowData.CREATE_DATE.Value.AddDays((double)rowData.SLA_DAYS);
                    if (rowData.IS_DELIVERED == "N")
                    {
                        if (DateTime.Now > expiryDate)
                        {
                            e.Row.BackColor = System.Drawing.Color.Red;
                            e.Row.ForeColor = System.Drawing.Color.White; // optional for contrast
                        }
                    }
                }

                // Example: If EntryDate is today → highlight differently
                if (rowData.IS_DELIVERED == "N")
                {
                    if (rowData.CREATE_DATE == DateTime.Now.Date)
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGreen;
                    }
                }


                //string strD = DataBinder.Eval(e.Row.DataItem, "CN_ID").ToString(); ;
                //HyperLink lnk = (HyperLink)e.Row.Cells[0].Controls[0];

                //string hLink = "javascript:tbopen(" + strD + ")";
                //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
                //{
                //    hLink = "javascript:tbopen(" + strD + ")";
                //}
                //else
                //{
                //    hLink = "~/WREL/ParcelCreation.aspx?id=" + strD;
                //}
                //lnk.NavigateUrl = hLink;


               
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                e.Row.Visible = false;
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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void SetGridInfo(int rowCount)
        {
            txtGridPageNo.Text = "0";
            lblGridPageInfo.Text = " of 0";
            if (GridView1.PageCount > 0)
            {
                txtGridPageNo.Text = (GridView1.PageIndex + 1).ToString();
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
                BindGridData(new List<dcCN_CREATION_MST>());
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
                BindGridData(new List<dcCN_CREATION_MST>());
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
                BindGridData(new List<dcCN_CREATION_MST>());
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "print")
            {
                int cnID = Convert.ToInt32(e.CommandArgument);

                clsPrmWREL prm = new clsPrmWREL();
                ReportOptions rptOption = GetReportOptions();
                prm.CN_ID = cnID;


                AppReport rpt = WRELRGN.CN_Barcode_Report(prm, rptOption);
                string rk = AppReport.SetAppReportToSession(rpt, this.Context);
                ShowReport(rk);

            }
        }

        private ReportOptions GetReportOptions()
        {
            ReportOptions rptOption = new ReportOptions();

            //rptOption.ReportViewMode = (ReportViewModeEnum)Convert.ToInt32(ddlReportFormat.SelectedValue);
            //rptOption.ReportOpenType = this.ReportOpenType;

            //AppInfo.SetAppInfoToReportOptions(rptOption);
            //CompanyInfo.SetCompanyInfoToReportOptions(rptOption, this.Context);
            //rptOption.UserName = base.LoginUserName;


            return rptOption;
        }
        private void ShowReport(string reportKey)
        {

            ReportOpenTypeEnum rptOpenType = this.ReportOpenType;
            ReportViewModeEnum rptViewMode = (ReportViewModeEnum)Convert.ToInt32(ddlReportViewMode.SelectedValue);

            bool pdfView = ddlReportViewType.SelectedValue == "1";

            string strWait = "true";
            string strIsPrint = "false";
            string strIsPDFAutoPrint = "false";
            string strPDFView = "false";


            switch (rptOpenType)
            {
                case ReportOpenTypeEnum.Preview:
                    if (ddlReportViewType.SelectedValue == "1")
                    {
                        strPDFView = "true";
                    }

                    break;
                case ReportOpenTypeEnum.Print:
                    rptViewMode = ReportViewModeEnum.InThisTab;
                    strWait = "false";
                    strIsPrint = "true";
                    break;
                case ReportOpenTypeEnum.Export:
                    //rptViewMode = ReportViewModeEnum.InThisTab;
                    rptViewMode = ReportViewModeEnum.InNewWindow;
                    strWait = "false";
                    break;
            }

            bool isPDFAutoPrint = true;
            if (Request.Browser.Browser.ToLower().Contains("ie") == true)
            {
                // isPDFAutoPrint = !AccSettings.IsIERsClientPrint;
            }

            strIsPDFAutoPrint = isPDFAutoPrint ? "true" : "false";


            //string strTime = DateTime.Now.ToString("hhmm");
            string strTime = DateTime.Now.ToFileTime().ToString();
            //string strTime = DateTime.Now now.getTime().toString();
            string url = this.ReportViewPageLink + "?rk=" + reportKey + "&_tt=" + strTime;
            if (pdfView && rptOpenType == ReportOpenTypeEnum.Preview)
            {
                url = this.ReportViewPDFPageLink + "?rk=" + reportKey + "&_tt=" + strTime;
            }

            //string url = this.ReportViewPageLink + "?rk=" + reportKey + "&_tt=" + strTime;
            //string jsScript = "$(function(){tbopen('" + reportKey + "'," + strWait + ");});";

            //string jsScript = "$(function(){tbopen('" + reportKey + "', " +  strIsPrint  +  "," + strWait + ");});";

            //string jsScript = "$(function(){tbopen('" + reportKey + "', " + strIsPrint +  "," + strIsPDFAutoPrint  + "," + strWait + ");});";

            string jsScript = "$(function(){tbopen('" + reportKey + "', " + strPDFView + ", " + strIsPrint + "," + strIsPDFAutoPrint + "," + strWait + ");});";

            //string jsScript = string.Format("$(function(){tbopen('{0}',{1},{2});});",reportKey,strIsPrint,strWait);


            switch (rptViewMode)
            {
                case ReportViewModeEnum.InThisTab:
                    if (rptOpenType == ReportOpenTypeEnum.Print)
                    {
                        ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
                    }
                    else
                    {
                        Response.Redirect(url, false);
                    }


                    break;
                case ReportViewModeEnum.InNewTab:
                    ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
                    break;
                case ReportViewModeEnum.InNewWindow:
                    // ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));
                    ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>reportInNewWindow('{0}');</script>", url));
                    break;
                case ReportViewModeEnum.InDialog:
                    break;
                default:
                    ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
                    break;
            }
            //ReportOpenTypeEnum rptOpenType = this.ReportOpenType;
            //ReportViewModeEnum rptViewMode = (ReportViewModeEnum)Convert.ToInt32(1);

            //bool pdfView = true;

            //string strWait = "true";
            //string strIsPrint = "false";
            //string strIsPDFAutoPrint = "false";
            //string strPDFView = "false";


            //switch (rptOpenType)
            //{
            //    case ReportOpenTypeEnum.Preview:
            //        if (ddlReportViewType.SelectedValue == "1")
            //        {
            //            strPDFView = "true";
            //        }

            //        break;
            //    case ReportOpenTypeEnum.Print:
            //        rptViewMode = ReportViewModeEnum.InThisTab;
            //        strWait = "false";
            //        strIsPrint = "true";
            //        break;
            //    case ReportOpenTypeEnum.Export:
            //        //rptViewMode = ReportViewModeEnum.InThisTab;
            //        rptViewMode = ReportViewModeEnum.InNewWindow;
            //        strWait = "false";
            //        break;
            //}

            //bool isPDFAutoPrint = true;
            //if (Request.Browser.Browser.ToLower().Contains("ie") == true)
            //{
            //    // isPDFAutoPrint = !AccSettings.IsIERsClientPrint;
            //}

            //strIsPDFAutoPrint = isPDFAutoPrint ? "true" : "false";


            ////string strTime = DateTime.Now.ToString("hhmm");
            //string strTime = DateTime.Now.ToFileTime().ToString();
            ////string strTime = DateTime.Now now.getTime().toString();
            //string url = this.ReportViewPageLink + "?rk=" + reportKey + "&_tt=" + strTime;
            //if (pdfView && rptOpenType == ReportOpenTypeEnum.Preview)
            //{
            //    url = this.ReportViewPDFPageLink + "?rk=" + reportKey + "&_tt=" + strTime;
            //}


            //string jsScript = "$(function(){tbopen('" + reportKey + "', " + strPDFView + ", " + strIsPrint + "," + strIsPDFAutoPrint + "," + strWait + ");});";


            //switch (rptViewMode)
            //{
            //    case ReportViewModeEnum.InThisTab:
            //        if (rptOpenType == ReportOpenTypeEnum.Print)
            //        {
            //            ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
            //        }
            //        else
            //        {
            //            Response.Redirect(url, false);
            //        }


            //        break;
            //    case ReportViewModeEnum.InNewTab:
            //        ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
            //        break;
            //    case ReportViewModeEnum.InNewWindow:
            //        ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));
            //        break;
            //    case ReportViewModeEnum.InDialog:
            //        break;
            //    default:
            //        ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
            //        break;
            //}
        }




    }
}
