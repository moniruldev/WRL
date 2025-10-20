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
using PG.Report.ReportEnums;
using PG.Report;
using PG.Report.ReportGen.WRELRGN;
using PG.DBClass.SecurityDC;

namespace PG.Web.WREL
{
    public partial class ClientWiseBillReport : BagePage
    {
        int CompanyID = 0;
        private dcUser loggedinUser = null;
        public string ItemListServiceLink = PageLinks.InventoryLink.GetLink_ItemList;
        public string ItemGroupListServiceLink = PageLinks.InventoryLink.GetLink_ItemGroupList;
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;

        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;
        public string ClientListServiceLink = PageLinks.InventoryLink.GetLink_ClientList;
        //public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        //public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;

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

            if (!IsPostBack)
            {
                hdnClientId.Value = loggedinUser.CLIENT_ID.ToString();
                FillCombo();
                SetDate();
                LoadData();
              
              
            }
            SetHyperLink();
        }

        private void FillCombo()
        {
           
        }

        private void SetDate()
        {
            var now = DateTime.Now;
            var firstDate = new DateTime(now.Year, now.Month, 1);
            txtFromDate.Text = Convert.ToDateTime(firstDate).ToString("dd-MMM-yyyy");//now.ToString("dd-MMM-yyyy");
            txtToDate.Text = now.ToString("dd-MMM-yyyy");

        }

        private void SetHyperLink()
        {
            //new button
            //string hLink = "javascript:tbopen(0)";
            //if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            //{
            //    hLink = "javascript:tbopen(0)";
            //    this.btnNewAdd.Attributes.Add("onclick", hLink);
            //}
            //else
            //{
            //    hLink = "javascript:tbopen(0)";
            //    this.btnNewAdd.Attributes.Add("onclick", hLink);
            //}
        }
        private void LoadData()
        {
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
            prmCn.CLIENT_ID = Conversion.StringToInt(hdnClientId.Value);
            prmCn.USER_TYPE = loggedinUser.UserType;
            prmCn.FromDate = fromDate;
            prmCn.ToDate = toDate;
       
            List<dcCN_CREATION_MST> listData = CN_CREATION_MSTBL.GetCNInfoList(prmCn, null);
           

        }


       


      

     
       

       

        private ReportOptions GetReportOptions()
        {
            ReportOptions rptOption = new ReportOptions();

            rptOption.ReportViewMode = ReportViewModeEnum.InNewWindow;
            rptOption.ReportOpenType = this.ReportOpenType;
            rptOption.ReportExportType = ReportExportTypeEnum.PDF;

            AppInfo.SetAppInfoToReportOptions(rptOption);
            CompanyInfo.SetCompanyInfoToReportOptions(rptOption, this.Context);
            rptOption.UserName = base.LoginUserName;

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

            string jsScript = "$(function(){tbopen('" + reportKey + "', " + strPDFView + ", " + strIsPrint + "," + strIsPDFAutoPrint + "," + strWait + ");});";

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
        }

        protected void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            if (txtClientName.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", "alert('Please Enter Client !!');", true);
                txtClientName.Focus();
                return;
            }

            clsPrmWREL prm = new clsPrmWREL();
            prm.CLIENT_ID = Conversion.StringToInt(hdnClientId.Value);
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
            prm.FromDate = fromDate;
            prm.ToDate = toDate;
            ReportOptions rptOption = GetReportOptions();

            AppReport rpt = WRELRGN.CNDateWiseBill_Report(prm, rptOption);

            if (rpt == null || rpt.DataSources.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", "alert('No data found for the selected criteria!');", true);
                return;
            }
            string rk = AppReport.SetAppReportToSession(rpt, this.Context);
            ShowReport(rk);

        }

        protected void btnClearFilter_Click(object sender, EventArgs e)
        {
            Cleartextbox();
            LoadData();

        }

        public void Cleartextbox()
        {
          

        }

       


    }
}
