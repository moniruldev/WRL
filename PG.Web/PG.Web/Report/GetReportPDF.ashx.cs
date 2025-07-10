using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Web.SessionState;

using PG.Core.Web;
using PG.Report;
using PG.Report.ReportEnums;

namespace PG.Web.Report
{
    /// <summary>
    /// Summary description for GetReportPDF
    /// </summary>
    public class GetReportPDF : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string reportKey = WebUtility.GetQueryString("rk", context);

            AppReport appReport = null;

            if (context.Session[reportKey] != null)
            {
                appReport = context.Session[reportKey] as AppReport;
                //context.Session[reportKey] = null;
            }

            if (appReport == null)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Report Not Specified!");
                context.Response.End();
                return;
            }

            if (ExportReport(appReport, context))
            {
                return;
            }


            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private bool ExportReport(AppReport rpt, HttpContext context)
        {

            LocalReport localReport = new LocalReport();
            AppReport.SetLocalReport(localReport, rpt);


            string exportFileName = "report";
            if (rpt.ReportOptions.ReportExportFileName != string.Empty)
            {
                exportFileName = rpt.ReportOptions.ReportExportFileName;
            }

            string exportType = "PDF";

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

            MemoryStream ms = new MemoryStream(renderBytes);
            //bool autoPrint = rpt.ReportOptions.ReportOpenType == ReportOpenTypeEnum.Print ? true : false;
            rpt.ReportOptions.IsAutoPrint = rpt.ReportOptions.ReportOpenType == ReportOpenTypeEnum.Print ? true : false;
            ms = AppReport.AddPrintJsToReportStream(ms, rpt.ReportOptions);
            renderBytes = ms.ToArray();

            
            //ZCore.Utility.Helper.GetFitTextWidth
            context.Response.Clear();
            context.Response.ContentType = mimeType;
            context.Response.AddHeader("content-disposition", "attachment;filename=" + exportFileName + "." + fileNameExtention);
            context.Response.BinaryWrite(renderBytes);

            //Server.Transfer("~/Login.aspx");
            context.Response.End();
            //context.ApplicationInstance.CompleteRequest();

            return true;
        }



    }
}