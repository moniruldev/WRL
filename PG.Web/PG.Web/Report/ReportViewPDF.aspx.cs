using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG.Core;

using PG.Core.Web;
using PG.Report;
using PG.Report.ReportEnums;

namespace PG.Web.Report
{
    public partial class ReportViewPDF : System.Web.UI.Page
    {
        public string ReportGetPDFPageLink = PageLinks.ReportLinks.GetLink_ReportGetPDF;
        protected void Page_Load(object sender, EventArgs e)
        {
            string reportKey = WebUtility.GetQueryString("rk", this.Context);
            AppReport appReport = null;
            
            if (Session[reportKey] != null)
            {
                appReport = Session[reportKey] as AppReport;
                //Session[reportKey] = null;
            }

            byte[] buffer;
            if (appReport == null)
            {
                buffer = AppReport.GetEmptyPDF("Error!! Report Not Specified!").ToArray();
                //Response.ContentType = "text/plain";
                //Response.Write("Report Not Specified!");
                //Response.End();
                //return;
            }
            else
            {
                buffer = AppReport.GetReportPDF(appReport);
            }

            //byte[] buffer = AppReport.GetReportPDF(appReport);

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", buffer.Length.ToString());
            Response.BinaryWrite(buffer);
        }

        
    }
}