using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using PG.Web.Report;
using PG.Web.PageLinks;
using PG.Report;

using PG.Report.ReportEnums;
using PG.Report.ReportClass;

namespace PG.Web.Controls
{
    public partial class PrintButton : System.Web.UI.UserControl
    {
        public delegate void PrintButtonClickEventHandler(object sender, EventArgs e);
        public event PrintButtonClickEventHandler PrintClick;


        private ReportOpenTypeEnum m_DefaultPrintAction = ReportOpenTypeEnum.Preview ;
        public ReportOpenTypeEnum DefaultPrintAction
        {
            get { return m_DefaultPrintAction; }
            set { 
                m_DefaultPrintAction = value;
                PrintActionChanged();
            }
        }

        private ReportViewModeEnum m_DefaultViewMode = ReportViewModeEnum.InNewTab;
        public ReportViewModeEnum DefaultViewMode
        {
            get { return m_DefaultViewMode; }
            set
            {
                m_DefaultViewMode = value;
                PrintPreviewChanged();
            }
        }


        private ReportExportTypeEnum m_DefaultExportType = ReportExportTypeEnum.PDF;
        public ReportExportTypeEnum DefaultExportType
        {
            get { return m_DefaultExportType; }
            set
            {
                m_DefaultExportType = value;
                ExportTypeChanged();
            }
        }

        private bool m_AutoPrint = false;
        public bool AutoPrint
        {
            get { return m_AutoPrint; }
            set { m_AutoPrint = value; }
        }

        private bool m_PDFAutoPrint = false;
        public bool PDFAutoPrint
        {
            get { return m_PDFAutoPrint; }
            set { m_PDFAutoPrint = value; }
        }


        public string m_ReportKey = string.Empty;
        public string ReportKey
        {
            get { return m_ReportKey; }
            set { m_ReportKey = value; }
        }

        public string m_ReportError = string.Empty;
        public string ReportError
        {
            get { return m_ReportError; }
            set { m_ReportError = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //first time

            }
            else
            {
                //
                this.DefaultPrintAction = (ReportOpenTypeEnum)Convert.ToInt32(hdnPrintAction.Value);
            }
        }

        private void PrintActionChanged()
        {
            switch (this.m_DefaultPrintAction)
            {
                case ReportOpenTypeEnum.Preview:
                    this.btnPrintAction.Text = "Priview";
                    
                    break;
                case ReportOpenTypeEnum.Print:
                    this.btnPrintAction.Text = "Print";
                    break;
                case ReportOpenTypeEnum.Export:
                    this.btnPrintAction.Text = "Export";
                    break;
            }
            this.hdnPrintAction.Value = ((int)this.m_DefaultPrintAction).ToString();
        }

        private void PrintPreviewChanged()
        {
            switch (this.m_DefaultViewMode)
            {
                case ReportViewModeEnum.InThisTab:
                    this.ddlReportViewMode.SelectedValue = "0";
                    break;
                case ReportViewModeEnum.InNewTab:
                    this.ddlReportViewMode.SelectedValue = "1";
                    break;
                case ReportViewModeEnum.InNewWindow:
                    this.ddlReportViewMode.SelectedValue = "2";
                    break;
                case ReportViewModeEnum.InDialog:
                    this.ddlReportViewMode.SelectedValue = "3";
                    break;

            }
        }

        private void ExportTypeChanged()
        {
            switch (this.m_DefaultExportType)
            {
                case ReportExportTypeEnum.PDF:
                    ddlExport.SelectedValue = "0";
                    break;
                case ReportExportTypeEnum.Excel:
                    ddlExport.SelectedValue = "1";
                    break;
                case ReportExportTypeEnum.WORD:
                    ddlExport.SelectedValue = "2";
                    break;
            }
        }



        public void PrintTask()
        {
            PrintTask(this.m_ReportKey, this.m_ReportError);
        }

        public void PrintTask(string pReportKey)
        {
            PrintTask(this.m_ReportKey, string.Empty);
        }

        public void PrintTask(string pReportKey, string pReportError)
        {

            this.m_ReportKey = pReportKey;
            this.m_ReportError = pReportError;

            this.hdnPrintReportKey.Value = pReportKey;
            this.hdnPrintError.Value = pReportError;
            this.hdnPrintAuto.Value = this.AutoPrint ? "1" : "0";

            this.hdnPrintReportPageLink.Value = ReportLinks.GetLink_ReportView;
            this.hdnPrintReportViewPdfPageLink.Value = ReportLinks.GetLink_ReportViewPDF;

            this.hdnPrintAction.Value = ((int)this.DefaultPrintAction).ToString();

            //bool isPDFAutoPrint = Request.Browser.Browser.ToLower().Contains("ie") == false;
            //bool isPDFAutoPrint = true;
            bool isPDFAutoPrint = this.PDFAutoPrint;


            if (pReportKey != string.Empty | pReportError != string.Empty)
            {
                if (Session[pReportKey] != null)
                {
                    AppReport rpt = (AppReport)Session[pReportKey];
                    rpt.ReportOptions.ReportViewMode = (ReportViewModeEnum)Convert.ToInt32(ddlReportViewMode.SelectedValue);
                    rpt.ReportOptions.ReportExportType = (ReportExportTypeEnum)Convert.ToInt32(ddlExport.SelectedValue);
                    rpt.ReportOptions.ReportOpenType = this.DefaultPrintAction;

                    //AppInfo.SetAppInfoToReportOptions(rpt.ReportOptions);
                    //CompanyInfo.SetCompanyInfoToReportOptions(rpt.ReportOptions, this.Context);

                    this.hdnPdfPrint.Value = "0";
                    if (rpt.ReportOptions.ReportOpenType == ReportOpenTypeEnum.Print)
                    {
                        hdnPdfPrint.Value = isPDFAutoPrint ? "1" : "0";
                    }

                    Session[pReportKey] = rpt;
                    //strWait = "false";
                }
            }
            
            //if (this.m_EditMode == FormDataMode.Edit | this.m_EditMode == FormDataMode.Read)
            //{
            //    int paymentReqID = this.PaymentReqID;
            //    NameValueCollection reportParams = new NameValueCollection();
            //    reportParams.Add("PaymentReqID", paymentReqID.ToString());
            //    string rptKey = AppReport.GenerateReport_PaymentRequisition(reportParams);
            //    this.hdnPrintReportKey.Value = rptKey;
            //    this.hdnPrintError.Value = string.Empty;

            //}
        }


        //public void PrintClicked()
        //{
        //    if (this.PrintClick != null)
        //    {
        //        this.PrintClick(sender, e);
        //    }
        //}



        protected void btnPrint_Click(object sender, EventArgs e)
        {
            this.DefaultPrintAction = ReportOpenTypeEnum.Print;

            if (this.PrintClick != null)
            {
                this.PrintClick(sender, e);
            }

            //PrintTask(this.m_ReportKey, this.m_ReportError);
        }

        protected void btnPrintExport_Click(object sender, EventArgs e)
        {
            this.DefaultPrintAction = ReportOpenTypeEnum.Export;
            

            if (this.PrintClick != null)
            {
                this.PrintClick(sender, e);
            }

            //PrintTask(this.m_ReportKey, this.m_ReportError);
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            this.DefaultPrintAction = ReportOpenTypeEnum.Preview;

            if (this.PrintClick != null)
            {
                this.PrintClick(sender, e);
            }

            //PrintTask(this.m_ReportKey, this.m_ReportError);
        }

        protected void ddlReportViewMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_DefaultViewMode = (ReportViewModeEnum)Convert.ToInt32(ddlReportViewMode.SelectedValue);
            
        }
        protected void ddlExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_DefaultExportType = (ReportExportTypeEnum)Convert.ToInt32(ddlExport.SelectedValue);
        }

        protected void btnPrintAction_Click(object sender, EventArgs e)
        {
            if (this.PrintClick != null)
            {
                this.PrintClick(sender, e);
            }
        }
    }
}