using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.ProductionBL;
using PG.Core.Web;
using PG.DBClass.AccountingDC;
using PG.DBClass.InventoryDC;
using PG.Report;
using PG.Report.ReportEnums;
using PG.Report.ReportGen.ProductionRGN;
using PG.Report.ReportRBL.InventoryRBL;
using PG.Report.ReportRBL.ProductionRBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PG.Web.MIS
{
    public partial class MIS_ProductionReport : BagePage
    {
       
        int CompanyID = 0;
        dcAccSettings AccSettings = null;

        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;

        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;


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
             this.InitDBContext();
            this.CompanyID = CompanyInfo.GetCompanyID();
            this.hdnCompanyID.Value = this.CompanyID.ToString();
            
            this.AppMessageCurrent = base.GetAppMessage();

            //this.AccSettings = AccSettingsBL.GetAccSettingByCompanyID(this.CompanyID);

            if (!IsPostBack) //first Time
            {
                FillCombo();
                txtFromDate.Text = DateTime.Now.ToString("01-MMM-yy");
                txtToDate.Text = DateTime.Now.ToString("dd-MMM-yy");
                SetReportParam();
            }
            else
            {

            }
        }


        private void FillCombo()
        {
            //Battery Category

            ddlchkTypeCategory.Items.Clear();
            //ddlLocation.Items.Add(new ListItem("All", "0"));
            ddlchkTypeCategory.AppendDataBoundItems = true;

            ddlchkTypeCategory.DataTextField = "BATERY_CAT_DESCR";
            ddlchkTypeCategory.DataValueField = "BATERY_CAT_ID";
            ddlchkTypeCategory.AppendDataBoundItems = true;
            ddlchkTypeCategory.DataSource = BATERY_CATEGORYBL.SND_GetBattery_CategoryList(); 
            ddlchkTypeCategory.DataBind();


        }


        protected void tvwReport_SelectedNodeChanged(object sender, EventArgs e)
        {
            string rptID = tvwReport.SelectedNode.Value;
            SetReportParam();
        }
        private void SetReportParam()
        {
            //int rptID = Convert.ToInt32(ddlReport.SelectedValue);

            //this.Form.DefaultButton = this.btnDisableEnter.UniqueID;



            int rptID = Convert.ToInt32(tvwReport.SelectedNode.Value);


            lblReportName.Text = tvwReport.SelectedNode.Text;


            


            lblDeptName.Visible = false;
            ddlDEPT_NAME.Visible = false;

            lblFromDate.Visible = false;
            txtFromDate.Visible = false;
            lblFromDate.Text = "Date:";


            lblToDate.Visible = false;
            txtToDate.Visible = false;
            lblToDate.Text = "To";

           // FillComboOpeningBalanceType();
             

            ReportIDEnum rptIDEnum = (ReportIDEnum)rptID;

            switch (rptIDEnum)
            {

                case ReportIDEnum.Lead_Consumption_Summary: //Lead_Consumption_Summary

                     
                    lblFromDate.Visible = true;
                    lblToDate.Visible = true;

                    txtFromDate.Visible = true;
                    txtToDate.Visible = true;

                    break;

                case ReportIDEnum.Monthly_ITEM_Production:  


                    lblFromDate.Visible = true;
                    lblToDate.Visible = true;

                    txtFromDate.Visible = true;
                    txtToDate.Visible = true;

                    break;

             
            }
        }


        protected void btnPrint_Click(object sender, EventArgs e)
        {
            this.ReportOpenType = ReportOpenTypeEnum.Print;
           // OpenReport();
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            this.ReportOpenType = ReportOpenTypeEnum.Preview;
             OpenReport();
        }


        private void OpenReport()
        {
            //int rptID = Convert.ToInt32(ddlReport.SelectedValue);
            int rptID = Convert.ToInt32(tvwReport.SelectedNode.Value);


            if (rptID == 0)
            {
                this.SetPageMessage("Please Select Report!", MessageTypeEnum.InvalidData, true);
                base.ShowPageMessage(lblMessage, true);
                //ddlReport.Focus();
                return;
            }


            ReportIDEnum rptIDEnum = (ReportIDEnum)rptID;


            switch (rptIDEnum)
            {
                case ReportIDEnum.Lead_Consumption_Summary: //chart of account
                    ReportLead_Consumption_Summary();
                    break;

                //case ReportIDEnum.Battery_Lead_Consumption :
                //    ReportBatteryLead_Consumption();
                //    break;
               
                //case ReportIDEnum.Battery_Production:
                //    ReportBatteryProduction();
                //    break;

                case ReportIDEnum.Monthly_ITEM_Production:
                    ReportMonthlyBatteryProduction();
                    break;
                case ReportIDEnum.Monthly_CAT_Production:
                    ReportMonthlyBatteryProduction();
                    break;
                default:
                    break;

            }
        }

        private List<string> GetBatteryCategoryList()
        {
            List<string> catid = new List<string>();
            foreach (ListItem item in ddlchkTypeCategory.Items)
            {
                if (item.Selected)
                {
                    catid.Add(item.Value);
                }
            }
            return catid;
        }


        private void ReportMonthlyBatteryProduction()
        {
            int companyID = this.CompanyID;
            clsPrmInventory prmLdg = new clsPrmInventory();
            //   prmLdg.CompanyID = companyID;
            List<string> cat_idlist = GetBatteryCategoryList();

            prmLdg.IsLocation = false;
            prmLdg.fromProdDate = txtFromDate.Text.Trim();
            prmLdg.toProdDate = txtToDate.Text.Trim();
            prmLdg.CatidList = cat_idlist;


            ReportOptions rptOption = GetReportOptions();
            AppReport rpt = ProductionRGN.GenerateMonthlyBatteryProduction(prmLdg, rptOption);
            string rk = AppReport.SetAppReportToSession(rpt, this.Context);

            ShowReport(rk);

        }


        private void ReportBatteryLead_Consumption()
        {
            int companyID = this.CompanyID;
            clsPrmInventory prmLdg = new clsPrmInventory();
            //   prmLdg.CompanyID = companyID;
            prmLdg.IsLocation = false;
            prmLdg.fromProdDate = txtFromDate.Text.Trim();
            prmLdg.toProdDate = txtToDate.Text.Trim();
            ReportOptions rptOption = GetReportOptions();
            AppReport rpt = ProductionRGN.GenerateBatteryLeadConsumption(prmLdg, rptOption);
            string rk = AppReport.SetAppReportToSession(rpt, this.Context);

            ShowReport(rk);

        }

        private void ReportLead_Consumption_Summary()
        {

            int companyID = this.CompanyID;

            clsPrmInventory prmLdg = new clsPrmInventory();
         //   prmLdg.CompanyID = companyID;
            prmLdg.IsLocation = false;
            prmLdg.fromProdDate = txtFromDate.Text.Trim();
            prmLdg.toProdDate = txtToDate.Text.Trim();

            ReportOptions rptOption = GetReportOptions();

            AppReport rpt = ProductionRGN.GeneratePureLeadConsumption(prmLdg, rptOption);
                //MaterialStockRBL.PureLead_RCV_ISSUE_DEPT(prmLdg);
                //ChartOfAccountsRGN.GenerateChartOfAccounts(prmLdg, rptOption);
            string rk = AppReport.SetAppReportToSession(rpt, this.Context);

            ShowReport(rk);

        }



        private void ShowReport(string reportKey)
        {
            ReportOpenTypeEnum rptOpenType = this.ReportOpenType;
            ReportViewModeEnum rptViewMode = (ReportViewModeEnum)Convert.ToInt32(ddlReportViewMode.SelectedValue);

            bool pdfView =   true;

            string strWait = "true";
            string strIsPrint = "false";
            string strIsPDFAutoPrint = "false";
            string strPDFView = "false";


            switch (rptOpenType)
            {
                case ReportOpenTypeEnum.Preview:
                    if ("1" == "1")
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
                    ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>reportInNewWindow('{0}');</script>", url));
                    //ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", url));
                    break;
                case ReportViewModeEnum.InDialog:
                    break;
                default:
                    ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "showreport", jsScript, true);
                    break;
            }
        }


        private ReportOptions GetReportOptions()
        {
            ReportOptions rptOption = new ReportOptions();

            rptOption.ReportViewMode = (ReportViewModeEnum)Convert.ToInt32(ddlReportViewMode.SelectedValue);
            rptOption.ReportOpenType = this.ReportOpenType;
            //rptOption.ReportExportType = (ReportExportTypeEnum)Convert.ToInt32(ddlExport.SelectedValue);
            //rptOption.DisbalePDFPrint = AccSettings.DisablePDFPrint;

            AppInfo.SetAppInfoToReportOptions(rptOption);
            CompanyInfo.SetCompanyInfoToReportOptions(rptOption, this.Context);
            rptOption.UserName = base.LoginUserName;


            return rptOption;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            this.ReportOpenType = ReportOpenTypeEnum.Export;
           // OpenReport();
        }
    }
}