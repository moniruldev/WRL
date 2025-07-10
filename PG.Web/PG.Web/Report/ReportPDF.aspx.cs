using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG.Core;
using PG.Core.Web;

namespace PG.Web.Report
{
    public partial class ReportPDF : System.Web.UI.Page
    {
        public string GetReportPDFPageLink = string.Empty;
        
        public string ReportGetPDFPageLink = PageLinks.ReportLinks.GetLink_ReportGetPDF;

        protected void Page_Load(object sender, EventArgs e)
        {
            string reportKey = WebUtility.GetQueryString("rk", this.Context);

            string getPDFURL = ReportGetPDFPageLink + "?rk=" + reportKey;
            this.GetReportPDFPageLink = getPDFURL;


            //this.GetReportPDFPageLink = "../test.pdf";
        }
    }
}