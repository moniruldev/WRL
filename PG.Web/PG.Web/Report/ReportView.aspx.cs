using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.IO;

using PG.Core.Web;
using PG.Web.PageLinks;
using PG.Report;

using PG.Report.ReportEnums;
using PG.DBClass.AccountingDC;
using PG.BLLibrary.AccountingBL;


namespace PG.Web.Report
{
    public partial class ReportView : BagePage
    {
        int CompanyID = 0;
        string roKey = string.Empty;
        string sessionFrameSrcTextName = "r_FrameSourceText";



        protected void Page_Load(object sender, EventArgs e)
        {
            this.CompanyID = CompanyInfo.GetCompanyID();
            roKey = this.GetPageQueryString("rk");
            if (!IsPostBack)
            {
                //dcAccSettings expreport = AccSettingsBL.GetAccSettingByCompanyID(this.CompanyID);
                //if (expreport.DisableReportExport)
                //{
                //     this.ReportViewer1.ShowExportControls = false;
                //    //btnExport.Enabled = false;

                //}
                //else
                //{
                //    this.ReportViewer1.ShowExportControls = true;
                //}

                //if (expreport.DisableReportPrint)
                //{
                //    this.ReportViewer1.ShowPrintButton = false;

                //}
                //else
                //{
                //    this.ReportViewer1.ShowPrintButton = true;
                //}

                AppReport rpt = null;


                hdnIsPrint.Value = "0";
                IFrameLiteral.Text = "";
                if (Session[roKey] != null)
                {
                    rpt = (AppReport)Session[roKey];
                    Session[roKey] = null;
                }

                if (rpt == null)
                {
                    Response.Write("Report Not Specified!");
                    this.ReportViewer1.Visible = false;
                    return;
                }

                if (rpt.ReportOptions.ReportOpenType == ReportOpenTypeEnum.Export)
                {
                    
                    if (ExportReport(rpt))
                    {
                        return;
                    }
                }

                if (rpt.ReportOptions.ReportOpenType == ReportOpenTypeEnum.Print)
                {
                    if (PrintReport(rpt))
                    {
                        return;
                    }
                }


                this.ReportViewer1.Visible = true;

                //for second
                ///---

                //ReportViewer1.LocalReport.s = AppReport.GetLocalReport(rpt);

                AppReport.SetLocalReport(ReportViewer1.LocalReport, rpt);

                //ReportViewer1
                ReportViewer1.ZoomPercent = rpt.ReportOptions.ReportZoomPercent;
                this.ReportViewer1.LocalReport.Refresh();
            }
        }

        private bool ExportReport(AppReport rpt)
        {

            LocalReport localReport = new LocalReport();
            AppReport.SetLocalReport(localReport, rpt);


            string exportFileName = "report";
            if (rpt.ReportOptions.ReportExportFileName != string.Empty)
            {
                exportFileName = rpt.ReportOptions.ReportExportFileName;
            }

            string exportType = "PDF";
            bool isPDF = false;
            switch (rpt.ReportOptions.ReportExportType)
            {
                case ReportExportTypeEnum.PDF:
                    exportType = "PDF";
                    isPDF = true;
                    break;
                case ReportExportTypeEnum.Excel:
                    exportType = "excel";
                    break;
                case ReportExportTypeEnum.WORD:
                    exportType = "word";
                    break;
            }


            //string reportType = "PDF";
            //string reportType = "excel";
            string mimeType;
            string encoding;
            string fileNameExtention;

            //string deviceInfo =
            //   "<DeviceInfo>" +
            //   "  <OutputFormat>PDF</OutputFormat>" +
            //   "  <PageWidth>8.5in</PageWidth>" +
            //   "  <PageHeight>11in</PageHeight>" +
            //   "  <MarginTop>0.25in</MarginTop>" +
            //   "  <MarginLeft>0.25in</MarginLeft>" +
            //   "  <MarginRight>0.25in</MarginRight>" +
            //   "  <MarginBottom>0.25in</MarginBottom>" +
            //   "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderBytes;

            //renderBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtention, out streams, out warnings);
            renderBytes = localReport.Render(exportType, null, out mimeType, out encoding, out fileNameExtention, out streams, out warnings);

            if (isPDF)
            {
                MemoryStream ms = new MemoryStream(renderBytes);
                //bool autoPrint = rpt.ReportOptions.ReportOpenType == ReportOpenTypeEnum.Print ? true : false;
                rpt.ReportOptions.IsAutoPrint = rpt.ReportOptions.ReportOpenType == ReportOpenTypeEnum.Print ? true : false;
                ms = AppReport.AddPrintJsToReportStream(ms, rpt.ReportOptions);
                renderBytes = ms.ToArray();
            }

            //ZCore.Utility.Helper.GetFitTextWidth
            Response.Clear();
            Response.ContentType = mimeType;
            Response.AddHeader("content-disposition", "attachment;filename=" + exportFileName + "." + fileNameExtention);
            Response.BinaryWrite(renderBytes);

            //Server.Transfer("~/Login.aspx");
            //Response.Flush();
            //Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();

            return true;
        }


        public string GetFrameSourceText()
        {
            string frameSrcText = string.Empty;
            if (Session[this.sessionFrameSrcTextName] != null)
            {
                frameSrcText = Convert.ToString(Session[this.sessionFrameSrcTextName]);
            }
            else
            {
                //string frameSrc = File. .Load(Request.GetApplicationPath() + @”PrintScript.txt”);

                string fileName = string.Empty;

                if (Request.Browser.Browser.ToLower().Contains("ie"))
                {
                    fileName = "~/Report/PrintScriptIE.txt";
                }
                else
                {
                    fileName = "~/Report/PrintScriptFF.txt";
                }


                string strPath = Server.MapPath(fileName);
                
                
                
                frameSrcText = File.ReadAllText(strPath);
                //frameSrc = frameSrc.Replace("[", "{");
                //frameSrc = frameSrc.Replace("]", "}");
                Session[this.sessionFrameSrcTextName] = frameSrcText;
            }

            return frameSrcText;
        }


        private bool PrintReport(AppReport rpt)
        {
            bool bStatus = false;
            hdnIsPrint.Value = "1";

            int marginLeft = 0;
            int marginTop = 0;
            int marginRight = 0;
            int marginBottom = 0;

            int pageWidth = 0;
            int pageHeight = 0;

            string rptName = rpt.ReportOptions.ReportTitle;

            string rptPath = ReportLinks.GetLink_ReportPrintData;
            
            //string  s=  this.Request.UserAgent;


            string rptParam =  "rk=" + this.roKey;
            rptName = "Print Test";
            
            //string codeBase = WebUtility.GetAbsoluteUrl("~/Report/") +  "RSClientPrint.cab";

            string codeBase = WebUtility.GetAbsoluteUrl("~/download/") + "RSClientPrint.cab";

            string frameSrcText = GetFrameSourceText();
            frameSrcText = string.Format(frameSrcText,marginLeft,marginTop,marginRight,
                                        marginBottom, pageWidth, pageHeight, rptPath, rptParam, rptName, codeBase);


            frameSrcText = frameSrcText.Replace("[", "{");
            frameSrcText = frameSrcText.Replace("]", "}");

            string sk_FrameSource = "sk_" + this.roKey;
            Session[sk_FrameSource] = frameSrcText;

            //string frameSrc = "<IFRAME id=\""PrintIFrame\"" src="PrintReport.aspx"/>";

            string printLink = ReportLinks.GetLink_ReportPrint + "?rk=" + this.roKey;

            string frameSrc = "<IFRAME id=\"PrintIFrame\" src=\"" + printLink + "\"/>";

            this.IFrameLiteral.Text = frameSrc;

            //Generate Report Stream
            LocalReport localReport = new LocalReport();
            AppReport.SetLocalReport(localReport, rpt);

            bStatus = true;

            return bStatus;
        }
    }
}
